using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000400 RID: 1024
	public class ReadPublishInvitionMessageToolbar : Toolbar
	{
		// Token: 0x0600254F RID: 9551 RVA: 0x000D8020 File Offset: 0x000D6220
		internal ReadPublishInvitionMessageToolbar(bool hideViewButton, bool disableSubscribeButton) : base("divShareToolbar", ToolbarType.Form)
		{
			this.hideViewButton = hideViewButton;
			this.disableSubscribeButton = disableSubscribeButton;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000D803C File Offset: 0x000D623C
		protected override void RenderButtons()
		{
			if (CalendarUtilities.CanSubscribeInternetCalendar())
			{
				base.RenderButton(ToolbarButtons.SubscribeToThisCalendar, this.disableSubscribeButton ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None);
			}
			if (!this.hideViewButton)
			{
				base.RenderButton(ToolbarButtons.ViewThisCalendar);
			}
		}

		// Token: 0x040019C1 RID: 6593
		private bool hideViewButton;

		// Token: 0x040019C2 RID: 6594
		private bool disableSubscribeButton;
	}
}
