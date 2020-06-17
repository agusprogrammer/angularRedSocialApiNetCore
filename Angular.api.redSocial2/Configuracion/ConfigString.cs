using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.api.redSocial2.Configuracion
{
    public class ConfigString
    {
        private const string CONEXSTRING = "server=localhost;port=3306;database=bd_red_social;user=root;password=mysql";

        public ConfigString(){ }

        public string obtConex(){
            return CONEXSTRING;
        }

    }
}
