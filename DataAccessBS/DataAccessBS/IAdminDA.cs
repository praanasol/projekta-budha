using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccessBS
{
    public interface IAdminDA
    {
        int insertCatagoryDA(BusinessEntitiesBS.Catagory_Entities.catagoryObj catObjDa);
        DataTable getCatgValues();
        int insertItemDA(BusinessEntitiesBS.ItemEntities.ItemObj itemObjDa);
        DataTable getItemValues(int grpCatId);
    }
}
