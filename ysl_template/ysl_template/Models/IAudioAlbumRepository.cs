using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IAudioAlbumRepository
	{
		List<AudioAlbum> getAllAudioAlbums();
		List<AudioAlbum> getAllAudioAlbumsForArtist(int artistId);
		AudioAlbum getAudioAlbum(int audioAlbumId);
		AudioAlbumDataForJSON getLatestSingle(int artistId);
		AudioAlbumDataForJSON getAudioAlbumDataForJSON(int audioAlbumId);
		AudioAlbum GetAccountAudioAlbumByTitle(int account, string title);
		List<Audio> GetAudioForAlbum(int album);
		List<AudioAlbum> getAudioAlbums(int start, int limit);
		List<AudioAlbumData> getAudioAlbumsWithCover(int start, int limit);
		int addAudioAlbum(AudioAlbum a);
		bool updateAudioAlbum(AudioAlbum a);
		bool AccountAudioAlbumExists(int account, string title);
	}
}
