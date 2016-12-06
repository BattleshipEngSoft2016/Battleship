using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{

    public class UserSkisContext : DbContext
    {
        public UserSkisContext()
            : base("DefaultConnection")
        {



        }

        public DbSet<UserSkin> UserSkins { get; set; }


    }


        [Table("UserSkins")]
        public class UserSkin
        {
            [Key, Column("Id")]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            [Column("UserId")]
            public int UserId { get; set; }

            [Column("SkinId")]
            public int SkinId { get; set; }


            public UserSkin()
            {
                
            }

            public UserSkin(int u, int s)
            {

                UserId = u;

                SkinId = s;
            }


        }
}