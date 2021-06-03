using BCM_test.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BCM_test.Infrastructure
{
	public abstract class AbstractReleveCentrale
    {
        protected readonly HttpClient _httpClient;

        public AbstractReleveCentrale()
        {
            _httpClient = new HttpClient();
        }

        /// <param name="from">Date d�but du relev�</param>
        /// <param name="to">Date fin du relev�</param>
        public abstract Task<IEnumerable<Production>> RecupererReleve(string from, string to);
    }
}