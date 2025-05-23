﻿using Class_Lib.Backend.Person_related;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Class_Lib.Backend.Database.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AppDbContext context) : base(context) { }

        // for the query builder
        new public QueryBuilderService<User> Query()
        {
            return new QueryBuilderService<User>(null, _context.Users);
        }

        public override async Task<IEnumerable<User>> GetByCriteriaAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
