using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JogadorJogoDaVelha
{

    /* CLASSE: RespostaDoServidor
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Esta classe contém os métodos para processar respostas XML enviadas
     *   pelo servidor de jogos.
     * Todos os atributos desta classe são produzidos com base no conteúdo
     *   das mensagens (respostas) do servidor.
     */
    public class RespostaDoServidor
    {
        public String respostaDoServidor;
        public Boolean respostaAfirmativa = false;
        public Boolean jogoEncerrado = false;
        public Boolean jogoEmpatado = false;
        public Boolean turnoDoJogadorAtual = false;
        public Boolean turnoDoAdversario = false;
        public Boolean aguardandoJogadores = false;
        public String acaoExecutada;
        public String respostaEspecifica;
        public int identificadorDoJogo;
        public int vencedor = -1;
        public DescricaoDeJogo[] Jogos;
        public String acaoEsperada;
        public int linhas;
        public int colunas;
        public int[] jogadores;
        public string[,] tabuleiro;
        public int indiceDoJogador = -1;

        public RespostaDoServidor(String resposta)
        {
            respostaDoServidor = resposta;
            //<SERVER ANSWER><STATUS>OK</STATUS><GENERAL ANSWER><ACTION PERFORMED>LISTING GAMES</ACTION PERFORMED></GENERAL ANSWER><SPECIFIC ANSWER><LIST GAMES><GAME><GID>0</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME><GAME><GID>1</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME></LIST GAMES></SPECIFIC ANSWER><GAME STATUS><GGSTATUS>NOTHING TO BE DONE</GGSTATUS></GAME STATUS></SERVER ANSWER>

            //<STATUS>OK</STATUS>
            Regex exp = new Regex(@"<STATUS>(\w+)</STATUS>", RegexOptions.IgnoreCase);
            MatchCollection MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                respostaAfirmativa = (MatchList[0].Groups[1].ToString() == "OK");
            }

            //<ACTION PERFORMED>LISTING GAMES</ACTION PERFORMED></GENERAL ANSWER><SPECIFIC ANSWER><LIST GAMES><GAME><GID>0</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME><GAME><GID>1</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME></LIST GAMES></SPECIFIC ANSWER><GAME STATUS><GGSTATUS>NOTHING TO BE DONE</GGSTATUS></GAME STATUS></SERVER ANSWER>
            exp = new Regex(@"<ACTION PERFORMED>(\w+)</ACTION PERFORMED>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                acaoExecutada = MatchList[0].Groups[1].ToString();
            }

            //<SPECIFIC ANSWER><LIST GAMES><GAME><GID>0</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME><GAME><GID>1</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME></LIST GAMES></SPECIFIC ANSWER><GAME STATUS><GGSTATUS>NOTHING TO BE DONE</GGSTATUS></GAME STATUS></SERVER ANSWER>
            exp = new Regex(@"<SPECIFIC ANSWER>(\w+)</SPECIFIC ANSWER>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                respostaEspecifica = MatchList[0].Groups[1].ToString();
            }

            //<LIST GAMES><GAME><GID>0</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME><GAME><GID>1</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME></LIST GAMES></SPECIFIC ANSWER><GAME STATUS><GGSTATUS>NOTHING TO BE DONE</GGSTATUS></GAME STATUS></SERVER ANSWER>
            exp = new Regex(@"<LIST GAMES>(.+)</LIST GAMES>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                String xmlListaDeJogos = MatchList[0].Groups[1].ToString();

                //<GAME><GID>0</GID><GT>tictactoe</GT><ENDED>false</ENDED><WP>true</WP><#PLAYERS>0</#PLAYERS></GAME>
                exp = new Regex(@"(<GAME><GID>\d+</GID><GT>\w+</GT><ENDED>\w+</ENDED><WP>\w+</WP><#PLAYERS>\d+</#PLAYERS></GAME>)", RegexOptions.IgnoreCase);

                MatchList = exp.Matches(MatchList[0].Groups[1].ToString());
                if (MatchList.Count > 0)
                {
                    Jogos = new DescricaoDeJogo[MatchList.Count];
                    for (int i = 0; i < MatchList.Count; i++)
                    {
                        Jogos[i] = new DescricaoDeJogo(MatchList[i].Groups[1].ToString());
                        //Jogos[i].exibirDadosDoJogo();
                    }
                }
            }

            //<GID>4</GID>
            exp = new Regex(@"<GID>(\d+)</GID>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                identificadorDoJogo = int.Parse(MatchList[0].Groups[1].ToString());
            }


            //<GGSTATUS>NOTHING TO BE DONE</GGSTATUS>
            exp = new Regex(@"<GGSTATUS>(.+)</GGSTATUS>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                acaoEsperada = MatchList[0].Groups[1].ToString();
                if (acaoEsperada.Equals("YOUR TURN", StringComparison.CurrentCultureIgnoreCase))
                {
                    turnoDoJogadorAtual = true;
                }
                if (acaoEsperada.Equals("ADVERSARY'S TURN", StringComparison.CurrentCultureIgnoreCase))
                {
                    turnoDoAdversario = true;
                }    
                else if (acaoEsperada.Equals("WAITING FOR PLAYERS", StringComparison.CurrentCultureIgnoreCase))
                {
                    aguardandoJogadores = true;
                }
                else if (acaoEsperada.Equals("GAME ENDED", StringComparison.CurrentCultureIgnoreCase))
                {
                    jogoEncerrado = true;
                }
            }


            //<LINES>3</LINES><COLUMNS>3</COLUMNS><PLAYERS>1;2;</PLAYERS><PLAYERNUMBER>2</PLAYERNUMBER>
            exp = new Regex(@"<LINES>(\d+)</LINES>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                linhas = int.Parse(MatchList[0].Groups[1].ToString());
            }

            exp = new Regex(@"<COLUMNS>(\d+)</COLUMNS>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                colunas = int.Parse(MatchList[0].Groups[1].ToString());
            }

            //<PLAYERS>1;2;</PLAYERS>
            exp = new Regex(@"<PLAYERS>(.+)</PLAYERS>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                String textoJogadores = MatchList[0].Groups[1].ToString();
                exp = new Regex(@"([-]*\d+);", RegexOptions.IgnoreCase);

                MatchList = exp.Matches(textoJogadores);
                if (MatchList.Count > 0)
                {
                    jogadores = new int[MatchList.Count];
                    for (int i = 0; i < MatchList.Count; i++)
                    {
                        jogadores[i] = int.Parse(MatchList[i].Groups[1].ToString());
                    }
                }
            }

            //Resposta: <GAME CURRENT BOARD>< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< >< ></GAME CURRENT BOARD>

            exp = new Regex(@"<GAME CURRENT BOARD>(.+)</GAME CURRENT BOARD>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                String textoTabuleiro = MatchList[0].Groups[1].ToString();
                exp = new Regex(@"(<.+>)<(.+)>", RegexOptions.IgnoreCase);

                MatchList = exp.Matches(textoTabuleiro);
                int i = this.linhas * this.colunas - 1;
                tabuleiro = new string[this.linhas, this.colunas];
                while (MatchList.Count > 0)
                {

                    int x = i / this.colunas;
                    int y = i - this.colunas * x;
                    tabuleiro[x, y] = MatchList[0].Groups[2].ToString();
                    textoTabuleiro = MatchList[0].Groups[1].ToString();
                    i--;
                    MatchList = exp.Matches(textoTabuleiro);
                }
                exp = new Regex(@"<(.+)>", RegexOptions.IgnoreCase);
                MatchList = exp.Matches(textoTabuleiro);

                tabuleiro[0, 0] = MatchList[0].Groups[1].ToString();

            }



            exp = new Regex(@"<WINNER>([-]*\d+)</WINNER>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                vencedor = int.Parse(MatchList[0].Groups[1].ToString());
            }

            exp = new Regex(@"<PLAYER>([-]*\d+)</PLAYER>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                indiceDoJogador = int.Parse(MatchList[0].Groups[1].ToString());
            }

            exp = new Regex(@"<DRAWNGAME>(DRAWN GAME)</DRAWNGAME>", RegexOptions.IgnoreCase);
            MatchList = exp.Matches(resposta);
            if (MatchList.Count > 0)
            {
                jogoEmpatado = true;
            }


        }
    }
}
