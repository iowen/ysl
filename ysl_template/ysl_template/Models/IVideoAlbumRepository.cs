using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IVideoAlbumRepository
	{
		List<VideoAlbum> getAllVideoAlbums();
		List<VideoAlbum> getAllVideoAlbumsForAccount(int accountId);
		VideoAlbum getVideoAlbum(int videoAlbumId);
		int addVideoAlbum(string title, string description);
		bool updateVideoAlbum(int id, string title, string description);
	}
}
