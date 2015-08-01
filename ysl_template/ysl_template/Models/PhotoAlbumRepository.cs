using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ysl_template.Models
{
	public class PhotoAlbumRepository : IPhotoAlbumRepository
	{
		private yslDataContext db;
		public PhotoAlbumRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<PhotoAlbum> getAllPhotoAlbums()
		{
			List<PhotoAlbum> result;
			try
			{
				result = this.db.PhotoAlbums.ToList<PhotoAlbum>();
			}
			catch (Exception)
			{
				result = new List<PhotoAlbum>();
			}
			return result;
		}
		public List<PhotoAlbumModel> getPhotoAlbumsWithPhotos()
		{
			List<PhotoAlbumModel> result;
			try
			{
				var list = this.db.PhotoAlbums.Join(this.db.PhotoAlbums, (PhotoAlbum a) => a.PhotoAlbumId, (PhotoAlbum p) => p.PhotoAlbumId, (a, p) => new PhotoAlbumModel{album = a, photos = p.PhotoAlbumItems.ToList().Select(i=> i.Photo).ToList()});
				result = list.ToList();
			}
			catch (Exception)
			{
				result = new List<PhotoAlbumModel>();
			}
			return result;
		}
		public List<PhotoAlbum> getAllPhotoAlbumsForAccount(int accountId)
		{
			return (
				from a in this.db.PhotoAlbums
				where a.AccountId == accountId
				select a).ToList<PhotoAlbum>();
		}
		public PhotoAlbum getPhotoAlbum(int photoAlbumId)
		{
			PhotoAlbum result;
			try
			{
				result = this.db.PhotoAlbums.Single((PhotoAlbum a) => a.PhotoAlbumId == photoAlbumId);
			}
			catch (Exception)
			{
				result = new PhotoAlbum
				{
					PhotoAlbumId = -1
				};
			}
			return result;
		}
		public PhotoAlbum GetAccountPhotoAlbumByTitle(int account, string title)
		{
			PhotoAlbum result;
			try
			{
				result = this.db.PhotoAlbums.Single((PhotoAlbum a) => a.AccountId == account && a.Title == title);
			}
			catch (Exception)
			{
				result = new PhotoAlbum
				{
					PhotoAlbumId = -1
				};
			}
			return result;
		}
		public int addPhotoAlbum(PhotoAlbum album)
		{
			this.db.PhotoAlbums.InsertOnSubmit(album);
			this.db.SubmitChanges();
			return album.PhotoAlbumId;
		}
		public bool updatePhotoAlbum(PhotoAlbum album)
		{
			PhotoAlbum photoAlbum = this.db.PhotoAlbums.Single((PhotoAlbum a) => a.PhotoAlbumId == album.PhotoAlbumId);
			if (photoAlbum.PhotoAlbumId > 0)
			{
				photoAlbum.Title = album.Title;
				photoAlbum.Description = album.Description;
				photoAlbum.Updated = new DateTime?(DateTime.Now);
				photoAlbum.PhotoAlbumItems = album.PhotoAlbumItems;
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
		public List<Photo> GetPhotosForAlbum(int album)
		{
			List<Photo> result;
			try
			{
				List<Photo> list = (
					from p in this.db.PhotoAlbums.Single((PhotoAlbum a) => a.PhotoAlbumId == album).PhotoAlbumItems
					select p.Photo).ToList<Photo>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<Photo>();
			}
			return result;
		}
		public bool AccountPhotoAlbumExists(int account, string title)
		{
			return (
				from a in this.db.PhotoAlbums
				where a.AccountId == account && a.Title == title
				select a).Any<PhotoAlbum>();
		}
		public List<PhotoAlbum> SearchPhotoAlbums(string term)
		{
			return (
				from p in this.db.PhotoAlbums
				where p.Description.Contains(term) || p.Title.Contains(term)
				select p).ToList<PhotoAlbum>();
		}
		public PhotoAlbumData getPhotoAlbumDataForJSON(int id)
		{
			PhotoAlbumData result;
            var pRepo = new PhotoRepository(new yslDataContext());
			try
			{
				PhotoAlbum photoAlbum = this.db.PhotoAlbums.Single((PhotoAlbum a) => a.PhotoAlbumId == id);
				List<PhotoAlbumItemData> photos = (
					from a in this.db.PhotoAlbums
					where a.PhotoAlbumId == id
					select a).Join(this.db.PhotoAlbumItems, (PhotoAlbum a) => a.PhotoAlbumId, (PhotoAlbumItem p) => p.PhotoAlbumId,(a,p) => new PhotoAlbumItemData{photo = pRepo.getPhotoAsModel(p.Photo), photoAlbumItemId = a.PhotoAlbumId}).ToList();
				PhotoAlbumData photoAlbumData = new PhotoAlbumData
				{
					photoAlbumId = id,
					description = photoAlbum.Description,
					title = photoAlbum.Title,
					photos = photos
				};
				result = photoAlbumData;
			}
			catch (Exception)
			{
				result = new PhotoAlbumData();
			}
			return result;
		}
	}
}
