using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.IRepo;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Lib
{
    /// <summary>
    /// class thực hiện logic nghiệp vụ về đề đọc file ghi file sử dụng thư viện EPPLus
    /// </summary>
    public class EPPLusAppService: IEPPLusAppService
    {
        /// <summary>
        /// Thực hiện đọc file excel và trả về danh sách nguyên vật liệu
        /// </summary>
        /// <param name="formFile">File Excel truyền vào</param>
        /// <returns>Danh sách các nguyên vật liệu</returns>
        public async Task<List<Material>> ReadFileExcelToGetMaterials(IFormFile formFile)
        {
            var materials = new List<Material>();
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


                        //Duyệt từng đối tượng(row) trong file excel
                        material.MaterialCode = worksheet.Cells[row, 1].Value?.ToString();
                        material.MaterialName = worksheet.Cells[row, 1].Value?.ToString();
                        material.MaterialFeature = worksheet.Cells[row, 1].Value?.ToString();
                        material.GroupMaterial = worksheet.Cells[row, 1].Value?.ToString();
                        material.MaterialNote = worksheet.Cells[row, 1].Value?.ToString();

                        materials.Add(material);
                    }
                }
                //Danh sách nguyên vật liệu trong file excel
                return materials;
            }
        }
    }
}
