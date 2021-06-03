using BCM_test.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BCM_test.Infrastructure
{
    public class HawesReleveCentrale : AbstractReleveCentrale
    {
        public HawesReleveCentrale()
        {
        }

        public override async Task<IEnumerable<Production>> RecupererReleve(string from, string to)
        {
            _httpClient.BaseAddress = new Uri($"https://interview.beta.bcmenergy.fr/hawes?from={from}&to={to}");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Production> releve = new List<Production>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);
                response.EnsureSuccessStatusCode();
                releve = JsonConvert.DeserializeObject<IEnumerable<Production>>(await response.Content.ReadAsStringAsync());
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