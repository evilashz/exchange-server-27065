using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BB RID: 955
	public sealed class MessageViewListToolbar : ViewListToolbar
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000CDEA1 File Offset: 0x000CC0A1
		public MessageViewListToolbar(bool isMultiLine, bool isPublicFolder, bool isOthersFolder, bool isSearchFolder, bool isWebPart, string folderClass, ReadingPanePosition readingPanePosition) : base(isMultiLine, readingPanePosition)
		{
			this.isPublicFolder = isPublicFolder;
			this.isWebPart = isWebPart;
			this.isSearchFolder = isSearchFolder;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000CDEC4 File Offset: 0x000CC0C4
		protected override void RenderButtons()
		{
			if (!this.isPublicFolder)
			{
				base.RenderButtons(ToolbarButtons.NewMessageCombo, new ToolbarButton[0]);
				return;
			}
			if (this.isWebPart)
			{
				base.RenderButtons(ToolbarButtons.NewWithPostIcon, new ToolbarButton[0]);
				return;
			}
			base.RenderButtons(ToolbarButtons.NewWithPostIcon, new ToolbarButton[]
			{
				ToolbarButtons.SearchInPublicFolder
			});
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000CDF20 File Offset: 0x000CC120
		protected override void RenderNewMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.NewMessage);
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderMenuItem(ToolbarButtons.NewSms);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderMenuItem(ToolbarButtons.NewAppointment);
				base.RenderMenuItem(ToolbarButtons.NewMeetingRequest);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Contacts))
			{
				base.RenderMenuItem(ToolbarButtons.NewContact);
				base.RenderMenuItem(ToolbarButtons.NewContactDistributionList);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Tasks))
			{
				base.RenderMenuItem(ToolbarButtons.NewTask);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.PublicFolders))
			{
				base.RenderMenuItem(ToolbarButtons.NewPost, this.isSearchFolder);
			}
		}

		// Token: 0x040018D7 RID: 6359
		private readonly bool isPublicFolder;

		// Token: 0x040018D8 RID: 6360
		private readonly bool isWebPart;

		// Token: 0x040018D9 RID: 6361
		private readonly bool isSearchFolder;
	}
}
