using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{
    public class Envio
    {
        public int TipoMensagem { get; set; }

        public List<System.Object> Objeto { get; set; }

        public Envio()
        {
            
        }
    }

    public class EnvioBarco
    {
        public int IdBarco { get; set; }

        public string TipoBarco { get; set; }

        public List<string> Coordenadas { get; set; }

        public EnvioBarco()
        {
            
        }
    }
}