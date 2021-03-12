using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200030E RID: 782
	public abstract class ViewListToolbar : Toolbar
	{
		// Token: 0x06001D8A RID: 7562 RVA: 0x000AC0C5 File Offset: 0x000AA2C5
		protected ViewListToolbar(bool isMultiLine, ReadingPanePosition readingPanePosition) : base("divTBL")
		{
			this.isMultiLine = isMultiLine;
			this.readingPanePosition = readingPanePosition;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x000AC0E7 File Offset: 0x000AA2E7
		protected virtual bool ShowMultiLineToggle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000AC0EA File Offset: 0x000AA2EA
		protected virtual bool ShowMarkComplete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x000AC0ED File Offset: 0x000AA2ED
		protected bool IsMultiLine
		{
			get
			{
				return this.isMultiLine;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x000AC0F5 File Offset: 0x000AA2F5
		protected virtual bool ShowCategoryButton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000AC0F8 File Offset: 0x000AA2F8
		protected void RenderButtons(ToolbarButton newButton, params ToolbarButton[] extraButtons)
		{
			if (newButton == null)
			{
				throw new ArgumentNullException("newButton");
			}
			if ((newButton.Flags & ToolbarButtonFlags.ComboMenu) == ToolbarButtonFlags.ComboMenu || (newButton.Flags & ToolbarButtonFlags.Menu) == ToolbarButtonFlags.Menu)
			{
				base.RenderButton(newButton, new Toolbar.RenderMenuItems(this.RenderAllNewMenuItems));
			}
			else
			{
				base.RenderButton(newButton);
			}
			base.RenderButton(ToolbarButtons.Delete);
			base.RenderFloatedSpacer(3);
			base.RenderButton(ToolbarButtons.Move);
			if (this.ShowCategoryButton)
			{
				base.RenderFloatedSpacer(3);
				base.RenderButton(ToolbarButtons.Categories);
			}
			base.RenderFloatedSpacer(3);
			this.RenderSharingButton();
			if (this.ShowMarkComplete)
			{
				base.RenderButton(ToolbarButtons.MarkCompleteNoText);
			}
			base.RenderButton(ToolbarButtons.ChangeView, new Toolbar.RenderMenuItems(this.RenderChangeViewMenuItems));
			base.RenderButton(ToolbarButtons.CheckMessages);
			if (extraButtons != null)
			{
				foreach (ToolbarButton button in extraButtons)
				{
					base.RenderButton(button);
				}
			}
			base.RenderFloatedSpacer(1, "divMeasure");
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000AC1E8 File Offset: 0x000AA3E8
		protected virtual void RenderSharingButton()
		{
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000AC1EA File Offset: 0x000AA3EA
		protected virtual void RenderNewMenuItems()
		{
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000AC1EC File Offset: 0x000AA3EC
		protected void RenderAllNewMenuItems()
		{
			this.RenderNewMenuItems();
			base.RenderCustomNewMenuItems();
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000AC1FC File Offset: 0x000AA3FC
		private void RenderChangeViewMenuItems()
		{
			ViewDropDownMenu viewDropDownMenu = new ViewDropDownMenu(base.UserContext, this.readingPanePosition, false, true);
			viewDropDownMenu.Render(base.Writer);
		}

		// Token: 0x04001620 RID: 5664
		private bool isMultiLine = true;

		// Token: 0x04001621 RID: 5665
		private ReadingPanePosition readingPanePosition;
	}
}
