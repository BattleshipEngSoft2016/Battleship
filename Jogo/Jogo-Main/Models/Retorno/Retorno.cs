using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Jogo_Main.Models.Retorno
{
    public class Retorno
    {
        public int TipoMensagem { get; set; }

        public Object Mensagem { get; set; }

        public Retorno(TipoMensagemRetorno tipo, Object obj = null)
        {

            TipoMensagem = (int) tipo;

            Mensagem = obj;
        }
    }
}