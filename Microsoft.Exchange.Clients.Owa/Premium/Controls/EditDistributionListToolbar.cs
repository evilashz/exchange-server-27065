using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000366 RID: 870
	public class EditDistributionListToolbar : Toolbar
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x000BCFCE File Offset: 0x000BB1CE
		internal EditDistributionListToolbar(Item item) : base(ToolbarType.View)
		{
			this.item = item;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000BCFE0 File Offset: 0x000BB1E0
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
			base.RenderButton(ToolbarButtons.NewMessageToDistributionList);
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderButton(ToolbarButtons.NewMeetingRequestToContact);
			}
			base.RenderButton(ToolbarButtons.Delete, flags2);
			base.RenderButton(ToolbarButtons.Flag, flags);
			base.RenderButton(ToolbarButtons.Categories, flags);
		}

		// Token: 0x0400177F RID: 6015
		private Item item;
	}
}
