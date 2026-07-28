using lab5.Models;

namespace lab5.Services
{
    public interface IDBServices
    {
        public Task<int> saveCV(CVBindingModel cvCMD);
        public Task <ViewProperty> getCVSummary(int cvId);
    }
}
