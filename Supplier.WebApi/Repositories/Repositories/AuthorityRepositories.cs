using Dtos;
using Repositories.IRepositories;
using Repositories.Repositories.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
   public  class AuthorityRepositories : BaseRepository<DataAuthority>,IAuthorityRepositories
    {
        public AuthorityRepositories(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }
    }
}
