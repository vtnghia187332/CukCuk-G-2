using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.Core.Entities
{
    public class Convertion : BaseEntity
    {
        /// <summary>
        /// Danh mục bảng chuyển đổi
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid ConvertionId { get; set; }

        /// <summary>
        /// Mã chuyển đổi
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? ConvertionCode { get; set; }

        /// <summary>
        /// Tên chuyển đổi
        /// created by: VTNghia - MF1108
        /// </summary>
        public string ConvertionName { get; set; }

        /// <summary>
        /// Tỉ lệ chuyển đổi
        /// created by: VTNghia - MF1108
        /// </summary>
        public int? RatingCovertion { get; set; }
        /// <summary>
        /// Phép tính chuyển đổi
        ///  created by: VTNghia - MF1108
        /// </summary>
        public int? ConvertionCalculation { get; set; }

        [NotMap]
        /// <summary>
        /// Phép tính chuyển đổi Enum
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? ConvertionCalculationName
        {
            get
            {
                switch (ConvertionCalculation)
                {
                    case (int)ConvertionCalculationEnum.Addition:
                        return "+";
                    case (int)ConvertionCalculationEnum.Subtraction:
                        return "-";
                    case (int)ConvertionCalculationEnum.Multiplication:
                        return "*";
                    case (int)ConvertionCalculationEnum.Division:
                        return "/";
                    default:
                        return "Khác";
                }
            }
            set { }
        }


        /// <summary>
        /// Mô tả chuyển đổi
        /// created by: VTNghia - MF1108
        /// </summary>
        public string? ConvertionDescription { get; set; }

        /// <summary>
        /// Khóa ngoại bảng Nguyên liệu
        /// created by: VTNghia - MF1108
        /// </summary>
        public Guid MaterialId { get; set; }
    }
}
