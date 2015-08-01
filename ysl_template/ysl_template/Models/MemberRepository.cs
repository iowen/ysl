using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
    public class MemberModel
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public PhotoModel Photo { get; set; }
        public string Email { get; set; }
    }
	public class MemberRepository : IMemberRepository
	{
		private yslDataContext db;
		public MemberRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Member> getAllMember()
		{
			List<Member> result;
			try
			{
				List<Member> list = this.db.Members.ToList<Member>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<Member>();
			}
			return result;
		}
		public Member getMember(int memberId)
		{
			Member result;
			try
			{
				Member member = this.db.Members.Single((Member a) => a.MemberId == memberId);
				result = member;
			}
			catch (Exception)
			{
				result = new Member();
			}
			return result;
		}
		public Member getMember(string memberName)
		{
			Member result;
			try
			{
				Member member = this.db.Members.Single((Member a) => a.Name == memberName);
				result = member;
			}
			catch (Exception)
			{
				result = new Member();
			}
			return result;
		}
		public int addMember(Member member)
		{
			int result;
			try
			{
				this.db.Members.InsertOnSubmit(member);
				this.db.SubmitChanges();
				result = member.MemberId;
			}
			catch (Exception)
			{
				result = -1;
			}
			return result;
		}
		public bool updateMember(Member member)
		{
			throw new NotImplementedException();
		}
        public MemberModel ConvertToModel(Member member)
        {
            MemberModel result = new MemberModel();
            PhotoRepository prepo = new PhotoRepository(new yslDataContext());
            result.MemberId = member.MemberId;
            result.Name = member.Name;
            result.FirstName = member.Account.FirstName;
            result.LastName = member.Account.LastName;
            result.Email = member.Account.Email;
            result.Photo = prepo.getPhotoAsModel(member.Photo);
            return result;
        }
	}
}
