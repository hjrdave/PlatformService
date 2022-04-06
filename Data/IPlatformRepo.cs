using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        IEnumerable<platform> GetAllPlatforms();
        platform GetPlatformbyId(int Id);
        void CreatePlatform(platform plat);

    }
}