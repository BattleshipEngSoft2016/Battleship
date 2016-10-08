using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Entidade
{
    class Celula
    {
        private bool pAtingido;
        private int pX;
        private int pY;
        private Segmento pConteudo;
        private Grade pGrade;        

        public bool Atingido
        {
            get
            {
                return pAtingido;
            }
        }

        public int X
        {
            get
            {
                return pX;
            }
        }

        public int Y
        {
            get
            {
                return pY;
            }
        }

        public Segmento Conteudo
        {
            get
            {
                return pConteudo;
            }
        }

        public Grade GradePai
        {
            get
            {
                return pGrade;
            }
        }

        public Celula(Grade grade, int x, int y)
        {
            this.pGrade = grade;
            this.pX = x;
            this.pY = y;
        }

        public void Alocar(Segmento s)
        {
            if (Equals(pGrade, s.ShipPai.Jogador.Grade))
            {
                if (s != null)
                {
                    if (pConteudo == null)
                    {
                        this.pConteudo = s;
                        s.Relocar(this);
                    }
                    else
                    {
                        throw new Exception("Célula a receber o segmento já está ocupada.");
                    }
                }
                else
                {
                    throw new Exception("Célula a receber o segmento não existe.");
                }
            }
            else
            {
                throw new Exception("Célula a receber o segmento não pertence à mesma grade que o navio.");
            }
        }

        public bool Acertar()
        {
            this.pAtingido = true;

            if (this.Conteudo != null)            
                return true;            
            else            
                return false;
            
        }
    }
}
