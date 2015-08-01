using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IArtistRepository
	{
		List<Artist> getAllArtist();
		Artist getArtist(int artistId);
		Artist getArtist(string artistName);
		int addArtist(Artist artist);
		bool updateArtist(Artist artist);
	}
}
