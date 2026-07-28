namespace lab5.Services
{
    public class PhotoServices : IPhotoServices
    {
        public string convertImgToPath(IFormFile image)
        {
          if(image == null)
                return "/uploads/default.png";
            //create path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", Guid.NewGuid().ToString() + "_" + image.FileName);
            // copy to harddisk
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            // return path
            return "/uploads/" + Path.GetFileName(filePath);

        }
    }
}
