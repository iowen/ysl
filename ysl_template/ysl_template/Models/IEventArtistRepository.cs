using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IEventArtistRepository
	{
		List<EventArtist> getAllEventArtist(int eventId);
		int addEventArtist(EventArtist artist);
		bool removeEventArtist(EventArtist artist);
	}
}
