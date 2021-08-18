using BusinessMaster.CustomValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessMaster.Models.ViewModels
{
    public class BusinessMasterViewModel
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name can't be empty")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Client Age can't be empty")]
        [RequiredGreaterThanZero]
        [Display(Name ="Client Age")]
        public int ClientAge { get; set; }
        [Required(ErrorMessage = "Client Budget can not be empty")]
        public int ClientBudget { get; set; }
        [Required, Display(Name = "Delivery Date")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [LessDate]
        public System.DateTime WorkDeliveryDate { get; set; }
        [Display(Name = "Product Picture")]
        public string Picture { get; set; }
        public IFormFile PicturPath { get; set; }
        //fk
        [Required(ErrorMessage = "It can not be empty"), ForeignKey("ServicesName")]
        public int ServicesNameId { get; set; }
        //nav
        public virtual ServicesName ServicesName { get; set; }
    }
}
