using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InterfacesBS.InterfacesDA;
using InterfacesBS.InterfacesBL;


namespace BusinessLogicBS.BusinessClasses
{
    public class AdminItems : IAdmin
    {

        #region IAdmin Members

        public int insertCatagory(BusinessEntitiesBS.Catagory_Entities.catagoryObj catObj)
        {
            try
            {

                IAdminDA catInsert = new DataAccessBS.AdminClasses.AdminDA();
                int catChk = catInsert.insertCatagoryDA(catObj);
                return catChk;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region IAdmin Members


        public DataTable getCatagories()
        {
            DataTable catDS = new DataTable();

            IAdminDA getCatg = new DataAccessBS.AdminClasses.AdminDA();
            catDS = getCatg.getCatgValues();
       
            if (catDS.Rows.Count > 0)
            {
                return catDS;                
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region IAdmin Members


        public int insertItems(BusinessEntitiesBS.ItemEntities.ItemObj itemObj)
        {

            IAdminDA itemValues = new DataAccessBS.AdminClasses.AdminDA();

            int itemId = itemValues.insertItemDA(itemObj);
            return itemId;
            
        }

        #endregion

        #region IAdmin Members


        public DataTable getItems(int grpCatId)
        {
            try
            {

                IAdminDA itemValues = new DataAccessBS.AdminClasses.AdminDA();
                return itemValues.getItemValues(grpCatId);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IAdmin Members


        public int insertGroup(BusinessEntitiesBS.GroupEntities.grpObj grpObj)
        {
            IAdminDA inertGrp = new DataAccessBS.AdminClasses.AdminDA();
            return inertGrp.insertGrpDA(grpObj);

        }

        #endregion
    }
}
