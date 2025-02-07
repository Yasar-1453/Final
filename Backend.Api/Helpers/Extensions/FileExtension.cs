namespace Backend.Api.Helpers.Extensions
{
    public static class FileExtension
    {
        public static string Upload(this IFormFile file, string rootPath, string foldername)
        {
            string filname = file.FileName;
            filname = Guid.NewGuid() + filname;
            string path = Path.Combine(rootPath, foldername, filname);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filname;
        }
    }
}
