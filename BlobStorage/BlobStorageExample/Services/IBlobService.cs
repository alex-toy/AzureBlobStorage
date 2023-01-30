using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobStorageExample.Services
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);
        //Task<IEnumerable<string>> ListBlobAsync(string name);
    }
}