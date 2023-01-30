using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobStorageExample.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> Delete(string name, string container)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(name);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<string>> GetAll(string container)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();
            List<string> files = new List<string>();
            await foreach(var blob in blobs) files.Add(blob.Name);

            return files;
        }

        public async Task<string> Get(string name, string container)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> Upload(string name, IFormFile file, string container)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(name);
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType,
            };

            Response<BlobContentInfo> res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            return res != null;
        }

        //public async Task<BlobInfo> GetBlobAsync(string name)
        //{
        //    BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("mycontainer");
        //    BlobClient blobCient = containerClient.GetBlobClient(name);
        //    BlobDownloadInfo blobDownloadInfo = await blobCient.DownloadAsync();
        //    BlobInfo blobInfo = new BlobInfo();
        //    return blobInfo;
        //}
    }
}
