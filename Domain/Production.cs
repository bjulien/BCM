using Newtonsoft.Json;
using System;

namespace BCM_test.Domain
{
	public class Production
    {
        /*
         * Date d�but de la production d'�l�ctricit�
         */
        public string start;
        /*
         * Date fin de la production d'�l�ctricit�
         */
        public string end;
        /*
         * Relev� de l'indicateur de la production d'�l�ctricit�
         */
        public int power;
    }
}