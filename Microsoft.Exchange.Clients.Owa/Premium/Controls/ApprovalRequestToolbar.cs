using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000315 RID: 789
	internal sealed class ApprovalRequestToolbar : Toolbar
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x000ADED7 File Offset: 0x000AC0D7
		internal ApprovalRequestToolbar() : base("tblARTB", ToolbarType.Preview)
		{
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000ADEE5 File Offset: 0x000AC0E5
		internal ApprovalRequestToolbar(bool isApproveEditingEnabled, bool isRejectEditingEnabled) : base("tblARTB", ToolbarType.Preview)
		{
			this.isApproveEditingEnabled = isApproveEditingEnabled;
			this.isRejectEditingEnabled = isRejectEditingEnabled;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000ADF04 File Offset: 0x000AC104
		protected override void RenderButtons()
		{
			if (this.isApproveEditingEnabled)
			{
				base.RenderButton(ToolbarButtons.ApprovalApproveMenu, new Toolbar.RenderMenuItems(this.RenderResponseEditingMenuItems));
			}
			else
			{
				base.RenderButton(ToolbarButtons.ApprovalApprove);
			}
			if (this.isRejectEditingEnabled)
			{
				base.RenderButton(ToolbarButtons.ApprovalRejectMenu, new Toolbar.RenderMenuItems(this.RenderResponseEditingMenuItems));
				return;
			}
			base.RenderButton(ToolbarButtons.ApprovalReject);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000ADF68 File Offset: 0x000AC168
		private void RenderResponseEditingMenuItems()
		{
			base.RenderMenuItem(ToolbarButtons.ApprovalEditResponse);
			base.RenderMenuItem(ToolbarButtons.ApprovalSendResponseNow);
		}

		// Token: 0x04001651 RID: 5713
		private const string ApprovalRequestToolbarId = "tblARTB";

		// Token: 0x04001652 RID: 5714
		private bool isApproveEditingEnabled;

		// Token: 0x04001653 RID: 5715
		private bool isRejectEditingEnabled;
	}
}
