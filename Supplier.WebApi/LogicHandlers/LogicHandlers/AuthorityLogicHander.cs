using Dtos;
using LogicHandlers.ILogicHandlers;
using LogicHandlers.LogicHandlers.Base;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlers.LogicHandlers
{
    public class AuthorityLogicHander : BaseLogicHandler<DataAuthority, IAuthorityRepositories>, ILogicHandlers.IAuthorityLogicHander
    {

        IAuthorityRepositories userepository;
        public AuthorityLogicHander(IAuthorityRepositories Repository) : base(Repository)
        {
            userepository = Repository;

        }

        public async Task<List<DataAuthority>> GetCostCenter()
        {
            return  await Task.Run(()=>new List<DataAuthority>() { new DataAuthority() { Id="1",PId="0"} });
        }
    }
}
