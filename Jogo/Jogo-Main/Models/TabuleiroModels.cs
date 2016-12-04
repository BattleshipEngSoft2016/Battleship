using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{
    public class TabuleirosContext : DbContext
    {

        public TabuleirosContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Tabuleiro> Tabuleiros { get; set; }

     
    }

    [Table("Tabuleiros")]
    public class Tabuleiro
    {
        [Key, Column("Id")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("NivelId")]
        public int NivelId { get; set; }

        [Column("SkinId")]
        public int SkinId { get; set; }

        [Column("Dados")]
        public string Dados { get; set; }

        public Tabuleiro(int u, int n, int s, string dados)
        {
            UserId = u;

            NivelId = n;

            SkinId = s;

            Dados = dados;
        }
    }
}