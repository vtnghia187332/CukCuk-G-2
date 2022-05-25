using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces;

namespace MISA.CukCuk.Api.Controllers
{
    public class StoragesController : MISABaseController<Storage>
    {
        #region DECLARE
        IStorageService _storageService;
        IStorageRepository _storageRepository;
        #endregion

        #region Constructor
        public StoragesController(IStorageService storageService, IStorageRepository storageRepository) : base(storageService, storageRepository)
        {
            _storageService = storageService;
            _storageRepository = storageRepository;
        }
        #endregion
    }
}
