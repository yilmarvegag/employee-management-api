using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EmployeeManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await FindByCondition(x => x.Username == username)
                .FirstOrDefaultAsync();
        }
    }
}
