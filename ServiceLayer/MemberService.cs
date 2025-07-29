using BOs.Entities;
using RepositoryLayer;
using ServiceLayer.DTO;

namespace ServiceLayer
{
    public class MemberService
    {
        private readonly MemberRepository _repo;
        public MemberService(MemberRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Member>> GetMembers()
        {
            return await _repo.GetAll();
        }

        public async Task<Member> Login(string email, string password)
        {
            return await _repo.Login(email, password);
        }
        public async Task<MemberDTO> GetMemberByEmailAsync(string email)
        {
            var member = await _repo.GetMemberByEmailAsync(email);
            if (member != null)
            {
                return new MemberDTO { MemberId = member.MemberId, Email = member.Email };
            }
            return null;
        }
    }
}
