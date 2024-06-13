using Microsoft.EntityFrameworkCore;
using ApiAuth.Context;
using ApiAuth.models;

namespace ApiAuth.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

    }


}
