//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_LuyenThiTracNghiem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOPHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOPHOC()
        {
            this.GIANGVIENs = new HashSet<GIANGVIEN>();
        }
    
        public string Malop { get; set; }
        public string Tenlop { get; set; }
        public string Mamonhoc { get; set; }
        public string MaHV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIANGVIEN> GIANGVIENs { get; set; }
        public virtual HOCVIEN HOCVIEN { get; set; }
    }
}
