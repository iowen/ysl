using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class VideoAlbumItemRepository : IVideoAlbumItemRepository
	{
		private yslDataContext db;
		public VideoAlbumItemRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<VideoAlbumItem> getAllVideoAlbumItems()
		{
			return (
				from a in this.db.VideoAlbumItems
				select a).ToList<VideoAlbumItem>();
		}
		public List<VideoAlbumItem> getAllVideoAlbumsForVideo(int videoId)
		{
			return (
				from a in this.db.VideoAlbumItems
				where a.VideoId == videoId
				select a).ToList<VideoAlbumItem>();
		}
		public List<VideoAlbumItem> getAllVideoAlbumsForAlbum(int albumId)
		{
			return (
				from a in this.db.VideoAlbumItems
				where a.VideoAlbumId == albumId
				select a).ToList<VideoAlbumItem>();
		}
		public VideoAlbumItem getVideoAlbumItem(int videoAlbumItemId)
		{
			VideoAlbumItem result;
			try
			{
				VideoAlbumItem videoAlbumItem = (
					from a in this.db.VideoAlbumItems
					where a.VideoAlbumItemId == videoAlbumItemId
					select a).FirstOrDefault<VideoAlbumItem>();
				result = videoAlbumItem;
			}
			catch (ArgumentNullException)
			{
				result = new VideoAlbumItem
				{
					VideoAlbumItemId = -1
				};
			}
			return result;
		}
		public int addVideoAlbumItem(int video, int album)
		{
			VideoAlbumItem videoAlbumItem = new VideoAlbumItem();
			videoAlbumItem.VideoId = video;
			videoAlbumItem.VideoAlbumId = album;
			this.db.VideoAlbumItems.InsertOnSubmit(videoAlbumItem);
			this.db.SubmitChanges();
			return videoAlbumItem.VideoAlbumItemId;
		}
		public bool updateVideoAlbumItem(int id, int video, int album)
		{
			VideoAlbumItem videoAlbumItem = this.getVideoAlbumItem(id);
			if (videoAlbumItem.VideoAlbumItemId > 0)
			{
				videoAlbumItem.VideoId = video;
				videoAlbumItem.VideoAlbumId = album;
				videoAlbumItem.Updated = new DateTime?(DateTime.Now);
				return true;
			}
			return false;
		}
	}
}
