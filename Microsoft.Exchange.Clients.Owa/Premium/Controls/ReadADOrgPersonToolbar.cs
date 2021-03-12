using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FB RID: 1019
	public class ReadADOrgPersonToolbar : Toolbar
	{
		// Token: 0x06002540 RID: 9536 RVA: 0x000D7859 File Offset: 0x000D5A59
		internal ReadADOrgPersonToolbar() : base(ToolbarType.Form)
		{
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000D7862 File Offset: 0x000D5A62
		protected override void RenderButtons()
		{
			if (base.UserContext.IsInstantMessageEnabled())
			{
				base.RenderInstantMessageButtons();
			}
		}
	}
}
