using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager
{

    /**
     * Esta classe estende o jogo de tabuleiro genérico (Game) para permitir
     * o gerenciamento de um jogo da velha (tabuleiro 3 x 3).
 
     */
    public class NovoJogoGame : Game
    {
        public override String getClass2()
        {
            return "NovoJogoGame";
        }
        new public static String getClass()
        {
            return "NovoJogoGame";
        }
        int PLAYER1 = -1;
        int PLAYER2 = -1;

        public NovoJogoGame(String name)
            : base(name)
        {
            GAME_TYPE = "novojogo";
            MAX_PLAYERS = 2;
            MIN_PLAYERS = 2;
            GAME_ENDED = false;
        }

        public NovoJogoGame(String name, int player1)
            : base(name, player1)
        {
            GAME_TYPE = "novojogo";
            MAX_PLAYERS = 2;
            MIN_PLAYERS = 2;
            GAME_ENDED = false;
        }

        public override void startGame()
        {
            Console.WriteLine("Iniciando um novo jogo (NovoJogo). Numero de Jogadores:" + PLAYERS.Count);
            if (PLAYERS.Count >= 2)
            {
                PLAYER1 = (int)PLAYERS[0];
                PLAYER2 = (int)PLAYERS[1];
                Console.WriteLine("Jogo Novo, jogadores: " + PLAYER1 + " ; " + PLAYER2);
                ACTUAL_PLAYER = PLAYER1;
                WAITING_PLAYERS = false;
                GAME_ENDED = false;
            }
            else
            {
                Console.Error.WriteLine("Numero errado de jogadores:" + PLAYERS.Count);
            }
        }

        public bool canAdd(int x, int y, Piece P)
        {
            if ((GAME_ENDED) || (WAITING_PLAYERS))
            {
                return false;
            }
            if (BOARD.getPiece(x, y).getClass2() != NullPiece.getClass())
            {
                return false;
            }
            int cont_O = BOARD.contPieces(PLAYER2);
            int cont_X = BOARD.contPieces(PLAYER1);

            if (P.getPlayer() == PLAYER2)
            {
                if (cont_O == cont_X - 1)
                {
                    ACTUAL_PLAYER = PLAYER1;
                    return true;
                }
            }
            else if ((P.getPlayer() == PLAYER1))
            {
                if (cont_O == cont_X)
                {
                    ACTUAL_PLAYER = PLAYER2;
                    return true;
                }
            }
            return false;
        }

        public override bool add(int x, int y, int Player)
        {
            NovoJogoPiece P = null;

            if (Player == PLAYER1)
            {
                P = new NovoJogoPiece("X", PLAYER1,NovoJogoPiece.PECA_X);
            }
            else if (Player == PLAYER2)
            {
                P = new NovoJogoPiece("O", PLAYER2, NovoJogoPiece.PECA_O);
            }
            else
            {
                Console.WriteLine("O Jogador que esta tentando jogar nao pertence ao jogo: '" + Player + "'. Jogadores no jogo: '" + PLAYER1 + "' e '" + PLAYER2 + "'.");
                return false;
            }
            if (canAdd(x, y, (Piece)P))
            {
                if (!BOARD.addPiece(x, y, (Piece)P)) { }
                else
                {
                    Console.WriteLine("Nao foi possivel adicionar a peca. Problema desconhecido.");
                }
                if ((BOARD.contPieces(new NovoJogoPiece("", -1,"")) == 9) && WINNER == -1)
                {
                    DRAWN_GAME = true;
                    GAME_ENDED = true;
                    LAST_MESSAGE = "VELHA";
                }
                if (gameEnded())
                {
                    if (DRAWN_GAME)
                    {
                        Console.WriteLine("Jogo encerrado. " + LAST_MESSAGE);
                    }
                    else
                    {
                        Console.WriteLine("Jogo encerrado. Vencedor: " + WINNER);
                    }
                }
                CONT_ID++;
                return true;
            }
            return false;
        }

        public override void initializeBoard()
        {
            BOARD = new Board(3, 3);
        }

        public override bool gameEnded()
        {
            if (GAME_ENDED)
            {
                return true;
            }
            if (player1Won())
            {
                WINNER = PLAYER1;
                GAME_ENDED = true;
                return true;
            }
            else if (player2Won())
            {
                WINNER = PLAYER2;
                GAME_ENDED = true;
                return true;
            }
            return false;
        }

        public bool player1Won()
        {
            if (WINNER != -1 && WINNER == PLAYER1)
            {
                return true;
            }
            else if (won(PLAYER1))
            {
                WINNER = PLAYER1;
                return true;
            }
            return false;
        }
        public bool player2Won()
        {
            if (WINNER != -1 && WINNER == PLAYER2)
            {
                return true;
            }
            else if (won(PLAYER2))
            {
                WINNER = PLAYER2;
                return true;
            }
            return false;
        }



        public bool won(int Player)
        {
            int c1, c2, c3;
            if (Player == -1)
            {
                return false;
            }
            for (int cont = 0; cont < 3; cont++)
            {

                c1 = BOARD.getPiece(0, cont).getPlayer();
                c2 = BOARD.getPiece(1, cont).getPlayer();
                c3 = BOARD.getPiece(2, cont).getPlayer();
                if (c1 == Player && c2 == Player && c3 == Player)
                {
                    return true;
                }


                c1 = BOARD.getPiece(cont, 0).getPlayer();
                c2 = BOARD.getPiece(cont, 1).getPlayer();
                c3 = BOARD.getPiece(cont, 2).getPlayer();
                if (c1 == Player && c2 == Player && c3 == Player)
                {
                    return true;
                }
            }

            c1 = BOARD.getPiece(0, 0).getPlayer();
            c2 = BOARD.getPiece(1, 1).getPlayer();
            c3 = BOARD.getPiece(2, 2).getPlayer();
            if (c1 == Player && c2 == Player && c3 == Player)
            {
                return true;
            }


            c1 = BOARD.getPiece(0, 2).getPlayer();
            c2 = BOARD.getPiece(1, 1).getPlayer();
            c3 = BOARD.getPiece(2, 0).getPlayer();
            if (c1 == Player && c2 == Player && c3 == Player)
            {
                return true;
            }
            return false;
        }
    }


}