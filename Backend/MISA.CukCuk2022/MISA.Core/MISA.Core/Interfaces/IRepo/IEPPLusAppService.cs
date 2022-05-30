using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.IRepo
{
    public interface IEPPLusAppService
    {
        /// <summary>
        /// Thực hiện đọc file excel và trả về danh sách nguyên vật liệu
        /// </summary>
        /// <param name="formFile">File Excel truyền vào</para
        Task<List<Material>> ReadFileExcelToGetMaterials(IFormFile formFile);
    }
}
