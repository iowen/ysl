using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ysl_template.Models
{
	public class MediaRepository : IMediaRepository
	{
		private yslDataContext db;
		public MediaRepository(yslDataContext context)
		{
			this.db = context;
		}
		public MediaModels.HomeFeaturedModel GetRecentMedia()
		{
			List<MediaModels.PhotoAlbumWithItemsModel> photoAlbums = new List<MediaModels.PhotoAlbumWithItemsModel>();
			List<MediaModels.AudioAlbumWithItemsModel> audioAlbums = new List<MediaModels.AudioAlbumWithItemsModel>();
			try
			{
				photoAlbums = (
					from a in (
						from p in this.db.PhotoAlbums
						where p.AccountId == 5
						select p).Select(a => new MediaModels.PhotoAlbumWithItemsModel{album=a, items = a.PhotoAlbumItems.ToList()}) select a ).OrderByDescending(a => a.album.Created).Take(5).ToList();
                   
			}
			catch (Exception)
			{
			}
            try
            {
                audioAlbums = (
                    from a in
                        (
                            from a in this.db.AudioAlbums
                            where a.ArtistId == 1
                            select a).Select(a => new MediaModels.AudioAlbumWithItemsModel { album = a, items = a.AudioAlbumItems.ToList() })
                    select a).OrderByDescending(a => a.album.Created).Take(5).ToList();
            }
            catch (Exception)
            {
            }
			return new MediaModels.HomeFeaturedModel
			{
				audioAlbums = audioAlbums,
				photoAlbums = photoAlbums
			};
		}
	}
}
