using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<T> : ControllerBase
    {
        #region DECLARE
        IBaseService<T> _ibaseService;
        IBaseRepository<T> _ibaseRepository;
        #endregion

        #region Constructor
        public MISABaseController(IBaseService<T> ibaseService, IBaseRepository<T> ibaseRepository)
        {
            _ibaseService = ibaseService;
            _ibaseRepository = ibaseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả đối tượng trong DB
        /// </summary>
        /// <returns>Danh sách các đối tượng</returns>
        /// created by: VTNghia - MF1108
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _ibaseRepository.Get();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi thực thể mới vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>số dòng trong DB bị thay đổi</returns>
        [HttpPost]
        public IActionResult Post(T entity)
        {
            try
            {
                var res = _ibaseService.InsertService(entity);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Sửa thực thể trong DB
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>số dòng bị thay đổi trong DB</returns>
        [HttpPut("{entityId}")]
        public IActionResult Put(T entity, Guid entityId)
        {
            try
            {
                var res = _ibaseService.UpdateService(entity, entityId);
                return Ok(res);
            }
            catch (MISAException ex)
            {
                ///Ghi log vào hệ thống
                ///
                var result = new MISAServiceResult
                {
                    UserMsg = MISAResource.VN_ErrorExcptionMsg,
                    DevMsg = ex.Message,
                    Data = ex.Data,
                };

                return StatusCode(400, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa thực thể
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>số dòng bị thay đổi trong DB</returns>
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _ibaseRepository.Delete(entityId);
                if (res > 0)
                {
                    return Ok(res);
                }
                else
                    return NotFound();
            }
            catch (MISAException ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Lấy thực thể bằng Id
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Thực thể tương ứng với Id</returns>
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var res = _ibaseRepository.GetById(entityId);
                return Ok(res);
            }
            catch (MISAException ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy mã mới cho thực thể
        /// </summary>
        /// <returns>Mã mới của thực thể</returns>
        [HttpGet("GetNewCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var res = _ibaseRepository.GetNewCode();
                return Ok(res);
            }
            catch (MISAException ex)
            {
                return HandleException(ex);
            }
        }




        /// <summary>
        /// Hàm xử lý Exception
        /// </summary>
        /// <returns>400 - Lỗi từ Client / 500 - Lỗi Server</returns>
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

        #endregion

    }
}
