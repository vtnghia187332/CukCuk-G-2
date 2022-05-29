using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.Core.Entities
{
    public class Material : BaseEntity
    {
        #region MaterialProperty

        /// <summary>
        /// Danh mục bảng nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid MaterialId { get; set; }

        [NotEmpty]
        /// <summary>
        /// Mã nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string MaterialCode { get; set; }

        [NotEmpty]
        /// <summary>
        /// Tên nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// Tính chất nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? MaterialFeature { get; set; }

        /// <summary>
        /// Ghi chú nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? MaterialNote { get; set; }

        /// <summary>
        /// Nhóm vật liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? GroupMaterial { get; set; }

        /// <summary>
        /// Theo dõi nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public int? Following { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// created by: VTNghia - MF1108
        /// </summary>
        public int? MaterialStockMin { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// created by: VTNghia - MF1108
        /// </summary>
        public DateTime? MaterialExpiry { get; set; }

        /// <summary>
        /// Mô tả nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// Khóa ngoại bảng ĐVT
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị Tính
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? UnitName { get; set; }

        /// <summary>
        /// Tên kho ngầm định
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid? StorageId { get; set; }

        [NotMap]
        /// <summary>
        /// Nhận biết đối tượng truyền vào từ file excel có hợp lệ hay không?
        /// created by: VTNghia - MF1108
        /// </summary>
        public bool? IsValid { get; set; } = true;


        [NotMap]
        /// <summary>
        /// Danh sách hứng lỗi
        /// </summary>
        public Dictionary<string, string>? ErrorValidateNotValid { get; set; } = new Dictionary<string, string>();

        #endregion
    }
}
