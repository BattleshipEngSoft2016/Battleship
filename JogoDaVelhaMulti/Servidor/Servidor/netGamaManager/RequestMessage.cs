using Servidor.netGameManager;
using System.Collections;
using System;
using System.Text.RegularExpressions;

namespace Servidor.netGameManager
{

    /* CLASSE: RequestMessage
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Classe que processa uma mensagem XML de requisi��o de um cliente,
     *   separando os diversos campos que comp�e a mensagem em atributos desta
     *   classe.
     */

    /*
     *  Exemplos de mensagem:
     *   LIST OF GAMES:	<REQUEST><RT>LIST GAMES</RT><GT>[all|checkers|deflexion|...]</GT><GS>[all|waiting_players]</GS></REQUEST>
     *   NEW GAME:		<REQUEST><RT>NEW GAME</RT><GT>[checkers|deflexion|...]CHECKERS</GT>[<GAME_NAME>[string game name]</GAME_NAME>,<PLAYER>[|player1_id]</PLAYER>]</REQUEST>
     *   JOIN GAME:	   	<REQUEST><RT>JOIN GAME</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER></REQUEST>   
     *   DELETE GAME:	<REQUEST><RT>DELETE GAME</RT><GID>[game_id]</GID></REQUEST>
     *   ADD PIECE:    	<REQUEST><RT>ADD PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[x value]</X><Y>[y value]</Y></REQUEST>
     *   MOVE PIECE:     <REQUEST><RT>MOVE PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[from x1]</X><Y>[from y1]</Y><X2>[to x2]</X2><Y2>[to y2]</Y2></REQUEST>
     *   ROTATE PIECE:	<REQUEST><RT>ROTATE PIECE</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER><X>[x value]</X><Y>[y value]</Y><SENSE>[CLOCKWISE|ANTICLOCKWISE]</SENSE></REQUEST>
     *   <REQUEST><RT>AM I IN THIS GAME</RT><GID>[game_id]</GID><PLAYER>[player_id]</PLAYER></REQUEST>
     */

    public class RequestMessage
    {
        public bool OK = false;
        public String RT = "";
        public String GT = "";
        public String GN = "";
        public int GID = -1;
        public int PLAYER = -1;
        public String SENSE = "";
        public int X = -1;
        public int Y = -1;
        public int X2 = -1;
        public int Y2 = -1;
        public String REQUEST = null;
        public AddressPort REQUESTER_ADDRESS = null;

        /* Construtor padr�o para a classe RequestMessage, processa a mensagem
         *   de requis�o XML recebida de um cliente e armazena seu conte�do
         *   nos atributos globais da classe.
         * Par�metros:
         *   request: mensagem XML de requisi��o (enviada por uma aplica��o
         *            cliente)
         *   address: endere�o IP e porta da aplica��o cliente que enviou
         *            a mensagem ao servidor.
         */
        public RequestMessage(String request, AddressPort address)
        {
            REQUEST = request;
            OK = false;
            Regex is_request = new Regex(@"<REQUEST>(.+)</REQUEST>", RegexOptions.IgnoreCase);
            MatchCollection m_class = is_request.Matches(request);
            if (m_class.Count > 0)
            {
                request = m_class[0].Groups[1].ToString();
                Regex kind_of_request = new Regex(@"(.+)<(.+)>(.+)<.+>", RegexOptions.IgnoreCase);
                m_class = kind_of_request.Matches(request);
                Console.WriteLine(request);
                while (m_class.Count > 0)
                {
                    String type = m_class[0].Groups[2].ToString();
                    String value = m_class[0].Groups[3].ToString();
                    request = m_class[0].Groups[1].ToString();
                    m_class = kind_of_request.Matches(request);
                    if (!(update_data(type, value)))
                    {
                        Console.Error.WriteLine("Field not known: " + type + " => " + value);
                    }
                }
                kind_of_request = new Regex(@"<(.+)>(.+)<(.+)>", RegexOptions.IgnoreCase);
                m_class = kind_of_request.Matches(request);
                if (m_class.Count > 0)
                {
                    String type = m_class[0].Groups[1].ToString();
                    String value = m_class[0].Groups[2].ToString();

                    if (!(update_data(type, value)))
                    {
                        Console.Error.WriteLine("Field not known: " + type + " => " + value);
                    }
                    OK = true;
                }
            }
        }

        /* M�todo auxiliar utilizado pelo construtor para atualizar alguns
         *    atributos da classe com base nos tipos de requisi��o contidas
         *    na mensagem de requisi��o recebida pelo construtor da classe.
         * Par�metros:
         *   type: tipo de tag XML que ser� processada
         *   value = valor (parte da mensagem) que est� sendo processada
         */
        private bool update_data(String type, String value)
        {
            if (type.Equals("RT", StringComparison.CurrentCultureIgnoreCase))
            {
                RT = value;
            }
            else
                if (type.Equals("GT", StringComparison.CurrentCultureIgnoreCase))
                {
                    GT = value;
                }
                else
                    if (type.Equals("GN", StringComparison.CurrentCultureIgnoreCase))
                    {
                        GN = value;
                    }
                    else
                        if (type.Equals("GID", StringComparison.CurrentCultureIgnoreCase))
                        {
                            GID = int.Parse(value);
                        }
                        else
                            if (type.Equals("PLAYER", StringComparison.CurrentCultureIgnoreCase))
                            {
                                PLAYER = int.Parse(value);
                            }
                            else
                                if (type.Equals("SENSE", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    SENSE = value;
                                }
                                else
                                    if (type.Equals("X", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        X = int.Parse(value);
                                    }
                                    else
                                        if (type.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            Y = int.Parse(value);
                                        }
                                        else
                                            if (type.Equals("X2", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                X2 = int.Parse(value);
                                            }
                                            else
                                                if (type.Equals("Y2", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    Y2 = int.Parse(value);
                                                }
                                                else
                                                {
                                                    return false;
                                                }
            return true;
        }


        public virtual String getClass2()
        {
            return "RequestMessage";
        }
        public static String getClass()
        {
            return "RequestMessage";
        }
    }
}