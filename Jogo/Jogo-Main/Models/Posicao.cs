using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{
    public class Posicao
    {
        public string Coordenada { get; set; }

        public int IdBarco { get; set; }

        public int TipoBarco { get; set; }

        public bool Destruido { get; set; }

        public Posicao(string c, int i, int t)
        {
            Coordenada = c;
            IdBarco = i;
            TipoBarco = t;
            Destruido = false;
        }
    }



    public class Barco
    {
        public int IdBarco { get; set; }

        public string TipoBarco { get; set; }

        public List<Coordenada> Coordenadas { get; set; }

        public Barco(EnvioBarco envio)
        {

            IdBarco = envio.IdBarco;

            TipoBarco = envio.TipoBarco;

            Coordenadas = new List<Coordenada>();
            
            Coordenadas = envio.Coordenadas.Select(x => new Coordenada(x)).ToList();
        }

        public void Afundar(string p)
        {
            var firstOrDefault = Coordenadas.FirstOrDefault(x => x.Valor == p);

            if (firstOrDefault != null)
                firstOrDefault.Destruido = true;
        }

        public bool Destruido()
        {
            return Coordenadas.All(x => x.Destruido);
        }
    }


    public class Coordenada
    {
        public string Valor { get; set; }

        public bool Destruido { get; set; }

        public Coordenada(string v)
        {
            Valor = v;
            Destruido = false;
        }
    }

}