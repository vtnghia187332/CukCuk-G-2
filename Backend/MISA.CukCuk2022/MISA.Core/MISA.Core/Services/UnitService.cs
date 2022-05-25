using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        IUnitRepository _unitRepository;
        public UnitService(IUnitRepository unitRepository) : base(unitRepository)
        {
            _unitRepository = unitRepository;
        }
    }
}
