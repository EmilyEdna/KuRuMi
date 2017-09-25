using KuRuMi.Mio.DataObject.AutoMapperDTO.BaseProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DataObject.AutoMapperDTO
{
    /// <summary>
    /// automapper映射配置
    /// </summary>
    public class MapperConfigurationImpl:IAutoMapper
    {
        public MapperConfigurationImpl()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<ModelDtoProfile>());
        }
    }
}
