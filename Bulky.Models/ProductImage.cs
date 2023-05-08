﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public  class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [ForeignKey("CategoryId")]
        public int ProductId { get; set; }

    }
}
