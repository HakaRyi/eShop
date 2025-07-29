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
            return await _context.Members
                .Include(x => x.Transactions)
                .Include(x => x.Orders)
                .Include(x => x.Feedbacks)
                .ToListAsync();
        }

        //Login Function --------------------------------------
        public async Task<Member> Login(string email, string password)
        {
            var isExist = await GetByEmail(email);
            try
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, isExist.Password);
                if (!isPasswordCorrect)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return isExist;
        }

        //Register Function --------------------------------------
        public async Task<Member> Register(Member member)
        {
            var entry = await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _context.Members
                .Include(x => x.Transactions)
                .Include(x => x.Orders)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(x => x.Feedbacks)
                .FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Member> GetById(int id)
        {
            return await _context.Members
                .Include(x => x.Transactions)
                .Include(x => x.Orders)
                .Include(x => x.Feedbacks)
                .FirstOrDefaultAsync(m => m.MemberId == id);
        }

        public async Task<Member> Update(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }
        //Get Member by Email
        public async Task<Member?> GetMemberByEmailAsync(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }
    }
}
