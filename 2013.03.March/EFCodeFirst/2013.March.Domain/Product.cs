using System.ComponentModel.DataAnnotations;

namespace _2013.March.Domain
{

    // Code First example using attributes (NOTE: ^ above dependency!)
    public class Product
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }
        
        [DataType(DataType.Currency)]
        public virtual double Price { get; set; }

        [Required]
        public virtual Category Category { get; set; }
    }
}
