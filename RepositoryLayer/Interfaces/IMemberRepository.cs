using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IMemberRepository
    {
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
        List<Member> GetAllMembers();
        Member GetMemberById(int id);
        Member GetMemberByEmail(string email);

    }
}
