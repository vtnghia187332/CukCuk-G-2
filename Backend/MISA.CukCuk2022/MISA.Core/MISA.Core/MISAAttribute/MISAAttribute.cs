using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttribute
{
    /// <summary>
    /// Không liên kết trường với DB
    /// created by: VTNghia - MF1108
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMap : Attribute
    {
    }

    /// <summary>
    /// Không được phép để trống
    /// created by: VTNghia - MF1108
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {

    }

    /// <summary>
    /// is Guid()
    /// created by: VTNghia - MF1108
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsGuid : Attribute
    {

    }

    /// <summary>
    /// Hiển thị tên tương ứng của trường dữ liệu
    /// created by: VTNghia - MF1108
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Độ dài lớn nhất của trường dữ liệu
    /// created by: VTNghia - MF1108
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Length { get; set; }
        public MaxLength(int length)
        {
            Length = length;
        }
    }

}
