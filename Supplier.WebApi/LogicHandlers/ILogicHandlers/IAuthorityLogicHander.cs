using Dtos;
using LogicHandlers.ILogicHandlers.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlers.ILogicHandlers
{
   public interface IAuthorityLogicHander : IBaseLogicHandler<DataAuthority>
    {
        Task<List<DataAuthority>> GetCostCenter();
    }
}
