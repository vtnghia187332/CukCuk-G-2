using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class StorageService : BaseService<Storage>, IStorageService
    {
        IStorageRepository _storageRepository;
        public StorageService(IStorageRepository storageRepository) : base(storageRepository)
        {
            _storageRepository = storageRepository;
        }
    }
}
