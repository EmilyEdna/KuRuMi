using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.ModelDTO
{
    public class AtricleDTO : MapperBaseDTO
    {
        public Guid Id { get; set; }
        public Guid atricleId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string category { get; set; }
        public string tips { get; set; }
        public DateTime date { get; set; }
        public int count { get; set; }
    }
}
