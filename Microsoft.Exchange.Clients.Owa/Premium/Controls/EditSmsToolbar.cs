using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000370 RID: 880
	public class EditSmsToolbar : Toolbar
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x000BE0D3 File Offset: 0x000BC2D3
		internal EditSmsToolbar() : base(ToolbarType.Form)
		{
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000BE0DC File Offset: 0x000BC2DC
		protected override void RenderButtons()
		{
			BrowserType browserType = base.UserContext.BrowserType;
			base.RenderHelpButton(HelpIdsLight.DefaultLight.ToString(), string.Empty);
			base.RenderButton(ToolbarButtons.SendSms);
			base.RenderButton(ToolbarButtons.SaveImageOnly);
			base.RenderButton(ToolbarButtons.AddressBook);
			base.RenderButton(ToolbarButtons.CheckNames);
		}
	}
}
