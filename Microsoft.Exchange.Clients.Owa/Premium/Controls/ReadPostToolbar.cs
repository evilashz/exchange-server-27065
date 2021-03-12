using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FF RID: 1023
	public class ReadPostToolbar : Toolbar
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x000D7F18 File Offset: 0x000D6118
		internal ReadPostToolbar(bool isEmbeddedItem, Item item) : base(ToolbarType.Form)
		{
			this.item = item;
			this.isEmbeddedItem = isEmbeddedItem;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000D7F30 File Offset: 0x000D6130
		protected override void RenderButtons()
		{
			base.RenderHelpButton(HelpIdsLight.DefaultLight.ToString(), string.Empty);
			base.RenderButton(ToolbarButtons.PostReply);
			base.RenderButton(ToolbarButtons.Reply);
			base.RenderButton(ToolbarButtons.Forward);
			bool flag = ItemUtility.UserCanEditItem(this.item);
			ToolbarButtonFlags flags = (flag && !this.isEmbeddedItem) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			base.RenderButton(ToolbarButtons.Flag, flags);
			base.RenderButton(ToolbarButtons.Categories, flags);
			base.RenderButton(ToolbarButtons.Print);
			bool flag2 = ItemUtility.UserCanDeleteItem(this.item);
			ToolbarButtonFlags flags2 = (flag2 && !this.isEmbeddedItem) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			base.RenderButton(ToolbarButtons.Delete, flags2);
			ToolbarButtonFlags toolbarButtonFlags = this.isEmbeddedItem ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None;
			base.RenderButton(ToolbarButtons.Move, toolbarButtonFlags);
			ToolbarButtonFlags flags3 = toolbarButtonFlags;
			base.RenderButton(ToolbarButtons.Previous, flags3);
			base.RenderButton(ToolbarButtons.Next, flags3);
		}

		// Token: 0x040019BF RID: 6591
		private Item item;

		// Token: 0x040019C0 RID: 6592
		private bool isEmbeddedItem;
	}
}
