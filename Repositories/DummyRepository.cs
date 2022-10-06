using UdemyWEBAPI.Interfaces;

namespace UdemyWEBAPI.Repositories
{
    public class DummyRepository : IDummyRepository
    {
        public string GetName()
        {
            return "Abdullah";
        }
    }
}
