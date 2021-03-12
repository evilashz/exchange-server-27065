using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200030F RID: 783
	public sealed class AddressBookViewListToolbar : ViewListToolbar
	{
		// Token: 0x06001D94 RID: 7572 RVA: 0x000AC229 File Offset: 0x000AA429
		public AddressBookViewListToolbar(bool isMultiLine, ReadingPanePosition readingPanePosition) : base(isMultiLine, readingPanePosition)
		{
			this.readingPanePosition = readingPanePosition;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000AC23A File Offset: 0x000AA43A
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.ReadingPaneOffSwap, (this.readingPanePosition == ReadingPanePosition.Off) ? ToolbarButtonFlags.Hidden : ToolbarButtonFlags.Image);
			base.RenderButton(ToolbarButtons.ReadingPaneRightSwap, (this.readingPanePosition == ReadingPanePosition.Right) ? ToolbarButtonFlags.Hidden : ToolbarButtonFlags.Image);
		}

		// Token: 0x04001622 RID: 5666
		private ReadingPanePosition readingPanePosition;
	}
}
