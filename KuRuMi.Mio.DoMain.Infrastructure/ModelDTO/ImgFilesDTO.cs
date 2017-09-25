using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.ModelDTO
{
    public class ImgFilesDTO: MapperBaseDTO
    {
        public Guid Id { get; set; }
        public Guid imgId { get; set; }
        public string imgUrl { get; set; }
        public string imgName { get; set; }
    }
}
