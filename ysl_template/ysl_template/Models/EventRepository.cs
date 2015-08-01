using System;
using System.Collections.Generic;
using System.Linq;

namespace ysl_template
{
    public partial class Event
    {
        public List<Photo> EventPhotos;
        public List<Audio> EventAudios;
        public List<Video> EventVideos;
        private Event e;

        public Event(Event e)
        {
            this.EventId = e.EventId;
            this.Ending = e.Ending;
            this.Start = e.Start;
            this.Title = e.Title;
            this.Description = e.Description;
            this.AccountId = e.AccountId;
            this.Created = e.Created;
            this.Updated = e.Updated;
            this.Account = e.Account;
            this.EventArtists = e.EventArtists;
            this.EventItems = e.EventItems;
            this.EventAudios = e.EventAudios;
            this.EventVideos = e.EventVideos;
            this.EventPhotos = e.EventPhotos;
            this.Location = e.Location;
            this.Photo = e.Photo;
            this.PhotoId = e.PhotoId;
            this.Venue = e.Venue;

        }
    }
}
namespace ysl_template.Models
{
	public class EventRepository : IEventRepository
	{
		private yslDataContext db;
		public EventRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Event> getAllEvents()
		{
			List<Event> result;
			try
			{
                List<Event> list = (
                    from e in this.db.Events
                    select e).ToList<Event>();
                using (List<Event>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Event e = enumerator.Current;
                        try
                        {
                            List<Photo> photos = (
                                from ep in this.db.EventItems
                                join p in this.db.Photos
                                on ep.RefId equals p.PhotoId
                                where ep.EventItemType.Trim().ToLower().Equals("photo")
                                where e.EventId.Equals(ep.EventId)
                                select p).ToList();
                            list.Find(a => a.EventId == e.EventId).EventPhotos = photos;
                        }
                        catch { }
                        try
                        {
                            List<Audio> audios = (
                                from ep in this.db.EventItems
                                join p in this.db.Audios
                                on ep.RefId equals p.AudioId
                                where ep.EventItemType.Trim().ToLower().Equals("audio")
                                where e.EventId.Equals(ep.EventId)
                                select p).ToList();
                            list.Find(a => a.EventId == e.EventId).EventAudios = audios;
                        }
                        catch { }
                        try
                        {
                            List<Video> videos = (
                                from ep in this.db.EventItems
                                join p in this.db.Videos
                                on ep.RefId equals p.VideoId
                                where ep.EventItemType.Trim().ToLower().Equals("video")
                                where e.EventId.Equals(ep.EventId)
                                select p).ToList();
                            list.Find(a => a.EventId == e.EventId).EventVideos = videos;
                        }
                        catch { }
                        
                    }
                }
				result = list;
			}
			catch (Exception)
			{
				result = new List<Event>();
			}
			return result;
		}
		public List<Event> getArtistEvents(int id, bool upcoming = true)
		{
			List<Event> result;
			try
			{
                if(!upcoming)
                result = (
					from e in this.db.Events
					where e.EventArtists.ToList<EventArtist>().Any((EventArtist a) => a.ArtistId == id)
                    orderby e.Start ascending
					select e).ToList<Event>();
                else
                    result = (
                    from e in this.db.Events
                    where e.EventArtists.ToList<EventArtist>().Any((EventArtist a) => a.ArtistId == id) 
                    where e.Start > DateTime.Now
                    orderby e.Start ascending
                    select e).ToList<Event>();
			}
			catch (Exception)
			{
				result = new List<Event>();
			}
			return result;
		}
		public Event getEvent(int id)
		{
			Event result;
			try
			{
				List<Event> list = (
					from e in this.db.Events
					where e.EventId == id
                   
					select e).ToList<Event>();
				result = ((list.Count > 0) ? list.ElementAt(0) : new Event());
                if (result.EventId > 0)
                {
                    var e = result;

                    try
                    {
                        List<Photo> photos = (
                            from ep in this.db.EventItems
                            join p in this.db.Photos
                            on ep.RefId equals p.PhotoId
                            where ep.EventItemType.Trim().ToLower().Equals("photo")
                            where e.EventId.Equals(ep.EventId)
                            select p).ToList();
                        result.EventPhotos = photos;
                    }
                    catch { }
                    try
                    {
                        List<Audio> audios = (
                            from ep in this.db.EventItems
                            join p in this.db.Audios
                            on ep.RefId equals p.AudioId
                            where ep.EventItemType.Trim().ToLower().Equals("audio")
                            where e.EventId.Equals(ep.EventId)
                            select p).ToList();
                        result.EventAudios = audios;
                    }
                    catch { }
                    try
                    {
                        List<Video> videos = (
                            from ep in this.db.EventItems
                            join p in this.db.Videos
                            on ep.RefId equals p.VideoId
                            where ep.EventItemType.Trim().ToLower().Equals("video")
                            where e.EventId.Equals(ep.EventId)
                            select p).ToList();
                        result.EventVideos = videos;
                    }
                    catch { }
                }
                      
			}
			catch (Exception)
			{
				result = new Event();
			}
			return result;
		}
		public List<Event> getRecentEvents()
		{
			List<Event> result;
			try
			{
				List<Event> list = (
					from e in this.db.Events
					orderby e.Start
					select e).Take(20).ToList<Event>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<Event>();
			}
			return result;
		}
		public List<EventData> getRecentEventsWithData()
		{
			List<EventData> result;
			try
			{
				List<Event> recentEvents = this.getRecentEvents();
				List<EventData> list = new List<EventData>();
				using (List<Event>.Enumerator enumerator = recentEvents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Event e = enumerator.Current;
						string photoLocation = (
							from p in this.db.Photos
							where p.PhotoId == e.PhotoId
							select p.Location).First<string>().ToString();
						list.Add(new EventData
						{
							_event = e,
							photoLocation = photoLocation
						});
					}
				}
				result = list;
			}
			catch (Exception)
			{
				result = new List<EventData>();
			}
			return result;
		}
		public List<Event> getUpcomingEvents()
		{
			List<Event> result;
			try
			{
				List<Event> list = (
					from e in this.db.Events
					where e.Start > (DateTime?)DateTime.Now
					orderby e.Start descending
					select e).Take(20).ToList<Event>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<Event>();
			}
			return result;
		}
		public int addEvent(Event _event)
		{
			this.db.Events.InsertOnSubmit(_event);
			this.db.SubmitChanges();
			return _event.EventId;
		}
		public bool updateEvent(int id, int account, string title, string location, string venue, string description, string start, string end, string date, int photo)
		{
			throw new NotImplementedException();
		}
	}
}
