using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.IO;
using System.Drawing;
using InterfacesBS.InterfacesBL;
using BusinessLogicBS;
using BusinessLogicBS.BusinessClasses;

namespace budhashop.ADMIN
{
    public partial class InsertPage : System.Web.UI.Page
    {
        public DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getcatgs();
                dt = getItems(Int32.Parse(grpCatDDL.SelectedValue.ToString()));
                if (dt != null)
                {
                    itemGrid.DataSource = dt;
                    itemGrid.DataBind();
                }
                else
                {
                    grpMsgLbl.Text = "No Data for items! change the catagory";
                }
            }




        }
        protected void getcatgs()
        {
            try
            {
                IAdmin getCats = new AdminItems();
                DataTable catgDt = getCats.getCatagories();

                CatagoryDDL.DataSource = catgDt;
                CatagoryDDL.DataTextField = "CategoryName";
                CatagoryDDL.DataValueField = "CategoryId";
                CatagoryDDL.DataBind();

                grpCatDDL.DataSource = catgDt;
                grpCatDDL.DataTextField = "CategoryName";
                grpCatDDL.DataValueField = "CategoryId";
                grpCatDDL.DataBind();

            }
            catch (Exception cExp)
            {
                ItemMessageLbl.Text = "Error occured" + cExp.Message;
            }


        }
        protected DataTable getItems(int grpCatId)
        {


            try
            {

                IAdmin getItems = new AdminItems();
                DataTable itemDT = getItems.getItems(grpCatId);

                return itemDT;
            }
            catch (Exception iExp)
            {

                grpMsgLbl.Text = "Error Occured" + iExp.Message;
                return null;
            }


        }


        protected void CatagoryBtn_Click(object sender, EventArgs e)
        {
            string catName = CatagoryTxt.Text;
            string catDesc = catDescTxt.InnerText.ToString();
            bool catStatus = catStatusCb.Checked;



            BusinessEntitiesBS.Catagory_Entities.catagoryObj catObj = new BusinessEntitiesBS.Catagory_Entities.catagoryObj();

            catObj.catagoryName = catName;
            catObj.catagoryDesc = catDesc;
            catObj.catagoryStatus = catStatus;

            try
            {

                IAdmin catInsert = new AdminItems();

                int insertCatChk = catInsert.insertCatagory(catObj);
                if (insertCatChk != -1)
                {
                    string NewDir = Server.MapPath("~/ItemImages/" + insertCatChk);
                    try
                    {
                        // Check if directory exists
                        if (!Directory.Exists(NewDir))
                        {
                            // Create the directory.
                            Directory.CreateDirectory(NewDir);
                            catMsgLbl.Text = "Catagory inserted";

                        }
                    }
                    catch (IOException _ex)
                    {
                        catMsgLbl.Text = "Error: Floder" + _ex.Message;
                    }



                }
                else
                {
                    catMsgLbl.Text = "Catagory not inserted";
                    catMsgLbl.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exp)
            {
                //throw exp;
                catMsgLbl.Text = "Unable to connect" + exp.Message;
                catMsgLbl.ForeColor = System.Drawing.Color.Red;
            }


        }

        protected void ItemBtn_Click(object sender, EventArgs e)
        {
            bool resFU = checkPhoto(itemImageFU);
            if (resFU)
            {
                try
                {

                    BusinessEntitiesBS.ItemEntities.ItemObj itemValues = new BusinessEntitiesBS.ItemEntities.ItemObj();

                    itemValues.itemName = ItemTxt.Text;
                    itemValues.itemDesc = descTxt.InnerText.ToString();
                    itemValues.itemCatagory = Int32.Parse(CatagoryDDL.SelectedValue);

                    itemValues.itemImagePath = itemImageFU.FileName.ToString();

                    itemValues.itemQty = Int32.Parse(qtyTxt.Text);
                    itemValues.itemBR = float.Parse(brateTxt.Text);
                    itemValues.itemNR = float.Parse(netRateTxt.Text);
                    itemValues.itemStatus = CheckBoxSts.Checked;

                    IAdmin insertItem = new AdminItems();
                    int itemId = insertItem.insertItems(itemValues);
                    if (itemId != -1)
                    {
                        //create folder for item images and save images. show result
                        string NewDir = Server.MapPath("~/ItemImages/" + Int32.Parse(CatagoryDDL.SelectedValue) + "/" + itemId);
                        try
                        {
                            // Check if directory exists
                            if (!Directory.Exists(NewDir))
                            {
                                // Create the directory.
                                Directory.CreateDirectory(NewDir);

                            }
                        }
                        catch (IOException _ex)
                        {
                            ItemMessageLbl.Text = "Error: Floder" + _ex.Message;
                        }

                        //string filename = Path.GetFileName(PhotoFU.FileName);
                        string filename = itemId + "Photo.jpg";
                        itemImageFU.SaveAs(Server.MapPath("~/ItemImages/" + Int32.Parse(CatagoryDDL.SelectedValue) + "/" + itemId + "/") + filename);

                        string filePath = Server.MapPath("~/ItemImages/" + Int32.Parse(CatagoryDDL.SelectedValue) + "/" + itemId + "/") + filename;
                        string newfileMed = itemId + "Photomedium.jpg";
                        string newfileSmall = itemId + "small.jpg";
                        string resizedImageMed = Server.MapPath("~/ItemImages/" + Int32.Parse(CatagoryDDL.SelectedValue) + "/" + itemId + "/") + newfileMed;
                        string resizedImageSmall = Server.MapPath("~/ItemImages/" + Int32.Parse(CatagoryDDL.SelectedValue) + "/" + itemId + "/") + newfileSmall;
                        System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);

                        System.Drawing.Bitmap bmpD = img as Bitmap;

                        Bitmap bmpDriverMed = new Bitmap(bmpD, 512, 372);
                        bmpDriverMed.Save(resizedImageMed, System.Drawing.Imaging.ImageFormat.Jpeg);

                        Bitmap bmpDriverSmall = new Bitmap(bmpD, 170, 126);
                        bmpDriverSmall.Save(resizedImageSmall, System.Drawing.Imaging.ImageFormat.Jpeg);

                        ItemMessageLbl.Text = "Item inserted sucessfully";

                    }
                    else
                    {
                        ItemMessageLbl.Text = "Error: unable to add item. Database Error!";
                    }
                }
                catch (Exception iExp)
                {
                    ItemMessageLbl.Text = "Error: " + iExp.Message;
                }

            }



        }

        protected bool checkPhoto(FileUpload imgFU)
        {
            System.Drawing.Image img = null;//System.Drawing.Image.FromStream(PhotoFU.PostedFile.InputStream);


            //Image uploadedImage = null;
            if (imgFU.HasFile && imgFU.FileName != string.Empty && imgFU.FileContent.Length > 0)
            {

                if (imgFU.FileContent.Length <= 1024000)
                {
                    try
                    {
                        img = System.Drawing.Image.FromStream(imgFU.PostedFile.InputStream);
                        if (img.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                            img.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid ||
                            img.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                        {
                            return true;
                        }
                        else
                        {
                            ItemMessageLbl.Text = "Selected file is not an image";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ItemMessageLbl.Text = "Selected file is not an image.<br />" + ex.Message;
                        return false;
                    }

                }
                else
                {
                    ItemMessageLbl.Text = "File is too Large. only 1mb allowed";
                    return false;

                }

            }
            else return false;


        }

        protected void grpCatDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grpCatId = Int32.Parse(grpCatDDL.SelectedValue.ToString());
            DataTable grpCatItemsDT = getItems(grpCatId);
            if (grpCatItemsDT != null)
            {
                itemGrid.DataSource = grpCatItemsDT;
                dt = new DataTable();
                dt = grpCatItemsDT;
                itemGrid.DataBind();
            }
            else
            {
                grpMsgLbl.Text = "No Data for items! change the catagory";
            }

        }


        protected void itemGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            itemGrid.PageIndex = e.NewPageIndex;
            //itemGrid.DataSource = dt;
            //itemGrid.DataBind();
        }

        protected void grpSubmitBtn_Click(object sender, EventArgs e)
        {

           
            bool flagItem;
            
            DataTable grpItemsDt = new DataTable();
            DataColumn dc = new DataColumn("GrpItem", typeof(int));
            grpItemsDt.Columns.Add(dc);
          
            
            foreach (GridViewRow dRow in itemGrid.Rows)
            {

             flagItem = (dRow.FindControl("itemChkBox") as CheckBox).Checked;
                 
                if (flagItem)
                 {
                     int itemId = Convert.ToInt32(itemGrid.DataKeys[dRow.RowIndex].Value);
                     
                     DataRow dr = grpItemsDt.NewRow();
                     dr[0] = itemId;

                     grpItemsDt.Rows.Add(dr);
                     
                 }
               
            }

            string grpItemString = "";
            bool first = true;
            if (grpItemsDt.Rows.Count > 0)
            {
                foreach (DataRow iRow in grpItemsDt.Rows)
                {
                    if (first)
                    {
                        grpItemString += int.Parse(iRow["GrpItem"].ToString());
                        first = false;
                    }
                    else
                    {
                        grpItemString += "," + int.Parse(iRow["GrpItem"].ToString());
                    }

                }

                BusinessEntitiesBS.GroupEntities.grpObj grpValus = new BusinessEntitiesBS.GroupEntities.grpObj();

                grpValus.grpName = grpNameTxt.Text.ToString();
                grpValus.grpDesc = grpDescTxt.Text.ToString();
                grpValus.grpBR =  float.Parse(grpBrTxt.Text.ToString());
                grpValus.grpNR = float.Parse(grpNrTxt.Text.ToString());
                grpValus.grpStatus = grpCb.Checked;
                grpValus.fixedGrp = grpStatusCb.Checked;
                grpValus.Quantity = Int32.Parse(grpQtyTxt.Text.ToString());
                //item string
                grpValus.itemIdStr = grpItemString;

                IAdmin insertGroup = new AdminItems();
                int grpId = insertGroup.insertGroup(grpValus);
                if (grpId != -1)
                {
                    grpMsgLbl.Text = "Group Inserted";
                }
                else
                {
                    grpMsgLbl.Text = "Error Occured! Group not inserted";
                }
            }
            else
            {
                grpMsgLbl.Text = "Select items for the group";
            }

            



        }

       


    }
}
