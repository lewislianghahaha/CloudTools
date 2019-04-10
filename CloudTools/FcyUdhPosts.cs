using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace fcy
{
     public  class FcyUdhPosts
    {
       
        
   
        public static string tkddel(string cOutSysKey)
        {
            string ret = "";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cOutSysKey", cOutSysKey);
            ret = FcyWeb.Post("/ws/Payments/delRefund", param);
            return ret;
        }
        public static string zfddel(string cOutSysKey)
        {
            string ret = "";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cOutSysKey", cOutSysKey);
            ret = FcyWeb.Post("/ws/Payments/delPayment", param);
            return ret;
        }

        public static string xsfhdelup(string cOutSysKey)
        {
            string ret = "";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cOutSysKey", cOutSysKey);
            ret = FcyWeb.Post("/ws/Orders/delDelivery", param);
            return ret;
        }
        public static string ddht(string dh)
        {
            string ret = "";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("orderno", dh);
            ret = FcyWeb.Post("/ws/Orders/orderConfirmBackApi", param);
            return ret;
        }

        public static string ysdup(DataRow row)
        {
            string ret = "";
            fcydata.Service ab = new fcydata.Service();
            if (row["je"].ToString().Trim().IndexOf("-") > 0)
            {
                row["je"] = -(decimal)row["je"];
                MessageBox.Show(row["je"].ToString());
                Dictionary<string, string> param = new Dictionary<string, string>();

                DateTime date = (DateTime)row["rq"];
                string strs = "{'iAmount':<je>,'cOutSysKey':'<dh>','oAgent':{'cCode':'<khbh>','cOutSysKey':'<khbh>'},'oSettlementWay':{'cErpCode':'<jsfs>'},'iPayMentStatusCode':2,'cRefundPayDirection': 'TOUDH','dPayFinishDate':'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','dReceiptDate':'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','remark':'<memo>'}";

                for (int j = 0; j < row.Table.Columns.Count; j++)
                {
                    strs = strs.Replace("<" + row.Table.Columns[j].ColumnName + ">", row[j].ToString().Trim());
                }
                param.Add("refund", strs);
                ret = FcyWeb.Post("/ws/Payments/saveRefund", param);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(ret);
                if (fcydata.FcyXml.GetNodeVal(xmldoc.DocumentElement, "data") != "")
                {
                    MessageBox.Show(ret);
                }

           

            }
            else
            {
                Dictionary<string, string> param = new Dictionary<string, string>();

                DateTime date = (DateTime)row["rq"];
                string strs = "{'iAmount':<je>,'cOutSysKey':'<dh>','oAgent':{'cErpCode':'<khbh>'},'oSettlementWay':{'cErpCode':'<jsfs>'},'cVoucherType':'NORMAL','iPayMentStatusCode':2,'dPayFinishDate':'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','dReceiptDate':'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','remark':'<memo>'}";

                for (int j = 0; j < row.Table.Columns.Count; j++)
                {
                    strs = strs.Replace("<" + row.Table.Columns[j].ColumnName + ">", row[j].ToString().Trim());
                }
                param.Add("payment", strs);

                ret = FcyWeb.Post("/ws/Payments/savePayment", param);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(ret);
                if (fcydata.FcyXml.GetNodeVal(xmldoc.DocumentElement, "data") != "")
                {
                    MessageBox.Show(ret);
                }
            }
            return ret;
        }
    }


}
