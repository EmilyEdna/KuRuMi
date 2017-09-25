using KuRuMi.Mio.DoMain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.Model
{
    [Table("ImgFiles")]
    public class ImgFiles : AggregateRoot
    {
        private Guid imgId;
        private string imgName;
        private string imgUrl;
       //private User user;

        public Guid ImgId { get => imgId; set => imgId = value; }
        public string ImgName { get => imgName; set => imgName = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
       // [ForeignKey("ImgId")]
       // public User User { get => user; set => user = value; }
    }
}
