using BCM_test.Domain;
using BCM_test.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCM_test.Application
{
    public class AgregateReleveService
    {
        private readonly BarnsleyReleveCentrale _barnsley = new BarnsleyReleveCentrale();
        private readonly HawesReleveCentrale _hawes = new HawesReleveCentrale();
        private readonly HounslowReleveCentrale _hounslow = new HounslowReleveCentrale();

        public AgregateReleveService()
        {
        }

        public async Task<string> RecupereTousLesReleves(string from, string to, string format)
        {
            IEnumerable<Production> hounslowProduction = await _hounslow.RecupererReleve(from, to);
            IEnumerable<Production> barnsleyProduction = await _barnsley.RecupererReleve(from, to);
            IEnumerable<Production> hawesProduction = await _hawes.RecupererReleve(from, to);

            IEnumerable<Production> finalArray = hounslowProduction.Concat(barnsleyProduction).Concat(hawesProduction).GroupBy(p => new { p.start, p.end }).Select(f => new Production
            {
                end = f.Key.end,
                start = f.Key.start,
                power = f.Sum(p => p.power)
            });

            EditionReleve editionReleve = new EditionReleve();
            string retour;
            switch (format) {
                case "json":
                    retour = editionReleve.EditionReleveJson(finalArray);
                    break;
                case "csv":
                    retour = editionReleve.EditionReleveJson(finalArray);
                    break;
                default:
                    retour = editionReleve.EditionReleveJson(finalArray);
                    break;
            }

            return retour;
        }
    }
}
