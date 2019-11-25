using Entity;
using Repositories.IRepositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        ///// <summary>
        ///// 新增用户
        ///// </summary>
        ///// <param name="userInfo"></param>
        ///// <returns></returns>
        //Task<int> AddUser(User userInfo);


        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //Task<bool> DeleteUserByIds(string[] ids);


        ///// <summary>
        ///// 删除用户根据id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DeleteUserById(string id);




        ///// <summary>
        ///// 修改用户
        ///// </summary>
        ///// <param name="userInfo"></param>
        ///// <returns></returns>
        //Task<bool> EditUser(User userInfo);


        ///// <summary>
        ///// 登陆
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //Task<List<User>> LoginAsync(string userName, string password);


        ///// <summary>
        ///// 根据ID查询
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //Task<User> QueryUserById(string userId);


        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //Task<List<User>> QueryUserAll();



    }
}
