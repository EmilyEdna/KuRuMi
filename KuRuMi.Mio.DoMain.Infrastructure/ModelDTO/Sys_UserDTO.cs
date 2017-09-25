using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.ModelDTO
{
    public class Sys_UserDTO : MapperBaseDTO
    {
        public Guid Id { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string email { get; set; }
    }
}
