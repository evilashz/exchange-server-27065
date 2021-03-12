using System;
using System.Text;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000297 RID: 663
	public class ConnectedAccounts : EcpContentPage
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x000869D0 File Offset: 0x00084BD0
		protected override void OnInitComplete(EventArgs e)
		{
			base.OnInitComplete(e);
			int num = (this.slbSubscription.Visible ? 1 : 0) + (this.slbForward.Visible ? 1 : 0) + (this.slbSendAs.Visible ? 1 : 0);
			if (num > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.slbSubscription.Visible)
				{
					stringBuilder.Append(OwaOptionStrings.ConnectedAccountsDescriptionForSubscription);
					stringBuilder.Append(' ');
				}
				if (this.slbForward.Visible)
				{
					stringBuilder.Append(OwaOptionStrings.ConnectedAccountsDescriptionForForwarding);
					stringBuilder.Append(' ');
				}
				if (this.slbSendAs.Visible)
				{
					stringBuilder.Append(OwaOptionStrings.ConnectedAccountsDescriptionForSendAs);
				}
				this.lblConnectedAccounts.Text = stringBuilder.ToString();
			}
		}

		// Token: 0x0400217D RID: 8573
		protected Label lblConnectedAccounts;

		// Token: 0x0400217E RID: 8574
		protected SlabControl slbSubscription;

		// Token: 0x0400217F RID: 8575
		protected SlabControl slbForward;

		// Token: 0x04002180 RID: 8576
		protected SlabControl slbSendAs;

		// Token: 0x04002181 RID: 8577
		protected SlabTable slabTable;
	}
}
