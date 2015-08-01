using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IPhotoRepository
	{
		List<Photo> getAllPhotos();
		List<Photo> getAllPhotosForAccount(int accountId);
		Photo getPhoto(int photoId);
		List<Photo> getRecentUploads();
		List<Photo> SearchPhotos(string term);
		PhotoModel getPhotoAsModel(Photo item);
		int addPhoto(Photo photo);
		bool updatePhoto(Photo photo);
	}
}
