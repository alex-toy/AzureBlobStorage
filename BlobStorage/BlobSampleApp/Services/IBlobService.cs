using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobStorageExample.Services
{
    public interface IBlobService
    {
        Task<string> Get(string name, string container);
        Task<IEnumerable<string>> GetAll(string container);
        Task<bool> Upload(string name, IFormFile file, string container);
        Task<bool> Delete(string name, string container);
    }
}