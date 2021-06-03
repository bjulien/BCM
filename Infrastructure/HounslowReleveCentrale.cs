using BCM_test.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BCM_test.Infrastructure
{
    public class HounslowReleveCentrale : AbstractReleveCentrale
    {
        public HounslowReleveCentrale()
        {
        }

        public override async Task<IEnumerable<Production>> RecupererReleve(string from, string to)
        {
            _httpClient.BaseAddress = new Uri($"https://interview.beta.bcmenergy.fr/hounslow?from={from}&to={to}");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            IList<Production> releve = new List<Production>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                IList<string> jsonArray = json.Split(null).ToList();
                foreach (string iteration in jsonArray.Skip(1))
                {
                    string[] production = iteration.Split(",");
                    releve.Add(new Production()
                    {
                        start = production[0],
                        end = production[1],
                        power = int.Parse(production[2])
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Ajouter système de log
            }

            return releve;
        }
    }
}