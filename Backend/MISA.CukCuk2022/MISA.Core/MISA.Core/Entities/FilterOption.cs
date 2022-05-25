using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class FilterOption
    {
        /// <summary>
        /// Toán tử tìm kiếm
        /// Begin - Bắt đầu 
        /// End - Kết thúc
        /// Equal - Bằng
        /// Contain - Bao gồm
        /// Not Contain - Không bao gồm
        /// </summary>
        public int Operator { get; set; }

        /// <summary>
        /// Tên cột cần tìm kiếm
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Giá trị ứng với tên cột tìm kiếm
        /// </summary>
        public string ColumnValue { get; set; }
    }
}
