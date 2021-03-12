using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003E0 RID: 992
	internal class PersonViewListToolbar : ViewListToolbar
	{
		// Token: 0x0600246B RID: 9323 RVA: 0x000D3C76 File Offset: 0x000D1E76
		public PersonViewListToolbar(bool isMultiLine, bool isPublicFolder, bool isWebpart, ReadingPanePosition readingPanePosition) : base(isMultiLine, readingPanePosition)
		{
			this.isPublicFolder = isPublicFolder;
			this.isWebpart = isWebpart;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000D3C8F File Offset: 0x000D1E8F
		protected override bool ShowCategoryButton
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000D3C94 File Offset: 0x000D1E94
		protected override void RenderButtons()
		{
			if (this.isPublicFolder && !this.isWebpart)
			{
				base.RenderButtons(ToolbarButtons.NewContactCombo, new ToolbarButton[]
				{
					ToolbarButtons.SearchInPublicFolder
				});
				return;
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IncludeImportContactListButton.Enabled)
			{
				base.RenderButtons(ToolbarButtons.NewContactCombo, new ToolbarButton[]
				{
					ToolbarButtons.ImportContactList
				});
				return;
			}
			base.RenderButtons(ToolbarButtons.NewContactCombo, new ToolbarButton[0]);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000D3D1A File Offset: 0x000D1F1A
		protected override void RenderSharingButton()
		{
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000D3D1C File Offset: 0x000D1F1C
		private void RenderShareContactMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.OpenSharedContact);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000D3D29 File Offset: 0x000D1F29
		protected override void RenderNewMenuItems()
		{
			if (base.UserContext.IsFeatureEnabled(Feature.Contacts))
			{
				base.RenderMenuItem(ToolbarButtons.NewContact);
				base.RenderMenuItem(ToolbarButtons.NewContactDistributionList);
			}
			if (!this.isPublicFolder)
			{
				base.RenderMenuItem(ToolbarButtons.NewMessage);
			}
		}

		// Token: 0x04001942 RID: 6466
		private readonly bool isPublicFolder;

		// Token: 0x04001943 RID: 6467
		private readonly bool isWebpart;
	}
}
