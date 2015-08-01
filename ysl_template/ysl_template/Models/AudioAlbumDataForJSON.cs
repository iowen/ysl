using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public class AudioAlbumDataForJSON
	{
		public int audioAlbumId;
		public string title;
		public string artist;
		public PhotoModel photo;
		public List<AudioAlbumItemModel> tracks;
        public bool isSingle;
        public bool isFeaturedOn;
	}
}
