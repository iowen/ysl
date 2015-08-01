using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IVideoRepository
	{
		List<Video> getAllVideos();
		List<Video> getAllVideosForAccount(int accountId);
		Video getVideo(int videoId);
		List<Video> getRecentUploads();
		int addVideo(int account, string title, string description, string location);
		bool updateVideo(int id, string title, string description, string locaiton);
	}
}
