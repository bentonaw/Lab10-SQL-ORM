﻿using System;
using System.Collections.Generic;

namespace Lab10.Models.DbModels
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
