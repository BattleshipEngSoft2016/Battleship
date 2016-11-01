using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jogo_BackOffice.Models
{


    public class SkinsContext : DbContext
    {
        public SkinsContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Skin> Skins { get; set; }
    }

    [Table("TB_SKINS_TEMA")]
    public class Skin { 
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [Column("ID_SKIN_TEMA")]
        public int Id { get; set; }

    	[Column("DS_NOME_SKIN_TEMA")]
        public string Nome { get; set; }

        [Column("DS_DESCRICAO_TEMA")]
        public string Descricao { get; set; }

        [Column("NR_VALOR_SKIN_TEMA")]
        public int Valor { get; set; }

        [Column("DS_IMG_COORDENADA")]
        public string ImagemCoordenada { get; set; } 	

        [Column("DS_IMG_NAVIO1")]
        public string ImagemNav01 { get; set; } 	

        [Column("DS_IMG_NAVIO2")]
        public string ImagemNav02 { get; set; } 	

        [Column("DS_IMG_NAVIO3")]
        public string ImagemNav03 { get; set; } 	

        [Column("DS_IMG_NAVIO4")]
        public string ImagemNav04 { get; set; } 

        [Column("DS_IMG_NAVIO5")]
        public string ImagemNav05 { get; set; }

        public void AtulizarDominio(SkinModel vm)
        {

            Nome = vm.Nome;

            Valor = vm.Valor;

            ImagemCoordenada = vm.ImagemCoordenada;

            ImagemNav01 = vm.ImagemNav01;

            ImagemNav02 = vm.ImagemNav02;

            ImagemNav03 = vm.ImagemNav03;

            ImagemNav04 = vm.ImagemNav04;

            ImagemNav05 = vm.ImagemNav05;
        }
    }


    public class SkinModel
    {

        public virtual string Nome { get; set; }

        public virtual string Descricao { get; set; }

        public virtual int Valor { get; set; }

        public virtual string ImagemCoordenada { get; set; }

        public virtual string ImagemNav01 { get; set; }

        public virtual string ImagemNav02 { get; set; }

        public virtual string ImagemNav03 { get; set; }

        public virtual string ImagemNav04 { get; set; }

        public virtual string ImagemNav05 { get; set; } 


    }

}