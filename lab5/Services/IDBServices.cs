using lab5.Models;

namespace lab5.Services
{
    public interface IDBServices
    {
        public Task<Guid> saveCV(CVBindingModel cvCMD);
        public Task<ViewProperty> getCVSummary(Guid token);
        public Task<List<ViewProperty>> getAllCVs(string? search);
    }
}
