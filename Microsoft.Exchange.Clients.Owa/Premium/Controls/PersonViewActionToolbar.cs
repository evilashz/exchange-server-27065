using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003DE RID: 990
	internal class PersonViewActionToolbar : ViewActionToolbar
	{
		// Token: 0x06002467 RID: 9319 RVA: 0x000D3B84 File Offset: 0x000D1D84
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.NewMessageToContacts);
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderButton(ToolbarButtons.SendATextMessage);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderButton(ToolbarButtons.NewMeetingRequestToContacts);
			}
			base.RenderButton(ToolbarButtons.ForwardAsAttachment);
		}
	}
}
