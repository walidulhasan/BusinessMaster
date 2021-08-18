using BusinessMaster.CustomValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace BusinessMaster.Models
{
    public class ServicesName
    {
        public int ServicesNameId { get; set; }
        [Required(ErrorMessage = "Serivces Name can't be Empty"), Display(Name = "Service Name")]
        //[UpperCase]
        //[CountryAttribute]
        public string ServiceName { get; set; }
        //nev
        public List<Client> Client { get; set; }
    }
    public class Client
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name can't be empty")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Client Age can't be empty")]
        public int ClientAge { get; set; }
        [Required(ErrorMessage = "Client Budget can not be empty")]
        public int ClientBudget { get; set; }
        [Required, Display(Name = "Delivery Date")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime WorkDeliveryDate { get; set; }
        public string Picture { get; set; }
        //fk
        [Required(ErrorMessage = "It can not be empty"), ForeignKey("ServicesName")]
        public int ServicesNameId { get; set; }
        //nav
        public virtual ServicesName ServicesName { get; set; }

    }
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        public DbSet<ServicesName> servicesNames { get; set; }
        public DbSet<Client> clients { get; set; }
    }
}
