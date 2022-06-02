using Dapper;
using MISA.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
    {

        #region DECLARE
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        #endregion

        #region Constructor

        public BaseRepository()
        {
            _connectionString = "Host=localhost; Port=3306; Database=misacukcukmaterial2022_dev; User Id = root; Password=12345678";
            _sqlConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Kiểm tra mã có bị lặp hay khônng?
        /// </summary>
        /// <param name="entityCode">Mã của thực thể</param>
        /// <returns>true - bị lặp / false: không bị lặp</returns>
        /// created by: VTNghia - MF1108
        public bool CheckCodeExist(string entityCode)
        {
            //Khai báo biến, tên bảng
            var className = typeof(T).Name;
            var parameters = new DynamicParameters();

            //Dùng câu lệnh query
            var sqlCommand = $"SELECT * FROM ({className}) WHERE ({className}Code) = @entityCode";
            parameters.Add("@entityCode", entityCode);

            //Gọi hàm query
            var res = _sqlConnection.QueryFirstOrDefault(sqlCommand, param: parameters);
            if (res == null)
            {
                //Không bị trùng Code
                return false;
            }
            
            return true;
        }
        /// <summary>
        /// Xóa bản ghi trong đối tượng
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        /// created by: VTNghia - MF1108
        public int Delete(Guid entityId)
        {
            //Khai báo biến, tên bảng
            var className = typeof(T).Name;
            var parameters = new DynamicParameters();

            parameters.Add($"@{className}Id", entityId);
            var rowsEffect = _sqlConnection.Execute($"Proc_Delete{className}ById", param: parameters, commandType: CommandType.StoredProcedure);

            return rowsEffect;
        }
        /// <summary>
        /// Lấy tất cả đối tượng trong DB
        /// </summary>
        /// <returns>Danh sách các đối tượng</returns>
        /// created by: VTNghia - MF1108
        public IEnumerable<T> Get()
        {
            //Khai báo tên ClassName
            var className = typeof(T).Name;
            //3. Thực hiện lấy dữ liệu Dapper
            var entities = _sqlConnection.Query<T>($"Proc_Get{className}s", commandType: CommandType.StoredProcedure);
            // Trả về kết quả
            return entities.AsList();
        }

        /// <summary>
        /// Lấy 1 bản ghi đối tượng bằng Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Đối tượng cụ thể</returns>
        /// created by: VTNghia - MF1108
        public T GetById(Guid entityId)
        {
            //Khai báo tên ClassName
            var className = typeof(T).Name;
            //Khai báo parameters
            DynamicParameters parameters = new DynamicParameters();
            //Thêm vào đối tượng parameters
            parameters.Add($"@{className}Id", entityId);
            //3. Thực hiện lấy dữ liệu Dapper
            var entities = _sqlConnection.QueryFirstOrDefault<T>($"Proc_Get{className}ById", param: parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả
            return entities;
        }
        /// <summary>
        /// Thêm mới  đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số dòng thay đổi trong DB</returns>
        /// created by: VTNghia - MF1108
        public int Insert(T entity)
        {
            //Khai báo biến
            var dynamicParam = new DynamicParameters();
            var className = typeof(T).Name;
            //Đọc từng prop của obj:
            var properties = entity.GetType().GetProperties();
            //Duyệt từng prop:
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                dynamicParam.Add($"@{propName}", propValue);
            }
            var rowsEffect = _sqlConnection.Execute($"Proc_Insert{className}", param: dynamicParam, commandType: CommandType.StoredProcedure);
            return rowsEffect;
        }
        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>số dòng bị ảnh hưởng trong bản ghi</returns>
        /// created by: VTNghia - MF1108
        public int Update(T entity, Guid entityId)
        {
            //Khai báo biến
            var dynamicParam = new DynamicParameters();
            //Khai báo tên class
            var className = typeof(T).Name;
            //Đọc từng prop của obj:
            var properties = entity.GetType().GetProperties();
            //Duyệt từng prop:
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                dynamicParam.Add($"@{propName}", propValue);

            }
            var rowsEffect = _sqlConnection.Execute($"Proc_Update{className}", param: dynamicParam, commandType: CommandType.StoredProcedure);
            return rowsEffect;
        }

        /// <summary>
        /// Tạo mới mã đối tượng mới
        /// </summary>
        /// <returns> Mã mới</returns>
        /// created by: VTNghia - MF1108
        public string GetNewCode()
        {
            //Khai báo tên ClassName
            var className = typeof(T).Name;
            //3. Thực hiện lấy dữ liệu Dapper
            var code = _sqlConnection.QueryFirstOrDefault<string>($"Proc_GetNew{className}Number", commandType: CommandType.StoredProcedure);

            // Trả về kết quả
            var newCode = $"{className}-{code}";
            return newCode;
        }


        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="entityIds">Danh sách Id của các đối tượng</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        public int DeleteMulti(List<Guid> entityIds)
        {
            //Khai báo tên bảng
            var tableName = typeof(T).Name;
            var tableNameLower = typeof(T).Name.ToLower();

            //Khai báo biến tạm hứng Ids
            List<string> temp = new List<string>();
            foreach (var id in entityIds)
            {
                string idString = $" {tableName}Id = '{id}' ";
                temp.Add(idString);
            }
            string condition = String.Join("OR", temp);
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@tableName", tableNameLower);
            paras.Add("@condition", condition);

            //sql command
            string sqlString = $"Delete from {tableNameLower} where {@condition}";
            //trả về kết quả
            var res = _sqlConnection.Execute(sqlString);
            return res;
        }

        /// <summary>
        /// Tìm kiếm trong CSDL theo tên gọi
        /// </summary>
        /// <param name="entityName">Tên gọi</param>
        /// <returns>Đối tượng tìm kiếm</returns>
        public T FindByName(string entityName)
        {
            //Khai báo tên ClassName
            var className = typeof(T).Name;
            //Khai báo parameters
            DynamicParameters parameters = new DynamicParameters();
            //Thêm vào đối tượng parameters
            parameters.Add($"@{className}Name", entityName);
            //3. Thực hiện lấy dữ liệu Dapper
            var data = _sqlConnection.QueryFirstOrDefault<T>($"Proc_Find{className}ByName", param: parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả
            return data;
        }


        #endregion
    }
}
