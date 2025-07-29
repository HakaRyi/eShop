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

        public async Task<bool> Register(RegisterDTO dto)
        {
            Member member = new Member();
            member.Email = dto.Email;
            member.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            member.CompanyName = dto.CompanyName;
            member.City = dto.City;
            member.Country = dto.Country;
            member.Status = true;

            var result = await _repo.Register(member);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isExistByEmail(string email)
        {
            var result = await _repo.GetByEmail(email);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Member> GetByEmail(string email)
        {
            var result = await _repo.GetByEmail(email);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Member> GetById(int id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> Update(UpdateMemberDTO dto)
        {
            Member member = await _repo.GetById(dto.MemberId);
            member.Email = dto.Email;
            member.CompanyName = dto.CompanyName;
            member.City = dto.City;
            member.Country = dto.Country;

            var result = await _repo.Update(member);
            if (result == null)
            {
                return false;
            }
            return true;
          
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
