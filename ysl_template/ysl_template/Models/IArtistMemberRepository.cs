using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IArtistMemberRepository
	{
		List<ArtistMember> getAllArtistMember();
		ArtistMember getArtistMember(int memberId);
		int addArtistMember(ArtistMember member);
		bool updateArtistMember(ArtistMember member);
	}
}
