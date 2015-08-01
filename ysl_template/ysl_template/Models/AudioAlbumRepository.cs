using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ysl_template.Models
{
	public class AudioAlbumRepository : IAudioAlbumRepository
	{
		private yslDataContext db;
		public AudioAlbumRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<AudioAlbum> getAudioAlbums(int start, int limit = 20)
		{
			List<AudioAlbum> result;
			try
			{
				result = this.db.AudioAlbums.Skip(start).Take(limit).ToList<AudioAlbum>();
			}
			catch
			{
				result = new List<AudioAlbum>();
			}
			return result;
		}
		public List<AudioAlbumData> getAudioAlbumsWithCover(int start, int limit = 100)
		{
			List<AudioAlbumData> result;
			try
			{
                List<AudioAlbumData> list = this.db.AudioAlbums.Join(this.db.Photos, (AudioAlbum a) => a.PhotoId, (Photo p) => p.PhotoId, (AudioAlbum, Photo) => new AudioAlbumData { album = AudioAlbum, photo = Photo, isSingle = AudioAlbum.IsSingle, isFeaturedOn = AudioAlbum.IsFeatured}).Skip(start).Take(limit).ToList<AudioAlbumData>();
				result = list;
			}
			catch
			{
				result = new List<AudioAlbumData>();
			}
			return result;
		}
		public List<AudioAlbum> getAllAudioAlbums()
		{
			List<AudioAlbum> result;
			try
			{
				result = this.db.AudioAlbums.ToList<AudioAlbum>();
			}
			catch
			{
				result = new List<AudioAlbum>();
			}
			return result;
		}
		public List<AudioAlbum> getAllAudioAlbumsForArtist(int artistId)
		{
			return (
				from a in this.db.AudioAlbums
				where a.ArtistId == (int?)artistId
				select a).ToList<AudioAlbum>();
		}
		public AudioAlbum getAudioAlbum(int audioAlbumId)
		{
			AudioAlbum result;
			try
			{
				AudioAlbum audioAlbum = this.db.AudioAlbums.Single((AudioAlbum a) => a.AudioAlbumId == audioAlbumId);
				result = audioAlbum;
			}
			catch
			{
				result = new AudioAlbum
				{
					AudioAlbumId = -1
				};
			}
			return result;
		}
		public List<Audio> GetAudioForAlbum(int album)
		{
			return (
				from a in this.db.AudioAlbumItems
				where a.AudioAlbumId == album
				join p in this.db.Audios on a.AudioId equals p.AudioId into ap
				from b in ap
				select b).ToList<Audio>();
		}
		public AudioAlbum GetAccountAudioAlbumByTitle(int account, string title)
		{
			AudioAlbum result;
			try
			{
				AudioAlbum audioAlbum = this.db.AudioAlbums.Single((AudioAlbum a) => a.ArtistId == (int?)account && a.Title == title);
				result = audioAlbum;
			}
			catch
			{
				result = new AudioAlbum
				{
					AudioAlbumId = -1
				};
			}
			return result;
		}
		public int addAudioAlbum(AudioAlbum album)
		{
			this.db.AudioAlbums.InsertOnSubmit(album);
			this.db.SubmitChanges();
			return album.AudioAlbumId;
		}
		public bool updateAudioAlbum(AudioAlbum aAlbum)
		{
			AudioAlbum audioAlbum = this.db.AudioAlbums.Single((AudioAlbum a) => a.AudioAlbumId == aAlbum.AudioAlbumId);
			if (audioAlbum.AudioAlbumId > 0)
			{
				audioAlbum.Title = aAlbum.Title;
				audioAlbum.Description = aAlbum.Description;
				audioAlbum.Updated = new DateTime?(DateTime.Now);
				audioAlbum.PhotoId = aAlbum.PhotoId;
				audioAlbum.AudioAlbumItems = aAlbum.AudioAlbumItems;
				audioAlbum.ArtistId = aAlbum.ArtistId;
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
		public bool AccountAudioAlbumExists(int artist, string title)
		{
			bool result;
			try
			{
				bool flag = (
					from a in this.db.AudioAlbums
					where a.ArtistId == (int?)artist && a.Title == title
					select a).Any<AudioAlbum>();
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public AudioAlbumDataForJSON getAudioAlbumDataForJSON(int audioAlbumId)
		{
			var pRepo = new PhotoRepository(new yslDataContext());
			var aRepo = new AudioAlbumItemRepository(new yslDataContext());
			AudioAlbumDataForJSON result;
			try
			{
				List<AudioAlbumDataForJSON> source = (
					from b in this.db.AudioAlbums
					where b.AudioAlbumId == audioAlbumId
					select b).Join(this.db.Photos, (AudioAlbum a) => a.PhotoId, (Photo p) => p.PhotoId,(AudioAlbum, Photo) => new AudioAlbumDataForJSON{artist = AudioAlbum.Artist.Name, audioAlbumId= AudioAlbum.AudioAlbumId, isSingle = AudioAlbum.IsSingle, isFeaturedOn = AudioAlbum.IsFeatured, photo = pRepo.getPhotoAsModel(Photo),title = AudioAlbum.Title, tracks= aRepo.getAudioAlbumItemAsModel(AudioAlbum.AudioAlbumItems.ToList())}).ToList<AudioAlbumDataForJSON>();
				result = source.First<AudioAlbumDataForJSON>();
			}
			catch
			{
				result = new AudioAlbumDataForJSON();
			}
			return result;
		}
		public AudioAlbumDataForJSON getLatestSingle(int artistId)
		{

			var pRepo = new PhotoRepository(new yslDataContext());
			var aRepo = new AudioAlbumItemRepository(new yslDataContext());
			AudioAlbumDataForJSON result;
			try
			{
				List<AudioAlbumDataForJSON> source = (
					from b in this.db.AudioAlbums
					where b.ArtistId == artistId
					select b into s
					orderby s.Created descending
                    select s).Join(this.db.Photos, (AudioAlbum a) => a.PhotoId, (Photo p) => p.PhotoId, (AudioAlbum, Photo) => new AudioAlbumDataForJSON { artist = AudioAlbum.Artist.Name, audioAlbumId = AudioAlbum.AudioAlbumId, photo = pRepo.getPhotoAsModel(Photo), title = AudioAlbum.Title, tracks = aRepo.getAudioAlbumItemAsModel(AudioAlbum.AudioAlbumItems.ToList()) }).ToList<AudioAlbumDataForJSON>();
				result = source.First<AudioAlbumDataForJSON>();
			}
			catch
			{
				result = new AudioAlbumDataForJSON();
			}
			return result;
		}
	}
}
