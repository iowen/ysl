using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public class MediaModels
	{
		public class PhotoAlbumWithItemsModel
		{
			public PhotoAlbum album;
			public List<PhotoAlbumItem> items;
		}
		public class AudioAlbumWithItemsModel
		{
			public AudioAlbum album;
			public List<AudioAlbumItem> items;
		}
		public class HomeFeaturedModel
		{
			public List<MediaModels.PhotoAlbumWithItemsModel> photoAlbums
			{
				get;
				set;
			}
			public List<MediaModels.AudioAlbumWithItemsModel> audioAlbums
			{
				get;
				set;
			}
		}
		public class PhotoSearchModel
		{
			public List<PhotoAlbum> photoAlbums
			{
				get;
				set;
			}
		}
	}
}
