namespace Models.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? Price { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}