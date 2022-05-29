using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.Core.Services
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        #region DECLARE
        IMaterialRepository _materialRepository;
        IConvertionRepository _convertionRepository;
        #endregion
        #region Constructor
        public MaterialService(IMaterialRepository materialRepository, IConvertionRepository convertionRepository) : base(materialRepository)
        {
            _materialRepository = materialRepository;
            _convertionRepository = convertionRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNGHIA - MF1108
        public object FilterMaterial(int? pageNumber, int? pageSize, List<FilterOption>? filters)
        {
            var errorMsg = new Dictionary<string, string>();

            //Xử lý filter :
            if (pageNumber < 0)
            {
                pageNumber = 1;
            }
            if (pageSize < 0)
            {
                pageSize = 10;
            }
            // tồn tại lỗi -> gửi lỗi lên controller API
            if (errorMsg.Count > 0)
            {
                throw new MISAException(MISAResource.VN_NotValidInput, errorMsg);
            }
            // sau khi validate dữ liệu đầu vào > gửi cho repo
            var res = _materialRepository.FilterMaterial(pageNumber, pageSize, filters);
            return res;
        }


        /// <summary>
        /// Thực hiện đọc tệp dữ liệu và xử lý nghiệp vụ nhập khẩu danh sách nguyên vật liệu
        /// </summary>
        /// <param name="formFile">Tệp chứa thông tin nguyên vật liệu</param>
        /// <returns>Danh sách nguyên vật liệu kèm theo trạng thái chi tiết kết quả nhập khẩu</returns>
        public async Task<List<Material>> Import(IFormFile formFile)
        {
            //Thực hiện validate dữ liệu 
            if (formFile == null || formFile.Length <= 0)
            {
                throw new MISAException("Tệp dữ liệu trống! Vui lòng kiểm tra lại");
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new MISAException("Tệp dữ liệu phải có định dạng là .xls hoặc .xlsx");
            }
            //1. Đọc dữ liệu có trong tệp

            var materials = new List<Material>();
            //Danh sách nhân viên hợp lệ
            var materialsValid = new List<Material>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    //2. Thực hiện build thành danh sách nguyên vật liệu với các dữ liệu đã đọc được trong tệp
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var material = new Material();
                        //Xử lý thông tin các trường dữ liệu

                        //Thêm Id mới cho từng đối tượng
                        material.MaterialId = Guid.NewGuid();

                        material.MaterialCode = ProcessValueToString(worksheet.Cells[row, 1].Value);
                        material.MaterialName = ProcessValueToString(worksheet.Cells[row, 2].Value);
                        material.MaterialFeature = ProcessValueToString(worksheet.Cells[row, 3].Value);
                        material.GroupMaterial = ProcessValueToString(worksheet.Cells[row, 4].Value);
                        material.MaterialNote = ProcessValueToString(worksheet.Cells[row, 5].Value);

                        //Thực hiện validate dữ liệu
                        _error = new Dictionary<string, string>();
                        base.ValidateObject(material);
                        if (_error.Count() > 0)
                        {
                            //Nếu xảy ra lỗi, thêm vào biến hiển thị lỗi
                            material.IsValid = false;
                            material.ErrorValidateNotValid = _error;
                        }
                        else
                        {
                            materialsValid.Add(material);
                        }

                        materials.Add(material);
                    }
                }
                //Thực hiện thêm danh sách nhân viên vào database
                //var materialImporteds = _materialRepository.Import(materialsValid);
                //return materialImporteds.ToList();
                return materials;
            }
        }

        private string? ProcessValueToString(object cellValue)
        {
            if (cellValue != null)
            {
                return cellValue.ToString();
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        public int InsertMaterialWithConvertions(Material material, List<Convertion>? convertions)
        {
            //Tạo ra Guid mới -> bind vào material
            var newMaterialId = Guid.NewGuid();
            material.MaterialId = newMaterialId;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //Thêm mới Material
                    var res = base.InsertService(material);

                    if (convertions != null && convertions.Count() > 0)
                    {
                        foreach (var convertion in convertions)
                        {
                            //Gán MaterialId vào trong convertion
                            if (newMaterialId != Guid.Empty)
                            {
                                convertion.MaterialId = newMaterialId;
                            }
                        }
                        //Thực hiện thêm mới convertions
                        _convertionRepository.InsertByMaterialId(convertions, newMaterialId);
                    }
                    scope.Complete();
                    //trả về data
                    return res;
                }
                catch (Exception e)
                {
                    throw;
                }


            }
        }


        #endregion
    }
}
