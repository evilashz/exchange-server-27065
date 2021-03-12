using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200032E RID: 814
	internal sealed class CalendarTimeZoneToolbar : Toolbar
	{
		// Token: 0x06001EF3 RID: 7923 RVA: 0x000B1A1E File Offset: 0x000AFC1E
		public CalendarTimeZoneToolbar() : base("divTimeZoneTBL")
		{
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000B1A2B File Offset: 0x000AFC2B
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.TimeZoneDropDown);
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x000B1A38 File Offset: 0x000AFC38
		public override bool IsRightAligned
		{
			get
			{
				return true;
			}
		}
	}
}
