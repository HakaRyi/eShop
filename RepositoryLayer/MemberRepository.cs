using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer
{
    public class MemberRepository : IMemberRepository
    {
        private readonly EShopContext _context;
        public MemberRepository(EShopContext context)
        {
            _context = context;
        }


        public void CreateMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void DeleteMember(Member member)
        {
            var availableAccount = _context.Members.FirstOrDefault(a=> a.MemberId == member.MemberId);
            if (availableAccount == null)
            {
                return;
            }
            else
            {
                _context.Members.Remove(availableAccount);
                _context.SaveChanges();
            }
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Member GetMemberByEmail(string email)
        {
            var member = _context.Members.FirstOrDefault(a => a.Email == email);
            return member;
        }

        public Member GetMemberById(int id)
        {
            var member = _context.Members.FirstOrDefault(a => a.MemberId == id);
            return member;
        }

        public void UpdateMember(Member member)
        {
            var tracker = _context.Members.Attach(member);  
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
