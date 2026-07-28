using lab5.Data;
using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Services
{

    public class DBServices : IDBServices
    {

        readonly AppDbContext _context;  // ① injected by DI

        readonly ILogger _logger;
        readonly IPhotoServices _photoServices;
        public DBServices(AppDbContext context, ILoggerFactory factory, IPhotoServices photoServices)
        {
            _context = context;
            _logger = factory.CreateLogger<DBServices>();
            _photoServices= photoServices;
        }

        public async Task<ViewProperty> getCVSummary(int cvId)
        {
            var cv= await _context.Cvs.Where(x => x.Id == cvId).SingleOrDefaultAsync();
            var viewprop = new ViewProperty
            {
                FirstName= cv.FirstName,
                LastName=cv.LastName,
                Birthday=cv.Birthday,
                Email= cv.Email,
                Gender= cv.Gender,
                Id= cv.Id,
                Nationalities=cv.Nationalities,
                photoPath= cv.PhotoPath,
                Skills=cv.Skills,
                


            };
            return viewprop;
        }

        public async Task<int> saveCV(CVBindingModel cmd)
        {
            var cv = new CV
            {
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                Birthday = cmd.Birthday,
                Email = cmd.Email,
                Password = cmd.Password,
                Nationalities = cmd.Nationalities,
                Gender = cmd.Gender,
                Skills = cmd.Skills,
                PhotoPath = _photoServices.convertImgToPath(cmd.Image),
               
               
            };
            _context.Add(cv);              // ④ tells EF Core to track this as "Added"
            await _context.SaveChangesAsync();  // ⑤ executes INSERT for cv

            return cv.Id;
        }
    }
}
