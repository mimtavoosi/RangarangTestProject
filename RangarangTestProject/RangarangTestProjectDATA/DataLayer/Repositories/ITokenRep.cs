using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangarangTestProjectDATA.Domain;

namespace Repositories
{
    public interface ITokenRep
    {
        public Task<ListResultObject<Token>> GetAllTokensAsync(int creatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "");

        public Task<RowResultObject<Token>> GetTokenByIdAsync(int TokenId);

        public Task<RowResultObject<Token>> FindTokenAsync(string Token, string type, bool status = true);

        public Task<BitResultObject> AddTokenAsync(Token Token);

        public Task<BitResultObject> EditTokenAsync(Token Token);

        public Task<BitResultObject> MakeTokenExpireAsync(int TokenId);

        public Task<BitResultObject> RemoveTokenAsync(Token Token);

        public Task<BitResultObject> RemoveTokenAsync(int TokenId);

        public Task<BitResultObject> ExistTokenAsync(int TokenId);
    }
}