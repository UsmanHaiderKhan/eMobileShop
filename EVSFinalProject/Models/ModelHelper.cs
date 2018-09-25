using EVSFinalProjectClasses.ProductMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Models
{
    public class ModelHelper
    {
        public static List<SelectListItem> ToSelectItemList(dynamic values)

        {
            List<SelectListItem> templist = null;
            if (values != null)
            {
                templist = new List<SelectListItem>();
                foreach (var v in values)
                {
                    templist.Add(new SelectListItem {Text = v.Name, Value = Convert.ToString(v.Id)});
                }
                templist.TrimExcess();
            }
            return templist;
        }

        public static List<ProductSummeryModel> ProductSummeryList(IEnumerable<Mobile> mobiles)
        {
            List<ProductSummeryModel> temp = new List<ProductSummeryModel>();
            if (mobiles != null)
            {
                temp.AddRange(mobiles.Select(m => ToProductSummary(m)));
                temp.TrimExcess();
            }
            return temp;
        }


        public static ProductSummeryModel ToProductSummary(Mobile mobile)
        {
            return new ProductSummeryModel
            {
                Id = mobile.Id,
                Name = mobile.Name,
                Price = mobile.Price,
                ImageUrl = (mobile.Images.Count > 0) ? mobile.Images.First().Url : null
            };
        }
    }
}
