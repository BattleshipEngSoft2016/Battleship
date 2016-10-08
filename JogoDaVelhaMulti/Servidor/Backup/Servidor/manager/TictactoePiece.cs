using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager
{

    /**
     * Esta classe implementa as pe�as do jogo da velha (+ ou -).
     * Ela estende a classe de pe�as gen�ricas {@link Piece}.
     * <p>
     * Pe�as de jogo da velha s�o usadas em {@link TictactoeGame} e 
     * {@link Tictactoe3DGame} 
     *  
     * @author Luciano Antonio Digiampietri
     * @version 0.5 beta - 03/15/2006
     *
     * @see Tictactoe3DGame
     * @see TictactoeGame
     * @see Piece
     * 
     */

    public class TictactoePiece : Piece
    {
        public override String getClass2()
        {
            return "TictactoePiece";
        }
        new public static String getClass()
        {
            return "TictactoePiece";
        }
        public TictactoePiece(String id, int player) : base(id, player)
        {
        }

        public override String getString()
        {
            return this.ID;
        }
    }
}