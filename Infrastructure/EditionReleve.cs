using BCM_test.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCM_test.Infrastructure
{
    class EditionReleve
    {
        public EditionReleve()
        {
        }

        public string EditionReleveJson(IEnumerable<Production> releveProduction)
        {
            return JsonConvert.SerializeObject(releveProduction);
        }

        public string EditionReleveCsv(IEnumerable<Production> releveProduction)
        {
            // implémenter création csv
            return string.Empty;
        }
    }
}
