using AutoMapper;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using KuRuMi.Mio.DoMain.Model.Model;
using System.Linq;

namespace KuRuMi.Mio.DataObject.AutoMapperDTO.BaseProfiles
{
    /// <summary>
    /// 创建映射关系
    /// </summary>
    public class ModelDtoProfile: Profile
    {
        public ModelDtoProfile() {
            CreateEntity();
            CreateDTO();

        }
        /// <summary>
        /// DTO转Entity
        /// </summary>
        public void CreateEntity() {
            CreateMap<Sys_UserDTO, Sys_User>();
            CreateMap<BannerDTO, Banner>();
            CreateMap<BlogsDTO, Blogs>();
            CreateMap<AtricleDTO, Atricle>();
            CreateMap<ImgFilesDTO, ImgFiles>();
        }

        /// <summary>
        /// Entity转DTO
        /// </summary>
        public void CreateDTO() {
            CreateMap<Sys_User, Sys_UserDTO>();
            CreateMap<Banner, BannerDTO>();
            CreateMap<Blogs, BlogsDTO>();
            CreateMap<Atricle, AtricleDTO>();
            CreateMap<ImgFiles, ImgFilesDTO>();
        }
    }
}
