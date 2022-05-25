using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces;

namespace MISA.CukCuk.Api.Controllers
{
   
    public class UnitsController : MISABaseController<Unit>
    {
        #region DECLARE
        IUnitService _unitService;
        IUnitRepository _unitRepository;
        #endregion

        #region Constructor
        public UnitsController(IUnitService unitService, IUnitRepository unitRepository) : base(unitService, unitRepository)
        {
            _unitService = unitService;
            _unitRepository = unitRepository;
        }
        #endregion
    }
}
