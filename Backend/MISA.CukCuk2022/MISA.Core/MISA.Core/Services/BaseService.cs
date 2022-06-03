using Dapper;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.MISAAttribute;
using MISA.Core.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region DECLARE
        IBaseRepository<T> _baseRepository;
        //tạo biến dictionary hứng lỗi
        protected Dictionary<string, string> _error;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
            _error = new Dictionary<string, string>();

        }
        #endregion

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Số dòng trong DB bị ảnh hưởng</returns>
        /// created by: VTNghia - MF1108
        public int InsertService(T entity)
        {
            //Khai báo biến
            var className = typeof(T).Name;
            var errorList = ValidateObject(entity);

            //Validate dữ liệu 
            //1. validate dùng chung
            if (errorList.Count() > 0)
            {
                throw new MISAException(MISAResource.VN_NotValidInput, errorList);
            }


            var entityCodeToCheck = typeof(T).GetProperty($"{className}Code").GetValue(entity, null).ToString();

            if (_baseRepository.CheckCodeExist(entityCodeToCheck) == true)
            {
                _error.Add($"{className}Code", $"Mã <{entityCodeToCheck}> đã tồn tại trong hệ thống");
                throw new MISAException(MISAResource.VN_NotValidInput, _error);
            }
            else
            {
                //Thực hiện insert dữ liệu
                var rowAdds = _baseRepository.Insert(entity);
                return rowAdds;
            }

        }


        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Id của đối tượng</param>
        /// <param name="entityId">Đối tượng</param>
        /// <returns>Số dòng trong DB bị ảnh hưởng</returns>
        ///  created by: VTNghia - MF1108
        public int UpdateService(T entity, Guid entityId)
        {
            var className = typeof(T).Name;

            var entityIdColumn = $"{className}Id";
            var entityCodeColumn = $"{className}Code";

            //Validate Id tồn tại
            //Lấy đối tượng cần update trong DB
            var entityInDb = _baseRepository.GetById(entityId);

            //Nếu đối tượng cần update không tồn tại -> văng ra lỗi cảnh báo
            if (entityInDb == null)
            {
                _error.Add(entityIdColumn, "Đối tượng không tồn tại");
                throw new MISAException("Đối tượng không tồn tại", _error);
            }

            //Validate mã entity ở request ~ entity trong DB có trùng ko
            //Lấy code của entity truyền vào ở body

            var entityCode = typeof(T).GetProperty(entityCodeColumn)?.GetValue(entity, null)?.ToString();
            var entityInDbCode = typeof(T).GetProperty(entityCodeColumn)?.GetValue(entityInDb, null)?.ToString();

            //So sánh 2 code có trùng (trùng thì pass)
            if (entityInDbCode != entityCode)
            {
                // Không trùng >> check code trong request có tồn tại trong db không
                var existCodeInReq = _baseRepository.CheckCodeExist(entityCode);

                // tồn tại >> báo lỗi
                if (existCodeInReq == true)
                {
                    // Báo lỗi mã đã tồn tại trong resource
                    _error.Add(entityCodeColumn, $"Mã <{entityCode}> đã tồn tại trong hệ thống");
                    throw new MISAException($"Mã <{entityCode}> đã tồn tại trong hệ thống", _error);
                }
                // Kiểm tra regex của mã nhân viên và mã đơn vị
                var regex = new Regex(@"[a-zA-Z]$");

                if (regex.IsMatch(entityCode))
                {
                    // Báo lỗi mã đã tồn tại trong resource
                    _error.Add(entityCodeColumn, $"Mã <{entityCode}> sai định dạng");
                    throw new MISAException($"Mã <{entityCode}> sai định dạng", _error);
                }
            }

            //Gọi đến repository để query dữ liệu
            var rowUpdates = _baseRepository.Update(entity, entityId);
            return rowUpdates;
        }

        /// <summary>
        /// Validate đối tượng với các điều kiện cụ thể
        /// </summary>
        /// <param name="entity">Đối tượng truyển vào để validate</param>
        /// <returns>true/false</returns>
        /// <exception cref="DataMisalignedException"></exception>
        public Dictionary<string, string> ValidateObject(T entity)
        {
            //Dictionary<string, string> error = new Dictionary<string, string>();

            //Lấy ra các property:
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);
                var propDisplayName = propName;
                //1. Các thông tin bắt buộc nhập

                var isDisplayName = property.IsDefined(typeof(DisplayName), true);
                if (isDisplayName)
                {
                    propDisplayName = (property.GetCustomAttributes(typeof(DisplayName), true).First() as DisplayName).Name;
                }

                //Kiểm tra Attribute [NotEmpty]
                var isNotEmpty = property.IsDefined(typeof(NotEmpty), true);
                if (isNotEmpty == true)
                {
                    //Kiểm tra trường dữ liệu có thuộc kiểu Guid
                    var isGuid = property.IsDefined(typeof(IsGuid), true);
                    if (isGuid == true && propValue.Equals(Guid.Empty))
                    {
                        _error.Add(propName, $"{propDisplayName} không được phép để trống");

                        throw new MISAException($"{propDisplayName} không được phép để trống");
                    }
                    else if (string.IsNullOrEmpty((string?)propValue))
                    {
                        _error.Add(propName, $"{propDisplayName} không được phép để trống");

                    }
                }



                //Kiểm tra Attribute [isMaxLength]
                var isMaxLength = property.IsDefined(typeof(MaxLength), true);
                if (isMaxLength)
                {
                    var length = (property.GetCustomAttributes(typeof(MaxLength), true).First() as MaxLength).Length;
                    if (propValue.ToString().Length > length)
                    {
                        throw new MISAException($"Thông tin {propDisplayName} không vượt quá {length} kí tự");
                        _error.Add(propName, $"Thông tin {propDisplayName} không vượt quá {length} kí tự");
                    }
                }
            }
            return _error;
        }

        /// <summary>
        /// Hàm check validate custom: Class kế thừa và override để validate dữ liệu dùng riêng
        /// </summary>
        /// <param name="entity">Đối tượng riêng được xử lý </param>
        /// <returns>true/false</returns>
        protected virtual bool ValidateCustom(T entity)
        {
            return true;
        }

       


    }
}
