using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IConvertionRepository : IBaseRepository<Convertion>
    {
        /// <summary>
        /// Lấy các đơn vị chuyển đổi theo MaterialId
        /// </summary>
        /// <param name="materialId">MaterialId</param>
        /// <returns>Danh sách các đơn vị chuyển đổi</returns>
         IEnumerable<Convertion> getByMaterialId(Guid materialId);
        /// <summary>
        /// Thêm danh sách bản ghi đơn vị chuyển đổi
        /// </summary>
        /// <param name="convertion">đối tượng "đơn vị chuyển đổi"</param>
        /// <param name="materialId">Danh mục bảng Material</param>
        /// <returns>Số dòng bị thay đổi trong Database</returns>
        /// created by: VTNghia - MF1108
        int InsertByMaterialId(List<Convertion> convertions , Guid materialId);

        /// <summary>
        /// Sửa danh sách bản ghi đơn vị chuyển đổi
        /// </summary>
        /// <param name="convertion">Đối tượng sửa đổi</param>
        /// <param name="ConvertionId">Danh mục bảng Convertion</param>
        /// <returns>Số dòng bị thay đổi trong Database</returns>
        /// created by: VTNghia - MF1108
        int UpdateByMaterialId(List<Convertion> convertions , Guid ConvertionId);

    }
}
