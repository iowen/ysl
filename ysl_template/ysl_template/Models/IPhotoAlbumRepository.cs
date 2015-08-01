using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IPhotoAlbumRepository
	{
		List<PhotoAlbum> getAllPhotoAlbums();
		List<PhotoAlbum> getAllPhotoAlbumsForAccount(int accountId);
		List<PhotoAlbumModel> getPhotoAlbumsWithPhotos();
		PhotoAlbumData getPhotoAlbumDataForJSON(int id);
		PhotoAlbum getPhotoAlbum(int photoAlbumId);
		PhotoAlbum GetAccountPhotoAlbumByTitle(int account, string title);
		List<Photo> GetPhotosForAlbum(int album);
		List<PhotoAlbum> SearchPhotoAlbums(string term);
		int addPhotoAlbum(PhotoAlbum album);
		bool updatePhotoAlbum(PhotoAlbum album);
		bool AccountPhotoAlbumExists(int account, string title);
	}
}
