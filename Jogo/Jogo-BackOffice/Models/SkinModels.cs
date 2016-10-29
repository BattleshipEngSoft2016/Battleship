using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Jogo.Models
{


    public class SkinsContext : DbContext
    {
        public SkinsContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Skin> Skins { get; set; }
    }

    // [Table("TB_GAME_SETTINGS")]
    public class Skin { 


    
    }

}