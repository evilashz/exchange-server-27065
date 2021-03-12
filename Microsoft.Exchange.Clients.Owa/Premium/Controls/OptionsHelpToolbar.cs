using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003D8 RID: 984
	public class OptionsHelpToolbar : Toolbar
	{
		// Token: 0x06002457 RID: 9303 RVA: 0x000D37F4 File Offset: 0x000D19F4
		public OptionsHelpToolbar() : base("tblTBH")
		{
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000D3801 File Offset: 0x000D1A01
		protected override void RenderButtons()
		{
			base.RenderHelpButton(HelpIdsLight.OptionsLight.ToString(), string.Empty, true);
		}
	}
}
