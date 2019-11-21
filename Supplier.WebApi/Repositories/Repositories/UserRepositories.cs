using Entity;
using Repositories.IRepositories;
using Repositories.Repositories.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Task<int> AddUser(User userInfo)
        {
            return this.Add(userInfo);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<bool> DeleteUserByIds(string[] ids)
        {
            return this.DeleteById(ids);
        }

        /// <summary>
        /// 删除用户根据id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteUserById(string id)
        {
            return this.DeleteById(id);
        }



        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Task<bool> EditUser(User userInfo)
        {
            return this.Update(userInfo);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<List<User>> LoginAsync(string userName, string password)
        {

            return this.Query(x => x.UserName.Equals(userName, StringComparison.Ordinal) && x.Password.Equals(password, StringComparison.Ordinal) && x.IsDelete == 0);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<User> QueryUserById(string userId)
        {
            return this.QueryById(userId);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> QueryUserAll()
        {
            return this.QueryAll();
        }




    }
}
