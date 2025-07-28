using BOs.Entities;
using RepositoryLayer;

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
    }
}
