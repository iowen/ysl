using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class VideoAlbumRepository : IVideoAlbumRepository
	{
		private yslDataContext db;
		public VideoAlbumRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<VideoAlbum> getAllVideoAlbums()
		{
			return (
				from a in this.db.VideoAlbums
				select a).ToList<VideoAlbum>();
		}
		public List<VideoAlbum> getAllVideoAlbumsForAccount(int accountId)
		{
			return (
				from a in this.db.VideoAlbums
				where a.AccountId == accountId
				select a).ToList<VideoAlbum>();
		}
		public VideoAlbum getVideoAlbum(int videoAlbumId)
		{
			VideoAlbum result;
			try
			{
				VideoAlbum videoAlbum = (
					from a in this.db.VideoAlbums
					where a.VideoAlbumId == videoAlbumId
					select a).FirstOrDefault<VideoAlbum>();
				result = videoAlbum;
			}
			catch (ArgumentNullException)
			{
				result = new VideoAlbum
				{
					VideoAlbumId = -1
				};
			}
			return result;
		}
		public int addVideoAlbum(string title, string description)
		{
			VideoAlbum videoAlbum = new VideoAlbum();
			videoAlbum.Title = title;
			videoAlbum.Description = description;
			this.db.VideoAlbums.InsertOnSubmit(videoAlbum);
			return videoAlbum.VideoAlbumId;
		}
		public bool updateVideoAlbum(int id, string title, string description)
		{
			VideoAlbum videoAlbum = this.getVideoAlbum(id);
			if (videoAlbum.VideoAlbumId > 0)
			{
				videoAlbum.Title = title;
				videoAlbum.Description = description;
				videoAlbum.Updated = new DateTime?(DateTime.Now);
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
	}
}
