using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("mycontainer");
            BlobClient blobCient = containerClient.GetBlobClient(name);
            BlobDownloadInfo blobDownloadInfo = await blobCient.DownloadAsync();
            BlobInfo blobInfo = new BlobInfo(blobDownloadInfo.Content, blobDownloadInfo.GetType());
            return blobInfo;
        }

        //public Task<IEnumerable<string>> ListBlobAsync(string name)
        //{

        //}
    }
}
