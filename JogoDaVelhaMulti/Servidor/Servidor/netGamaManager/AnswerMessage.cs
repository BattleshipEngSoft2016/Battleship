using Servidor.netGameManager;
using System.Collections;
using System;

namespace Servidor.netGameManager
{

    /* CLASSE: AnswerMessage
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Esta classe contém os valores (constantes) para a comunicação com o 
     *   servidor. Entre os valores presentes aqui estão: o tipo de requisição,
     *   os tipos de estado (status) geral do jogo, respostas afirmativas e
     *   negativas.
     * A classe é utilizada pelo servidor de jogos para a criação das respostas
     *   às consultas do usuário (cliente).
     */


    public class AnswerMessage
    {
        public virtual String getClass2()
        {
            return "AnswerMessage";
        }
        public static String getClass()
        {
            return "AnswerMessage";
        }
        public bool OK = false;
        public int GID = -1;
        public String GENERAL_MESSAGE = "";
        public String SPECIFIC_MESSAGE = "";
        public String GAME_GENERAL_STATUS = "";
        public const String YOU_ARE_HERE = "<ACTION PERFORMED>YOU ARE IN THIS GAME</ACTION PERFORMED>";
        public const String YOU_ARE_NOT_HERE = "<ACTION PERFORMED>YOU ARE NOT IN THIS GAME</ACTION PERFORMED>";
        public const String TURN_ENDED = "<ACTION PERFORMED>TURN ENDED</ACTION PERFORMED>";
        public const String PIECE_ADDED = "<ACTION PERFORMED>PIECE ADDED</ACTION PERFORMED>";
        public const String PIECE_MOVED = "<ACTION PERFORMED>PIECE MOVED</ACTION PERFORMED>";
        public const String PIECE_ROTATED = "<ACTION PERFORMED>PIECE ROTATED</ACTION PERFORMED>";
        public const String NEW_GAME = "<ACTION PERFORMED>NEW GAME CREATED</ACTION PERFORMED>";
        public const String JOIN_GAME = "<ACTION PERFORMED>PLAYER JOINED TO GAME</ACTION PERFORMED>";
        public const String CANNOT_JOIN_GAME = "<ACTION PERFORMED>CANNOT JOIN GAME</ACTION PERFORMED>";
        public const String CANNOT_END_TURN = "<ACTION PERFORMED>CANNOT END TURN</ACTION PERFORMED>";
        public const String CANNOT_ADD_PIECE = "<ACTION PERFORMED>CANNOT ADD PIECE</ACTION PERFORMED>";
        public const String CANNOT_MOVE_PIECE = "<ACTION PERFORMED>CANNOT MOVE PIECE</ACTION PERFORMED>";
        public const String CANNOT_ROTATE_PIECE = "<ACTION PERFORMED>CANNOT ROTATE PIECE</ACTION PERFORMED>";
        public const String DELETE_GAME = "<ACTION PERFORMED>GAME DELETED</ACTION PERFORMED>";
        public const String OUT_OF_PATTERN = "<ACTION PERFORMED>REQUEST OUT OF PATTERN</ACTION PERFORMED>";
        public const String INVALID_REQUEST_TYPE = "<ACTION PERFORMED>INVALID REQUEST TYPE</ACTION PERFORMED>";
        public const String SERVER_STATUS = "<ACTION PERFORMED>DESCRIBING SERVER STATUS</ACTION PERFORMED>";
        public const String GAME_STATUS = "<ACTION PERFORMED>DESCRIBING GAME STATUS</ACTION PERFORMED>";
        public const String GAME_BOARD = "<ACTION PERFORMED>SHOWING GAME BOARD</ACTION PERFORMED>";
        public const String GAME_LIST = "<ACTION PERFORMED>LISTING GAMES</ACTION PERFORMED>";
        public const String UNKNOWN_ERROR = "<ACTION PERFORMED>UNKNOWN ERROR</ACTION PERFORMED>";
        public const String GAME_DOES_NOT_EXIST = "<ACTION PERFORMED>GAME DOES NOT EXISTS</ACTION PERFORMED>";

        /*
         * GAME GENERAL STATUS VALUES
         */
        public const String GGS_GAME_DOES_NOT_EXIST = "<GGSTATUS>GAME DOES NOT EXISTS</GGSTATUS>";
        public const String GGS_NOTHING_TO_BE_DONE = "<GGSTATUS>NOTHING TO BE DONE</GGSTATUS>";
        public const String GGS_WAITING_FOR_PLAYERS = "<GGSTATUS>WAITING FOR PLAYERS</GGSTATUS>";
        public const String GGS_GAME_ENDED = "<GGSTATUS>GAME ENDED</GGSTATUS>";
        public const String GGS_YOUR_TURN = "<GGSTATUS>YOUR TURN</GGSTATUS>";
        public const String GGS_ADVERSARY_TURN = "<GGSTATUS>ADVERSARY'S TURN</GGSTATUS>";
        public const String DRAWN_GAME = "<DRAWNGAME>DRAWN GAME</DRAWNGAME>";
        public const String PLAYER1 = "<PLAYERNUMBER>1</PLAYERNUMBER>";
        public const String PLAYER2 = "<PLAYERNUMBER>2</PLAYERNUMBER>";

        /*
         * Construtor padrão da classe. Recebe os dados para a preparação
         *   da resposta (a mensagem XML de resposta é gerada pelo método
         *   message2string().
         * Parâmetros:
         *   status: sim ou não significando se a mensagem foi ou não atendida
         *   general: respota geral (alto nível) à requisição
         *   specific: resposta específica (detalhada) à requisição
         *   gameID: identificador do jogo a que a mensagem se refere (há 
         *     mensagens que não são específicas de nenhum jogo: id=-1
         */
        public AnswerMessage(bool status, String general, String specific, int gameID)
        {
            OK = status;
            GENERAL_MESSAGE = general;
            SPECIFIC_MESSAGE = specific;
            GID = gameID;
        }

        /* Método que junta os quatro componentes da resposta do servidor em
         *   uma string XML: (i) OK/NOK; (ii) resposta geral; (iii) resposta
         *   específica; e (iv) identificador do jogo.
         */
        public String message2string()
        {
            String answer_string = "<SERVER ANSWER>";
            if (OK)
            {
                answer_string += "<STATUS>OK</STATUS>";
            }
            else
            {
                answer_string += "<STATUS>NOK</STATUS>";
            }
            answer_string += "<GENERAL ANSWER>" + GENERAL_MESSAGE + "</GENERAL ANSWER>";
            answer_string += "<SPECIFIC ANSWER>" + SPECIFIC_MESSAGE + "</SPECIFIC ANSWER>";
            answer_string += "<GAME STATUS>" + GAME_GENERAL_STATUS + "</GAME STATUS>";
            answer_string += "</SERVER ANSWER>";
            return answer_string;
        }
    }

}