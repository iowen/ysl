using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public class AudioAlbumData
	{
		public List<AudioAlbumItem> tracks;
		public AudioAlbum album;
		public Photo photo;
        public bool isSingle;
        public bool isFeaturedOn;
	}
}
