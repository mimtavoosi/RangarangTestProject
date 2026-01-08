using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IUserRep
    {
        Task<ListResultObject<User>> GetAllUsersAsync(int creatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "");

        Task<RowResultObject<User>> GetUserByIdAsync(int userId);

        Task<RowResultObject<User>> AuthenticateAsync(string userName, string password, int loginType);

        Task<BitResultObject> AddUserAsync(User user);

        Task<BitResultObject> EditUserAsync(User user);

        Task<BitResultObject> RemoveUserAsync(User user);

        Task<BitResultObject> RemoveUserAsync(int userId);

        Task<BitResultObject> ExistUserAsync(string fieldValue, string fieldName);
    }
}