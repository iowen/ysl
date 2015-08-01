using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class PhotoRepository : IPhotoRepository
	{
		private yslDataContext db;
		public PhotoRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Photo> getAllPhotos()
		{
			return this.db.Photos.ToList<Photo>();
		}
		public List<Photo> getAllPhotosForAccount(int accountId)
		{
			return (
				from a in this.db.Photos
				where a.AccountId == accountId
				select a).ToList<Photo>();
		}
		public Photo getPhoto(int photoId)
		{
			Photo result;
			try
			{
				result = this.db.Photos.Single((Photo p) => p.PhotoId == photoId);
			}
			catch (Exception)
			{
				result = new Photo
				{
					PhotoId = -1
				};
			}
			return result;
		}
		public List<Photo> getRecentUploads()
		{
			return (
				from p in this.db.Photos
				orderby p.Uploaded descending
				select p).Take(20).ToList<Photo>();
		}
		public List<Photo> SearchPhotos(string term)
		{
			return (
				from p in this.db.Photos
				where p.Description.Contains(term) || p.Title.Contains(term)
				select p).ToList<Photo>();
		}
		public int addPhoto(Photo photo)
		{
			this.db.Photos.InsertOnSubmit(photo);
			this.db.SubmitChanges();
			return photo.PhotoId;
		}
		public bool updatePhoto(Photo photoItem)
		{
			Photo photo = this.getPhoto(photoItem.PhotoId);
			if (photo.PhotoId > 0)
			{
				photo.Title = photoItem.Title;
				photo.Description = photoItem.Description;
				photo.Location = photoItem.Location;
				photo.Updated = DateTime.Now;
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
		public PhotoModel getPhotoAsModel(Photo item)
		{
			return new PhotoModel
			{
				Description = item.Description,
				Location = item.Location,
				PhotoId = item.PhotoId,
				Title = item.Title
			};
		}
	}
}
