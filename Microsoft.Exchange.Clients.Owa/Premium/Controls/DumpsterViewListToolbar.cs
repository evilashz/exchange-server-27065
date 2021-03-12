using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000361 RID: 865
	public sealed class DumpsterViewListToolbar : ViewListToolbar
	{
		// Token: 0x0600208B RID: 8331 RVA: 0x000BC996 File Offset: 0x000BAB96
		public DumpsterViewListToolbar() : base(true, ReadingPanePosition.Off)
		{
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000BC9A0 File Offset: 0x000BABA0
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.Recover);
			base.RenderDivider();
			base.RenderButton(ToolbarButtons.Purge);
		}
	}
}
