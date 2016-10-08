using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace JogadorJogoDaVelha
{
    /// <summary>
    /// Esta classe controla um formulário para se jogar ou assistir a uma
    ///   partida de jogo da velha. Também é possível acionar um bot para 
    ///   jogar automaticamente no seu lugar.
    /// </summary>
    public partial class JogoDaVelha : Form
    {
        private enum tiposDeJogadores { Jogador1, Jogador2, Observador };
        private tiposDeJogadores jogadorAtual = tiposDeJogadores.Observador;
        private Inicio formPai;
        private int jogador;
        private int jogo;
        private Label[] tabLabel = new Label[9];
        private bool botAtivado = false;
        private bool botJogou = false;
        private int[] tabuleiro = new int[9];
        public JogoDaVelha(int jogoId, int jogadorId, Inicio pai)
        {
            jogador = jogadorId;
            jogo = jogoId;
            formPai = pai;
            InitializeComponent();

            tabLabel[0] = lbl0;
            tabLabel[1] = lbl1;
            tabLabel[2] = lbl2;
            tabLabel[3] = lbl3;
            tabLabel[4] = lbl4;
            tabLabel[5] = lbl5;
            tabLabel[6] = lbl6;
            tabLabel[7] = lbl7;
            tabLabel[8] = lbl8;


            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.entrarEmUmJogo(jogoId, jogadorId));
            RespostaDoServidor resp1 = new RespostaDoServidor(resposta);

            if (resp1.aguardandoJogadores)
            {
                jogadorAtual = tiposDeJogadores.Jogador1;
                lblJogadorAtual.Text = "Jogador1: X";
                lblStatus.Text = "aguardando jogador2 O";
                timer1.Enabled = true;
            }
            else
            {
                String resposta2 = formPai.enviandoMensagem(MensagensDeRequisicao.estouNesteJogo(jogoId, jogadorId));
                if (resposta2 == null)
                {
                    this.Close();
                    return;
                }
                RespostaDoServidor resp2 = new RespostaDoServidor(resposta2);

                if (resp2.respostaAfirmativa)
                {
                    if (resp2.indiceDoJogador == 1)
                    {
                        jogadorAtual = tiposDeJogadores.Jogador1;
                        lblJogadorAtual.Text = "Jogador1: X";
                    }
                    else
                    {
                        jogadorAtual = tiposDeJogadores.Jogador2;
                        lblJogadorAtual.Text = "Jogador2: O";
                    }
                }
                else
                {
                    jogadorAtual = tiposDeJogadores.Observador;
                    lblJogadorAtual.Text = "Observando Jogo.";
                    btnBot.Enabled = false;
                    timer1.Enabled = true;
                    timer1.Interval = 5000;
                }
            }

            if (resp1.jogoEncerrado)
            {
                verificarStatus(resp1);
            }
            lerTabuleiro();

        }

        /// <summary>
        /// Envia a mensagem XML passada como parâmetro para o servidor atual e retorna a resposta do servidor.
        /// Este método cria uma nova thread para o envio da mensagem e aguarda a resposta em uma variável compartilhada.
        /// A mensagem é enviada utilizando-se o protocolo UDP.
        /// </summary>
        /// <param name="mensagem">mensagem XML de requisição que será enviado ao servidor.</param>
        private void lerTabuleiro()
        {
            String mensagem = formPai.enviandoMensagem("<REQUEST><RT>game board</RT><GID>" + jogo + "</GID></REQUEST>");
            Regex exp = new Regex(@"<GAME CURRENT BOARD>(.+)</GAME CURRENT BOARD>", RegexOptions.IgnoreCase);
            Regex exp2 = new Regex(@"<(.)>", RegexOptions.IgnoreCase);
            RespostaDoServidor resp1 = new RespostaDoServidor(mensagem);
            //int[] tabuleiro = new int[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int id = i *3 + j;
                    int valor = 0;

                    if (resp1.tabuleiro[i,j] == "-")
                    {
                        valor = -1;
                        tabLabel[id].Text = "O";
                    }
                    else if (resp1.tabuleiro[i, j] == "+")
                    {
                        valor = +1;
                        tabLabel[id].Text = "X";
                    }
                    tabuleiro[id] = valor;

                }
            }
                        
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.estadoDoJogo(jogo, jogador));
            RespostaDoServidor resp = new RespostaDoServidor(resposta);

            if (!resp.jogoEncerrado)
            {
                if (resp.turnoDoJogadorAtual)
                {
                    lblStatus.Text = "seu turno";
                    if (botAtivado && !botJogou) jogarAutomaticamente();

                    timer1.Enabled = true;
                }
                else if(resp.turnoDoAdversario)
                {
                    if (jogadorAtual != tiposDeJogadores.Observador) { lblStatus.Text = "turno do adversário"; }
                    timer1.Enabled = true;
                }
                botJogou = false;
            }
        }

        /// <summary>
        /// Dado um tabuleiro e o valor do jogador no tabuleiro (-1 ou 1)
        ///   retorna a posição da jogada que lhe dará a vitória, caso exista
        ///   e -1 caso contrário.
        /// </summary>
        /// <param name="TABULEIRO">arranjo de 9 inteiros contendo os valores
        ///    -1, 0 ou 1 correspondendo a um tabuleiro de jogo da velha</param>
        /// <param name="jog"> identificador se as peças do jogador atual 
        ///    valem -1 ou 1</param>
        int retornaJogadaVitoriaDerrota(int[] TABULEIRO, int jog)
        {
            /* uma jogada de vitoria ou derrota é caracterizada por duas marcas 
             * seguidas com o mesmo valor e o terceiro valor livre                 */
            int valorAtual = jog;
            if (jogadorAtual == tiposDeJogadores.Jogador2) valorAtual = -1 * valorAtual;
            if ((TABULEIRO[0] + TABULEIRO[1] + TABULEIRO[2]) == 2 * valorAtual)
            {
                if (TABULEIRO[0] == 0) return 0;
                else if (TABULEIRO[1] == 0) return 1;
                else return 2;
            }
            if ((TABULEIRO[3] + TABULEIRO[4] + TABULEIRO[5]) == 2 * valorAtual)
            {
                if (TABULEIRO[3] == 0) return 3;
                else if (TABULEIRO[4] == 0) return 4;
                else return 5;
            }
            if ((TABULEIRO[6] + TABULEIRO[7] + TABULEIRO[8]) == 2 * valorAtual)
            {
                if (TABULEIRO[6] == 0) return 6;
                else if (TABULEIRO[7] == 0) return 7;
                else return 8;
            }
            if ((TABULEIRO[0] + TABULEIRO[3] + TABULEIRO[6]) == 2 * valorAtual)
            {
                if (TABULEIRO[0] == 0) return 0;
                else if (TABULEIRO[3] == 0) return 3;
                else return 6;
            }
            if ((TABULEIRO[1] + TABULEIRO[4] + TABULEIRO[7]) == 2 * valorAtual)
            {
                if (TABULEIRO[1] == 0) return 1;
                else if (TABULEIRO[4] == 0) return 4;
                else return 7;
            }
            if ((TABULEIRO[2] + TABULEIRO[5] + TABULEIRO[8]) == 2 * valorAtual)
            {
                if (TABULEIRO[2] == 0) return 2;
                else if (TABULEIRO[5] == 0) return 5;
                else return 8;
            }
            if ((TABULEIRO[2] + TABULEIRO[4] + TABULEIRO[6]) == 2 * valorAtual)
            {
                if (TABULEIRO[2] == 0) return 2;
                else if (TABULEIRO[4] == 0) return 4;
                else return 6;
            }
            if ((TABULEIRO[0] + TABULEIRO[4] + TABULEIRO[8]) == 2 * valorAtual)
            {
                if (TABULEIRO[0] == 0) return 0;
                else if (TABULEIRO[4] == 0) return 4;
                else return 8;
            }
            return -1;
        } /* retornaJogadaVitoriaDerrota */


        /// <summary>
        /// Dado um tabuleiro, o valor do jogador no tabuleiro e o número de 
        ///   jogadas já realizadas, retorna a posição da próxima jogada, ou
        ///   -1 no caso de não existir uma próxima jogada.
        /// </summary>
        /// <param name="TABULEIRO">arranjo de 9 inteiros contendo os valores
        ///    -1, 0 ou 1 correspondendo a um tabuleiro de jogo da velha</param>
        /// <param name="jod"> identificador se as peças do jogador atual 
        ///    valem -1 ou 1</param>
        /// <param name="jogadasEfetuadas">número de jogadas já efetuadas no jogo atual</param>
        public int retornaOutraJogada(int[] TABULEIRO, int jog, int jogadasEfetuadas)
        {
            int res = -1;
            if (jogadasEfetuadas == 3)
            {
                if ((TABULEIRO[0] * TABULEIRO[8] == 1 || TABULEIRO[2] * TABULEIRO[6] == 1) && TABULEIRO[4] != 0)
                {
                    return 1;
                }

                if ((TABULEIRO[1] * TABULEIRO[8] == 1 || TABULEIRO[0] * TABULEIRO[5] == 1 || TABULEIRO[1] * TABULEIRO[5] == 1) && TABULEIRO[4] != 0)
                {
                    return 2;
                }
            }

            // tente jogar no meio ou nas diagonais (maior chance de vitoria ou impedir derrota
            if (jogadasEfetuadas <= 2)
            {
                if (TABULEIRO[4] == 0) res = 4;
                else if (TABULEIRO[0] == 0) res = 0;
                else res = 8;
                return res;
            }
            else
            {
                res = -1;
                if (TABULEIRO[4] == 0) res = 4;
                else if (TABULEIRO[6] == 0) res = 6;
                else if (TABULEIRO[8] == 0) res = 8;
                else if (TABULEIRO[0] == 0) res = 0;
                else if (TABULEIRO[2] == 0) res = 2;
                if (res != -1)
                {
                    return res;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (TABULEIRO[i] == 0) return i;
            }
            return -1;
        }


        /// <summary>
        /// Executa uma jogada automaticaticamente, através de um sistema especialista.
        /// </summary>
        public void jogarAutomaticamente()
        {
            if (lblStatus.Text == "seu turno")
            {
                int jogAtual = -1;
                if (jogadorAtual == tiposDeJogadores.Jogador2) jogAtual = 1;
                int[] TABULEIRO = new int[9];
                int jogadasDisponiveis = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (tabLabel[i].Text == "X")
                    {
                        TABULEIRO[i] = -1;
                    }
                    else if (tabLabel[i].Text == "O")
                    {
                        TABULEIRO[i] = 1;
                    }
                    else
                    {
                        jogadasDisponiveis++;
                    }
                }


                int jogada = retornaJogadaVitoriaDerrota(TABULEIRO, jogAtual);
                if (jogada == -1) { jogada = retornaJogadaVitoriaDerrota(TABULEIRO, jogAtual * -1); }
                if (jogada == -1) { jogada = retornaOutraJogada(TABULEIRO, jogAtual, 9 - jogadasDisponiveis); }
                if (jogada > -1)
                {
                    int x = jogada / 3;
                    int y = jogada % 3;
                    String resposta = formPai.enviandoMensagem("<REQUEST><RT>add piece</RT><GID>" + jogo + "</GID><PLAYER>" + jogador + "</PLAYER><X>" + x + "</X><Y>" + y + "</Y></REQUEST>");
                    //MessageBox.Show(jogada + " " + resposta);
                    botJogou = true;
                    lerTabuleiro();
                }
            }
        }


        /// <summary>
        /// Solicita ao servidor a adição de uma peça na casa clicada.
        /// </summary>
        private void lbl0_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo,jogador,0,0));
            lerTabuleiro();
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 0, 1));
            lerTabuleiro();
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 0, 2));
            lerTabuleiro();
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 1, 0));
            lerTabuleiro();
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 1, 1));
            lerTabuleiro();
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 1, 2));
            lerTabuleiro();
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 2, 0));
            lerTabuleiro();
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 2, 1));
            lerTabuleiro();
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.adicionarPeca(jogo, jogador, 2, 2));
            lerTabuleiro();
        }


        /// <summary>
        /// Método acionado pelo Tick do Timer timer1.
        /// Envia mensagem ao servidor para verificar se há mudanças no jogo e
        ///   atualiza o formulário com os dados da resposta do servidor.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            String resposta = formPai.enviandoMensagem(MensagensDeRequisicao.estadoDoJogo(jogo, jogador));
            RespostaDoServidor resp1 = new RespostaDoServidor(resposta);
            if (resp1.jogoEncerrado)
            {
                verificarStatus(resp1);
            }
            else if (resp1.turnoDoJogadorAtual)
            {
                lblStatus.Text = "seu turno";
                if (botAtivado) jogarAutomaticamente();
                else timer1.Enabled = false;
                lerTabuleiro();
                verificarStatus(resp1);
            }
        }

        /// <summary>
        /// Verifica o ganhador do jogo, caso este esteja encerrado.
        /// </summary>
        /// <param name="resp1">resposta do servidor contendo o estado do jogo.</param>
        private void verificarStatus(RespostaDoServidor resp1)
        {
            if (resp1.jogoEncerrado)
            {
                if (resp1.vencedor == jogador)
                {
                    lblStatus.Text = "Você Venceu!";
                }
                else if (resp1.vencedor == -1)
                {
                    lblStatus.Text = "Empate!";
                }
                else
                {
                    if (jogadorAtual == tiposDeJogadores.Observador)
                    {
                        lblStatus.Text = "Fim de Jogo!";
                    }
                    else
                    {
                        lblStatus.Text = "Você perdeu.";
                    }
                }
                timer1.Enabled = false;
                btnBot.Enabled = false;
                lerTabuleiro();
            }
        }

        /// <summary>
        /// Ativa ou desativa o jogador automático (bot).
        /// </summary>
        private void btnBot_Click(object sender, EventArgs e)
        {

            if (!botAtivado)
            {
                botAtivado = true;
                jogarAutomaticamente();
                btnBot.Text = "Desativar Bot";
            }
            else
            {
                btnBot.Text = "Ativar Bot";
                botAtivado = false;
            }

        }
    }
}