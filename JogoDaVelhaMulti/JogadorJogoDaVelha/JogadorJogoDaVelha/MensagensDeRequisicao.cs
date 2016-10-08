using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace JogadorJogoDaVelha
{
    /* CLASSE: MensagensDeRequisicao
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Esta classe cont�m os m�todos para a produ��o das mensagens XML que
     *   ser�o enviadas ao servidor de jogos.
     */
    static class MensagensDeRequisicao
    {
        public enum TipoDeJogo { JogoDaVelha2D, Xadrez, Damas, XadrezDinamico };
        public enum TipoDeOrientacao { SentidoHorario, SentidoAntiHorario };

        /* Produz a mensagem para requisi��o das informa��es de um dado tabuleiro. 
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que est� solicitando o tabuleiro.
         */
        static public String receberTabuleiro(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>GAME BOARD</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

         /* Produz a mensagem para requisi��o das informa��es gerais de um dado jogo. 
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o estado
         *   identificadorDoJogador: id do jogador que est� solicitando as informa��es
         */
        static public String estadoDoJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>GAME STATUS</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita a rota��o de uma pe�a no sentido
         *   hor�rio ou anti-hor�rio (jogada v�lida no jogo deflexion)
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que est� solicitando o tabuleiro.
         *   origemX: posi��o X (linha) em que a pe�a est�.
         *   origemY: posi��o Y (coluna) em que a pe�a est�.
         *   sentido: sentido que a pe�a ser� rodada (hor�rio ou anti-hor�rio)
         */
        static public String rotacionarPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY, TipoDeOrientacao sentido)
        {
            String orientacao = "CLOCKWISE";
            if (sentido == TipoDeOrientacao.SentidoAntiHorario) orientacao = "COUNTERCLOCKWISE";
            return "<REQUEST><RT>ROTATE PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y><SENSE>" + orientacao + "</SENSE></REQUEST>";
        }

        /* Produz a mensagem para verificar se o jogador est� no jogo.
         * Se sim, retorna qual jogador ele � (Jogador1, Jogador2, etc).
         * Par�metros:
         *   identificadorDoJogo: id do jogo
         *   identificadorDoJogador: id do jogador que est� verificando se
         *     est� ou n�o no tabuleiro.
         */
        static public String estouNesteJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>AM I IN THIS GAME</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita que um dado jogo seja apagado.
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer apagar
         */
        static public String apagarJogo(int identificadorDoJogo)
        {
            return "<REQUEST><RT>DELETE GAME</RT><GID>" + identificadorDoJogo + "</GID></REQUEST>";
        }

        /* Produz a mensagem para um jogador entrar num jogo.
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer entrar
         *   identificadorDoJogador: id do jogador que tentando entrar no jogo.
         */
        static public String entrarEmUmJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>join game</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que requisita a lista de jogos dispon�veis no
         *   servidor de jogos. 
         */
        static public String listarJogos()
        {
            return "<REQUEST><RT>LIST GAMES</RT></REQUEST>";
        }

        /* Produz a mensagem que requisita a lista de jogos de um dado tipo, dispon�veis no
         *   servidor de jogos. Exemplos de tipo: tictactoe, chess, checkers
         */
        static public String listarJogos(String tipo)
        {
            return "<REQUEST><RT>LIST GAMES</RT><GT>"+tipo+"</GT></REQUEST>";
        }


        /* Produz a mensagem para a cria��o de um novo jogo no servidor. 
         * Par�metro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         */
        static public String novoJogo(String tipo)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT></REQUEST>";
        }

        /* Produz a mensagem para a cria��o de um novo jogo no servidor e j�
         *   adiciona o jogador ao jogo.
         * Par�metros:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   identificadorDoJogador: id do jogador que entrar� no jogo.
         */
        static public String novoJogo(String tipo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem para a cria��o de um novo jogo no servidor. 
         * Par�metro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   descricaoDaInstanciaDoJogo: t�tulo da inst�ncia do jogo que ser�
         *     criada
         */
        static public String novoJogo(String tipo, String descricaoDaInstanciaDoJogo)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><GAME_NAME>" + descricaoDaInstanciaDoJogo + "</GAME_NAME></REQUEST>";
        }

        /* Produz a mensagem para a cria��o de um novo jogo no servidor e j�
         *   adiciona o jogador ao jogo.
         * Par�metro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   descricaoDaInstanciaDoJogo: t�tulo da inst�ncia do jogo que ser�
         *     criada
         *   identificadorDoJogador: id do jogador que entrar� no jogo.
         */
        static public String novoJogo(String tipo, String descricaoDaInstanciaDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><GAME_NAME>" + descricaoDaInstanciaDoJogo + "</GAME_NAME><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita o movimento de uma pe�a.
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que est� solicitando o tabuleiro.
         *   origemX: posi��o X (linha) em que a pe�a est�.
         *   origemY: posi��o Y (coluna) em que a pe�a est�.
         *   destinoX: posi��o X (linha) para onde a pre�a ir�.
         *   destinoY: posi��o Y (coluna) para onde a pre�a ir�.
         */
        static public String moverPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY, int destinoX, int destinoY)
        {
            return "<REQUEST><RT>MOVE PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y><X2>" + destinoX + "</X2><Y2>" + destinoY + "</Y2></REQUEST>";
        }

        /* Produz a mensagem que solicita a adi��o de uma peca em uma dada
         *   posi��o (m�todo utilizado pelo jogo da velha).
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que est� solicitando o tabuleiro.
         *   origemX: posi��o X (linha) em que a pe�a ser� adicionada.
         *   origemY: posi��o Y (coluna) em que a pe�a ser� adicionada.
         */
        static public String adicionarPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY)
        {
            return "<REQUEST><RT>ADD PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y></REQUEST>";
        }

        /* Produz a mensagem que solicita a finaliza��o de um turno
         *   (jogada v�lida no jogo de damas, quando o jogador pode comer uma
         *    segunda pe�a, mas n�o o deseja fazer)
         * Par�metros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que est� solicitando o tabuleiro.
         */
        static public String finalizarTurno(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>END TURN</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }


        /* M�todo auxiliar para converter uma vari�vem do tipo enumer�vel
         *   TipoDeJogo para uma String contendo o nome do jogo
         * Par�metro:
         *   tipo: tipo do jogo cujo nome se deseja converter
         */
        static private String converteTipoDeJogoParaString(TipoDeJogo tipo)
        {
            if (tipo == TipoDeJogo.JogoDaVelha2D)
            {
                return "tictactoe";
            }
            else if (tipo == TipoDeJogo.Damas)
            {
                return "checkers";
            }
            else if (tipo == TipoDeJogo.Xadrez)
            {
                return "chess";
            }
            else if (tipo == TipoDeJogo.XadrezDinamico)
            {
                return "dinamicchess";
            }
            else if (tipo == TipoDeJogo.Damas)
            {
                return "chess";
            }
            return "JogoIndefinido";
        }
    }
}