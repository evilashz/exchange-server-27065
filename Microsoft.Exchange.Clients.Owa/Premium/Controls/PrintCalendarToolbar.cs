using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003E7 RID: 999
	internal sealed class PrintCalendarToolbar : Toolbar
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000D4300 File Offset: 0x000D2500
		public PrintCalendarToolbar(CalendarViewType viewType) : base("divTBL")
		{
			this.viewType = viewType;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000D4314 File Offset: 0x000D2514
		protected override void RenderButtons()
		{
			ToolbarButtonFlags toolbarButtonFlags = ToolbarButtonFlags.Text;
			ToolbarButtonFlags toolbarButtonFlags2 = ToolbarButtonFlags.Pressed;
			base.RenderButton(ToolbarButtons.PrintCalendarLabel);
			base.RenderFloatedSpacer(20);
			base.RenderButton(ToolbarButtons.PrintDailyView, (this.viewType == CalendarViewType.Min) ? (toolbarButtonFlags | toolbarButtonFlags2) : toolbarButtonFlags);
			base.RenderFloatedSpacer(20);
			base.RenderButton(ToolbarButtons.PrintWeeklyView, (this.viewType == CalendarViewType.Weekly || this.viewType == CalendarViewType.WorkWeek) ? (toolbarButtonFlags | toolbarButtonFlags2) : toolbarButtonFlags);
			base.RenderFloatedSpacer(20);
			base.RenderButton(ToolbarButtons.PrintMonthlyView, (this.viewType == CalendarViewType.Monthly) ? (toolbarButtonFlags | toolbarButtonFlags2) : toolbarButtonFlags);
		}

		// Token: 0x04001967 RID: 6503
		private const int SpaceBetweenButtons = 20;

		// Token: 0x04001968 RID: 6504
		private readonly CalendarViewType viewType;
	}
}
