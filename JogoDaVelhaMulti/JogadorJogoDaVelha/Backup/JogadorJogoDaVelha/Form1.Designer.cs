namespace JogadorJogoDaVelha
{
    partial class Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.btnConsultarJogos = new System.Windows.Forms.Button();
            this.btnNovoJogo = new System.Windows.Forms.Button();
            this.txtJogador = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvJogos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCriarEEntrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultarJogos
            // 
            this.btnConsultarJogos.Location = new System.Drawing.Point(31, 212);
            this.btnConsultarJogos.Name = "btnConsultarJogos";
            this.btnConsultarJogos.Size = new System.Drawing.Size(100, 64);
            this.btnConsultarJogos.TabIndex = 0;
            this.btnConsultarJogos.Text = "Consultar Jogos Criados";
            this.btnConsultarJogos.UseVisualStyleBackColor = true;
            this.btnConsultarJogos.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnNovoJogo
            // 
            this.btnNovoJogo.Location = new System.Drawing.Point(157, 212);
            this.btnNovoJogo.Name = "btnNovoJogo";
            this.btnNovoJogo.Size = new System.Drawing.Size(228, 21);
            this.btnNovoJogo.TabIndex = 0;
            this.btnNovoJogo.Text = "Criar Novo Jogo";
            this.btnNovoJogo.UseVisualStyleBackColor = true;
            this.btnNovoJogo.Click += new System.EventHandler(this.btnNovoJogo_Click);
            // 
            // txtJogador
            // 
            this.txtJogador.Location = new System.Drawing.Point(342, 19);
            this.txtJogador.Name = "txtJogador";
            this.txtJogador.Size = new System.Drawing.Size(43, 20);
            this.txtJogador.TabIndex = 2;
            this.txtJogador.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(284, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Jogador:";
            // 
            // dgvJogos
            // 
            this.dgvJogos.AllowUserToAddRows = false;
            this.dgvJogos.AllowUserToDeleteRows = false;
            this.dgvJogos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvJogos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJogos.Location = new System.Drawing.Point(31, 45);
            this.dgvJogos.MultiSelect = false;
            this.dgvJogos.Name = "dgvJogos";
            this.dgvJogos.ReadOnly = true;
            this.dgvJogos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJogos.Size = new System.Drawing.Size(354, 161);
            this.dgvJogos.TabIndex = 8;
            this.dgvJogos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJogos_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "IP:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(50, 19);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(82, 20);
            this.txtIP.TabIndex = 10;
            this.txtIP.Text = "127.0.0.1";
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Porta:";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(181, 19);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(42, 20);
            this.txtPorta.TabIndex = 12;
            this.txtPorta.Text = "50027";
            this.txtPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Servidor:";
            // 
            // btnCriarEEntrar
            // 
            this.btnCriarEEntrar.Location = new System.Drawing.Point(157, 239);
            this.btnCriarEEntrar.Name = "btnCriarEEntrar";
            this.btnCriarEEntrar.Size = new System.Drawing.Size(228, 37);
            this.btnCriarEEntrar.TabIndex = 15;
            this.btnCriarEEntrar.Text = "Criar e Entrar num Novo Jogo";
            this.btnCriarEEntrar.UseVisualStyleBackColor = true;
            this.btnCriarEEntrar.Click += new System.EventHandler(this.btnCriarEEntrar_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 280);
            this.Controls.Add(this.btnCriarEEntrar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPorta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.dgvJogos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJogador);
            this.Controls.Add(this.btnNovoJogo);
            this.Controls.Add(this.btnConsultarJogos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inicio";
            this.Text = "Jogo da Velha";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Inicio_FormClosed);
            this.Load += new System.EventHandler(this.Inicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarJogos;
        private System.Windows.Forms.Button btnNovoJogo;
        private System.Windows.Forms.TextBox txtJogador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvJogos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCriarEEntrar;
    }
}

