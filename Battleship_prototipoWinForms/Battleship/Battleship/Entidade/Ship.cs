using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Entidade
{

    enum Orientacao
    {
        Horizontal = 1,
        Vertical = 2
    }

    class Ship
    {

        private int pTamanho;
        private string pNome;
        private Segmento[] pSegmentos;
        private Jogador pJogador;

        public int Tamanho { 
            get
            {
                return pTamanho;
            }
        }

        public string Nome
        {
            get
            {
                return pNome;
            }
        }

        public Segmento[] Segmentos
        {
            get
            {
                return pSegmentos;
            }
        }

        public Jogador Jogador
        {
            get
            {
                return pJogador;
            }
            set
            {
                this.pJogador = value;
            }
        }

        public Ship(int tamanho, string nome)
        {
            this.pTamanho = tamanho;
            this.pNome = nome;

            this.pSegmentos = new Segmento[this.Tamanho];

            for (int i = 0; i < this.Tamanho; i++)
            {
                this.pSegmentos[i] = new Segmento(this,i);
            }
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", this.Nome, this.Tamanho, this.Destruido());
        }

        public bool Atingido()
        {
            if (Segmentos != null)
            {
                bool d = false;
                foreach (Segmento s in this.Segmentos)
                {
                    if (s.CelulaLocal.Atingido)
                    {
                        d = true;
                        break;
                    }
                }

                return d;
            }
            else
            {
                return false;
            }
        }

        public bool Destruido()
        {
            if (Segmentos != null)
            {
                bool d = true;
                foreach (Segmento s in this.Segmentos)
                {
                    if (!s.CelulaLocal.Atingido)
                    {
                        d = false;
                        break;
                    }
                }

                return d;
            }
            else
            {
                return false;
            }
        }

        public int QtdSegmentosAtingidos()
        {
            return pSegmentos.Count(s => s.CelulaLocal.Atingido);
        }

        public int QtdSegmentosNaoAtingidos()
        {
            return pSegmentos.Count(s => !s.CelulaLocal.Atingido);
        }

        public bool Posicionar(int X, int Y, Orientacao ori)
        {
            bool bolOK = true;

            int iMax, jMax;
            int segAtual = 0;

            switch (ori)
            {
                case Orientacao.Horizontal:
                    iMax = this.Tamanho - 1;
                    jMax = 0;
                    break;
                case Orientacao.Vertical:
                    iMax = 0;
                    jMax = this.Tamanho - 1;
                    break;
                default:
                    iMax = this.Tamanho - 1;
                    jMax = 0;
                    break;
            }

            if (this.pJogador.Grade == null)
            {
                bolOK = false;
            }
            else
            {
                if (this.pJogador.Grade.TamanhoX - (iMax + X + 1) < 0)
                {
                    bolOK = false;
                }

                if (this.pJogador.Grade.TamanhoY - (jMax + Y + 1) < 0)
                {
                    bolOK = false;
                }
            }

            if (bolOK)
            {
                for (int j = 0; j <= jMax; j++)
                {
                    for (int i = 0; i <= iMax; i++)
                    {
                        if (this.pJogador.Grade.Celulas[X + i, Y + j].Conteudo != null)
	                    {
                            bolOK = false;
                            break;
	                    }
                        
                        segAtual++;
                        
                    }
                }   
            }

            segAtual = 0;

            if (bolOK)
            {               

                for (int j = 0; j <= jMax; j++)
                {
                    for (int i = 0; i <= iMax; i++)
                    {
                        this.pJogador.Grade.Celulas[X + i, Y + j].Alocar(this.Segmentos[segAtual]);
                        segAtual++;
                    }
                }   
            }

            return bolOK;
        }
    }
}
