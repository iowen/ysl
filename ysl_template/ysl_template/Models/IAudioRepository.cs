using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IAudioRepository
	{
		List<Audio> getAllAudio();
		List<Audio> getAllAudioForAccount(int accountId);
		Audio getAudio(int audioId);
		List<Audio> getRecentUploads();
		AudioModel convertAudioToModel(Audio item);
		int addAudio(Audio audio);
		bool updateAudio(Audio audio);
	}
}
