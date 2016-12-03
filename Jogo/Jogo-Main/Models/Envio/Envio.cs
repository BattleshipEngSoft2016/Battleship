using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{
    public class Envio
    {
        public int TipoMensagem { get; set; }

        public List<EnvioBarco> Objeto { get; set; }

    }

    public class EnvioBarco
    {
        public int IdBarco { get; set; }

        public int TipoBarco { get; set; }

        public List<string> Coordenadas { get; set; }


    }
}