using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class BaseEntity
    {
        #region BaseProperty

        /// <summary>
        /// Ngày Tạo
        /// created by: VTNghia - MF1108
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        ///  created by: VTNghia - MF1108
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        ///  created by: VTNghia - MF1108
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        ///  created by: VTNghia - MF1108
        /// </summary>
        public string? ModifiedBy { get; set; }

        #endregion

    }
}
