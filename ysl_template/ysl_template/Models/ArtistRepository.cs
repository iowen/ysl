using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{

    public class ArtistModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<MemberModel> Members { get; set; }
        public PhotoModel Photo { get; set; }
        public string Bio { get; set; }
    }
	public class ArtistRepository : IArtistRepository
	{
		private yslDataContext db;
		public ArtistRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Artist> getAllArtist()
		{
			List<Artist> result;
			try
			{
				List<Artist> list = this.db.Artists.ToList<Artist>();
				result = list;
			}
			catch (Exception)
			{
				result = new List<Artist>();
			}
			return result;
		}
        public List<ArtistMenuDataModel> updateArtistList(List<ArtistMenuDataModel> artists)
        {
            List<ArtistMenuDataModel> result;
            try
            {
                int count = this.db.Artists.Count() - artists.Count;
                count = (count < 0) ? count = count/-1 : count; 
                int countInList = this.db.Artists.Count(a => artists.Any(p => p.artistId == a.ArtistId));
                if (count == 0 && countInList == artists.Count)
                    result = artists;
                else
                {
                    List<Artist> arts = this.db.Artists.ToList();
                    result = artists.FindAll(a => arts.Any(p => p.ArtistId == a.artistId));
                    var arrts = arts.FindAll(a => artists.Any(p => p.artistId != a.ArtistId)); 
                    foreach (Artist current in arrts)
                    {
                        result.Add(new ArtistMenuDataModel
                        {
                            artistId = current.ArtistId,
                            name = current.Name,
                            photo = current.Photo
                        });
                    }
                }
            }
            catch (Exception)
            {
                result = artists;
            }
            return result;
        }
		public Artist getArtist(int artistId)
		{
			Artist result;
			try
			{
				Artist artist = this.db.Artists.Single((Artist a) => a.ArtistId == artistId);
				result = artist;
			}
			catch (Exception)
			{
				result = new Artist();
			}
			return result;
		}
		public Artist getArtist(string artistName)
		{
			Artist result;
			try
			{
				Artist artist = this.db.Artists.Single((Artist a) => a.Name == artistName);
				result = artist;
			}
			catch (Exception)
			{
				result = new Artist();
			}
			return result;
		}
		public int addArtist(Artist artist)
		{
			int result;
			try
			{
				this.db.Artists.InsertOnSubmit(artist);
				this.db.SubmitChanges();
				result = artist.ArtistId;
			}
			catch (Exception)
			{
				result = -1;
			}
			return result;
		}
		public bool updateArtist(Artist artist)
		{
			throw new NotImplementedException();
		}
        public ArtistModel ConvertToModel(Artist artist)
        {
            MemberRepository mRepo = new MemberRepository(new yslDataContext());
            PhotoRepository pRepo = new PhotoRepository(new yslDataContext());
            ArtistModel result = new ArtistModel();
            result.ArtistId = artist.ArtistId;
            result.Bio = artist.Bio;
            result.Name = artist.Name;
            result.Photo = pRepo.getPhotoAsModel(artist.Photo);
            result.Members = new List<MemberModel>();
            foreach (var m in artist.ArtistMembers)
            {
                var mem = mRepo.ConvertToModel(m.Member);
                result.Members.Add(mem);
            }
            return result;
        }
	}
}
