using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Số dòng trong DB bị ảnh hưởng</returns>
        /// created by: VTNghia - MF1108
        public int InsertService(T entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Id của đối tượng</param>
        /// <param name="entityId">Đối tượng</param>
        /// <returns>Số dòng trong DB bị ảnh hưởng</returns>
        ///  created by: VTNghia - MF1108
        public int UpdateService(T entity, Guid entityId);

    }
}
