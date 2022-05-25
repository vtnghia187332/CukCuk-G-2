using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class MaterialWithConvertion
    {

        /// <summary>
        /// Đối tượng Nguyên vật liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public Material material { get; set; }

        /// <summary>
        /// Danh sách bộ chuyển đổi ứng với đối tượng Nguyên vật liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public List<Convertion>? convertions { get; set; }
    }
}
