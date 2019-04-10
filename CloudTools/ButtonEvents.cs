using fcydata;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;

namespace CloudTools
{
    public class ButtonEvents : AbstractBillPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);//订单退回操作

            if (e.BarItemKey == "tbButton")
            {
                //定义获取表头信息对像
                var docScddIds1 = View.Model.DataObject;
                ////获取表头中单据编号信息
                var dhstr = docScddIds1["BillNo"].ToString(); //这里的BillNo为单据编号中"绑定实体属性"项中获得

                fcy.Service.CnnStr = "http://172.16.4.252/websys/service.asmx";
                fcy.Service.userdmstr = "feng";
                fcy.Service.passwordstr = "";
                var ab = new Service();

                var udhstr = ab.str("select udh from axsdd where dh='" + dhstr + "'");
                if (udhstr.Trim().Length > 0)
                {
                    ab.sqlcmd("delete uddzy where cOrderNo='" + udhstr + "'");
                    ab.sqlcmd("delete uOrder where cOrderNo='" + udhstr + "'");
                    fcy.FcyUdhPosts.ddht(udhstr);
                    View.ShowMessage($"{dhstr}已在U订货平台内删除");
                }
                else
                {
                    View.ShowMessage($"没有在U订货平台查找到{dhstr}单据记录,故没有执行删除操作");
                }
                //View.ShowMessage($"当前的单据编号为:{fBillNo}");

            }
        }
    }
}
