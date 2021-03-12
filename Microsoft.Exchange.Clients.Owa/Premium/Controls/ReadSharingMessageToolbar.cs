using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000401 RID: 1025
	public class ReadSharingMessageToolbar : Toolbar
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x000D8073 File Offset: 0x000D6273
		internal ReadSharingMessageToolbar(SharingMessageType sharingMessageType, bool disableSharingButton) : base("divShareToolbar", ToolbarType.Form)
		{
			this.sharingMessageType = sharingMessageType;
			this.disableSharingButton = disableSharingButton;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000D8090 File Offset: 0x000D6290
		protected override void RenderButtons()
		{
			ToolbarButtonFlags flags = this.disableSharingButton ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None;
			if (this.sharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				base.RenderButton(ToolbarButtons.AddThisCalendar, flags);
			}
			if (this.sharingMessageType.IsRequest)
			{
				base.RenderButton(ToolbarButtons.SharingMyCalendar, flags);
			}
		}

		// Token: 0x040019C3 RID: 6595
		internal const string Id = "divShareToolbar";

		// Token: 0x040019C4 RID: 6596
		private SharingMessageType sharingMessageType;

		// Token: 0x040019C5 RID: 6597
		private bool disableSharingButton;
	}
}
