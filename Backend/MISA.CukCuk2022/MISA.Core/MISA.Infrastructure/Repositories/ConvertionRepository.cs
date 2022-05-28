using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MySqlConnector;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MISA.Core.Exceptions;
using MISA.Core.Resources;

namespace MISA.Infrastructure.Repositories
{
    public class ConvertionRepository : BaseRepository<Convertion>, IConvertionRepository
    {
        #region DECLARE
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        #endregion

        #region Constructor

        public ConvertionRepository()
        {
            _connectionString = "Host=localhost; Port=3306; Database=misacukcukmaterial2022_dev; User Id = root; Password=123456";
            _sqlConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        /// <summary>
        /// Lấy các đơn vị chuyển đổi theo MaterialId
        /// </summary>
        /// <param name="materialId">MaterialId</param>
        /// <returns>Danh sách các đơn vị chuyển đổi</returns>
        /// created by: VTNghia - MF1108
        public IEnumerable<Convertion> getByMaterialId(Guid materialId)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"@MaterialId", materialId);
            var data = _sqlConnection.Query<Convertion>($"Proc_GetConvertionByMaterialId", param: parameters, commandType: CommandType.StoredProcedure);
            return data.AsList();
        }

        /// <summary>
        /// Thêm danh sách bộ chuyển đổi đơn vị theo danh mục Nguyên Vật liệu
        /// </summary>
        /// <param name="materialId">danh mục Nguyên Vật liệu</param>
        ///  <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>Số dòng thay đổi trong DB</returns>
        ///  created by: VTNghia - MF1108
        public int InsertByMaterialId(List<Convertion> convertions, Guid materialId)
        {
            var rowsEffects = 0;
            //Duyệt vòng for để lấy ra dữ liệu
            foreach (var convertion in convertions)
            {
                //Khai báo biến
                var dynamicParam = new DynamicParameters();
                //Đọc từng prop của obj:
                var properties = convertion.GetType().GetProperties();
                Dictionary<string, string> error = new Dictionary<string, string>();

                //Duyệt từng prop:
                foreach (var prop in properties)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(convertion);

                    //if (propName == "ConvertionCode")
                    //{

                    //    var propValue1 = propValue.ToString();
                       
                    //    if (String.IsNullOrEmpty(propValue1))
                    //    {
                    //        error.Add(propName, $"Mã <{propValue}> không được để trống");
                    //        throw new MISAException(MISAResource.VN_NotValidInput, error);
                    //    }
                    //    else
                    //    {
                    //        var isExist = base.CheckCodeExist(propValue1);
                    //        if (isExist)
                    //        {
                    //            error.Add(propName, $"Mã <{propValue}> đã tồn tại trong hệ thống");
                    //            throw new MISAException(MISAResource.VN_NotValidInput, error);
                    //        }

                    //    }
                    //}
                    dynamicParam.Add($"@{propName}", propValue);
                }
                //số dòng ảnh hưởng trong DB
                var rowsEffect = _sqlConnection.Execute($"Proc_InsertConvertion", param: dynamicParam, commandType: CommandType.StoredProcedure);
                rowsEffects += rowsEffect;
            }
            //Tổng số dòng ảnh hưởng trong DB
            return rowsEffects;
        }

        /// <summary>
        /// Sửa danh sách bộ chuyển đổi đơn vị theo danh mục Nguyên Vật liệu
        /// </summary>
        /// <param name="materialId">danh mục Nguyên Vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>Số dòng thay đổi trong DB</returns>
        ///  created by: VTNghia - MF1108
        public int UpdateByMaterialId(List<Convertion> convertions, Guid ConvertionId)
        {
            var rowsEffects = 0;
            //Duyệt vòng for để lấy ra dữ liệu
            foreach (var convertion in convertions)
            {
                //Khai báo biến
                var dynamicParam = new DynamicParameters();
                //Đọc từng prop của obj:
                var properties = convertion.GetType().GetProperties();
                //Duyệt từng prop:
                foreach (var prop in properties)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(convertion);
                    dynamicParam.Add($"@{propName}", propValue);
                    //Lấy Trường MaterialId ra và truyền materialId vào 
                }
                //số dòng ảnh hưởng trong DB
                var rowsEffect = _sqlConnection.Execute($"Proc_UpdateConvertion", param: dynamicParam, commandType: CommandType.StoredProcedure);
                rowsEffects += rowsEffect;
            }
            //Tổng số dòng ảnh hưởng trong DB
            return rowsEffects;
        }
    }
}