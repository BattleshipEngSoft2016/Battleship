using Servidor.netGameManager;
using System.Collections;
using System;
using System.Threading;
using Servidor.manager;
using System.Net.Sockets;
using Servidor.netGamaManager;

namespace Servidor.netGameManager
{

    /* CLASSE: GameServer
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Classe que gerencia diversos jogos (instâncias das sub-classes da 
     *   classe Game).
     * Abre portas de comunicação TCP e UDP para que clientes possam se
     *   comunicar com o servidor.
     */

    /*
     * COMUNICATION PATERN 
     *  ANSWER  PATERN:
     *   
     *  REQUEST PATERN: 
     *   LIST OF GAMES:	<REQUEST><RT>LIST GAMES</RT><GT>[all|checkers|deflexion|...]</GT><GS>[all|waiting_players]</GS></REQUEST>
     *   SERVER STATUS:	<REQUEST><RT>SERVER STATUS</RT></REQUEST>
     *   NEW GAME:		<REQUEST><RT>NEW GAME</RT><GT>[checkers|deflexion|...]CHECKERS</GT>[<GAME_NAME>[string game name]</GAME_NAME>,<PLAYER>[|player1_id]</PLAYER>]</REQUEST>
     *   GAME STATUS: 	<REQUEST><RT>GAME STATUS</RT><GID>[game_id]</GID></REQUEST>
     *   GAME BOARD: 	<REQUEST><RT>GAME BOARD</RT><GID>[game_id]</GID><PLAYER>[]</PLAYER></REQUEST>
     *   JOIN GAME:	   	<REQUEST><RT>JOIN GAME</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER></REQUEST>   
     *   DELETE GAME:	<REQUEST><RT>DELETE GAME</RT><GID>[game_id]</GID></REQUEST>
     *   ADD PIECE:    	<REQUEST><RT>ADD PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[x value]</X><Y>[y value]</Y></REQUEST>
     *   END TURN:    	<REQUEST><RT>END TURN</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER></REQUEST>
     *   MOVE PIECE:    	<REQUEST><RT>MOVE PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X1>[from x1]</X1><Y1>[from y1]</Y1><X2>[to x2]</X2><Y2>[to y2]</Y2></REQUEST>
     *   ROTATE PIECE:	<REQUEST><RT>ROTATE PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[x value]</X><Y>[y value]</Y><SENSE>[CLOCKWISE|ANTICLOCKWISE]</SENSE></REQUEST>
     *   <REQUEST><RT>ROTATE PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[x value]</X><Y>[y value]</Y><SENSE>[CLOCKWISE|COUNTERCLOCKWISE]</SENSE></REQUEST>
     *   <REQUEST><RT>AM I IN THIS GAME</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER></REQUEST>
     *   
     * RT 	= REQUEST TYPE
     * GT 	= GAME TYPE : checkers, tictactoe, deflexion
     * GID 	= GAME ID
     * GS	= GAME STATUS
     * WT 	= WAITING PLAYERS
     */

    public class GameServer
    {
        private Thread udpThread = null;
        private Thread tcpThread = null;
        private UDPServer udpServer = null;
        private TCPServer tcpServer = null;

        private String[] LOG;
        private int oldestLog = -1;
        private int nextLog = 0;
        private int logSize = 0;
            
        public static int game_last_id = -1;

        /**
         * Default port on which to provide TCP and UDP
         * echo services.
         * This is not final, so a caller could change it
         * to provide echo services on a different port.
         */
        public int PORT = 8623;
        public string HOST = "172.16.36.85";

        /** 
         * Create a DualServer object to offer echo service
         * on TCP and UDP ports on the default IP address.
         */

        public GameServer(String host, int port)
        {
            PORT = port;
            HOST = host;
            return;
         }

        public GameServer(String host, int port, int pLogSize)
        {
            PORT = port;
            HOST = host;
            LOG = new string[pLogSize];
            logSize = pLogSize;
            return;
         }



