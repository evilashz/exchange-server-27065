using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000072 RID: 114
	public class CalendarReminderSlab : SlabControl
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x00056570 File Offset: 0x00054770
		protected void Page_Load(object sender, EventArgs e)
		{
			foreach (object obj in Enum.GetValues(typeof(CalendarReminder)))
			{
				CalendarReminder calendarReminder = (CalendarReminder)obj;
				ListItemCollection items = this.ddlDefaultReminderTime.Items;
				string text = LocalizedDescriptionAttribute.FromEnum(typeof(CalendarReminder), calendarReminder);
				int num = (int)calendarReminder;
				items.Add(new ListItem(text, num.ToString()));
			}
		}

		// Token: 0x04001B4D RID: 6989
		protected DropDownList ddlDefaultReminderTime;
	}
}
