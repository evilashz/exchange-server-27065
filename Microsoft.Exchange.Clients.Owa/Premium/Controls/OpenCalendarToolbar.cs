using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200036A RID: 874
	public class OpenCalendarToolbar : Toolbar
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x000BDB54 File Offset: 0x000BBD54
		internal OpenCalendarToolbar() : base("openCalendarTb", ToolbarType.Form)
		{
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000BDB62 File Offset: 0x000BBD62
		public override bool HasBigButton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000BDB65 File Offset: 0x000BBD65
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.ShowCalendar);
		}
	}
}
