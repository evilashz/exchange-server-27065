using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FA RID: 1018
	internal sealed class PublishedCalendarViewToolbar : Toolbar
	{
		// Token: 0x0600253E RID: 9534 RVA: 0x000D77CB File Offset: 0x000D59CB
		public PublishedCalendarViewToolbar() : base("divTBL")
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000D77D8 File Offset: 0x000D59D8
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.Today);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.DayView);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.WeekView);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.MonthView, ToolbarButtonFlags.Pressed);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.Subscribe);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.PrintCalendar, ToolbarButtonFlags.None);
			base.RenderFloatedSpacer(1, "divMeasure");
		}
	}
}
