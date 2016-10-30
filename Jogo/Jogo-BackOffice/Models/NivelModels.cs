using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jogo_BackOffice.Models
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

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID_CONFIGURACAO")]
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
        }



    public class NivelModel
    {
        public virtual string Nome { get; set; }

        public virtual byte QtdColunas { get; set; }

        public virtual byte QtdLinhas { get; set; }

        public virtual byte QtdNav01 { get; set; }

        public virtual byte QtdNav02 { get; set; }

        public virtual byte QtdNav03 { get; set; }

        public virtual byte QtdNav04 { get; set; }

        public virtual byte QtdNav05 { get; set; }

        public virtual Int16 TempoPosicionamento { get; set; }

        public virtual byte TempoJogada { get; set; }


    }

}
