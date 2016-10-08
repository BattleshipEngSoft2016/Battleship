using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Battleship.Entidade;

namespace Battleship
{
    class GradeControle : Control
    {

        private Grade pGrade;

        public Grade Grade
        {
            get
            {
                return this.pGrade;
            }
            set
            {
                this.pGrade = value;
                this.Invalidate();
            }

        }


        public GradeControle() : base()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Grade != null)
            {
                this.SuspendLayout();

                var b1 = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(b1, e.ClipRectangle);

                double tamX = (Convert.ToDouble(e.ClipRectangle.Width - 11) / Convert.ToDouble(this.pGrade.TamanhoX));
                double tamY = (Convert.ToDouble(e.ClipRectangle.Height - 11) / Convert.ToDouble(this.pGrade.TamanhoY));

                //tamX = Math.Round(tamX);
                //tamY = Math.Round(tamY);

                var b2 = new SolidBrush(Color.White);
                var b3 = new SolidBrush(Color.Red);
                var f = new Font("Arial", 9);
                string s = "";

                for (int i = 0; i < Grade.TamanhoY; i++)
                {
                    for (int j = 0; j < Grade.TamanhoX; j++)
                    {
                        e.Graphics.FillRectangle(
                            b2,
                            (((float)tamX + 1) * j) + 1,
                            (((float)tamY + 1) * i) + 1,
                            (float)tamX,
                            (float)tamY
                        );

                        if (this.Grade.Celulas[j,i].Conteudo != null)
                        {
                            s = this.Grade.Celulas[j, i].Conteudo.ShipPai.Nome.Substring(0, 2) + "-" + this.Grade.Celulas[j, i].Conteudo.Sequencia.ToString();                            
                            e.Graphics.DrawString(
                                s,
                                f,
                                (this.Grade.Celulas[j, i].Atingido) ? b3 : b1,
                                new Point(
                                    Convert.ToInt32(((tamX + 1) * j) + (tamX / 2) - 12),
                                    Convert.ToInt32(((tamY + 1) * i) + (tamY / 2) - 12)
                                    )
                                );
                        }
                        else
                        {
                            if (this.Grade.Celulas[j, i].Atingido)
                            {
                                e.Graphics.DrawString(
                                "X",
                                f,
                                b3,
                                new Point(
                                    Convert.ToInt32(((tamX + 1) * j) + (tamX / 2) - 6),
                                    Convert.ToInt32(((tamY + 1) * i) + (tamY / 2) - 6)
                                    )
                                );
                            }
                        }
                    }
                }

                this.ResumeLayout();
            }            

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.Invalidate();
        }

    }
}
