using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InterfacesBS.InterfacesBL
{
    public interface IAdmin
    {
        int insertCatagory(BusinessEntitiesBS.Catagory_Entities.catagoryObj catObj);
        DataTable getCatagories();
        int insertItems(BusinessEntitiesBS.ItemEntities.ItemObj itemObj);
        DataTable getItems(int grpCatId);
        int insertGroup(BusinessEntitiesBS.GroupEntities.grpObj grpObj);
    }
}
