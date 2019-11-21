using Entity;
using LogicHandlers.ILogicHandlers.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlers.ILogicHandlers
{



    public interface IUserLogicHandler : IBaseLogicHandler<User>
    {

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        Task<int> AddUser(User userInfo);


        Task<int> AddManyUser(List<User> userList);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteUserByIds(string[] ids);


        /// <summary>
        /// 删除用户根据id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUserById(string id);




        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(User userInfo);


        /// <summary>
        /// 批量更新用户
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        Task<int> UpdateManyUser(List<User> userList);





        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> QueryUserById(string userId);


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<User>> QueryUserAll();
    }
}
