using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace JogadorJogoDaVelha
{
    /* CLASSE: MensagensDeRequisicao
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Esta classe contém os métodos para a produção das mensagens XML que
     *   serão enviadas ao servidor de jogos.
     */
    static class MensagensDeRequisicao
    {
        public enum TipoDeJogo { JogoDaVelha2D, Xadrez, Damas, XadrezDinamico };
        public enum TipoDeOrientacao { SentidoHorario, SentidoAntiHorario };

        /* Produz a mensagem para requisição das informações de um dado tabuleiro. 
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que está solicitando o tabuleiro.
         */
        static public String receberTabuleiro(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>GAME BOARD</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

         /* Produz a mensagem para requisição das informações gerais de um dado jogo. 
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o estado
         *   identificadorDoJogador: id do jogador que está solicitando as informações
         */
        static public String estadoDoJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>GAME STATUS</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita a rotação de uma peça no sentido
         *   horário ou anti-horário (jogada válida no jogo deflexion)
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que está solicitando o tabuleiro.
         *   origemX: posição X (linha) em que a peça está.
         *   origemY: posição Y (coluna) em que a peça está.
         *   sentido: sentido que a peça será rodada (horário ou anti-horário)
         */
        static public String rotacionarPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY, TipoDeOrientacao sentido)
        {
            String orientacao = "CLOCKWISE";
            if (sentido == TipoDeOrientacao.SentidoAntiHorario) orientacao = "COUNTERCLOCKWISE";
            return "<REQUEST><RT>ROTATE PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y><SENSE>" + orientacao + "</SENSE></REQUEST>";
        }

        /* Produz a mensagem para verificar se o jogador está no jogo.
         * Se sim, retorna qual jogador ele é (Jogador1, Jogador2, etc).
         * Parâmetros:
         *   identificadorDoJogo: id do jogo
         *   identificadorDoJogador: id do jogador que está verificando se
         *     está ou não no tabuleiro.
         */
        static public String estouNesteJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>AM I IN THIS GAME</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita que um dado jogo seja apagado.
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer apagar
         */
        static public String apagarJogo(int identificadorDoJogo)
        {
            return "<REQUEST><RT>DELETE GAME</RT><GID>" + identificadorDoJogo + "</GID></REQUEST>";
        }

        /* Produz a mensagem para um jogador entrar num jogo.
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer entrar
         *   identificadorDoJogador: id do jogador que tentando entrar no jogo.
         */
        static public String entrarEmUmJogo(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>join game</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que requisita a lista de jogos disponíveis no
         *   servidor de jogos. 
         */
        static public String listarJogos()
        {
            return "<REQUEST><RT>LIST GAMES</RT></REQUEST>";
        }

        /* Produz a mensagem que requisita a lista de jogos de um dado tipo, disponíveis no
         *   servidor de jogos. Exemplos de tipo: tictactoe, chess, checkers
         */
        static public String listarJogos(String tipo)
        {
            return "<REQUEST><RT>LIST GAMES</RT><GT>"+tipo+"</GT></REQUEST>";
        }


        /* Produz a mensagem para a criação de um novo jogo no servidor. 
         * Parâmetro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         */
        static public String novoJogo(String tipo)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT></REQUEST>";
        }

        /* Produz a mensagem para a criação de um novo jogo no servidor e já
         *   adiciona o jogador ao jogo.
         * Parâmetros:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   identificadorDoJogador: id do jogador que entrará no jogo.
         */
        static public String novoJogo(String tipo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem para a criação de um novo jogo no servidor. 
         * Parâmetro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   descricaoDaInstanciaDoJogo: título da instância do jogo que será
         *     criada
         */
        static public String novoJogo(String tipo, String descricaoDaInstanciaDoJogo)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><GAME_NAME>" + descricaoDaInstanciaDoJogo + "</GAME_NAME></REQUEST>";
        }

        /* Produz a mensagem para a criação de um novo jogo no servidor e já
         *   adiciona o jogador ao jogo.
         * Parâmetro:
         *   tipo: tipo de jogo a ser criado (tictactoe, checkers, chess,
         *     deflexion, etc)
         *   descricaoDaInstanciaDoJogo: título da instância do jogo que será
         *     criada
         *   identificadorDoJogador: id do jogador que entrará no jogo.
         */
        static public String novoJogo(String tipo, String descricaoDaInstanciaDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>new game</RT><GT>" + tipo + "</GT><GAME_NAME>" + descricaoDaInstanciaDoJogo + "</GAME_NAME><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }

        /* Produz a mensagem que solicita o movimento de uma peça.
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que está solicitando o tabuleiro.
         *   origemX: posição X (linha) em que a peça está.
         *   origemY: posição Y (coluna) em que a peça está.
         *   destinoX: posição X (linha) para onde a preça irá.
         *   destinoY: posição Y (coluna) para onde a preça irá.
         */
        static public String moverPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY, int destinoX, int destinoY)
        {
            return "<REQUEST><RT>MOVE PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y><X2>" + destinoX + "</X2><Y2>" + destinoY + "</Y2></REQUEST>";
        }

        /* Produz a mensagem que solicita a adição de uma peca em uma dada
         *   posição (método utilizado pelo jogo da velha).
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que está solicitando o tabuleiro.
         *   origemX: posição X (linha) em que a peça será adicionada.
         *   origemY: posição Y (coluna) em que a peça será adicionada.
         */
        static public String adicionarPeca(int identificadorDoJogo, int identificadorDoJogador, int origemX, int origemY)
        {
            return "<REQUEST><RT>ADD PIECE</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER><X>" + origemX + "</X><Y>" + origemY + "</Y></REQUEST>";
        }

        /* Produz a mensagem que solicita a finalização de um turno
         *   (jogada válida no jogo de damas, quando o jogador pode comer uma
         *    segunda peça, mas não o deseja fazer)
         * Parâmetros:
         *   identificadorDoJogo: id do jogo que se quer o tabuleiro
         *   identificadorDoJogador: id do jogador que está solicitando o tabuleiro.
         */
        static public String finalizarTurno(int identificadorDoJogo, int identificadorDoJogador)
        {
            return "<REQUEST><RT>END TURN</RT><GID>" + identificadorDoJogo + "</GID><PLAYER>" + identificadorDoJogador + "</PLAYER></REQUEST>";
        }


        /* Método auxiliar para converter uma variávem do tipo enumerável
         *   TipoDeJogo para uma String contendo o nome do jogo
         * Parâmetro:
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