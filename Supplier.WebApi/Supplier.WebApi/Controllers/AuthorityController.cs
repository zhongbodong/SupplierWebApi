using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;
using LogicHandlers.ILogicHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Supplier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        public IAuthorityLogicHander AuthorityService { get; }

        public AuthorityController(IAuthorityLogicHander authorityService)
        {
            AuthorityService = authorityService;
        }

        [HttpGet]
        public async Task<List<DataAuthority>> GetCostCenter()
        {
            return await AuthorityService.GetCostCenter();
        }


    }
}