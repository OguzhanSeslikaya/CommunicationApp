using Communication.Entity.DTO.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.StorageServices
{
    public interface IStorage
    {
        Task<UploadFileDTO?> uploadAsync(string pathOrContainer, IFormFile file);
        Task deleteAsync(string path);
        List<string> getFiles(string pathOrContainer);
        bool hasFile(string pathOrContainer, string fileName);
        DirectoryInfo dir(string path);
        Task<UploadFileDTO?> uploadWithBytesAsync(string pathOrContainer, byte[] bytes);
        Task<UploadFileDTO?> mixTwoAudioFiles(string filePath1, string filePath2, string pathOrContainer);
    }
}
