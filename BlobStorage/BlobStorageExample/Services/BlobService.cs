using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorageExample.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageExample.Services
{
    public class BlobService : IBlobService
    {
        private const string BlobContainerName = "mycontainer";
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            BlobClient blobCient = containerClient.GetBlobClient(name);
            BlobDownloadInfo blobDownloadInfo = await blobCient.DownloadAsync();
            BlobInfo blobInfo = new BlobInfo(blobDownloadInfo.Content, blobDownloadInfo.GetType());
            return blobInfo;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            AsyncPageable<BlobItem> blobItems = containerClient.GetBlobsAsync();
            List<string> items = new List<string>();

            await foreach (var item in blobItems)
            {
                items.Add(item.Name);
            }

            return items;
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            BlobHttpHeaders httpHeaders = new BlobHttpHeaders() { ContentType = filePath.GetContentType() };
            await blobClient.UploadAsync(filePath, httpHeaders);
        }

        public async Task UploadContentBlobAsync(string content, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            await using var memoryStream = new MemoryStream(bytes);
            BlobHttpHeaders httpHeaders = new BlobHttpHeaders() { ContentType = fileName.GetContentType() };
            await blobClient.UploadAsync(memoryStream, httpHeaders);
        }

        public async Task DeleteBlobAsync(string BlobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(BlobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
