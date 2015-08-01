using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public static class GlobalVariables
	{
        private static int _defaultEventImageId;
        public static int DefaultEventImageId
        {
            get
            {
                if (_defaultEventImageId <= 0)
                {
                    int photoId = 1012;
                    GlobalVariables.SetDefaultEventImageId(photoId);
                }
                return _defaultEventImageId;
            }
        }

        private static void SetDefaultEventImageId(int photoId)
        {
            _defaultEventImageId = photoId;
        }
		private static List<ArtistMenuDataModel> _artist = new List<ArtistMenuDataModel>();
		public static List<ArtistMenuDataModel> Artist
		{
			get
			{
				GlobalVariables.SetArtist();
				return GlobalVariables._artist;
			}
		}
		public static void SetArtist()
		{
            ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
            _artist = artistRepository.updateArtistList(_artist);
		}
	}
}
