using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Unit : BaseEntity
    {
        /// <summary>
        /// Danh mục bảng ĐVT
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Mã ĐVT
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? UnitCode { get; set; }

        /// <summary>
        /// Tên ĐVT
        /// created by: VTNghia - MF1108
        /// </summary>
        public string UnitName { get; set; }

    }
}
