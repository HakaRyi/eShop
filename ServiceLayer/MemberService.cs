using BOs.Entities;
using RepositoryLayer;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class MemberService : IMemberService
    {
        private readonly MemberRepository _repo;
        public MemberService(MemberRepository repo)
        {
            _repo = repo;
        }

        public void CreateMember(Member member) => _repo.CreateMember(member);
        public void DeleteMember(Member member) => _repo.DeleteMember(member);
        public List<Member> GetAllMembers() => _repo.GetAllMembers();
        public Member GetMemberByEmail(string email) => _repo.GetMemberByEmail(email);
        public Member GetMemberById(int id) => _repo.GetMemberById(id);
        public void UpdateMember(Member member) => _repo.UpdateMember(member);
    }
}
