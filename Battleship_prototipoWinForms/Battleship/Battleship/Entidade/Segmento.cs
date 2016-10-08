using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Entidade
{
    class Segmento
    {
        private int pSequencia;
        private Ship pShip;
        private Celula pCelula;

        public int Sequencia
        {
            get
            {
                return pSequencia;
            }
        }

        public Ship ShipPai
        {
            get
            {
                return pShip;
            }
        }

        public Celula CelulaLocal
        {
            get
            {
                return pCelula;
            }
        }

        public Segmento(Ship shipPai, int sequencia)
        {
            if (shipPai == null)
            {
                throw new Exception("Navio não pode ser nulo.");
            }

            this.pShip = shipPai;
            this.pSequencia = sequencia;
        }

        public void Relocar(Celula c)
        {
            if (Equals(this,c.Conteudo))
            {
                this.pCelula = c;  
            }
            else
            {
                throw new Exception("Segmento não está na célula informada.");
            }
                      
        }
    }
}
