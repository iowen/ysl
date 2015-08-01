using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class AudioAlbumItemRepository : IAudioAlbumItemRepository
	{
		private yslDataContext db;
		public AudioAlbumItemRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<AudioAlbumItem> getAllAudioAlbumItems()
		{
			return (
				from a in this.db.AudioAlbumItems
				select a).ToList<AudioAlbumItem>();
		}
		public List<AudioAlbumItem> getAllAudioAlbumsForAudio(int audioId)
		{
			return (
				from a in this.db.AudioAlbumItems
				where a.AudioId == audioId
				select a).ToList<AudioAlbumItem>();
		}
		public List<AudioAlbumItem> getAllAudioAlbumItemsForAlbum(int albumId)
		{
			return (
				from a in this.db.AudioAlbumItems
				where a.AudioAlbumId == albumId
				select a).ToList<AudioAlbumItem>();
		}
		public AudioAlbumItem getAudioAlbumItem(int audioAlbumItemId)
		{
			AudioAlbumItem result;
			try
			{
				AudioAlbumItem audioAlbumItem = (
					from a in this.db.AudioAlbumItems
					where a.AudioAlbumItemId == audioAlbumItemId
					select a).FirstOrDefault<AudioAlbumItem>();
				result = audioAlbumItem;
			}
			catch (ArgumentNullException)
			{
				result = new AudioAlbumItem
				{
					AudioAlbumItemId = -1
				};
			}
			return result;
		}
		public int addAudioAlbumItem(AudioAlbumItem audioAlbumItem)
		{
			this.db.AudioAlbumItems.InsertOnSubmit(audioAlbumItem);
			this.db.SubmitChanges();
			return audioAlbumItem.AudioAlbumItemId;
		}
		public bool updateAudioAlbumItem(AudioAlbumItem item)
		{
			AudioAlbumItem audioAlbumItem = this.db.AudioAlbumItems.Single((AudioAlbumItem a) => a.AudioAlbumItemId == item.AudioAlbumItemId);
			bool result;
			try
			{
				if (audioAlbumItem.AudioAlbumItemId > 0)
				{
					audioAlbumItem.AudioId = item.AudioId;
					audioAlbumItem.AudioAlbumId = item.AudioAlbumId;
					audioAlbumItem.Updated = new DateTime?(DateTime.Now);
					audioAlbumItem.Track = item.Track;
					this.db.SubmitChanges();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public AudioAlbumItemModel getAudioAlbumItemAsModel(AudioAlbumItem item)
		{
			AudioRepository audioRepository = new AudioRepository(new yslDataContext());
			return new AudioAlbumItemModel
			{
				audioAlbumItemId = item.AudioAlbumItemId,
				trackNumber = item.Track,
				audio = audioRepository.convertAudioToModel(item.Audio)
			};
		}
		public List<AudioAlbumItemModel> getAudioAlbumItemAsModel(List<AudioAlbumItem> items)
		{
			List<AudioAlbumItemModel> list = new List<AudioAlbumItemModel>();
			foreach (AudioAlbumItem current in items)
			{
				list.Add(this.getAudioAlbumItemAsModel(current));
			}
			return list;
		}
		public bool deleteAudioAlbumItem(AudioAlbumItem item)
		{
			bool result;
			try
			{
				this.db.AudioAlbumItems.DeleteOnSubmit(item);
				this.db.SubmitChanges();
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}
}
