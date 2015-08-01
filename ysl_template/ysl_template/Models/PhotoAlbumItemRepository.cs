using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class PhotoAlbumItemRepository : IPhotoAlbumItemRepository
	{
		private yslDataContext db;
		public PhotoAlbumItemRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<PhotoAlbumItem> getAllPhotoAlbumItems()
		{
			return (
				from a in this.db.PhotoAlbumItems
				select a).ToList<PhotoAlbumItem>();
		}
		public List<PhotoAlbumItem> getAllPhotoAlbumsForPhoto(int photoId)
		{
			return (
				from a in this.db.PhotoAlbumItems
				where a.PhotoId == photoId
				select a).ToList<PhotoAlbumItem>();
		}
		public List<PhotoAlbumItem> getAllPhotoAlbumsForAlbum(int albumId)
		{
			return (
				from a in this.db.PhotoAlbumItems
				where a.PhotoAlbumId == albumId
				select a).ToList<PhotoAlbumItem>();
		}
		public PhotoAlbumItem getPhotoAlbumItem(int photoAlbumItemId)
		{
			PhotoAlbumItem result;
			try
			{
				PhotoAlbumItem photoAlbumItem = (
					from a in this.db.PhotoAlbumItems
					where a.PhotoAlbumItemId == photoAlbumItemId
					select a).FirstOrDefault<PhotoAlbumItem>();
				result = photoAlbumItem;
			}
			catch (ArgumentNullException)
			{
				result = new PhotoAlbumItem
				{
					PhotoAlbumItemId = -1
				};
			}
			return result;
		}
		public int addPhotoAlbumItem(int photo, int album)
		{
			PhotoAlbumItem photoAlbumItem = new PhotoAlbumItem();
			photoAlbumItem.PhotoId = photo;
			photoAlbumItem.PhotoAlbumId = album;
			this.db.PhotoAlbumItems.InsertOnSubmit(photoAlbumItem);
			this.db.SubmitChanges();
			return photoAlbumItem.PhotoAlbumItemId;
		}
		public bool updatePhotoAlbumItem(int id, int photo, int album)
		{
			PhotoAlbumItem photoAlbumItem = this.getPhotoAlbumItem(id);
			if (photoAlbumItem.PhotoAlbumItemId > 0)
			{
				photoAlbumItem.PhotoId = photo;
				photoAlbumItem.PhotoAlbumId = album;
				photoAlbumItem.Updated = new DateTime?(DateTime.Now);
				return true;
			}
			return false;
		}
	}
}
