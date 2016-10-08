using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Battleship.Entidade;

namespace Battleship
{
    public partial class Form1 : Form
    {

        private Jogador jogador1;
        private Jogador jogador2;
        private int a, b;

        public Form1()
        {
            InitializeComponent();
            a = 0;
            b = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            jogador1 = new Jogador("Fulano");

            var grade = new Grade(10, 10);
            jogador1.Grade = grade;

            var Ship1 = new Ship(2, "Destroyer");
            var Ship2 = new Ship(3, "Submarino");
            var Ship3 = new Ship(3, "Couraçado");
            var Ship4 = new Ship(4, "Cruzador");
            var Ship5 = new Ship(5, "Porta-aviões");

            jogador1.AdicionarShip(Ship1);
            jogador1.AdicionarShip(Ship2);
            jogador1.AdicionarShip(Ship3);
            jogador1.AdicionarShip(Ship4);
            jogador1.AdicionarShip(Ship5);
            
            Ship1.Posicionar(5, 3, Orientacao.Horizontal);
            Ship2.Posicionar(1, 2, Orientacao.Horizontal);
            Ship3.Posicionar(4, 5, Orientacao.Vertical);
            Ship4.Posicionar(8, 4, Orientacao.Vertical);
            Ship5.Posicionar(0, 2, Orientacao.Vertical);

            this.gradeControle1.Grade = grade;

            AtualizaListaDebug();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.jogador1.Grade.Celulas[a, b].Acertar())
                {
                    InformaUsuario("Acerto.");
                    if (this.jogador1.Grade.Celulas[a, b].Conteudo.ShipPai.Destruido())
                        InformaUsuario(String.Format("Você afundou o {0}.", this.jogador1.Grade.Celulas[a, b].Conteudo.ShipPai.Nome));

                }
                else
                    InformaUsuario("Água.");

                a++;

                if (a == this.jogador1.Grade.TamanhoX)
                {
                    a = 0;
                    b++;
                }

                if (b == this.jogador1.Grade.TamanhoY)
                {
                    b = 0;
                }

                this.gradeControle1.Invalidate();
            }
            catch (Exception ex)
            {
            }
        }

        private void InformaUsuario(string s)
        {
            label1.Text = s;
            AtualizaListaDebug();
        }

        private void AtualizaListaDebug()
        {
            listBox1.Items.Clear();

            listBox1.Items.Add(String.Format("{1} \tDerrotado|{0}", this.jogador1.Nome, this.jogador1.Derrotado()));
            listBox1.Items.Add(String.Format("{1} \tNavios Atingidos|{0}", this.jogador1.Nome, this.jogador1.QtdNaviosAtingidos()));
            listBox1.Items.Add(String.Format("{1} \tNavios Destruidos|{0}", this.jogador1.Nome, this.jogador1.QtdNaviosDestruidos()));
            listBox1.Items.Add(String.Format("{1} \tNavios Não Atingidos|{0}", this.jogador1.Nome, this.jogador1.QtdNaviosNaoAtingidos()));
            listBox1.Items.Add(String.Format("{1} \tNavios Não Destruidos|{0}", this.jogador1.Nome, this.jogador1.QtdNaviosNaoDestruidos()));
            listBox1.Items.Add(String.Format("{1} \tSegmentos Atingidos|{0}", this.jogador1.Nome, this.jogador1.QtdSegmentosAtingidos()));
            listBox1.Items.Add(String.Format("{1} \tSegmentos Não Atingidos|{0}", this.jogador1.Nome, this.jogador1.QtdSegmentosNaoAtingidos()));

            listBox1.Items.Add(String.Format("{1} \tCélulas Atingidas|{0}", String.Format("Grade{0}",this.jogador1.Nome), this.jogador1.Grade.QtdCelulasAtingidas()));
            listBox1.Items.Add(String.Format("{1} \tCélulas Não Atingidas|{0}", String.Format("Grade{0}", this.jogador1.Nome), this.jogador1.Grade.QtdCelulasNaoAtingidas()));
        }
    }
}
