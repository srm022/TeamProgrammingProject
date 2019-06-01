using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System.IO;
using System.Security;

namespace PZProject.Handlers.Utils
{
    public interface ICloudBlobService
    {
        CloudBlockBlob GetBlobForFile(string fileName);
        void CreateBlobForFile(IFormFile file);
    }

    public class CloudBlobService : ICloudBlobService
    {
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly SystemSettings _settings;

        public CloudBlobService(IOptions<SystemSettings> settings)
        {
            _settings = settings.Value;
            _cloudBlobClient = ConfigureBlobClient();
        }

        private CloudBlobClient ConfigureBlobClient()
        {
            var connectionString = _settings.StorageAccountConnectionString;
            if (!CloudStorageAccount.TryParse(connectionString, out var cloudStorageAccount))
                throw new SecurityException("Could not access storage.");

            return cloudStorageAccount.CreateCloudBlobClient();
        }

        public CloudBlockBlob GetBlobForFile(string fileName)
        {
            var blockBlob = GetBlockBlobReference(fileName);

            return blockBlob;
        }

        public void CreateBlobForFile(IFormFile file)
        {
            var container = _cloudBlobClient.GetContainerReference(_settings.StorageAccountBlobContainerName);
            var blockBlob = container.GetBlockBlobReference(file.FileName);

            using (Stream stream = file.OpenReadStream())
            {
                blockBlob.UploadFromStream(stream);
            }
        }

        private CloudBlockBlob GetBlockBlobReference(string fileName)
        {
            var container = _cloudBlobClient.GetContainerReference(_settings.StorageAccountBlobContainerName);
            var blockBlob = container.GetBlockBlobReference(fileName);
            return blockBlob;
        }
    }
}