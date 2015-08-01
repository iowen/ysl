using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class ArtistMemberRepository : IArtistMemberRepository
	{
		private yslDataContext db;
		public ArtistMemberRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<ArtistMember> getAllArtistMember()
		{
			List<ArtistMember> result;
			try
			{
				List<ArtistMember> list = this.db.ArtistMembers.ToList<ArtistMember>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<ArtistMember>();
			}
			return result;
		}
		public ArtistMember getArtistMember(int memberId)
		{
			ArtistMember result;
			try
			{
				ArtistMember artistMember = this.db.ArtistMembers.Single((ArtistMember a) => a.ArtistMemberId == memberId);
				result = artistMember;
			}
			catch (Exception)
			{
				result = new ArtistMember();
			}
			return result;
		}
		public int addArtistMember(ArtistMember member)
		{
			int result;
			try
			{
				this.db.ArtistMembers.InsertOnSubmit(member);
				this.db.SubmitChanges();
				result = member.ArtistMemberId;
			}
			catch (Exception)
			{
				result = -1;
			}
			return result;
		}
		public bool updateArtistMember(ArtistMember member)
		{
			throw new NotImplementedException();
		}
	}
}
