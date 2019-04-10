using fcydata;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;

namespace CloudDeveopTest
{
    public class thEvents : AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);//订单退回操作

            if (e.BarItemKey == "helloWorld")
            {
                //定义获取表头信息对像
                var docScddIds1 = View.Model.DataObject;
                ////获取表头中单据编号信息
                string dhstr = docScddIds1["BillNo"].ToString(); //这里的BillNo为单据编号中"绑定实体属性"项中获得

                fcydata.Service.CnnStr = "http://172.16.4.252/websys/service.asmx";
                fcydata.Service.userdmstr = "feng";
                fcydata.Service.passwordstr = "";
                fcydata.Service ab = new Service();
              string ret=  fcy.FcyUdhPosts.tkddel(dhstr);
            
                View.ShowMessage(ret);
            }
        }
    }
}
 