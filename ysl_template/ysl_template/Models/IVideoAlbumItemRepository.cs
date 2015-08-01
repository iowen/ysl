using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IVideoAlbumItemRepository
	{
		List<VideoAlbumItem> getAllVideoAlbumItems();
		List<VideoAlbumItem> getAllVideoAlbumsForVideo(int videoId);
		List<VideoAlbumItem> getAllVideoAlbumsForAlbum(int albumId);
		VideoAlbumItem getVideoAlbumItem(int VideoAlbumItemId);
		int addVideoAlbumItem(int video, int album);
		bool updateVideoAlbumItem(int id, int video, int album);
	}
}
