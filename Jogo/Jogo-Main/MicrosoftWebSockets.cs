using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Jogo_Main.Models;
using Microsoft.Web.WebSockets;
namespace UsingWebSockets
{
    public class MicrosoftWebSockets : WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();
        
        public string name;
        
        public bool jogando;

        public List<Barco> barcos;

        public override void OnOpen()
        {
            name = this.WebSocketContext.QueryString["chatName"];
            clients.Add(this);
            clients.Broadcast(name + " entrou no Jogo ");
            
        }
        
        public override void OnMessage(string message)
        {
            //clients.FirstOrDefault(x =>x.);


            try
            {
                var envio = new JavaScriptSerializer().Deserialize<Envio>(message);

                switch (envio.TipoMensagem)
                {
                    case (int)TipoMensagem.TipoMontagem:
                        {
                            barcos = new List<Barco>();

                            foreach (var x in envio.Objeto)
                            {
                                var p = x as EnvioBarco;

                                if (p != null)
                                {
                                    barcos.Add(new Barco(p));
                                }

                            }

                        }
                        break;
                    case (int)TipoMensagem.TipoJogada:
                        {
                            //var p = envio.Objeto as string;

                            //var b = barcos.FirstOrDefault(x => x.Coordenadas.Any(y => y.Valor == p));

                            //if(b != null)
                            //{
                            //    barcos.FirstOrDefault(x => x.Coordenadas.Any(y => y.Valor == p)).Coordenadas.FirstOrDefault(
                            //        x => x.Valor == p).Destruido = true;

                            //    voce acertou 
                            //}
                            //else
                            //{

                            //}
                        }
                        break;
                }

                var webSocketHandler = clients.SingleOrDefault(r => ((MicrosoftWebSockets)r).name == "Julio");
                if (webSocketHandler != null)
                    webSocketHandler.Send("Rola");

                clients.Broadcast(string.Format("<b>{0}</b>:<br/> {1}", name, message));

            }
            catch (Exception ex)
            {
                
                throw;
            }
            

        }

        public override void OnClose()
        {
            clients.Remove(this);
            clients.Broadcast(string.Format("{0} saiu da sala.", name));
        }

    }
}