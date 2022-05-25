using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Storage : BaseEntity
    {
        /// <summary>
        /// Danh mục bảng Kho ngầm định
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid StorageId { get; set; }

        /// <summary>
        /// Mã kho
        /// created by: VTNghia - MF1108
        /// </summary>
        public string StorageCode { get; set; }

        /// <summary>
        /// Tên kho
        /// created by: VTNghia - MF1108
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// Địa chỉ kho
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? StorageAddress { get; set; }

    }
}
