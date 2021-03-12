using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000423 RID: 1059
	public sealed class TaskViewListToolbar : ViewListToolbar
	{
		// Token: 0x060025D1 RID: 9681 RVA: 0x000DB24A File Offset: 0x000D944A
		public TaskViewListToolbar(bool isPublicFolder, bool isOthersFolder, bool isWebpart, ReadingPanePosition readingPanePosition) : base(false, readingPanePosition)
		{
			this.isPublicFolder = isPublicFolder;
			this.isWebpart = isWebpart;
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000DB263 File Offset: 0x000D9463
		protected override bool ShowMultiLineToggle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000DB266 File Offset: 0x000D9466
		protected override bool ShowMarkComplete
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000DB26C File Offset: 0x000D946C
		protected override void RenderButtons()
		{
			if (!this.isPublicFolder)
			{
				base.RenderButtons(ToolbarButtons.NewTaskCombo, new ToolbarButton[0]);
				return;
			}
			if (this.isWebpart)
			{
				base.RenderButtons(ToolbarButtons.NewWithTaskIcon, new ToolbarButton[0]);
				return;
			}
			base.RenderButtons(ToolbarButtons.NewWithTaskIcon, new ToolbarButton[]
			{
				ToolbarButtons.SearchInPublicFolder
			});
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000DB2C8 File Offset: 0x000D94C8
		protected override void RenderSharingButton()
		{
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000DB2CA File Offset: 0x000D94CA
		private void RenderShareTaskMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.OpenSharedTask);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000DB2D7 File Offset: 0x000D94D7
		protected override void RenderNewMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.NewTask);
			base.RenderMenuItem(ToolbarButtons.NewMessage);
		}

		// Token: 0x04001A1C RID: 6684
		private readonly bool isPublicFolder;

		// Token: 0x04001A1D RID: 6685
		private readonly bool isWebpart;
	}
}
