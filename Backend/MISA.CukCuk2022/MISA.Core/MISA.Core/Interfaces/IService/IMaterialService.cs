using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IMaterialService : IBaseService<Material>
    {
        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNghia - MF1108
        public object FilterMaterial(int? pageNumber, int? pageSize, List<FilterOption>? filter);


        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        ///  created by: VTNghia - MF1108
        int InsertMaterialWithConvertions(Material material, List<Convertion>? convertions);

        /// <summary>
        /// Thực hiện đọc tệp dữ liệu và xử lý nghiệp vụ nhập khẩu danh sách nguyên vật liệu
        /// </summary>
        /// <param name="formFile">Tệp chứa thông tin nguyên vật liệu</param>
        /// <returns>Danh sách nguyên vật liệu kèm theo trạng thái chi tiết kết quả nhập khẩu</returns>
        Task<List<Material>> Import(IFormFile formFile);
    }
}
