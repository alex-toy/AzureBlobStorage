using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobStorageExample.Services
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);
        Task UploadFileBlobAsync(string filePath, string fileName);
        Task UploadContentBlobAsync(string filePath, string fileName);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task DeleteBlobAsync(string BlobName);
    }
}