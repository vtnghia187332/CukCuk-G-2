using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        /// <summary>
        /// Tìm kiếm trong CSDL theo tên của ĐVT
        /// </summary>
        /// <param name="entityName">Tên gọi</param>
        /// <returns>Đối tượng tìm kiếm</returns>
        Unit FindByUnitName(string entityName);

        /// <summary>
        /// Thêm mới ĐVT
        /// </summary>
        /// <param name="unitName">Tên ĐVT</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        int InsertNewData(string unitName);
    }
}
