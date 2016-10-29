using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jogo.Models
{
    public class NiveisContext : DbContext
    {
        public NiveisContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Nivel> Niveis { get; set; }
    }

        [Table("TB_GAME_SETTINGS")]
        public class Nivel
        { 

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID_CONFIGURACAO")]
        public int Id { get; set; }

        [Column("DS_NOME_CONFIGURACAO")]
        public string Nome { get; set; }



        }



        //public class SkinsModel
        //{
        //    //[Required]
        //    //[Display(Name = "User name")]
        //    //public string UserName { get; set; }

        //    //[Required]
        //    //[DataType(DataType.Password)]
        //    //[Display(Name = "Password")]
        //    //public string Password { get; set; }

        //    //[Display(Name = "Remember me?")]
        //    //public bool RememberMe { get; set; }
        //}

    }
