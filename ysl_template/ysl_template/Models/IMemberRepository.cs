using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IMemberRepository
	{
		List<Member> getAllMember();
		Member getMember(int memberId);
		Member getMember(string memberName);
		int addMember(Member member);
		bool updateMember(Member member);
	}
}
