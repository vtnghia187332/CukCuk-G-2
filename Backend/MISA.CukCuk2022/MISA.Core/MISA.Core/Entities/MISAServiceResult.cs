using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class MISAServiceResult
    {
        /// <summary>
        /// Câu cảnh báo dành cho client
        /// created by: VTNghia - MF1108
        /// </summary>
        public String? UserMsg { get; set; } = null;

        /// <summary>
        /// Câu cảnh báo dành cho developer
        /// created by: VTNghia - MF1108
        /// </summary>
        public String? DevMsg { get; set; } = null;

        /// <summary>
        /// Dữ liệu hiển thị 
        /// created by: VTNghia - MF1108
        /// </summary>
        public object? Data { get; set; } = null;

    }
}
