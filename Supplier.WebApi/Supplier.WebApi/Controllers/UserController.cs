using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dtos;
using Entity;
using LogicHandlers.ILogicHandlers;
using Microsoft.AspNetCore.Mvc;
using Utils.Redis;

namespace Supplier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogicHandler userService;
        readonly IRedisCacheManager _redisCacheManager;
        IMapper _mapper;
        public UserController(IUserLogicHandler userService, IRedisCacheManager _redisCacheManager, IMapper mapper)
        {
            this.userService = userService;
            this._redisCacheManager = _redisCacheManager;
            this._mapper = mapper;
        }

        /// <summary>
        /// 根据Id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById/{id}")]
        public async Task<ResultData> GetUserById(string id)
        {

            //throw new System.Exception("测试异常日志");
            //var exist = _redisCacheManager.KeyExists(id);
            //_redisCacheManager.Set(id,"111111", TimeSpan.FromSeconds(200));
            //var val = _redisCacheManager.GetValue(id);
            var model = await userService.QueryById(id);
            return new ResultData()
            {
                Count = 1,
                Data = model,
                Code = 200,
                Msg = ""
            };
        }


        /// <summary>
        /// 根据所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserAll")]
        public async Task<ResultData> GetUserAll()
        {
            var list = await userService.QueryAll();
            return new ResultData()
            {
                Count = list.Count,
                Data = list,
                Code = 200,
                Msg = ""
            };
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">实体</param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public async Task<ResultData> AddUser(User user)
        {
            var result = await userService.AddUser(user);
            //影响的行数
            if (result > 0)
            {
                return new ResultData()
                {

                    Data = result,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Data = result,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        [HttpPost("AddUserList")]
        public async Task<ResultData> AddUserList(List<User> userList)
        {
            if (await userService.AddManyUser(userList) > 0)
            {
                return new ResultData()
                {
                    Count = userList.Count,
                    Data = userList,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 0,
                    Data = null,
                    Code = 500,
                    Msg = ""
                };
            }
        }


        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public async Task<ResultData> UpdateUser(User user)
        {
            var result = await userService.UpdateUser(user);
            //返回成功（true）失败（false）
            if (result)
            {
                return new ResultData()
                {
                    Data = result,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Data = result,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        [HttpPost("UpdateUserList")]
        public async Task<ResultData> UpdateUserList(List<User> userList)
        {
            if (await userService.UpdateManyUser(userList) > 0)
            {
                return new ResultData()
                {
                    Count = userList.Count,
                    Data = userList,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Count = 0,
                    Data = null,
                    Code = 500,
                    Msg = ""
                };
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("DeleteUser")]
        public async Task<ResultData> DeleteUser(User user)
        {
            var result = await userService.Delete(user);
            //返回成功（true）失败（false）
            if (result)
            {
                return new ResultData()
                {
                    Data = result,
                    Code = 200,
                    Msg = ""
                };
            }
            else
            {
                return new ResultData()
                {
                    Data = result,
                    Code = 500,
                    Msg = ""
                };
            }
        }



     

        /// <summary>
        /// MapTest
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("UserToDto")]
        public LoginDto UserToDto(User user)
        {
            LoginDto model = null;
            model = _mapper.Map<LoginDto>(user);
            return model;
        }
    }
}