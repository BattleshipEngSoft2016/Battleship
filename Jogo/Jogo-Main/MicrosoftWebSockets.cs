using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Jogo_Main.Models;
using Jogo_Main.Models.Retorno;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;

namespace UsingWebSockets
{
    public class MicrosoftWebSockets : WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();

        public string name;

        public int Id;

        public int NivelId;

        public bool Jogando;

        public bool StatusTabuleiro;

        public List<Barco> Barcos;

        public override void OnOpen()
        {
            name = this.WebSocketContext.QueryString["chatName"];

            var nivel = this.WebSocketContext.QueryString["NivelId"];

            var id = this.WebSocketContext.QueryString["Id"];

            if (!string.IsNullOrEmpty(nivel))
            {
                NivelId = int.Parse(nivel);
            }

            if (!string.IsNullOrEmpty(id))
            {
               Id = int.Parse(id);
            }

            if (clients.Count >= 2)
            {
                this.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Mensagem, "Sala Cheia.... Você esta em Espera")));
            }
            else if (clients.Count == 1)
            {
                clients.FirstOrDefault().Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Mensagem, string.Format("{0} entrou no Jogo", name))));

                clients.Add(this);

                this.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Montar, "Monte seu Tabuleiro")));
            }
            else
            {
                clients.Add(this);

                clients.Broadcast(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Mensagem, string.Format("{0} entrou no Jogo", name))));

                this.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Montar, "Monte seu Tabuleiro")));
            }

        }

        public override void OnMessage(string message)
        {
            try
            {
                var envio = JsonConvert.DeserializeObject<Envio>(message);
                switch (envio.TipoMensagem)
                {
                    case (int)TipoMensagem.Montagem:
                        {
                            Barcos = new List<Barco>();

                            foreach (var item in envio.Objeto)
                            {
                                var itemParce = item as Newtonsoft.Json.Linq.JObject;

                                if (itemParce != null)
                                {
                                    var barco = itemParce.ToObject<EnvioBarco>();

                                    Barcos.Add(new Barco(barco));

                                    StatusTabuleiro = true;

                                }

                            }

                            if (clients.All(x => ((MicrosoftWebSockets)x).StatusTabuleiro))
                            {
                                clients.Broadcast(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Inicio, "Jogo Iniciado")));

                                ObterOponente().Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.MandoDeJogo, "Sua Vez !!!")));
                            }
                            else
                            {
                                this.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Aguarde, "Aguarde o proximo Jogador terminar a Montagem....")));
                            }

                        }
                        break;
              
                        case (int)TipoMensagem.Jogada:
                        {
                            var jogada = envio.Objeto.FirstOrDefault();

                            if (jogada != null)
                            {
                                var opoente = ObterOponente();
                                    
                                if(opoente != null)
                                {
                                    var barco = opoente.Barcos.FirstOrDefault(x => x.Coordenadas.Any(y => y.Valor == jogada.ToString() && !y.Destruido));

                                    if (barco != null)
                                    {
                                        opoente.Barcos.Remove(barco);

                                        barco.Afundar(jogada.ToString());

                                        opoente.Barcos.Add(barco);

                                        AtualizarBarcos(opoente);

                                        Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Acerto, "Você acerto !!!!!")));

                                        if (barco.Coordenadas.All(x => x.Destruido))
                                        {
                                            opoente.Send(
                                                JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Destruido, "Ops Você perdeu seu barco !!!")));

                                        }

                                    }
                                    else
                                    {
                                        Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Erro, "Você Erro !!!!")));
                                    }
                                }
                            }
                        }

                        VerificarFinalJogo();

                        ObterOponente().Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.MandoDeJogo, "Sua Vez !!!!")));
                        break;
                }

                var webSocketHandler = clients.SingleOrDefault(r => ((MicrosoftWebSockets)r).name == "Julio");
                
                if (webSocketHandler != null)
                    webSocketHandler.Send("Rola");

            }
            catch (Exception ex)
            {
                this.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Fail, "Mensagem com Erro")));
            }
        }

        public override void OnClose()
        {
            clients.Remove(this);
            clients.Broadcast(string.Format("{0} saiu da sala.", name));
        }

        private void AtualizarBarcos(MicrosoftWebSockets client)
        {
            ((MicrosoftWebSockets) clients.FirstOrDefault(x => ((MicrosoftWebSockets) x).Id == client.Id)).Barcos = client.Barcos;

            clients.FirstOrDefault(x => ((MicrosoftWebSockets) x).Id == client.Id).Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Atingido, "Você foi atingindo !!!")));
           
        }

        private void VerificarFinalJogo()
        {
            foreach (var client in clients)
            {
                var item = (MicrosoftWebSockets) client;

                if (item.Barcos.All(x => x.Destruido()))
                {
                    Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Ganho, "Você Ganho !!")));

                    AtualizarPontos();

                    client.Send(JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Perdeu, "Você Perdeu")));

                    clients.Broadcast(
                        JsonConvert.SerializeObject(new Retorno(TipoMensagemRetorno.Finalizado, "Jogo Finalizado !!!")));

                    FinalizarJogo();
                }
            }
        }

        private void FinalizarJogo()
        {
            foreach (var client in clients) client.Close();
        }

        public void AtualizarPontos()
        {
            using (var db = new UsersContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserId == Id);

                var numero = Barcos.Select(x => x.Coordenadas.Count(y => !y.Destruido)).Sum(x => x);

                if (user != null) user.Pontos += numero * 10;

                db.SaveChanges();
            }
        }

        public MicrosoftWebSockets ObterOponente()
        {
            return ((MicrosoftWebSockets)clients.FirstOrDefault(x => ((MicrosoftWebSockets)x).Id != Id));
        }

    }
}