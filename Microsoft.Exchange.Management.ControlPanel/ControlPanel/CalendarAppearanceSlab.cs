using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200005C RID: 92
	public class CalendarAppearanceSlab : SlabControl
	{
		// Token: 0x06001A23 RID: 6691 RVA: 0x00053D02 File Offset: 0x00051F02
		protected void Page_Load(object sender, EventArgs e)
		{
			this.FillWeekDaysDropDownList();
			this.FillFirstWeekOfYearDropDownList();
			this.FillStartTimeDropDownList();
			this.FillEndTimeDropDownList();
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00053D1C File Offset: 0x00051F1C
		private void FillFirstWeekOfYearDropDownList()
		{
			Array values = Enum.GetValues(typeof(FirstWeekRules));
			foreach (object obj in values)
			{
				FirstWeekRules firstWeekRules = (FirstWeekRules)obj;
				if (firstWeekRules != FirstWeekRules.LegacyNotSet)
				{
					ListItemCollection items = this.ddlFirstWeekOfYear.Items;
					string text = LocalizedDescriptionAttribute.FromEnum(typeof(FirstWeekRules), firstWeekRules);
					int num = (int)firstWeekRules;
					items.Add(new ListItem(text, num.ToString()));
				}
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00053DB4 File Offset: 0x00051FB4
		private void FillWeekDaysDropDownList()
		{
			Array values = Enum.GetValues(typeof(Microsoft.Exchange.Data.Storage.Management.DayOfWeek));
			foreach (object obj in values)
			{
				Microsoft.Exchange.Data.Storage.Management.DayOfWeek dayOfWeek = (Microsoft.Exchange.Data.Storage.Management.DayOfWeek)obj;
				ListItemCollection items = this.ddlWeekStartDay.Items;
				string text = LocalizedDescriptionAttribute.FromEnum(typeof(Microsoft.Exchange.Data.Storage.Management.DayOfWeek), dayOfWeek);
				int num = (int)dayOfWeek;
				items.Add(new ListItem(text, num.ToString()));
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00053E48 File Offset: 0x00052048
		private void FillStartTimeDropDownList()
		{
			int num = 0;
			int num2 = 1410;
			for (int i = num; i <= num2; i += 30)
			{
				DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)i);
				this.ddlWorkingHoursStartTime.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00053EB4 File Offset: 0x000520B4
		private void FillEndTimeDropDownList()
		{
			int num = 30;
			int num2 = 1410;
			for (int i = num; i <= num2; i += 30)
			{
				DateTime dateTime = DateTime.UtcNow.Date + TimeSpan.FromMinutes((double)i);
				this.ddlWorkingHoursEndTime.Items.Add(new ListItem(dateTime.ToString(RbacPrincipal.Current.TimeFormat), i.ToString()));
			}
			DateTime dateTime2 = DateTime.UtcNow.Date + TimeSpan.FromMinutes(1439.0);
			this.ddlWorkingHoursEndTime.Items.Add(new ListItem(dateTime2.ToString(RbacPrincipal.Current.TimeFormat), "1439"));
		}

		// Token: 0x04001B08 RID: 6920
		protected DropDownList ddlWeekStartDay;

		// Token: 0x04001B09 RID: 6921
		protected DropDownList ddlFirstWeekOfYear;

		// Token: 0x04001B0A RID: 6922
		protected DropDownList ddlWorkingHoursStartTime;

		// Token: 0x04001B0B RID: 6923
		protected DropDownList ddlWorkingHoursEndTime;
	}
}
