using Entity;
using LogicHandlers.ILogicHandlers;
using LogicHandlers.LogicHandlers.Base;
using Microsoft.Extensions.Configuration;
using Repositories.IRepositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicHandlers.LogicHandlers
{
 


    public class UserLogicHandler : BaseLogicHandler<User, IUserRepository>, IUserLogicHandler
    {
        IUserRepository userepository;
        ConfigMsg configMsg;
        public UserLogicHandler(IUserRepository Repository, IConfiguration configuration) : base(Repository)
        {
            userepository = Repository;

            configMsg = JsonSerializer.Deserialize<ConfigMsg>(configuration.GetSection("AppSetting").Value);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Task<int> AddUser(User userInfo)
        {
          
            return this.Repository.Add(userInfo);
        }

        /// <summary>
        /// 新增多个用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Task<int> AddManyUser(List<User> userList)
        {
            
            return this.Repository.AddMany(userList);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<bool> DeleteUserByIds(string[] ids)
        {
            return this.Repository.DeleteById(ids);
        }

        /// <summary>
        /// 删除用户根据id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteUserById(string id)
        {
            // this.Repository.DeleteUserById(id);
            return this.Repository.DeleteById(id);
        }



        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Task<bool> UpdateUser(User userInfo)
        {
            return this.Repository.Update(userInfo);
        }

        /// <summary>
        /// 批量更新用户
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateManyUser(List<User> userList)
        {
            return await this.Repository.UpdateMany(userList);
        }


     

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<User> QueryUserById(string userId)
        {
            return this.Repository.QueryById(userId);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> QueryUserAll()
        {

            ////    this.Repository.QueryMuch<User,User,User, User>((u,t,s) => new object[] {
            ////JoinType.Left,u.UserId==s.UserId,JoinType.Left,u.UserId==s.UserId}, (u,t, s) => new User { UserId = u.UserId, UserName = u.UserName },(u,t,s)=>   u.UserId=="0");
            return this.Repository.QueryAll();
        }


    }
}
