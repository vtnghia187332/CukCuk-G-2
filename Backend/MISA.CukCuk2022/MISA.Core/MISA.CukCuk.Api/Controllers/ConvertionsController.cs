using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;

namespace MISA.CukCuk.Api.Controllers
{
    public class ConvertionsController : MISABaseController<Convertion>
    {
        #region DECLARE
        IConvertionService _convertionService;
        IConvertionRepository _convertionRepository;
        #endregion

        #region Constructor
        public ConvertionsController(IConvertionService convertionService, IConvertionRepository convertionRepository) : base(convertionService, convertionRepository)
        {
            _convertionService = convertionService;
            _convertionRepository = convertionRepository;
        }
        #endregion

        /// <summary>
        /// Lấy danh sách bộ chuyển đổi đơn vị theo danh mục Nguyên Vật liệu
        /// </summary>
        /// <param name="materialId">danh mục Nguyên Vật liệu</param>
        /// <returns>danh sách bộ chuyển đổi đơn vị</returns>
        /// created by: VTNghia - MF1108
        [HttpGet("ConvertionsOfMaterial/{materialId}")]
        public IActionResult GetByMaterialId(Guid materialId)
        {
            try
            {
                var datas = _convertionRepository.getByMaterialId(materialId);
                return Ok(datas);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm danh sách bộ chuyển đổi đơn vị theo danh mục Nguyên Vật liệu
        /// </summary>
        /// <param name="materialId">danh mục Nguyên Vật liệu</param>
        /// <returns>danh sách bộ chuyển đổi đơn vị</returns>
        ///  created by: VTNghia - MF1108
        [HttpPost("ConvertionsOfMaterial/{materialId}")]
        public IActionResult InsertByMaterialId(List<Convertion> convertions,Guid materialId)
        {
            try
            {
                var data = _convertionRepository.InsertByMaterialId(convertions,materialId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa danh sách bộ chuyển đổi đơn vị theo danh mục Nguyên Vật liệu
        /// </summary>
        /// <param name="materialId">danh mục Nguyên Vật liệu</param>
        /// <returns>danh sách bộ chuyển đổi đơn vị</returns>
        ///  created by: VTNghia - MF1108
        [HttpPut("ConvertionsOfMaterial/{materialId}")]
        public IActionResult UpdateByMaterialId(List<Convertion> convertions, Guid materialId)
        {
            try
            {
                var data = _convertionRepository.UpdateByMaterialId(convertions, materialId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }



        /// <summary>
        /// Hàm xử lý Exception
        /// </summary>
        /// <returns>400 - Lỗi từ Client / 500 - Lỗi Server</returns>
        ///  created by: VTNghia - MF1108
        private IActionResult HandleException(Exception ex)
        {
            if (ex is MISAException)
            {
                var result = new MISAServiceResult
                {
                    UserMsg = MISAResource.VN_ErrorExcptionMsg,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                };

                return StatusCode(400, result);
            }
            else
            {
                var result = new MISAServiceResult
                {
                    UserMsg = MISAResource.VN_ErrorExcptionMsg,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                };
                return StatusCode(500, result);
            }

        }
    }
}
