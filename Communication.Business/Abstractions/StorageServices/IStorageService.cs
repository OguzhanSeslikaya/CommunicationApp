using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.StorageServices
{
    public interface IStorageService : IStorage
    {
        public string storageName { get; }
    }
}
