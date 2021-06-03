using Newtonsoft.Json;
using System;

namespace BCM_test.Domain
{
	public class Production
    {
        /*
         * Date début de la production d'éléctricité
         */
        public string start;
        /*
         * Date fin de la production d'éléctricité
         */
        public string end;
        /*
         * Relevé de l'indicateur de la production d'éléctricité
         */
        public int power;
    }
}