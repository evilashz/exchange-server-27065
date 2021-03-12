using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B3 RID: 947
	public class MessageItemManageToolbar : Toolbar
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x000CCB98 File Offset: 0x000CAD98
		public MessageItemManageToolbar(bool isPublicFolder, bool allowConversationView, bool includeEmptyFolderButton, bool isMultiLine, bool isOthersFolder, bool isWebPart, string folderClass, ReadingPanePosition readingPanePosition, bool isConversationView, bool isNewestOnTop, bool isShowTree, bool isDeletedItems, bool isJunkEmail) : base("divMsgItemTB")
		{
			this.isPublicFolder = isPublicFolder;
			this.allowConversationView = allowConversationView;
			this.includeEmptyFolderButton = includeEmptyFolderButton;
			this.isWebPart = isWebPart;
			this.folderClass = folderClass;
			this.isOthersFolder = isOthersFolder;
			this.readingPanePosition = readingPanePosition;
			this.isDeletedItems = isDeletedItems;
			this.isJunkEmail = isJunkEmail;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000CCBF5 File Offset: 0x000CADF5
		protected void RenderNewMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.NewMessage);
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderMenuItem(ToolbarButtons.NewSms);
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderMenuItem(ToolbarButtons.NewMeetingRequest);
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000CCC34 File Offset: 0x000CAE34
		protected void RenderAllNewMenuItems()
		{
			this.RenderNewMenuItems();
			base.RenderCustomNewMenuItems();
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000CCC44 File Offset: 0x000CAE44
		protected override void RenderButtons()
		{
			if (this.isPublicFolder)
			{
				base.RenderButton(ToolbarButtons.NewWithPostIcon);
			}
			else
			{
				base.RenderButton(ToolbarButtons.NewMessageCombo, new Toolbar.RenderMenuItems(this.RenderAllNewMenuItems));
			}
			if (this.allowConversationView)
			{
				base.RenderButton(ToolbarButtons.DeleteCombo, new Toolbar.RenderMenuItems(this.RenderDeleteMenuItems));
				base.RenderButton(ToolbarButtons.CancelIgnoreConversationCombo, ToolbarButtonFlags.Hidden, new Toolbar.RenderMenuItems(this.RenderCancelIgnoreConversationMenuItems));
			}
			else
			{
				base.RenderButton(ToolbarButtons.DeleteTextOnly);
			}
			base.RenderButton(ToolbarButtons.MoveTextOnly);
			if (this.includeEmptyFolderButton)
			{
				base.RenderButton(ToolbarButtons.EmptyFolder);
			}
			if (!this.isWebPart && !this.isPublicFolder && (string.IsNullOrEmpty(this.folderClass) || ObjectClass.IsMessageFolder(this.folderClass)) && !this.isDeletedItems && !this.isJunkEmail)
			{
				base.RenderButton(ToolbarButtons.FilterCombo, this.isOthersFolder ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None);
			}
			base.RenderButton(ToolbarButtons.ChangeView, ToolbarButtonFlags.Menu, new Toolbar.RenderMenuItems(this.RenderChangeViewMenuItems));
			if (this.isPublicFolder && !this.isWebPart)
			{
				base.RenderButton(ToolbarButtons.SearchInPublicFolder);
			}
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000CCD77 File Offset: 0x000CAF77
		private void RenderDeleteMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.DeleteInDropDown);
			base.RenderMenuItem(ToolbarButtons.IgnoreConversation);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000CCD8F File Offset: 0x000CAF8F
		private void RenderCancelIgnoreConversationMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.CancelIgnoreConversationInDropDown);
			base.RenderMenuItem(ToolbarButtons.DeleteInCancelIgnoreConversationDropDown);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000CCDA8 File Offset: 0x000CAFA8
		private void RenderChangeViewMenuItems()
		{
			ViewDropDownMenu viewDropDownMenu = new ViewDropDownMenu(base.UserContext, this.readingPanePosition, this.allowConversationView, true);
			viewDropDownMenu.Render(base.Writer);
		}

		// Token: 0x040018C6 RID: 6342
		private readonly bool isPublicFolder;

		// Token: 0x040018C7 RID: 6343
		private readonly bool includeEmptyFolderButton;

		// Token: 0x040018C8 RID: 6344
		private bool allowConversationView;

		// Token: 0x040018C9 RID: 6345
		private bool isWebPart;

		// Token: 0x040018CA RID: 6346
		private bool isOthersFolder;

		// Token: 0x040018CB RID: 6347
		private bool isDeletedItems;

		// Token: 0x040018CC RID: 6348
		private bool isJunkEmail;

		// Token: 0x040018CD RID: 6349
		private string folderClass;

		// Token: 0x040018CE RID: 6350
		private ReadingPanePosition readingPanePosition;
	}
}
