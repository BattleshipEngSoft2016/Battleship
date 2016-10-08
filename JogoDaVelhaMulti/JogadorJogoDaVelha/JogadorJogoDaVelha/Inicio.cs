using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

namespace JogadorJogoDaVelha
{

    /// <summary>
    /// Esta classe exibe os jogos da velha de um servidor de jogos, além de permitir a
    ///   requisição de criação de novos jogos e a entrada do jogador num jogo.
    /// </summary>
    public partial class Inicio : Form
    {
        private const int intervaloMinimoDeEsperaDoServidor = 100; // em milissegundos
        private const int numeroDeVerificacoesDaRespostaDoServidor = 50; // quantidade de vezes que se verifica se o servidor respondeu antes de considerar erro

        /// <summary>
        /// Construtor padrão da classe, apenas inicializa os componentes do
        ///   formulário.
        /// </summary>
        public Inicio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Envia a mensagem XML passada como parâmetro para o servidor atual e retorna a resposta do servidor.
        /// Este método cria uma nova thread para o envio da mensagem e aguarda a resposta em uma variável compartilhada.
        /// A mensagem é enviada utilizando-se o protocolo UDP.
        /// </summary>
        /// <param name="mensagem">mensagem XML de requisição que será enviado ao servidor.</param>
        public String enviandoMensagem(String mensagem)
        {
            String host = txtIP.Text;
            int port = 50027;
            try
            {
                port = int.Parse(txtPorta.Text);
            }
            catch
            {
                MessageBox.Show("Número de porta inválida: " + txtPorta.Text + ". Utilizando porta padrão 50027.");
                txtPorta.Text = "50027";
                port = 50027;
            }
            string[] resposta = new string[1];

            EnviarMensagem enviar = new EnviarMensagem(mensagem, host, port, resposta);
            Thread enviarThread = new Thread(enviar.executarEnvio);
            enviarThread.Start();
            int cont = 0;
            while (resposta[0] == null && cont < numeroDeVerificacoesDaRespostaDoServidor)
            {
                cont++;
                Thread.Sleep(intervaloMinimoDeEsperaDoServidor);
            }

            if (cont == numeroDeVerificacoesDaRespostaDoServidor)
            {
                MessageBox.Show("Tempo de resposta do servidor esgotado. Você tem certeza que está conectado no servidor correto?");
                enviarThread.Abort();
            }
            else
            {
                if (resposta[0] == "") resposta[0] = null;
            }

            return resposta[0];

        }

        /// <summary>
        /// Requisita a lista de jogos da velha abertos no servidor e atualiza
        /// o DataGridView dgvJogos com essa lista.
        /// </summary>
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            btnConsultarJogos.Enabled = false;
            String resposta = enviandoMensagem(MensagensDeRequisicao.listarJogos("tictactoe"));
            DataTable dtJogos = new DataTable();
            dtJogos.Columns.Add(new DataColumn("idDoJogo", Type.GetType("System.String")));
            dtJogos.Columns.Add(new DataColumn("tipo", Type.GetType("System.String")));
            dtJogos.Columns.Add(new DataColumn("status", Type.GetType("System.String")));

            if (resposta != null)
            {
                RespostaDoServidor resp = new RespostaDoServidor(resposta);
                DescricaoDeJogo[] jogos = resp.Jogos;

                if (jogos != null)
                {
                    for (int i = 0; i < jogos.Length; i++)
                    {
                        DataRow novaLinha = dtJogos.NewRow();
                        novaLinha["idDoJogo"] = jogos[i].identificadorDoJogo;
                        String tipo = "Jogo da Velha";
                        novaLinha["tipo"] = tipo;
                        String status = "Jogo em andamento.";
                        if (jogos[i].jogoEncerrado) status = "Encerrado.";
                        if (jogos[i].jogoEncerrado)
                        {
                            status = "Encerrado.";
                        }
                        else
                        {
                            if (jogos[i].esperandoJogadores)
                            {
                                status = "Aguardando jogadores.";
                            }

                        }
                        novaLinha["status"] = status;
                        dtJogos.Rows.Add(novaLinha);
                    }
                }
            }
            dgvJogos.DataSource = dtJogos;
            btnConsultarJogos.Enabled = true;
        }

        /// <summary>
        /// Solicita ao servidor a criação de um novo jogo da velha.
        /// </summary>
        private void btnNovoJogo_Click(object sender, EventArgs e)
        {
            String resposta = enviandoMensagem(MensagensDeRequisicao.novoJogo("tictactoe"));
            btnConsultar_Click(sender, e);
        }

        /// <summary>
        /// Quando o usuário der duplo clique numa linha do DataGridView 
        ///   dgvJogos, entra no jogo selecionado (solicita essa entrada ao
        ///   servidor) e abre um formulário do tipo JogoDaVelha
        ///   para que o usuário possa interagir (ou observar, caso o jogo
        ///   não esteja aguardando mais jogadores) o jogo.
        /// </summary>
        private void dgvJogos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = e.RowIndex;
            if (id >= 0)
            {
                int idJogador = -1;
                int idJogo = -1;
                idJogador = int.Parse(txtJogador.Text);
                idJogo = int.Parse(dgvJogos.Rows[id].Cells[0].Value.ToString());
                String tipo = dgvJogos.Rows[id].Cells[1].Value.ToString();
                JogoDaVelha f = new JogoDaVelha(idJogo, idJogador, this);
                f.Show();
            }
        }

        /// <summary>
        /// Durante a leitura do formulário atual, os valores de algumas
        ///   variáveis globais são inicializados.
        /// Por exemplo, o DataSource do DataGridView dgvJogos, além disso
        ///   o id do usuário (gerado aleatoriamente)
        /// </summary>     
        private void Inicio_Load(object sender, EventArgs e)
        {
            DataTable dtJogos = new DataTable();
            dtJogos.Columns.Add(new DataColumn("idDoJogo", Type.GetType("System.String")));
            dtJogos.Columns.Add(new DataColumn("tipo", Type.GetType("System.String")));
            dtJogos.Columns.Add(new DataColumn("status", Type.GetType("System.String")));
            dgvJogos.DataSource = dtJogos;

            Random rand = new Random();
            int id = (int)(100 * rand.NextDouble());
            txtJogador.Text = id.ToString();
        }

        /// <summary>
        /// Solicita ao servidor a criação de um novo jogo da velha.
        /// Ao entrar no jogo, um novo formulário do tipo JogoDaVelha
        ///   é criado para que o usuário interaja com este novo jogo.
        /// </summary>        
        private void btnCriarEEntrar_Click(object sender, EventArgs e)
        {
            String resposta = enviandoMensagem(MensagensDeRequisicao.novoJogo("tictactoe"));
            if (resposta == null) return;
            btnConsultar_Click(sender, e);
            RespostaDoServidor resp = new RespostaDoServidor(resposta);
            if (resp.respostaAfirmativa)
            {
                int idJogador = -1;
                int idJogo = -1;
                idJogador = int.Parse(txtJogador.Text);
                idJogo = resp.identificadorDoJogo;
                JogoDaVelha f = new JogoDaVelha(idJogo, idJogador, this);
                f.Show();
            }
        }

        /// <summary>
        /// Com o fechamento deste formulário (que é o formulário principal)
        ///   encerra todas as threads e outros formulários que por ventura
        ///   estejam abertos e retorna 0 (OK) para o sistema.
        /// </summary>
        private void Inicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}