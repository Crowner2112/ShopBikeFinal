namespace Models.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int Quantity { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}