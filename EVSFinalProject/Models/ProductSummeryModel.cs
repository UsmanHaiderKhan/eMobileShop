﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSFinalProject.Models
{
    public class ProductSummeryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string ImageUrl { get; set; }
    }
}