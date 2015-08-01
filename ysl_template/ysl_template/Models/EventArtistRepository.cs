using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class EventArtistRepository : IEventArtistRepository
	{
		private yslDataContext db;
		public EventArtistRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<EventArtist> getAllEventArtist(int eventId)
		{
			List<EventArtist> result;
			try
			{
				result = (
					from a in this.db.EventArtists
					where a.EventId == eventId
					select a).ToList<EventArtist>();
			}
			catch (Exception)
			{
				result = new List<EventArtist>();
			}
			return result;
		}
		public int addEventArtist(EventArtist artist)
		{
			int result;
			try
			{
				this.db.EventArtists.InsertOnSubmit(artist);
				this.db.SubmitChanges();
				result = artist.EventArtistId;
			}
			catch (Exception)
			{
				result = -1;
			}
			return result;
		}
		public bool removeEventArtist(EventArtist artist)
		{
			bool result;
			try
			{
				EventArtist entity = this.db.EventArtists.Single((EventArtist a) => a.EventArtistId == artist.EventArtistId);
				this.db.EventArtists.DeleteOnSubmit(entity);
				this.db.SubmitChanges();
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}
}
