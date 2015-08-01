using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IAudioAlbumItemRepository
	{
		List<AudioAlbumItem> getAllAudioAlbumItems();
		List<AudioAlbumItem> getAllAudioAlbumsForAudio(int audioId);
		List<AudioAlbumItem> getAllAudioAlbumItemsForAlbum(int albumId);
		AudioAlbumItem getAudioAlbumItem(int audioAlbumItemId);
		AudioAlbumItemModel getAudioAlbumItemAsModel(AudioAlbumItem item);
		List<AudioAlbumItemModel> getAudioAlbumItemAsModel(List<AudioAlbumItem> items);
		int addAudioAlbumItem(AudioAlbumItem audioAlbumItem);
		bool updateAudioAlbumItem(AudioAlbumItem item);
		bool deleteAudioAlbumItem(AudioAlbumItem item);
	}
}
