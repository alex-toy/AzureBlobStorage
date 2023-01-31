using BlobStorageExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlobSampleApp.Controllers
{
    public class BlobFilesController : Controller
    {
        private const string ContainerName = "mycontainer";
        private readonly IBlobService _blobService;

        public BlobFilesController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<string> files = await _blobService.GetAll(ContainerName);
            return View(files);
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            bool res = await _blobService.Upload(fileName, file, ContainerName);

            if (res) return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string name)
        {
            string files = await _blobService.Get(name, ContainerName);
            return Redirect(files);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            await _blobService.Delete(name, ContainerName);
            return RedirectToAction("Index");
        }
    }
}
