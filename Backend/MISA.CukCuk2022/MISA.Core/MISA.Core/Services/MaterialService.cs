using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.IRepo;
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
        IEPPLusAppService _iEPPLusAppService;

        #endregion
        #region Constructor
        public MaterialService(IMaterialRepository materialRepository, IConvertionRepository convertionRepository, IEPPLusAppService iEPPLusAppService) : base(materialRepository)
        {
            _materialRepository = materialRepository;
            _convertionRepository = convertionRepository;
            _iEPPLusAppService = iEPPLusAppService;
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

            var materialsValid = new List<Material>();

            //Thực hiện đọc file excel và trả về danh sách nguyên vật liệu
            List<Material> materials = await _iEPPLusAppService.ReadFileExcelToGetMaterials(formFile);

            var _errorCount = 0;
            foreach (var material in materials)
            {
                _error = new Dictionary<string, string>();
                //Validate thông tin file excel
                base.ValidateObject(material);
                if (_error.Count() > 0)
                {
                    //Check điều kiện: để trống
                    material.IsValid = false;
                    material.ErrorValidateNotValid = _error;
                    _errorCount++;
                }
                //check code trùng lặp trong file excel
                else if (CheckCodeExistInFile(material.MaterialCode, material.MaterialId, materials))
                //else if (materials.Any(item => item.MaterialCode == material.MaterialCode) && materials.Any(item => item.MaterialId != material.MaterialId))
                {
                    material.IsValid = false;
                    _error.Add("MaterialCode", $"Mã nguyên vật liệu không được phép trùng lặp trong file");
                    material.ErrorValidateNotValid = _error;
                    _errorCount++;
                }
                //Check code trùng lặp trong DB(Nếu không trùng tên trong file excel)
                else if (_materialRepository.CheckCodeExist(material.MaterialCode) == true)
                {
                    material.IsValid = false;
                    _error.Add("MaterialCode", $"Mã nguyên vật liệu đã tồn tại");
                    material.ErrorValidateNotValid = _error;
                    _errorCount++;
                }
                else
                {
                    //Thêm vào danh sách nguyên vật liệu hợp lệ
                    materialsValid.Add(material);
                }
            }


            //Nếu không có lỗi trùng -> Thêm vào CSDL
            //foreach (var materialIsValid in materialsValid)
            //{
            //    _materialRepository.Insert(materialIsValid);
            //}

            //return materialImporteds.ToList();
            return materials;
        }

        /// <summary>
        /// Thêm những nguyên vật liệu hợp lệ xuống CSDL
        /// </summary>
        /// <param name="_mateiralsIsValid">Danh sách các nguyên vật liệu hợp lệ để thêm vào CSDl</param>
        /// <returns>Số dòng bị ảnh hưởng trong CSDL</returns>
        public int InsertMaterialIsVaildToDB(List<Material> _mateiralsIsValid)
        {
            return 1;
        }

        /// <summary>
        /// Kiểm tra giá trị trùng lặp mã code trong File Excel
        /// </summary>
        /// <param name="_materialCodeToCheck">Giá trị mã code để kiểm tra</param>
        /// <param name="_materialsInFile">Danh sách nguyên vật liệu để kiểm tra</param>
        /// <returns>true: Trùng lặp  / False: Không trùng lặp</returns>
        public bool CheckCodeExistInFile(string materialCodeToCheck, Guid materialIdToCheck, List<Material> materialsInFile)
        {
            //return materialsInFile.Select(item => item.MaterialCode).Contains(materialCodeToCheck);
            foreach (var materialToCheck in materialsInFile)
            {
                if (materialToCheck.MaterialCode == materialCodeToCheck && materialToCheck.MaterialId != materialIdToCheck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// commnent
        /// </summary>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        //private string? ProcessValueToString(object cellValue)
        //{
        //    if (cellValue != null)
        //    {
        //        return cellValue.ToString();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


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
                catch (Exception ex)
                {
                    throw new MISAException("Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp");
                }
            }
        }
        /// <summary>
        /// Xử lý sự kiện config thông tin file Excel
        /// </summary>
        /// <param name="list">Danh sách nguyên vật liệu</param>
        /// <param name="stream">Stream</param>
        /// <param name="excelName">Tên file Excel</param>
        /// <returns>thông tin config file</returns>
        public ObjectForExport ConfigFileToExport(List<Material> list, MemoryStream stream, string excelName)
        {
            var _objectForExport = new ObjectForExport();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;

            _objectForExport.excelName = excelName;

            return _objectForExport;
        }

        #endregion
    }
}
