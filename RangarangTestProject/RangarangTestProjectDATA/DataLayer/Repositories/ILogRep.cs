using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface ILogRep
    {
        public Task<ListResultObject<Log>> GetAllLogsAsync(int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<Log>> GetLogByIdAsync(int LogId);
        public Task<BitResultObject> AddLogAsync(Log Log);
        public Task<BitResultObject> EditLogAsync(Log Log);
        public Task<BitResultObject> RemoveLogAsync(Log Log);
        public Task<BitResultObject> RemoveLogAsync(int LogId);
        public Task<BitResultObject> ExistLogAsync(int LogId);
    }
}
