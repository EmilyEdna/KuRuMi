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
    [Table("Sys_User")]
    public class Sys_User : AggregateRoot
    {
        private string userName;
        private string passWord;
        private string email;
        //private List<Banner> banner;
        //private List<Blogs> blogs;
        //private List<Atricle> atricle;
        //private List<ImgFiles> imgFiles;

        [Required, MaxLength(50)]
        public string UserName { get => userName; set => userName = value; }
        [Required, MaxLength(50)]
        public string PassWord { get => passWord; set => passWord = value; }
        public string Email { get => email; set => email = value; }
        //public List<Banner> Banner { get => banner; set => banner = value; }
        //public List<Blogs> Blogs { get => blogs; set => blogs = value; }
        //public List<Atricle> Atricle { get => atricle; set => atricle = value; }
        //public List<ImgFiles> ImgFiles { get => imgFiles; set => imgFiles = value; }
    }
}
