using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020002FE RID: 766
	public class AddContactToolbar : Toolbar
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x000A7152 File Offset: 0x000A5352
		internal AddContactToolbar() : base(ToolbarType.AddContact)
		{
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000A715B File Offset: 0x000A535B
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.InviteContact);
			base.RenderButton(ToolbarButtons.AddressBook);
			base.RenderButton(ToolbarButtons.CheckNames);
		}
	}
}
