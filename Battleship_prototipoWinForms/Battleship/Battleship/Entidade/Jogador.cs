using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Entidade
{
    class Jogador
    {
        private List<Ship> lstShips;
        private Grade pGrade;

        public string Nome { get; set; }

        public List<Ship> Ships
        {
            get
            {
                return lstShips;
            }
        }

        public Grade Grade
        {
            get
            {
                return pGrade;
            }
            set
            {
                this.pGrade = value;
            }
        }

        public Jogador(string nome)
        {
            this.Nome = nome;
            lstShips = new List<Ship>();
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public void AdicionarShip(int tamanho, string nome)
        {
            var s = new Ship(tamanho, nome);
            lstShips.Add(s);
            s.Jogador = this;
        }
        public void AdicionarShip(Ship ship)
        {
            lstShips.Add(ship);
            ship.Jogador = this;
        }

        public int QtdNaviosDestruidos()
        {
            return lstShips.Count(s => s.Destruido());
        }

        public int QtdNaviosAtingidos()
        {
            return lstShips.Count(s => s.Atingido());
        }

        public int QtdNaviosNaoDestruidos()
        {
            return lstShips.Count(s => !s.Destruido());
        }

        public int QtdNaviosNaoAtingidos()
        {
            return lstShips.Count(s => !s.Atingido());
        }

        public int QtdSegmentosAtingidos()
        {
            return lstShips.Sum(s => s.QtdSegmentosAtingidos());
        }

        public int QtdSegmentosNaoAtingidos()
        {
            return lstShips.Sum(s => s.QtdSegmentosNaoAtingidos());
        }

        public bool Derrotado()
        {
            return (QtdNaviosDestruidos() == lstShips.Count);
        }

    }
}
