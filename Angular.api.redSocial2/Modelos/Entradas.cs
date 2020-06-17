using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.api.redSocial2.Modelos
{
    public class Entradas
    {
        public Entradas(){ }

        public int idEntrada { get; set; }
        public int idUsu { get; set; }
        public string tituloEntrada { get; set; }
        public string textoEntrada { get; set; }
        public DateTime fechaCreacionEntrada { get; set; } //fecha automatica
    }
}
