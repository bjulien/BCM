using BCM_test.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BCM_test.Infrastructure
{
    public class BarnsleyReleveCentrale : AbstractReleveCentrale
    {
        public BarnsleyReleveCentrale()
        {
        }

        public override async Task<IEnumerable<Production>> RecupererReleve(string from, string to)
        {
            _httpClient.BaseAddress = new Uri($"https://interview.beta.bcmenergy.fr/barnsley?from={from}&to={to}");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            IList<Production> releve = new List<Production>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);
                response.EnsureSuccessStatusCode();

                JArray json = JArray.Parse(await response.Content.ReadAsStringAsync());


                long timestampSuivant = long.Parse(json.FirstOrDefault()?["start_time"].ToString());
                foreach (JObject iteration in json)
                {
                    // V�rification de trous dans les donn�es (Dans l'id�al mutualiser ce code pour chaque impl�mentation de l'abstract)
                    if (long.Parse(iteration.GetValue("end_time").ToString()) != timestampSuivant + 900)
                    {
                        releve.Add(new Production()
                        {
                            start = (long.Parse(iteration.GetValue("start_time").ToString()) - 900).ToString(),
                            end = (long.Parse(iteration.GetValue("end_time").ToString()) - 900).ToString(),
                            power = int.Parse(iteration.Previous?.ToObject<JObject>().GetValue("value").ToString()) + int.Parse(iteration.Next?.ToObject<JObject>().GetValue("value").ToString())
                        });
                    }

                    releve.Add(new Production()
                    {
                        start = iteration.GetValue("start_time").ToString(),
                        end = iteration.GetValue("end_time").ToString(),
                        power = int.Parse(iteration.GetValue("value").ToString())
                    });
                        
                    timestampSuivant = long.Parse(iteration.GetValue("start_time").ToString()) + 900;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Ajouter syst�me de log
            }

            return releve;
        }
    }
}