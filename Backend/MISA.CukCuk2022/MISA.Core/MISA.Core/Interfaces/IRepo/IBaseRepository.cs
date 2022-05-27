using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy tất cả đối tượng trong DB
        /// </summary>
        /// <returns>Danh sách các đối tượng</returns>
        /// created by: VTNghia - MF1108
        public IEnumerable<T> Get();

        /// <summary>
        /// Thêm mới  đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số dòng thay đổi trong DB</returns>
        /// created by: VTNghia - MF1108
        public int Insert(T entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>số dòng bị ảnh hưởng trong bản ghi</returns>
        /// created by: VTNghia - MF1108
        public int Update(T entity, Guid entityId);

        /// <summary>
        /// Xóa bản ghi trong đối tượng
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        /// created by: VTNghia - MF1108
        public int Delete(Guid entityId);

        /// <summary>
        /// Lấy 1 bản ghi đối tượng bằng Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Đối tượng cụ thể</returns>
        /// created by: VTNghia - MF1108
        public T GetById(Guid entityId);

        /// <summary>
        /// Kiểm tra mã có bị lặp hay khônng?
        /// </summary>
        /// <param name="entityCode">Mã của thực thể</param>
        /// <returns>true - bị lặp / false: không bị lặp</returns>
        /// created by: VTNghia - MF1108
        public bool CheckCodeExist(string entityCode);

        /// <summary>
        /// Tạo mới mã đối tượng mới
        /// </summary>
        /// <returns> Mã mới</returns>
        /// created by: VTNghia - MF1108
        public string GetNewCode();


        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="entityIds">Danh sách Id của các đối tượng</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        public int DeleteMulti(List<Guid> entityIds);




    }
}
