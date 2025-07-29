using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class MemberRepository
    {
        private readonly EShopContext _context;
        public MemberRepository(EShopContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAll()
        {
            return await _context.Members.ToListAsync();
        }

        //Login Function --------------------------------------
        public async Task<Member> Login(string email, string password)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
        }
    }
}
