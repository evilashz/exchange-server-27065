using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200030B RID: 779
	public sealed class AddressBookViewActionToolbar : ViewActionToolbar
	{
		// Token: 0x06001D85 RID: 7557 RVA: 0x000AB460 File Offset: 0x000A9660
		protected override void RenderButtons()
		{
			base.Writer.Write("<div class=\"tbNoBreakDiv\">");
			base.RenderButton(ToolbarButtons.NewMessageToContacts);
			if (base.UserContext.IsInstantMessageEnabled())
			{
				base.RenderInstantMessageButtons();
			}
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderButton(ToolbarButtons.SendATextMessage);
			}
			base.Writer.Write("</div>");
			base.RenderHelpButton(HelpIdsLight.AddressBookLight.ToString(), string.Empty, true);
		}
	}
}
