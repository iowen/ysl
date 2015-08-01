using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class AudioRepository : IAudioRepository
	{
		private yslDataContext db;
		public AudioRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Audio> getAllAudio()
		{
			return this.db.Audios.ToList<Audio>();
		}
		public List<Audio> getAllAudioForAccount(int accountId)
		{
			return (
				from a in this.db.Audios
				where a.AccountId == accountId
				select a).ToList<Audio>();
		}
		public Audio getAudio(int audioId)
		{
			Audio result;
			try
			{
				Audio audio = this.db.Audios.Single((Audio a) => a.AudioId == audioId);
				result = audio;
			}
			catch
			{
				result = new Audio
				{
					AudioId = -1
				};
			}
			return result;
		}
		public List<Audio> getRecentUploads()
		{
			return (
				from a in this.db.Audios
				orderby a.Uploaded descending
				select a).Take(20).ToList<Audio>();
		}
		public int addAudio(Audio audio)
		{
			this.db.Audios.InsertOnSubmit(audio);
			this.db.SubmitChanges();
			return audio.AccountId;
		}
		public bool updateAudio(Audio audioAdded)
		{
			Audio audio = this.db.Audios.Single((Audio a) => a.AudioId == audioAdded.AudioId);
			if (audio.AudioId > 0)
			{
				audio.Title = audioAdded.Title;
				audio.Description = audioAdded.Description;
				audio.Location = audioAdded.Location;
				audio.Updated = new DateTime?(DateTime.Now);
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
		public AudioModel convertAudioToModel(Audio item)
		{
			return new AudioModel
			{
				AudioId = item.AudioId,
				Description = item.Description,
				Location = item.Location,
				Title = item.Title
			};
		}
	}
}
