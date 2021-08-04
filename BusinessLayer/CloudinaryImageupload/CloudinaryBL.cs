using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BusinessLayer.CloudinaryImageupload
{
    public class CloudinaryBL
    {
        static Cloudinary cloudinary;
        static string Folder = "BookStore";

        public CloudinaryBL(IConfiguration config)
        {
            Account myAccount = new Account { ApiKey = config["Cloudinary:ApiKey"],
                ApiSecret = config["Cloudinary:ApiSecret"], Cloud = config["Cloudinary:Cloud"]
            };
            cloudinary = new Cloudinary(myAccount);
        }
        
        public string UploadImage(string BookName, IFormFile BookCover)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(BookName, BookCover.OpenReadStream()),
                PublicId = BookName,
                UseFilename = true,
                UniqueFilename = true,
                Folder = Folder,
                Overwrite = true
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var result = uploadResult.Url.AbsoluteUri;
            return result;
        }
        public bool RenameImage(string imgName, long newName)
        {
            var renameParams = new RenameParams(imgName, newName.ToString());

            var renameResult = cloudinary.Rename(renameParams);
            return renameResult.Error == null;
        }

      /*  public IFormFile GetImage(long BookID)
        {
            var result = cloudinary.Lis
            Stream stream;
        }*/
    }
}
