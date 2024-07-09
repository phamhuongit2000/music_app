using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LoveMusic.Service
{
    public class FilesService
    {
        private readonly Cloudinary _cloudinary;

        public FilesService(IConfiguration configuration)
        {
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            var account = new Account(
                cloudinarySettings["CloudName"],
                cloudinarySettings["ApiKey"],
                cloudinarySettings["ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImgToCloudinary(IFormFile imgFile, string folderPath)
        {
            if (imgFile == null || imgFile.Length == 0)
            {
                throw new ArgumentException("Avatar file is missing or empty");
            }

            using (var stream = imgFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imgFile.FileName, stream),
                    Folder = folderPath
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString();
            }
        }

        public async Task<string> UploadAudioToCloudinary(IFormFile audioFile, string folderPath)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                throw new ArgumentException("Avatar file is missing or empty");
            }

            using (var stream = audioFile.OpenReadStream())
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(audioFile.FileName, stream),
                    Folder = folderPath
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString();
            }
        }

        public async Task<string> UploadVideoToCloudinary(IFormFile videoFile, string folderPath)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                throw new ArgumentException("Avatar file is missing or empty");
            }

            using (var stream = videoFile.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(videoFile.FileName, stream),
                    Folder = folderPath
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString();
            }
        }
    }
}
