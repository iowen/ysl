using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public class PhotoAlbumData
	{
		public int photoAlbumId;
		public string title;
		public string description;
		public List<PhotoAlbumItemData> photos;
	}
}
