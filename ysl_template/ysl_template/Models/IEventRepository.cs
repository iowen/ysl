using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IEventRepository
	{
		List<Event> getAllEvents();
        List<Event> getArtistEvents(int id, bool upcoming);
		Event getEvent(int id);
		List<Event> getRecentEvents();
		List<EventData> getRecentEventsWithData();
		List<Event> getUpcomingEvents();
		int addEvent(Event _event);
		bool updateEvent(int id, int account, string title, string location, string venue,string description, string start, string end, string date, int photo);
	}
}
