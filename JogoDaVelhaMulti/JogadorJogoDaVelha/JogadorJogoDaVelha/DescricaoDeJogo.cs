using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JogadorJogoDaVelha
{
    /* CLASSE: DescricaoDeJogo
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Esta classe contém a descrição resumida de um jogo (enviada pelo servidor).
     * Um jogo tem os seguintes atributos: id, tipo, finalizado, número de 
     *   jogadores e se está ou não esperando jogadores.
     * Esta classe é utilizada pela classe RespostaDoServidor para
     *   representar todos os jogos abertos no servidor.
     */
    public class DescricaoDeJogo
    {
        public int identificadorDoJogo = -1;
        public String tipoDoJogo = "";
        public bool jogoEncerrado = true;
        public bool esperandoJogadores = false;
        public int jogadoresNoJogo = -1;

        /* O construtor recebe o texto XML (enviado pelo servidor) contendo os
         *   detalhes de um jogo. Este texto é processado e os atributos do
         *   jogo são extraídos e armazenados nos atributos desta classe.
         */
        public DescricaoDeJogo(String xmlDoJogo)
        {
            Regex exp = new Regex(@"<GID>(\d+)</GID><GT>(\w+)</GT><ENDED>(\w+)</ENDED><WP>(\w+)</WP><#PLAYERS>(\d+)</#PLAYERS>", RegexOptions.IgnoreCase);

            MatchCollection MatchList = exp.Matches(xmlDoJogo);
            if (MatchList.Count > 0)
            {
                identificadorDoJogo = int.Parse(MatchList[0].Groups[1].ToString());
                tipoDoJogo = MatchList[0].Groups[2].ToString();
                if (MatchList[0].Groups[3].ToString().Equals("false", StringComparison.CurrentCultureIgnoreCase)) jogoEncerrado = false;
                if (MatchList[0].Groups[4].ToString().Equals("true",StringComparison.CurrentCultureIgnoreCase)) esperandoJogadores = true;
                jogadoresNoJogo = int.Parse(MatchList[0].Groups[5].ToString());
            }
        }

        /* 
         * Retorna uma string contendo os valores dos atributos desta classe.
         */
        public String exibirDadosDoJogo(){
            return "Id: "+identificadorDoJogo.ToString()+", Tipo: "+tipoDoJogo+", Encerrado: " + jogoEncerrado.ToString()+", Esperando Jogadores: "+esperandoJogadores.ToString()+", Jogadores: "+jogadoresNoJogo.ToString();
        }
    }
}
