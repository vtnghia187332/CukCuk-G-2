using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces;
using MISA.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        #region DECLARE
        IMaterialRepository _materialRepository;
        IConvertionRepository _convertionRepository;
        #endregion
        #region Constructor
        public MaterialService(IMaterialRepository materialRepository, IConvertionRepository convertionRepository) : base(materialRepository)
        {
            _materialRepository = materialRepository;
            _convertionRepository = convertionRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lọc danh sách nguyên vật liệu
        /// <param name="pageNumber"> Số địa chỉ trang hiện tại</param>
        /// <param name="pageSize"> Số nhân viên / 1 trang</param>
        /// <param name="filter">Từ khóa cần tìm kiếm</param>
        /// </summary>
        /// <returns>Danh sách các nhân viên khớp với điều kiện tìm kiếm</returns>
        /// created by: VTNGHIA - MF1108
        public object FilterMaterial(int? pageNumber, int? pageSize, List<FilterOption>? filters)
        {
            var errorMsg = new Dictionary<string, string>();

            //Xử lý filter :
            if (pageNumber < 0)
            {
                pageNumber = 1;
            }
            if (pageSize < 0)
            {
                pageSize = 10;
            }
            // tồn tại lỗi -> gửi lỗi lên controller API
            if (errorMsg.Count > 0)
            {
                throw new MISAException(MISAResource.VN_NotValidInput, errorMsg);
            }
            // sau khi validate dữ liệu đầu vào > gửi cho repo
            var res = _materialRepository.FilterMaterial(pageNumber, pageSize, filters);
            return res;
        }


        /// <summary>
        /// Thêm mới bản ghi đối tượng Nguyên vật liệu và danh sách bộ chuyển đổi ứng với Nguyên vật liệu
        /// </summary>
        /// <param name="material">đối tượng Nguyên vật liệu</param>
        /// <param name="convertions">danh sách bộ chuyển đổi</param>
        /// <returns>số dòng bị ảnh hưởng</returns>
        public int InsertMaterialWithConvertions(Material material, List<Convertion>? convertions)
        {
            //Tạo ra Guid mới -> bind vào material
            var newMaterialId = Guid.NewGuid();
            material.MaterialId = newMaterialId;
            //validate data

            //Thêm mới Material
           var res =  base.InsertService(material);

            if (convertions != null && convertions.Count() > 0)
            {
                foreach (var convertion in convertions)
                {
                    //Gán MaterialId vào trong convertion
                    if (newMaterialId != Guid.Empty)
                    {
                        convertion.MaterialId = newMaterialId;
                    }
                }
                //Thực hiện thêm mới convertions
                _convertionRepository.InsertByMaterialId(convertions, newMaterialId);
            }

            //trả về data
            return res;
        }


        #endregion
    }
}
