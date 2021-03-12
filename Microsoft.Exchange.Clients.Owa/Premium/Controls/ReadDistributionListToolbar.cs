using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FD RID: 1021
	public class ReadDistributionListToolbar : Toolbar
	{
		// Token: 0x06002546 RID: 9542 RVA: 0x000D797F File Offset: 0x000D5B7F
		internal ReadDistributionListToolbar(bool isInDeleteItems, DistributionList distributionList) : base(ToolbarType.Form)
		{
			this.isInDeleteItems = isInDeleteItems;
			this.distributionList = distributionList;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000D7998 File Offset: 0x000D5B98
		protected override void RenderButtons()
		{
			ToolbarButtonFlags flags = ItemUtility.UserCanDeleteItem(this.distributionList) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			ToolbarButtonFlags flags2 = ItemUtility.UserCanEditItem(this.distributionList) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			base.RenderButton(ToolbarButtons.NewMessageToDistributionList);
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderButton(ToolbarButtons.NewMeetingRequestToContact);
			}
			base.RenderButton(ToolbarButtons.Delete, flags);
			if (!this.isInDeleteItems)
			{
				base.RenderButton(ToolbarButtons.Flag, flags2);
			}
			base.RenderButton(ToolbarButtons.Categories, flags2);
		}

		// Token: 0x040019B2 RID: 6578
		private bool isInDeleteItems;

		// Token: 0x040019B3 RID: 6579
		private DistributionList distributionList;
	}
}
