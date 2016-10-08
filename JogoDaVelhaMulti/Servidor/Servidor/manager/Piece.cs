using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager
{

    /**
     * Esta é a super-classe de todas as peças de um Jogo de Tabuleiro {@link Game}.
     *  
     * @author Luciano Antonio Digiampietri
     * @version 1.0 beta - 03/11/2008
     * 
     * @see Board
     * @see Game
     */
    public class Piece
    {
        public virtual String getClass2()
        {
            return "Piece";
        }
        public static String getClass()
        {
            return "Piece";
        }
        public String ID = "";
        public int PLAYER = -1;

        public Piece(String id, int player)
        {
            this.ID = id;
            this.PLAYER = player;
        }

        public Piece(String id)
        {
            this.ID = id;
        }

        public virtual String getString()
        {
            return ID;
        }

        public String getId()
        {
            return ID;
        }

        public virtual int getForce()
        {
            return -1;
        }

        public virtual String discoverPiece(int force)
        {
            return "tmp";
        }

        public void setId(String id)
        {
            this.ID = id;
        }

        public int getPlayer()
        {
            return PLAYER;
        }

    }
}