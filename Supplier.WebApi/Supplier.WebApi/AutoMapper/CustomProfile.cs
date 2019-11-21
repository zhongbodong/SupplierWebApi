using AutoMapper;
using Entity;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.WebApi.AutoMapper
{
    public class CustomProfile : Profile
    {
       
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            //创建映射
            CreateMap<User, LoginDto>()
                .ForMember(d => d.SystemId, o => o.MapFrom(s => s.AddTime));//属性名称不一致


        }
    }
}
