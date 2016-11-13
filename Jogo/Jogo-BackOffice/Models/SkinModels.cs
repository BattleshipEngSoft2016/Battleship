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

        public Skin()
        {
            
        }

        public Skin(SkinModel vm)
        {
            Nome = vm.nome;

            Valor = vm.valor;

            ImagemCoordenada = vm.ImagemCoordenada;

            ImagemNav01 = vm.imagemNav01;

            ImagemNav02 = vm.imagemNav02;

            ImagemNav03 = vm.imagemNav03;

            ImagemNav04 = vm.imagemNav04;

            ImagemNav05 = vm.imagemNav05;

        }

        public void AtulizarDominio(SkinModel vm)
        {

            Nome = vm.nome;

            Valor = vm.valor;

            ImagemCoordenada = vm.ImagemCoordenada;

            ImagemNav01 = vm.imagemNav01;

            ImagemNav02 = vm.imagemNav02;

            ImagemNav03 = vm.imagemNav03;

            ImagemNav04 = vm.imagemNav04;

            ImagemNav05 = vm.imagemNav05;
        }
    }


    public class SkinModel
    {

        public virtual string nome { get; set; }

        public virtual string descricao { get; set; }

        public virtual int valor { get; set; }

        public virtual string ImagemCoordenada { get; set; }

        public virtual string imagemNav01 { get; set; }

        public virtual string imagemNav02 { get; set; }

        public virtual string imagemNav03 { get; set; }

        public virtual string imagemNav04 { get; set; }

        public virtual string imagemNav05 { get; set; } 


    }

}