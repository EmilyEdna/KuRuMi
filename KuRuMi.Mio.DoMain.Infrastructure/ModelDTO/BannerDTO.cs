using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.ModelDTO
{
    public class BannerDTO: MapperBaseDTO
    {
        public Guid Id { get; set; }
        public string bannerId { get; set; }
        public string descript { get; set; }
        public string imgUrl { get; set; }
    }
}
