using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;
using OfficeOpenXml;

namespace MISA.CukCuk.Api.Controllers
{
    public class MaterialsController : MISABaseController<Material>
    {
        #region DECLARE
        IMaterialService _materialService;
        IMaterialRepository _materialRepository;
        #endregion

        #region Constructor
        public MaterialsController(IMaterialService materialService, IMaterialRepository materialRepository) : base(materialService, materialRepository)
        {
            _materialService = materialService;
            _materialRepository = materialRepository;
        }
        #endregion

        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNghia - MF1108
        [HttpPut("filter")]
        public IActionResult MaterialFilter([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromBody] List<FilterOption>? filters)
        {
            try
            {
                var entities = _materialService.FilterMaterial(pageNumber, pageSize, filters);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        ///  created by: VTNghia - MF1108
        [HttpPost("MaterialWithConvertions")]
        public IActionResult MaterialWithConvertions(MaterialWithConvertion materialWithConvertion)
        {
            try
            {
                var _material = materialWithConvertion.material;
                var _convertions = materialWithConvertion.convertions;
                var data = _materialService.InsertMaterialWithConvertions(_material, _convertions);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý hàm nhập khẩu nguyên vật liệu ra file Excel
        /// </summary>
        /// <param name="formFile">File excel</param>
        /// <returns>Danh sách các nguyên vật liệu</returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile formFile)
        {
            try
            {
                var res = _materialService.Import(formFile);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý hàm xuất khẩu nguyên vật liệu ra file Excel
        /// </summary>
        /// <param name="materialsExport">Danh sách nguyên vật liệu xuất ra file excel</param>
        /// <returns>File Excel</returns>
        [HttpPost("Export")]
        public async Task<IActionResult> Export([FromBody] List<Material>? materialsExport)
        {
            var list = new List<Material>();
            //Kiểm tra điều kiện danh sách Nguyên vật liệu muốn xuất file Excel(khác null)
            if (materialsExport.Count() > 0)
            {
                list = materialsExport.ToList();
            }
            else
            {
                // query data from database  
                await Task.Yield();
                //Lấy danh sách nguyên vật liệu dưới DB
                list = (List<Material>)_materialRepository.Get();
            }
            //Setup những trường sẽ được sẽ được xuất khẩu: 
            var _objectForExport = new ObjectForExport();
            var stream = new MemoryStream();
           
            //Khai báo thông tin file excel
            string excelName = $"Nguyen-vat-lieu-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //config file excel
            _objectForExport = _materialService.ConfigFileToExport(list, stream, excelName);

            //trả về file excel
            return File(stream, "application/octet-stream", _objectForExport.excelName);
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
