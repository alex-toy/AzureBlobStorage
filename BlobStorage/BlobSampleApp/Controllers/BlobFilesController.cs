using BlobStorageExample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobSampleApp.Controllers
{
    public class BlobFilesController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobFilesController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<string> files = await _blobService.GetAll("mycontainer");
            return View(files);
        }
    }
}
