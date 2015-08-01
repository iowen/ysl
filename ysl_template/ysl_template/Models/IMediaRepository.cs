using System;
namespace ysl_template.Models
{
	public interface IMediaRepository
	{
		MediaModels.HomeFeaturedModel GetRecentMedia();
	}
}
