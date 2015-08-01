using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class VideoRepository : IVideoRepository
	{
		private yslDataContext db;
		public VideoRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Video> getAllVideos()
		{
			return (
				from a in this.db.Videos
				select a).ToList<Video>();
		}
		public List<Video> getAllVideosForAccount(int accountId)
		{
			return (
				from a in this.db.Videos
				where a.AccountId == accountId
				select a).ToList<Video>();
		}
		public Video getVideo(int videoId)
		{
			Video result;
			try
			{
				Video video = (
					from a in this.db.Videos
					where a.VideoId == videoId
					select a).FirstOrDefault<Video>();
				result = video;
			}
			catch (ArgumentNullException)
			{
				result = new Video
				{
					VideoId = -1
				};
			}
			return result;
		}
		public List<Video> getRecentUploads()
		{
			return (
				from v in this.db.Videos
				orderby v.Uploaded descending
				select v).Take(20).ToList<Video>();
		}
		public int addVideo(int account, string title, string description, string location)
		{
			Video video = new Video();
			video.Title = title;
			video.AccountId = account;
			video.Description = description;
			video.Location = location;
			this.db.Videos.InsertOnSubmit(video);
			this.db.SubmitChanges();
			return video.AccountId;
		}
		public bool updateVideo(int id, string title, string description, string locaiton)
		{
			Video video = this.getVideo(id);
			if (video.VideoId > 0)
			{
				video.Title = title;
				video.Description = description;
				video.Location = locaiton;
				video.Updated = new DateTime?(DateTime.Now);
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
	}
}
