using KuRuMi.Mio.DoMain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Model.Model
{
    [Table("Blogs")]
    public class Blogs: AggregateRoot
    {
        //[Description("博客Id")]
        private Guid blogsId;
        private string title;
        private string content;
        private string category;
        private string tips;
        private DateTime date;
        private int count;

        public Guid BlogsId { get => blogsId; set => blogsId = value; }
        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public string Category { get => category; set => category = value; }
        public string Tips { get => tips; set => tips = value; }
        public DateTime Date { get => date; set => date = value; }
        [DefaultValue(0)]
        public int Count { get => count; set => count = value; }
    }
}
