using Communication.Business.Abstractions.StorageServices;
using Communication.Entity.DTO.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Concretes.StorageServices
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string storageName { get => _storage.GetType().Name; }

        public async Task deleteAsync(string path)
        {
            await _storage.deleteAsync(path);
        }

        public DirectoryInfo dir(string path)
        {
            return _storage.dir(path);
        }

        public List<string> getFiles(string pathOrContainer)
        {
            return _storage.getFiles(pathOrContainer);
        }

        public bool hasFile(string pathOrContainer, string fileName)
        {
            return _storage.hasFile(pathOrContainer, fileName);
        }

        public async Task<UploadFileDTO?> mixTwoAudioFiles(string filePath1, string filePath2, string pathOrContainer)
        {
            return await _storage.mixTwoAudioFiles(filePath1, filePath2, pathOrContainer);
        }

        public async Task<UploadFileDTO?> uploadAsync(string pathOrContainer, IFormFile file)
        {
            return await _storage.uploadAsync(pathOrContainer, file);
        }

        public async Task<UploadFileDTO?> uploadWithBytesAsync(string pathOrContainer, byte[] bytes)
        {
            return await _storage.uploadWithBytesAsync(pathOrContainer, bytes);
        }
    }
}
