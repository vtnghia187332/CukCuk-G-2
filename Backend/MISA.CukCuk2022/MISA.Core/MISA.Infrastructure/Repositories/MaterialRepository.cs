using MISA.Core.Entities;
using MISA.Core.Interfaces;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MISA.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace MISA.Infrastructure.Repositories
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        #region DECLARE
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        private IConvertionRepository _convertionRepository;
        private IUnitRepository _iUnitRepository;
        #endregion

        #region Constructor

        public MaterialRepository(IConvertionRepository injection, IUnitRepository iUnitRepository)
        {
            _connectionString = "Host=localhost; Port=3306; Database=misacukcukmaterial2022_dev; User Id = root; Password=12345678";
            _sqlConnection = new MySqlConnection(_connectionString);
            _convertionRepository = injection;
            _iUnitRepository = iUnitRepository;

        }
        #endregion

        #region Method
        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNGHIA - MF1108
        public object FilterMaterial(int? pageNumber, int? pageSize, List<FilterOption>? filter)
        {
            //1. Khai báo Procedure ấy dữ liệu
            var procedureName = "Proc_GetMaterialFilterPaging";

            // 1.1 xử lý filter option thành string
            var filterString = ConvertCommandFilter(filter);

            //2. Thêm tham số vào lệnh sql
            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@Filter", filterString);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var data = _sqlConnection.Query<Material>(sql: procedureName, param: parameters, commandType: CommandType.StoredProcedure);

            var totalRecord = parameters.Get<Int32>("@TotalRecord");
            var totalPage = parameters.Get<Int32>("@TotalPage");

            var filterPagingData = new
            {
                totalRecord,
                totalPage,
                data
            };
            return filterPagingData;
        }

        /// <summary>
        /// Kiểm tra sự trùng lặp của mã Nguyên vật liệu
        /// </summary>
        /// <param name="materialCode">Mã code Nguyên vật liệu</param>
        /// <returns>true: Mã code bị trùng || false: Mã code không bị trùng</returns>
        ///  created by: VTNGHIA - MF1108
        public bool CheckMaterialCode(string materialCode)
        {
            //3. Khai Báo dữ liệu truy vấn
            var sqlCommand = $"SELECT MaterialCode FROM material WHERE MaterialCode = @MaterialCode";
            var parameters = new DynamicParameters();
            parameters.Add("@MaterialCode", materialCode);
            var res = _sqlConnection.QueryFirstOrDefault<string>(sqlCommand, parameters, commandType: System.Data.CommandType.Text);
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Chuyển đổi định dạng của câu lệnh tìm kiếm
        /// </summary>
        /// <param name="filterCommands">Câu lệnh tìm kiếm</param>
        /// <returns>Nội dung câu lệnh tìm kiếm để query dưới Database</returns>
        ///  created by: VTNGHIA - MF1108
        private string ConvertCommandFilter(List<FilterOption> filterCommands)
        {
            //Clear value
            var command = "";
            if (filterCommands.Count() == 0)
            {
                return command = null;
            }
            // Mặc định toán tử tìm kiếm là: Contain

            //duyệt danh sách các filter
            foreach (var filter in filterCommands)
            {
                //Phân biệt operator để filter
                var filterString = "";
                switch (filter.Operator)
                {
                    //Duyệt các phần từ -> Phân biệt operator - Column - Value -> Generate câu lệnh tìm kiếm
                    //Chứa keyword
                    case (int)MISAEnum.Operator.Contain:
                        filterString = $"{filter.ColumnName} LIKE '%{filter.ColumnValue}%' AND ";
                        command += filterString;
                        break;
                    //Không chứa keyword
                    case (int)MISAEnum.Operator.NotContain:
                        filterString = $"{filter.ColumnName} NOT LIKE '%{filter.ColumnValue}%' AND ";
                        command += filterString;
                        break;
                    //Bắt đầu từ keyword
                    case (int)MISAEnum.Operator.Begin:
                        filterString = $"{filter.ColumnName} LIKE '{filter.ColumnValue}%' AND ";
                        command += filterString;
                        break;
                    //Kết thúc từ keyword
                    case (int)MISAEnum.Operator.End:
                        filterString = $"{filter.ColumnName} LIKE '%{filter.ColumnValue}' AND ";
                        command += filterString;
                        break;
                    //Bằng keyword
                    case (int)MISAEnum.Operator.Equal:
                        filterString = $"{filter.ColumnName} LIKE '{filter.ColumnValue}' AND ";
                        command += filterString;
                        break;

                    //Chứa keyword
                    default:
                        filterString = $"{filter.ColumnName} LIKE '%{filter.ColumnValue}%' AND ";
                        command += filterString;
                        break;
                }
            }
            // Xóa chữ AND ở cuối
            command = command.Remove(command.Length - 4);
            return command;
        }

        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        ///  created by: VTNGHIA - MF1108
        public int InsertMaterialWithConvertions(Material material, List<Convertion> convertions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu vào Database
        /// </summary>
        /// <param name="materials">Danh sách nguyên vật liệu đã được validate dữ liệu (hợp lệ)</param>
        /// <returns>Danh sách những nhân viên đã được thêm vào database</returns>
        public IEnumerable<Material> Import(List<Material> materials)
        {
            foreach (var material in materials)
            {
                Insert(material);
            }
            return materials;
        }


        //    public async Task<IActionResult> Export()
        //    {
        //        // query data from database  
        //        await Task.Yield();

        //        var list = new List<Material>()
        //{
        //        new Material { MaterialCode = "M-19876", MaterialName = "catcher" },
        //        new Material { MaterialCode = "M-19876", MaterialName = "james" },
        //};
        //        var stream = new MemoryStream();

        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //            workSheet.Cells.LoadFromCollection(list, true);
        //            package.Save();
        //        }
        //        stream.Position = 0;
        //        string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

        //        return File(stream, "application/octet-stream", excelName);
        //        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        //    }


        #endregion

    }
}
