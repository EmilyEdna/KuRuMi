using KuRuMi.Mio.DoMain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.Model
{
    [Table("Banner")]
    public class Banner:AggregateRoot
    {
        private Guid bannerId;
        private string descript;
        private string imgUrl;
       // private User user;

        public Guid BannerId { get => bannerId; set => bannerId = value; }
        public string Descript { get => descript; set => descript = value; }
        [MaxLength(50)]
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
       // [ForeignKey("BannerId")]
       // public virtual User User { get => user; set => user = value; }
      
    }
}
