using Communication.Business.Abstractions.StorageServices.LocalStorage;
using Communication.Entity.DTO.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Threading.Channels;


namespace Communication.Business.Concretes.StorageServices.LocalStorage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task deleteAsync(string path)
        {
            File.Delete(path);
        }

        public List<string> getFiles(string pathOrContainer)
        {
            DirectoryInfo dir = new DirectoryInfo(pathOrContainer);
            return dir.GetFiles().Select(f => f.Name).ToList();
        }

        public bool hasFile(string pathOrContainer, string fileName)
        {
            return File.Exists($"{pathOrContainer}\\{fileName}");
        }

        public async Task<UploadFileDTO?> uploadWithBytesAsync(string pathOrContainer, byte[] bytes)
        {
            lock (new object())
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string newName = fileRenameAsync(uploadPath, "voice.wav");

                using (FileStream fs = new FileStream(Path.Combine(uploadPath, newName), FileMode.Create))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(bytes);
                }


                return new() { fileName = newName, path = $"{uploadPath}\\{newName}" };
            }


        }
        
        public async Task<UploadFileDTO?> mixTwoAudioFiles(string filePath1,string filePath2,string pathOrContainer)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string newName = fileRenameAsync(uploadPath, "mixedVoice.wav");


            try
            {
                var waveFileReader1 = new WaveFileReader(filePath1);


                var waveFileReader2 = new WaveFileReader(filePath2);

                var convertedWaveStream = new WaveFormatConversionStream(new WaveFormat(waveFileReader2.WaveFormat.SampleRate, waveFileReader2.WaveFormat.BitsPerSample, waveFileReader2.WaveFormat.Channels), waveFileReader1);


                var waveProvider1 = new WaveChannel32(convertedWaveStream);

                var waveProvider2 = new WaveChannel32(waveFileReader2);

                var mixer = new WaveMixerStream32(new[] { waveProvider1, waveProvider2 }, true);

                var waveFileWriter = new WaveFileWriter(Path.Combine(uploadPath, newName), mixer.WaveFormat);
                byte[] buffer = new byte[4096];
                while (mixer.Read(buffer, 0, buffer.Length) > 0)
                {
                    waveFileWriter.Write(buffer, 0, buffer.Length);
                }

                waveFileWriter.Close();
                waveFileReader1.Close();
                waveFileReader2.Close();

                Console.WriteLine("Mixleme tamamlandı. Çıkış dosyası: ");
                return new() { fileName = newName, path = $"{uploadPath}\\{newName}" };

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }


            return null;
        }


        public async Task<UploadFileDTO?> uploadAsync(string pathOrContainer, IFormFile file)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string newName = fileRenameAsync(uploadPath, file.FileName);
            bool kontrol = await copyFileAsync(Path.Combine(uploadPath, newName), file);
            if (kontrol)
            {
                return new() { fileName=newName, path= $"{uploadPath}\\{newName}" };
            }

            return null;
        }

        private string fileRenameAsync(string path, string fileName)
        {

            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            int ek = 0;
            string newName;
            while (true)
            {
                ek++;
                newName = $"{oldName}-{ek}{extension}";
                if (!File.Exists($"{path}\\{newName}"))
                {
                    return newName;
                }
            }
        }

        private async Task<bool> copyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DirectoryInfo dir(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir;
        }
    }
}
