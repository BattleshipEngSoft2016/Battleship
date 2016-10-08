using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Entidade
{
    class Grade
    {
        private int pTamanhoX;
        private int pTamanhoY;
        private int pQuantidadeCelulas;
        private Celula[,] pCelulas;

        public Celula[,] Celulas
        {
            get
            {
                return pCelulas;
            }
        }

        public int TamanhoX
        {
            get
            {
                return pTamanhoX;
            }
        }

        public int TamanhoY
        {
            get
            {
                return pTamanhoY;
            }
        }

        public int QuantidadeCelulas
        {
            get
            {
                return pQuantidadeCelulas;
            }
        }

        public Grade(int tamX, int tamY)
        {
            this.pTamanhoX = tamX;
            this.pTamanhoY = tamY;
            this.pQuantidadeCelulas = tamX * tamY;

            if (tamX > 0 && tamY > 0)
            {
                this.pCelulas = new Celula[tamX, tamY];

                for (int i = 0; i < tamX; i++)
                {
                    for (int j = 0; j < tamY; j++)
                    {
                        this.pCelulas[i, j] = new Celula(this, i, j);
                    }
                }
            }
            
        }

        public int QtdCelulasAtingidas()
        {
            int c = 0;
            for (int i = 0; i < pTamanhoX; i++)
            {
                for (int j = 0; j < pTamanhoY; j++)
                {
                    if (pCelulas[i, j].Atingido)
                        c++;
                }
            }

            return c;
        }

        public int QtdCelulasNaoAtingidas()
        {
            int c = 0;
            for (int i = 0; i < pTamanhoX; i++)
            {
                for (int j = 0; j < pTamanhoY; j++)
                {
                    if (!pCelulas[i, j].Atingido)
                        c++;
                }
            }

            return c;
        }
    }
}
