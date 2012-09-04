using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntitiesBS.ItemEntities
{
    public class ItemObj
    {
        public string itemName { get; set; }
        public string itemDesc { get; set; }
        public string itemImagePath { get; set; }
        public int itemCatagory { get; set; }
        public float itemBR { get; set; }
        public float itemNR { get; set; }
        public int itemQty { get; set; }
        public bool   itemStatus { get; set; }
    }
}
