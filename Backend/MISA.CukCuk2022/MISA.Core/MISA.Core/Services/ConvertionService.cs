using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class ConvertionService : BaseService<Convertion>, IConvertionService
    {
        IConvertionRepository _convertionRepository;
        public ConvertionService(IConvertionRepository convertionRepository) : base(convertionRepository)
        {
            _convertionRepository = convertionRepository;
        }
    }
}
