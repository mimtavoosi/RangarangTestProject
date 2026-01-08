using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using Microsoft.EntityFrameworkCore;
using RangarangTestProjectDATA.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Services
{
    public class UserRep : IUserRep
    {
        private TheDbContext _context;

        public UserRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddUserAsync(User user)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                result.ID = user.ID;
                _context.Entry(user).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditUserAsync(User user)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                result.ID = user.ID;
                _context.Entry(user).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistUserAsync(string fieldValue, string fieldName)
        {
            BitResultObject result = new BitResultObject();
            int userId = 0;
            try
            {
                switch (fieldName.ToLower().Trim())
                {
                    case "id":
                    default:
                        {
                            var theUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.ID == long.Parse(fieldValue)) ?? new User();
                            userId = theUser.ID;
                            break;
                        }
                    case "username":
                        {
                            var theUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == fieldValue) ?? new User();
                            userId = theUser.ID;
                            break;
                        }
                    case "email":
                        {
                            var theUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == fieldValue) ?? new User();
                            userId = theUser.ID;
                            break;
                        }
                    case "nationalcode":
                        {
                            var theUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.NationalCode == fieldValue) ?? new User();
                            userId = theUser.ID;
                            break;
                        }
                }
                result.ID = userId;
                result.Status = userId > 0;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<RowResultObject<User>> AuthenticateAsync(string userName, string password, int loginType)
        {
            RowResultObject<User> result = new RowResultObject<User>();
            try
            {
                switch (loginType)
                {
                    default:
                    case 1:
                        {
                            result.Status = await _context.Users
               .AsNoTracking()
               .AnyAsync(x => x.Username == userName && x.PasswordHash == password.ToHash());
                            if (result.Status)
                            {
                                var loginRow = await _context.Users
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Username == userName && x.PasswordHash == password.ToHash());
                                if (loginRow != null)
                                {
                                    result.Result = loginRow;
                                    result.ErrorMessage = $"احراز هویت موفق بود";
                                }
                                else
                                {
                                    result.Status = false;
                                    result.ErrorMessage = $"احراز هویت ناموفق بود";

                                }

                            }
                            else
                            {
                                result.ErrorMessage = $"احراز هویت ناموفق بود";
                            }
                        }
                        break;
                    
                }

            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<ListResultObject<User>> GetAllUsersAsync(int creatorId = 0,int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<User> results = new ListResultObject<User>();
            try
            {
                IQueryable<User> query = _context.Users.AsNoTracking();

                if (creatorId > 0)
                {
                    query = query.Where(x=> x.CreatorId == creatorId);
                }

                query = query
               .Where(x =>
                   (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.Contains(searchText)) ||
                      (!string.IsNullOrEmpty(x.LastName) && x.LastName.Contains(searchText)) ||
                       (!string.IsNullOrEmpty(x.Email) && x.Email.Contains(searchText)) ||
                       (!string.IsNullOrEmpty(x.NationalCode) && x.NationalCode.Contains(searchText)) ||
                       (!string.IsNullOrEmpty(x.Username) && x.Username.Contains(searchText))
               );

                results.TotalCount = query.Count();
                results.PageCount = DbTools.GetPageCount(results.TotalCount, pageSize);
                results.Results = await query.OrderByDescending(x => x.CreateDate)
                     .SortBy(sortQuery).ToPaging(pageIndex, pageSize)
                    .ToListAsync();


            }
            catch (Exception ex)
            {
                results.Status = false;
                results.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return results;
        }

        public async Task<RowResultObject<User>> GetUserByIdAsync(int userId)
        {
            RowResultObject<User> result = new RowResultObject<User>();
            try
            {
                result.Result = await _context.Users
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.ID == userId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveUserAsync(User user)
        {
            BitResultObject result = new BitResultObject();
            try
            {

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                result.ID = user.ID;
                _context.Entry(user).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveUserAsync(int userId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var user = await GetUserByIdAsync(userId);
                result = await RemoveUserAsync(user.Result);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }
    }
}