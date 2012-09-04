using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessEntitiesBS.Catagory_Entities;
using BusinessEntitiesBS.ItemEntities;
using BusinessEntitiesBS.GroupEntities;

namespace InterfacesBS.InterfacesDA
{
    public interface IAdminDA
    {
        int insertCatagoryDA(catagoryObj catObjDa);
        DataTable getCatgValues();
        int insertItemDA(ItemObj itemObjDa);
        DataTable getItemValues(int grpCatId);
        int insertGrpDA(grpObj grpObjDa);

    }
}
