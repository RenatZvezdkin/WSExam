//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConferenceSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public partial class Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Events()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public int LengthInDays { get; set; }
        public Nullable<int> CityId { get; set; }
    
        public virtual Cities Cities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
        public string WillBeFor { get => $"Будет длиться {LengthInDays} дней" ; }
        public Visibility CanChange { get => Manager.Instance.LoggedUser != null && Manager.Instance.LoggedUser.RoleId == 1 ? Visibility.Visible : Visibility.Hidden; }
    }
}