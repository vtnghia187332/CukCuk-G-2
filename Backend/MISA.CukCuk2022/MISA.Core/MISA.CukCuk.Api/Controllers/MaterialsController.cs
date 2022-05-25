﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;

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
                var entities = _materialService.FilterMaterial(pageNumber, pageSize, filters );
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
