using Dapper;
using MISA.Core.Entities;
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
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        string _connectionString;
        protected MySqlConnection _sqlConnection;

        #region Constructor
        public UnitRepository(IConvertionRepository injection)
        {
            _connectionString = "Host=localhost; Port=3306; Database=misacukcukmaterial2022_dev; User Id = root; Password=123456";
            _sqlConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        /// <summary>
        /// Tìm kiếm trong CSDL theo tên của ĐVT
        /// </summary>
        /// <param name="entityName">Tên gọi</param>
        /// <returns>Đối tượng tìm kiếm</returns>
        public Unit FindByUnitName(string entityName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@UnitName", entityName);
            //3. Thực hiện lấy dữ liệu Dapper
            var data = _sqlConnection.QueryFirstOrDefault<Unit>($"Proc_FindUnitByName", param: parameters, commandType: CommandType.StoredProcedure);
            // Trả về kết quả
            return data;
        }

        /// <summary>
        /// Thêm mới ĐVT
        /// </summary>
        /// <param name="unitName">Tên ĐVT</param>
        /// <returns>Số dòng bị ảnh hưởng trong DB</returns>
        public int InsertNewData(string unitName)
        {
            //Khai báo biến
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@UnitId", null);
            parameters.Add($"@UnitCode", null);
            parameters.Add($"@UnitName", unitName);
            parameters.Add($"@CreatedDate", null);
            parameters.Add($"@CreatedBy", null);
            parameters.Add($"@ModifiedDate", null);
            parameters.Add($"@ModifiedBy", null);
            var res = _sqlConnection.Execute($"Proc_InsertUnit", param: parameters, commandType: CommandType.StoredProcedure);
            return res;
        }

    }
}
