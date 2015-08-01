using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IPhotoAlbumItemRepository
	{
		List<PhotoAlbumItem> getAllPhotoAlbumItems();
		List<PhotoAlbumItem> getAllPhotoAlbumsForPhoto(int PhotoId);
		List<PhotoAlbumItem> getAllPhotoAlbumsForAlbum(int albumId);
		PhotoAlbumItem getPhotoAlbumItem(int photoAlbumItemId);
		int addPhotoAlbumItem(int photo, int album);
		bool updatePhotoAlbumItem(int id, int photo, int album);
	}
}