         public virtual String getClass2()
         {
             return "GameServer";
         }
         public static String getClass()
         {
             return "GameServer";
         }

         public static Hashtable GAMES = new Hashtable();

         public static Hashtable GAMES_PLAYERS = new Hashtable();

         /**
  * Test main - test out the echo server on its
  * default port, or on a port specified by args[0].
  */
         public void startServer()
         {
             Console.WriteLine("Starting Game Server");
             udpServer = new UDPServer(HOST, PORT,this);
             tcpServer = new TCPServer(HOST, PORT,this);
             Console.WriteLine("Starting UDP and TCP Servers");

             udpThread = new Thread(udpServer.startUDPServer);
             tcpThread = new Thread(tcpServer.startTCPServer);

             udpThread.Start();
             tcpThread.Start();

         }

         public void stopServer()
         {

             tcpServer.stopTCPServer();
             udpServer.stopUDPServer();

             Console.WriteLine("Aborting threads.");

             tcpThread.Abort();
             udpThread.Abort();

             Console.WriteLine("Server ended.");
             //Environment.Exit(0);
         }




        public static AnswerMessage executeRequest(RequestMessage req_message)
        {

            Console.WriteLine("Request message read: " + req_message.OK);
            if (req_message.OK)
            {
                if (req_message.RT.Equals("new game", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running new game");
                    return newGame(req_message);
                }
                if (req_message.RT.Equals("server status", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running server status");
                    return serverStatus();
                }
                if (req_message.RT.Equals("list games", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running list games");
                    return listGames(req_message);
                }
                if (req_message.RT.Equals("game status", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running game status");
                    return gameStatus(req_message);
                }
                if (req_message.RT.Equals("game board", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running game board");
                    return gameCurrentBoard(req_message);
                }
                if (req_message.RT.Equals("current board", StringComparison.CurrentCultureIgnoreCase))
                {
                    return gameCurrentBoard(req_message);
                }
                if (req_message.RT.Equals("join game", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running join game " + req_message.GID);
                    return joinGame(req_message);
                }
                if (req_message.RT.Equals("am i in this game", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running am i in this game " + req_message.GID + req_message.PLAYER);
                    return amIInThisGame(req_message);
                }
                if (req_message.RT.Equals("delete game", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running delete game " + req_message.GID);
                    return deleteGame(req_message);
                }
                if (req_message.RT.Equals("add piece", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running add piece " + req_message.GID);
                    return addPiece(req_message);
                }
                if (req_message.RT.Equals("end turn", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running end turn " + req_message.GID);
                    return endTurn(req_message);
                }

                if (req_message.RT.Equals("rotate piece", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running rotate piece " + req_message.GID);
                    return rotatePiece(req_message);
                }
                if (req_message.RT.Equals("move piece", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("### Running move piece " + req_message.GID);
                    return movePiece(req_message);
                }
            }
            else
            {
                Console.WriteLine("Command did not match.");
                return new AnswerMessage(false, AnswerMessage.OUT_OF_PATTERN, "", -1);

            }

            return new AnswerMessage(false, AnswerMessage.INVALID_REQUEST_TYPE, "", -1);
        }


        private static AnswerMessage serverStatus()
        {
            String STATUS = "<SERVER STATUS>";
            for (int cont = 0; cont <= game_last_id; cont++)
            {
                if (GAMES.ContainsKey(cont))
                {
                    STATUS = STATUS + "<" + cont + ":" + ((Game)GAMES[cont]).getClass2() + ":" + ((manager.Game)GAMES[cont]).getGameName() + ">";
                }
            }

            return new AnswerMessage(true, AnswerMessage.SERVER_STATUS, STATUS, -1);

        }

        private static AnswerMessage listGames(RequestMessage rm)
        {
            String STATUS = "<LIST GAMES>";
            for (int cont = 0; cont <= game_last_id; cont++)
            {
                if (GAMES.ContainsKey(cont))
                {
                    manager.Game TEMP = (manager.Game)GAMES[cont];
                    if ((rm.GT.Length <= 1) || rm.GT.Equals(TEMP.GAME_TYPE, StringComparison.CurrentCultureIgnoreCase) || rm.GT.Equals("all", StringComparison.CurrentCultureIgnoreCase))
                    {
                        String this_game = "<GAME>";
                        this_game += "<GID>" + cont + "</GID>";
                        this_game += "<GT>" + TEMP.GAME_TYPE + "</GT>";
                        this_game += "<ENDED>" + TEMP.gameEnded() + "</ENDED>";
                        this_game += "<WP>" + TEMP.WAITING_PLAYERS + "</WP>";
                        this_game += "<#PLAYERS>" + TEMP.PLAYERS.Count + "</#PLAYERS>";
                        this_game += "</GAME>";

                        STATUS += this_game;
                    }
                }

            }
            STATUS += "</LIST GAMES>";
            return new AnswerMessage(true, AnswerMessage.GAME_LIST, STATUS, -1);
        }


        private static AnswerMessage gameStatus(RequestMessage message)
        {

            int gameID = message.GID;

            if (GAMES.ContainsKey(gameID))
            {
                String STATUS = "<GAME STATUS>";
                //STATUS += ((manager.Game)GAMES[gameID]).getStatus();
                return new AnswerMessage(true, AnswerMessage.GAME_STATUS, STATUS, gameID);
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }

        private static AnswerMessage amIInThisGame(RequestMessage message)
        {
            int gameID = message.GID;
            int playerID = message.PLAYER;
            if (GAMES.ContainsKey(gameID))
            {
                if ((manager.Game)GAMES[gameID] != null)
                {
                    int playerIndex = ((manager.Game)GAMES[gameID]).isPlayerInThisGame(playerID);
                    if (playerIndex > -1)
                    {
                        return new AnswerMessage(true, AnswerMessage.YOU_ARE_HERE, "<GID>" + gameID + "</GID><PLAYER>" + playerIndex + "</PLAYER>", gameID);
                    }
                }
            }
            return new AnswerMessage(false, AnswerMessage.YOU_ARE_NOT_HERE, "<GID>" + gameID + "</GID>", gameID);
        }

        private static AnswerMessage joinGame(RequestMessage message)
        {
            int gameID = message.GID;
            AddressPort ClientAddress = message.REQUESTER_ADDRESS;
            Console.Write("JOIN GAME steps: 1");
            if (GAMES.ContainsKey(gameID))
            {
                Console.Write(" 2");
                if ((manager.Game)GAMES[gameID] != null)
                {
                    Console.Write(" 3");

                    if (((manager.Game)GAMES[gameID]).getPlayers().Count < ((manager.Game)GAMES[gameID]).getMaxPlayers())
                    {
                        Console.Write(" 4");
                        if (((manager.Game)GAMES[gameID]).WAITING_PLAYERS)
                        {
                            Console.Write(" 5");
                            if (((manager.Game)GAMES[gameID]).joinGame(message.PLAYER))
                            {
                                ((ArrayList)GAMES_PLAYERS[gameID]).Add(ClientAddress);

                                ArrayList TempPlayers = ((manager.Game)GAMES[gameID]).getPlayers();
                                String StringPlayers = "";
                                int PlayerNumber = TempPlayers.Count;
                                String PlayerTag = "";
                                if (PlayerNumber == 1)
                                {
                                    PlayerTag = AnswerMessage.PLAYER1;
                                }
                                if (PlayerNumber == 2)
                                {
                                    PlayerTag = AnswerMessage.PLAYER2;
                                }
                                if (TempPlayers != null)
                                {
                                    for (int cont = 0; cont < TempPlayers.Count; cont++)
                                    {
                                        StringPlayers += (int)TempPlayers[cont] + ";";
                                    }
                                }
                                Console.WriteLine("");
                                Console.WriteLine("Number of players: " + ((ArrayList)GAMES_PLAYERS[gameID]).Count);
                                return new AnswerMessage(true, AnswerMessage.JOIN_GAME, "<GID>" + gameID + "</GID><GN>" + ((manager.Game)GAMES[gameID]).getGameName() + "</GN><GT>" + ((manager.Game)GAMES[gameID]).getGameType() + "</GT><LINES>" + ((manager.Game)GAMES[gameID]).getLines() + "</LINES><COLUMNS>" + ((manager.Game)GAMES[gameID]).getColumns() + "</COLUMNS><PLAYERS>" + StringPlayers + "</PLAYERS>" + PlayerTag, gameID);
                            }
                            else
                            {
                                return new AnswerMessage(false, AnswerMessage.CANNOT_JOIN_GAME, "<GID>" + gameID + "</GID><REASON>THE GAME IS NOT WAITING FOR PLAYERS</REASON>", gameID);
                            }
                        }
                        else
                        {
                            return new AnswerMessage(false, AnswerMessage.CANNOT_JOIN_GAME, "<GID>" + gameID + "</GID><REASON>THE GAME IS NOT WAITING FOR PLAYERS</REASON>", gameID);
                        }
                    }
                    else
                    {
                        return new AnswerMessage(false, AnswerMessage.CANNOT_JOIN_GAME, "<GID>" + gameID + "</GID><REASON>THE GAME IS FULL OF PLAYERS</REASON>", gameID);
                    }
                }
                else
                {
                    return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
                }
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }


        private static AnswerMessage deleteGame(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                GAMES.Remove(gameID);
                return new AnswerMessage(false, AnswerMessage.DELETE_GAME, "<GID>" + gameID + "</GID>", gameID);
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }


        private static AnswerMessage addPiece(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                int x = message.X;
                int y = message.Y;
                int Player = message.PLAYER;

                if (((manager.Game)GAMES[gameID]).add(x, y, Player))
                {
                    return new AnswerMessage(true, AnswerMessage.PIECE_ADDED, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y>", gameID);

                }
                else
                {
                    Console.WriteLine("Adding piece: " + message.X + "," + message.Y + "player " + message.PLAYER);
                    Console.WriteLine("Game Actual Player: " + ((manager.Game)GAMES[gameID]).getActualPlayer());
                    return new AnswerMessage(false, AnswerMessage.CANNOT_ADD_PIECE, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y><REASON>UNKNOWN REASON</REASON>", gameID);
                }
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }

        }

        private static AnswerMessage endTurn(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                int Player = message.PLAYER;

                if (((manager.Game)GAMES[gameID]).endTurn(Player))
                {
                    return new AnswerMessage(true, AnswerMessage.TURN_ENDED, "<GID>" + gameID + "</GID>", gameID);
                }
                else
                {
                    Console.WriteLine("Ending turn: " + "player " + message.PLAYER);
                    Console.WriteLine("Game Actual Player: " + ((manager.Game)GAMES[gameID]).getActualPlayer());
                    return new AnswerMessage(false, AnswerMessage.CANNOT_END_TURN, "<GID>" + gameID + "</GID><REASON>UNKNOWN REASON</REASON>", gameID);
                }
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }

        }

        private static AnswerMessage rotatePiece(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                int x = message.X;
                int y = message.Y;
                String sense = message.SENSE;
                int Player = message.PLAYER;
                Console.WriteLine("Rotating piece: " + message.X + "," + message.Y + "player " + message.PLAYER + " sense:" + sense);
                if (((manager.Game)GAMES[gameID]).rotate(x, y, sense, Player))
                {
                    return new AnswerMessage(true, AnswerMessage.PIECE_ROTATED, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y><SENSE>" + sense + "</SENSE>", gameID);

                }
                else
                {
                    return new AnswerMessage(false, AnswerMessage.CANNOT_ROTATE_PIECE, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y><SENSE>" + sense + "</SENSE><REASON>UNKNOWN REASON</REASON>", gameID);
                }
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }

        private static AnswerMessage movePiece(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                int x = message.X;
                int y = message.Y;
                int x2 = message.X2;
                int y2 = message.Y2;
                int Player = message.PLAYER;
                Console.WriteLine("Moving piece:  FROM " + message.X + "," + message.Y + "  TO " + message.X2 + "," + message.Y2 + " Player " + message.PLAYER);
                if (((manager.Game)GAMES[gameID]).move(x, y, x2, y2, Player))
                {
                    return new AnswerMessage(true, AnswerMessage.PIECE_MOVED, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y><X2>" + message.X2 + "</X2><Y2>" + message.Y2 + "</Y2>", gameID);
                }
                else
                {
                    return new AnswerMessage(false, AnswerMessage.CANNOT_MOVE_PIECE, "<GID>" + gameID + "</GID><X>" + message.X + "</X><Y>" + message.Y + "</Y><X2>" + message.X2 + "</X2><Y2>" + message.Y2 + "</Y2><REASON>UNKNOWN REASON</REASON>", gameID);
                }
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }



        private static AnswerMessage gameCurrentBoard(RequestMessage message)
        {
            int gameID = message.GID;
            if (GAMES.ContainsKey(gameID))
            {
                String STATUS = "<GAME CURRENT BOARD>";
                STATUS += ((manager.Game)GAMES[gameID]).currentBoard();
                STATUS += "</GAME CURRENT BOARD><LINES>" + ((manager.Game)GAMES[gameID]).getLines() + "</LINES><COLUMNS>" + ((manager.Game)GAMES[gameID]).getColumns() + "</COLUMNS><GAME INFO>" + ((manager.Game)GAMES[gameID]).LAST_MESSAGE + "</GAME INFO>";
                return new AnswerMessage(true, AnswerMessage.GAME_BOARD, "<GID>" + gameID + "</GID>" + STATUS, gameID);
            }
            else
            {
                return new AnswerMessage(false, AnswerMessage.GAME_DOES_NOT_EXIST, "<GID>" + gameID + "</GID>", gameID);
            }
        }


        private static AnswerMessage newGame(RequestMessage message)
        {
            AddressPort ClientAddress = message.REQUESTER_ADDRESS;
            manager.Game NEW_GAME = null;
            String game_type = message.GT;
            String game_name = message.GN;
            int player1 = message.PLAYER;

            Console.WriteLine("NEW GAME: game type " + game_type + ", game name: " + game_name + ", player id:" + player1);
            if (game_type.Equals("checkers", StringComparison.CurrentCultureIgnoreCase))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.CheckersGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.CheckersGame(game_name, player1);
                }
            }
            else if (game_type.Equals("novojogo", StringComparison.CurrentCultureIgnoreCase))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.NovoJogoGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.NovoJogoGame(game_name, player1);
                }
            }

            else if ((game_type.Equals("tictactoe", StringComparison.CurrentCultureIgnoreCase)) || (game_type.Equals("tic tac toe", StringComparison.CurrentCultureIgnoreCase)))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.TictactoeGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.TictactoeGame(game_name, player1);
                }
            }
            else if (game_type.Equals("deflexion", StringComparison.CurrentCultureIgnoreCase))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.DeflexionGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.DeflexionGame(game_name, player1);
                }
            }
            else if (game_type.Equals("chess", StringComparison.CurrentCultureIgnoreCase))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.ChessGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.ChessGame(game_name, player1);
                }
            }
            else if (game_type.Equals("checkers", StringComparison.CurrentCultureIgnoreCase))
            {
                if (player1 < 0)
                {
                    NEW_GAME = new manager.CheckersGame(game_name);
                }
                else
                {
                    NEW_GAME = new manager.CheckersGame(game_name, player1);
                }
            }


            if (NEW_GAME != null)
            {
                game_last_id = 1 + game_last_id;
                NEW_GAME.GAME_ENDED = false;
                //if (GAMES.ContainsKey(game_last_id))
                GAMES.Add(game_last_id, NEW_GAME);

                Console.WriteLine("   => NEW GAME CREATED " + game_last_id + " players:  " + ((manager.Game)(GAMES[game_last_id])).getPlayers().Count);
                GAMES_PLAYERS.Add(game_last_id, new ArrayList());
                if (player1 >= 0)
                {
                    ((ArrayList)(GAMES_PLAYERS[game_last_id])).Add(ClientAddress);
                    Console.WriteLine("SIZE: " + ((ArrayList)(GAMES_PLAYERS[game_last_id])).Count);


                }

                return new AnswerMessage(true, AnswerMessage.NEW_GAME, "<GID>" + game_last_id + "</GID><LINES>" + ((manager.Game)GAMES[game_last_id]).getLines() + "</LINES><COLUMNS>" + ((manager.Game)GAMES[game_last_id]).getColumns() + "</COLUMNS>", game_last_id);

            }
            return new AnswerMessage(false, AnswerMessage.NEW_GAME, "", -1);

        }




        public void start()
        {
            //tcpThread.Listen(65535);
            //udpThread.start();
            return;
        }



        public static String gameGeneralStatus(int gameID, int player)
        {

            if (GAMES.ContainsKey(gameID))
            {
                if ((manager.Game)GAMES[gameID] != null)
                {
                    manager.Game g = (manager.Game)GAMES[gameID];
                    if (g.GAME_ENDED)
                    {
                        if (g.WINNER < 0)
                        {
                            return AnswerMessage.GGS_GAME_ENDED + AnswerMessage.DRAWN_GAME + "<GAME LAST MESSAGE>" + g.LAST_MESSAGE + "</GAME LAST MESSAGE>";
                        }
                        else
                        {
                            return AnswerMessage.GGS_GAME_ENDED + "<WINNER>" + g.WINNER + "</WINNER>" + "<GAME LAST MESSAGE>" + g.LAST_MESSAGE + "</GAME LAST MESSAGE>";
                        }
                    }
                    if (g.WAITING_PLAYERS)
                    {
                        return AnswerMessage.GGS_WAITING_FOR_PLAYERS;
                    }
                    String PlayerTag = "";
                    if (g.getActualPlayer() == (int)g.PLAYERS[0])
                    {
                        PlayerTag = AnswerMessage.PLAYER1;
                    }
                    if (g.getActualPlayer() == (int)g.PLAYERS[1])
                    {
                        PlayerTag = AnswerMessage.PLAYER2;
                    }
                    if (g.getActualPlayer() == player)
                    {
                        return AnswerMessage.GGS_YOUR_TURN + PlayerTag;
                    }
                    if (g.getActualPlayer() != player)
                    {
                        return AnswerMessage.GGS_ADVERSARY_TURN + PlayerTag;
                    }
                    return "UNKNOW STATUS";
                }
                return AnswerMessage.GGS_GAME_DOES_NOT_EXIST;
            }
            return null;
        }

        public void insertLogMessage(string logType, string logMessage)
        {
            if (logSize > 0)
            {
                logMessage = "["+DateTime.Now+"]\t"+logType+"\t"+logMessage;
                LOG[nextLog] = logMessage;
                nextLog = (nextLog + 1) % logSize;
                if (nextLog < oldestLog) oldestLog = nextLog;
                else if (oldestLog == -1) oldestLog = 0;
            }
        }

        public string[] getLog()
        {
            int size = 0;
            if (oldestLog>=0) size = (logSize + nextLog - oldestLog - 1) % 100 + 1;
            string[] tempLog = new string[size];
            for (int i = size-1; i >= 0; i--)
            {
                tempLog[i] = LOG[(oldestLog + size-1 -i)%logSize];
            }
            return tempLog;
        }

    }

}