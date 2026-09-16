using lab5.Data;
using lab5.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<ViewProperty> getCVSummary(Guid token)
        {
            var cv= await _context.Cvs.Where(x => x.PublicToken == token).SingleOrDefaultAsync();
            var viewprop = new ViewProperty
            {
                FirstName= cv.FirstName,
                LastName=cv.LastName,
                Birthday=cv.Birthday,
                Email= cv.Email,
                Gender= cv.Gender,
                Id= cv.Id,
                PublicToken= cv.PublicToken,
                Nationalities=cv.Nationalities,
                photoPath= cv.PhotoPath,
                Skills=cv.Skills,
            };
            return viewprop;
        }

        public async Task<List<ViewProperty>> getAllCVs(string? search)
        {
            var query = _context.Cvs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search));
            }

            var cvs = await query
                .OrderByDescending(x => x.Id)
                .Select(cv => new ViewProperty
                {
                    Id = cv.Id,
                    PublicToken = cv.PublicToken,
                    FirstName = cv.FirstName,
                    LastName = cv.LastName,
                    Birthday = cv.Birthday,
                    Email = cv.Email,
                    Gender = cv.Gender,
                    Nationalities = cv.Nationalities,
                    Skills = cv.Skills,
                    photoPath = cv.PhotoPath
                })
                .ToListAsync();

            return cvs;
        }

        public async Task<Guid> saveCV(CVBindingModel cmd)
        {
            var cv = new CV
            {
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                Birthday = cmd.Birthday,
                Email = cmd.Email,
                Password = HashPassword(cmd.Password),
                Nationalities = cmd.Nationalities,
                Gender = cmd.Gender,
                Skills = cmd.Skills,
                PhotoPath = _photoServices.convertImgToPath(cmd.Image),
            };
            _context.Add(cv);              // ④ tells EF Core to track this as "Added"
            await _context.SaveChangesAsync();  // ⑤ executes INSERT for cv

            return cv.PublicToken;
        }

        // Simple one-way hash so we never store the raw password.
        // For a production app, prefer BCrypt.Net-Next (adds salting automatically):
        //   dotnet add package BCrypt.Net-Next
        //   BCrypt.Net.BCrypt.HashPassword(password)
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}
