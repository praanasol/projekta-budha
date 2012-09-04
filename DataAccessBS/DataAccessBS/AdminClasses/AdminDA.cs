using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccessAutoRaksha.Supported_Classes;
using System.Data;
using InterfacesBS.InterfacesDA;

namespace DataAccessBS.AdminClasses
{
    public class AdminDA:IAdminDA
    {

        #region IAdminDA Members

        public int insertCatagoryDA(BusinessEntitiesBS.Catagory_Entities.catagoryObj catObjDa)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[3];

                //Catgory parameters
                sqlParams[0] = new SqlParameter("@catagoryName", catObjDa.catagoryName);
                sqlParams[1] = new SqlParameter("@CatagoryDescription", catObjDa.catagoryDesc);
                sqlParams[2] = new SqlParameter("@catStatus", catObjDa.catagoryStatus);
                DataTable idcDt = DBHelper.ExecuteDataset(DBCommon.ConnectionString, "USP_INSERT_CATAGORY", sqlParams).Tables[0];
                int idCat = Convert.ToInt32(idcDt.Rows[0].ItemArray[0].ToString());

                if (idcDt.Rows.Count > 0)
                {
                    return idCat;
                }
                else
                {
                    return -1;
                }
            }
            catch(SqlException exc)
            {
                return -1;
                throw exc;
            }

        }

        #endregion

        #region IAdminDA Members


        public DataTable getCatgValues()
        {
            try
            {

               DataTable catDT = DBHelper.ExecuteDataset(DBCommon.ConnectionString, "USP_GET_CATAGORIES").Tables[0];

                if (catDT.Rows.Count > 0)
                {
                    return catDT;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IAdminDA Members


        public int insertItemDA(BusinessEntitiesBS.ItemEntities.ItemObj itemObjDa)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[7];

                //Catgory parameters
                sqlParams[0] = new SqlParameter("@itemName", itemObjDa.itemName);
                sqlParams[1] = new SqlParameter("@itemDescription", itemObjDa.itemDesc);
                sqlParams[2] = new SqlParameter("@catId", itemObjDa.itemCatagory);
                sqlParams[3] = new SqlParameter("@itemBR", itemObjDa.itemBR);
                sqlParams[4] = new SqlParameter("@itemNR", itemObjDa.itemNR);
                sqlParams[5] = new SqlParameter("@itemQty", itemObjDa.itemQty);
                sqlParams[6] = new SqlParameter("@itemSts", itemObjDa.itemStatus);

                DataTable idDt = DBHelper.ExecuteDataset(DBCommon.ConnectionString, "USP_INSERT_ITEMS", sqlParams).Tables[0];
                int returnedId = Convert.ToInt32(idDt.Rows[0].ItemArray[0].ToString());

               string imgPath = "~/ItemImages/" + itemObjDa.itemCatagory+"/"+returnedId+ "/" +returnedId+"small.jpg";

               SqlParameter[] imgSqlParams = new SqlParameter[2];
               imgSqlParams[0] = new SqlParameter("@itemID", returnedId);
               imgSqlParams[1] = new SqlParameter("@itemImagePath", imgPath);
               
                int retImgPth = DBHelper.ExecuteNonQuery(DBCommon.ConnectionString, "USP_INSERT_ITEM_IMAGEPATH", imgSqlParams);
              
                if (idDt.Rows.Count > 0 && retImgPth > 0)
                {
                    return returnedId;
                }
                else
                {
                    return -1;
                }
            }
            catch (SqlException exc)
            {
                return -1;
                throw exc;
            }
        }


        #endregion

        #region IAdminDA Members


        public DataTable getItemValues(int grpCatId)
        {
            try
            {
                SqlParameter[] grpCatIdParams = new SqlParameter[1];
                grpCatIdParams[0] = new SqlParameter("@catID", grpCatId);

                DataTable itemDT = DBHelper.ExecuteDataset(DBCommon.ConnectionString, "USP_GET_ITEMS", grpCatIdParams).Tables[0];

                if (itemDT.Rows.Count > 0)
                {
                    return itemDT;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IAdminDA Members


        public int insertGrpDA(BusinessEntitiesBS.GroupEntities.grpObj grpObjDa)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[7];

                //Catgory parameters
                sqlParams[0] = new SqlParameter("@grpName", grpObjDa.grpName);
                sqlParams[1] = new SqlParameter("@grpDesc", grpObjDa.grpDesc);
                sqlParams[2] = new SqlParameter("@grpSts", grpObjDa.grpStatus);
                sqlParams[3] = new SqlParameter("@grpBR", grpObjDa.grpBR);
                sqlParams[4] = new SqlParameter("@grpNR", grpObjDa.grpNR);
                sqlParams[5] = new SqlParameter("@grpChk", grpObjDa.fixedGrp);
                sqlParams[6] = new SqlParameter("@grpQty", grpObjDa.Quantity);

                DataTable idDt = DBHelper.ExecuteDataset(DBCommon.ConnectionString, "USP_INSERT_GROUP", sqlParams).Tables[0];
                int returnedId = Convert.ToInt32(idDt.Rows[0].ItemArray[0].ToString());

                

                SqlParameter[] grpSqlParams = new SqlParameter[3];
                grpSqlParams[0] = new SqlParameter("@grpID", returnedId);
                grpSqlParams[1] = new SqlParameter("@separator", ",");
                grpSqlParams[2] = new SqlParameter("@Array", grpObjDa.itemIdStr);

                int retItemGrp = DBHelper.ExecuteNonQuery(DBCommon.ConnectionString, "ParseArray", grpSqlParams);

                if (idDt.Rows.Count > 0 && retItemGrp == -1)
                {
                    return returnedId;
                }
                else
                {
                    return -1;
                }
            }
            catch (SqlException exc)
            {
                return -1;
                throw exc;
            }
        }

        #endregion
    }
}
