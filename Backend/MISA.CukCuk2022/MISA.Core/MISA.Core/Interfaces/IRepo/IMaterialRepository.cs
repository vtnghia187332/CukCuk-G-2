using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNGHIA - MF1108
         object FilterMaterial(int? pageNumber, int? pageSize, List<FilterOption>? filter);

        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        ///  created by: VTNGHIA - MF1108
        int InsertMaterialWithConvertions(Material material, List<Convertion> convertions);

        /// <summary>
        /// Kiểm tra sự trùng lặp của mã Nguyên vật liệu
        /// </summary>
        /// <param name="materialCode">Mã code Nguyên vật liệu</param>
        /// <returns>true: Mã code bị trùng || false: Mã code không bị trùng</returns>
        ///  created by: VTNGHIA - MF1108
        bool CheckMaterialCode(string materialCode);
    }
}
