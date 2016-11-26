using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jogo_Main.Models
{
    public class NiveisContext : DbContext
    {
        public NiveisContext()
            : base("DefaultConnection")
        {



        }

        public DbSet<Nivel> Niveis { get; set; }
    }

    [Table("Niveis")]
    public class Nivel
    {

        [Key, Column("ID_CONFIGURACAO")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("DS_NOME_CONFIGURACAO")]
        public string Nome { get; set; }

        [Column("QT_COLUNAS")]
        public byte QtdColunas { get; set; }

        [Column("QT_LINHAS")]
        public byte QtdLinhas { get; set; }

        [Column("QT_NAVIO1")]
        public byte QtdNav01 { get; set; }

        [Column("QT_NAVIO2")]
        public byte QtdNav02 { get; set; }

        [Column("QT_NAVIO3")]
        public byte QtdNav03 { get; set; }

        [Column("QT_NAVIO4")]
        public byte QtdNav04 { get; set; }

        [Column("QT_NAVIO5")]
        public byte QtdNav05 { get; set; }

        [Column("NR_TEMPO_POSICIONAMENTO")]
        public Int16 TempoPosicionamento { get; set; }

        [Column("NR_TEMPO_JOGADA")]
        public byte TempoJogada { get; set; }

        public Nivel()
        {

        }

        public Nivel(NivelModel vm)
        {

            Nome = vm.nome;

            QtdColunas = vm.qtdColunas;

            QtdLinhas = vm.qtdLinhas;

            QtdNav01 = vm.qtdPortaAvioes;
            
            QtdNav02 = vm.qtdDestroiers;

            QtdNav03 = vm.qtdEncouracados;

            QtdNav04 = vm.qtdCruzadores;

            QtdNav05 = vm.qtdSubmarinos;


        }

        public void AtualizarDominio(NivelModel vm)
        {
            Nome = vm.nome;

            QtdColunas = vm.qtdColunas;

            QtdLinhas = vm.qtdLinhas;

            QtdNav01 = vm.qtdPortaAvioes;

            QtdNav02 = vm.qtdDestroiers;

            QtdNav03 = vm.qtdEncouracados;

            QtdNav04 = vm.qtdCruzadores;

            QtdNav05 = vm.qtdSubmarinos;

        }
    }



    public class NivelModel
    {
        public virtual int id { get; set; }

        public virtual string nome { get; set; }

        public virtual byte qtdColunas { get; set; }

        public virtual byte qtdLinhas { get; set; }

        public virtual byte qtdPortaAvioes { get; set; }

        public virtual byte qtdDestroiers { get; set; }

        public virtual byte qtdEncouracados { get; set; }

        public virtual byte qtdCruzadores { get; set; }

        public virtual byte qtdSubmarinos { get; set; }

        public virtual Int16 tempoPosicionamento { get; set; }

        public virtual byte tempoJogada { get; set; }


    }

}
