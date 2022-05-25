using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class MISAException : Exception
    {
        /// <summary>
        /// Hiển thị thông báo trên Client
        /// created by: VTNghia - MF1108
        /// </summary>
        public string MISAMessenger { get; set; }
        /// <summary>
        /// Hiển thị thông báo trên Server
        /// created by: VTNghia - MF1108
        /// </summary>
        public IDictionary MISAErrorData { get; set; }
        /// <summary>
        /// Hiển thị lỗi
        /// </summary>
        /// <param name="msg">Thông điệp hiển thị lỗi</param>
        /// <param name="data">Lỗi</param>
        /// created by: VTNghia - MF1108
        public MISAException(string msg, IDictionary data = null)
        {
            this.MISAMessenger = msg;
            MISAErrorData = data;
        }
        /// <summary>
        /// override lại thông điệp lỗi trả về
        /// created by: VTNghia - MF1108
        /// </summary>
        public override string Message => this.MISAMessenger;
        /// <summary>
        /// override lại dữ liệu lỗi trả về
        /// created by: VTNghia - MF1108
        /// </summary>
        public override IDictionary Data => MISAErrorData;
    }
}
