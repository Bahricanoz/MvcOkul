//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcOkul.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    
    public partial class Tbl_Kulüpler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Kulüpler()
        {
            this.Tbl_Ogrenciler = new HashSet<Tbl_Ogrenciler>();
        }
    
        public int KulupId { get; set; }
        [Required(ErrorMessage ="Lütfen Külüp Adını Giriniz...")]
        public string KlpAd { get; set; }
        public Nullable<int> KlpKontenjan { get; set; }
        public Nullable<bool> KlpDurum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Ogrenciler> Tbl_Ogrenciler { get; set; }
    }
}
