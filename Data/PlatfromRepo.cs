using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(platform plat)
        {

            if (plat == null)
            {
                throw new ArgumentNullException();
            }
            _context.platforms.Add(plat);
        }

        public IEnumerable<platform> GetAllPlatforms()
        {
            return _context.platforms.ToList();
        }

        public platform GetPlatformbyId(int Id)
        {
            return _context.platforms.Where(p => p.Id == Id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }

}