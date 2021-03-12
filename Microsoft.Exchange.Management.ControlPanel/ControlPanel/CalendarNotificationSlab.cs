using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047A RID: 1146
	public class CalendarNotificationSlab : SlabControl
	{
		// Token: 0x06003995 RID: 14741 RVA: 0x000AF1C0 File Offset: 0x000AD3C0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			int num = 0;
			int num2 = 1410;
			for (int i = num; i <= num2; i += 30)
			{
				DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)i);
				this.ddlDailyAgendaNotificationSendTime.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
			}
			for (int j = 1; j <= 7; j++)
			{
				this.ddlCalendarUpdateNextDays.Items.Add(new ListItem(j.ToString(), j.ToString()));
			}
			this.chkCalendarUpdate.Text = string.Format(this.chkCalendarUpdate.Text, "<span id=\"spnNextDays\"></span>&nbsp;");
		}

		// Token: 0x040026B4 RID: 9908
		protected DropDownList ddlDailyAgendaNotificationSendTime;

		// Token: 0x040026B5 RID: 9909
		protected DropDownList ddlCalendarUpdateNextDays;

		// Token: 0x040026B6 RID: 9910
		protected CheckBox chkCalendarUpdate;
	}
}
