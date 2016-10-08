using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager
{

    /**
     * Esta classe define um jogo de tabuleiro genérico.
     * Todo tipo de interação com qualquer jogo de tabuleiro deve ser feita
     * utilizando algum dos métodos da interface pública desta classe. 
     * Cada sub-classe desta classe deverá implementar os métodos correspondentes
     * às ações possíveis de cada tipo de jogo.
     * Cada jogo contém um tabuleiro {@link Board} e cada tabuleiro contém peças (@link piece}.
     *  
     * @author Luciano Antonio Digiampietri
     * @version 1.0 beta - 03/11/2008
     * 
     * @see Board
     * @see Piece
     */
    public abstract class Game
    {
        public virtual String getClass2()
        {
            return "Game";
        }

        public static String getClass()
        {
            return "Game";
        }

        public String GAME_NAME = "Generic Game";
        public int ACTUAL_PLAYER = -1;
        public Board BOARD = null;
        public int WINNER = -1;
        public bool GAME_ENDED = false;
        public bool DRAWN_GAME = false;
        public ArrayList PLAYERS = new ArrayList();
        public int CONT_ID = -1;
        public String GAME_TYPE = "generic game";
        public int MAX_PLAYERS = 2;
        public bool WAITING_PLAYERS = true;
        public int MIN_PLAYERS = 0;
        public bool GAME_STARTED = false;
        public String LAST_MESSAGE = " ";

        public Game(String name)
        {
            GAME_NAME = name;
            initializeBoard();
            CONT_ID++;
            GAME_ENDED = false;
        }

        public Game()
        {
            GAME_NAME = "new game";
            PLAYERS = new ArrayList();
            initializeBoard();
            CONT_ID++;
            GAME_ENDED = false;
        }

        public int getActualPlayer()
        {
            return ACTUAL_PLAYER;
        }

        public Game(String name, int Player)
        {
            GAME_NAME = name;
            PLAYERS.Add(Player);
            initializeBoard();
            CONT_ID++;
        }

        public virtual void initializeBoard()
        {
            Console.WriteLine("Trying to initialize board - game.");
            BOARD = null;
        }

        public bool canAdd(int x, int y, int Player)
        {
            return false;
        }

        public int getCurrentGameId()
        {
            return CONT_ID;
        }

        public String currentBoard()
        {
            return BOARD.board2string();
        }

        public virtual bool move(int x1, int y1, int x2, int y2, int Player)
        {
            Console.WriteLine("Trying to move.");
            return false;
        }

        public virtual bool endTurn(int Player)
        {
            Console.WriteLine("Trying to end turn.");
            return false;
        }

        public virtual bool canMove(int x1, int y1, int x2, int y2, int Player)
        {
            Console.WriteLine("Trying to can move.");
            return false;
        }

        public virtual bool add(int x, int y, int Player)
        {
            Console.WriteLine("Trying to add.");
            return false;
        }

        public String getGameName()
        {
            return GAME_NAME;
        }

        public int getLines()
        {
            return BOARD.getLines();
        }

        public int getColumns()
        {
            return BOARD.getColumns();
        }

        public virtual bool gameEnded()
        {
            return GAME_ENDED;
        }

        public int getMaxPlayers()
        {
            return MAX_PLAYERS;
        }

        public int getMinPlayers()
        {
            return MIN_PLAYERS;
        }

        public String getGameType()
        {
            return GAME_TYPE;
        }

        public virtual bool rotate(int x, int y, String direction, int Player)
        {
            return false;
        }

        public ArrayList getPlayers()
        {
            return PLAYERS;
        }

        public int isPlayerInThisGame(int playerID)
        {
            for (int i = 0; i < PLAYERS.Count; i++)
            {
                if (playerID == (int)PLAYERS[i]) return (i + 1);
            }
            return -1;
        }

        public bool joinGame(int playerID)
        {
            if (WAITING_PLAYERS)
            {
                if (PLAYERS.Count < MAX_PLAYERS)
                {
                    if (!(PLAYERS.Contains(playerID)))
                    {

                        PLAYERS.Add(playerID);
                        Console.WriteLine("Joining player: " + playerID);
                        CONT_ID++;
                        if (PLAYERS.Count == MAX_PLAYERS)
                        {
                            startGame();
                            ACTUAL_PLAYER = (int)PLAYERS[0];
                            WAITING_PLAYERS = false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual void startGame()
        {
            WAITING_PLAYERS = false;
            Console.WriteLine("generic class game => startGame");
        }

        public bool waitingPlayers()
        {
            return WAITING_PLAYERS;
        }

        public String getMessage()
        {
            return LAST_MESSAGE;
        }

        public bool gameDraw()
        {
            return DRAWN_GAME;
        }
    }
}