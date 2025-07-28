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
    }
}
