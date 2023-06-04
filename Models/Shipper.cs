﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace assignment4.Models
{
    [Table("shippers")]
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("shipper_id")]
        public int ShipperId { get; set; }
        [Column("company_name")]
        [StringLength(40)]
        public string CompanyName { get; set; } = null!;
        [Column("phone")]
        [StringLength(24)]
        public string? Phone { get; set; }

        [InverseProperty(nameof(Order.ShipViaNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
