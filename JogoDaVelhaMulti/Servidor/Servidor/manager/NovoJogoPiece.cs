using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager
{

    /**
     * Esta classe implementa as peças do jogo da velha (X ou O).
     * Ela estende a classe de peças genéricas {@link Piece}.
     */

    public class NovoJogoPiece : Piece
    {
        public const String PECA_X = "X";
        public const String PECA_O = "O";
        private String type = PECA_X;
        public override String getClass2()
        {
            return "NovoJogoPiece";
        }
        new public static String getClass()
        {
            return "NovoJogoPiece";
        }
        public NovoJogoPiece(String id, int player, String type)
            : base(id, player)
        {
            this.type = type;
	    }
        public override String getString()
        {
            return ID+type+ID;
        }
    }
}