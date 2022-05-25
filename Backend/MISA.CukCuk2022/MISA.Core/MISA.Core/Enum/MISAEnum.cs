using MISA.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enum
{
    public class MISAEnum
    {
        /// <summary>
        /// Phép tính chuyển đổi:
        /// Addition: Cộng
        /// Subtraction: trừ
        /// Multiplication: Nhân
        /// Division: Chia
        /// created by: VTNghia - MF1108
        /// </summary>
        public enum ConvertionCalculationEnum
        {
            Addition = 0,
            Subtraction = 1,
            Multiplication = 2,
            Division = 3,
        }

        /// <summary>
        /// Theo dõi Nguyên vật liệu
        /// NotFollowing: Không theo dõi
        /// Following: Theo dõi
        /// created by: VTNghia - MF1108
        /// </summary>
        public enum MaterialFollowingEnum 
        {
            NotFollowing = 0,
            Following = 1,

        }

        /// <summary>
        /// Toán tử tìm kiếm
        /// NotContain: Không bao gồm
        /// Begin: Bắt đầu bằng 
        /// End: Kết thúc bằng
        /// Equal: Tìm kiểm bằng
        /// created by: VTNghia - MF1108
        /// </summary>
        public enum Operator
        {
            NotContain= 0,
            Begin = 1,
            End = 2,
            Equal = 3,
            Contain = 4,
        }

        
    }
}
