using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000365 RID: 869
	public class EditContactToolbar : Toolbar
	{
		// Token: 0x0600209F RID: 8351 RVA: 0x000BCF2F File Offset: 0x000BB12F
		internal EditContactToolbar(Item item) : base(ToolbarType.View)
		{
			this.item = item;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000BCF40 File Offset: 0x000BB140
		protected override void RenderButtons()
		{
			ToolbarButtonFlags flags = ToolbarButtonFlags.None;
			if (this.item != null && !ItemUtility.UserCanEditItem(this.item))
			{
				flags = ToolbarButtonFlags.Disabled;
			}
			ToolbarButtonFlags flags2 = ToolbarButtonFlags.None;
			if (this.item != null && !ItemUtility.UserCanDeleteItem(this.item))
			{
				flags2 = ToolbarButtonFlags.Disabled;
			}
			base.RenderButton(ToolbarButtons.SaveAndClose, flags);
			base.RenderButton(ToolbarButtons.NewMessageToContact);
			base.RenderButton(ToolbarButtons.Delete, flags2);
			base.RenderButton(ToolbarButtons.AttachFile, flags);
			base.RenderButton(ToolbarButtons.Flag, flags);
			base.RenderButton(ToolbarButtons.Categories, flags);
		}

		// Token: 0x0400177E RID: 6014
		private Item item;
	}
}
