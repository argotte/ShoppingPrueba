﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba.Models
{
    public class HomeViewModel
    {
        public ICollection<ProductsHomeViewModel> Products { get; set; }

        public float Quantity { get; set; }

    }
}
