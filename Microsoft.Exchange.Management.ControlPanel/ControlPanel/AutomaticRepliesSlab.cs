using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200025C RID: 604
	public class AutomaticRepliesSlab : SlabControl
	{
		// Token: 0x060028DF RID: 10463 RVA: 0x00080D64 File Offset: 0x0007EF64
		protected void Page_Load(object sender, EventArgs e)
		{
			if (RbacPrincipal.Current.IsInRole("ExternalReplies") && !RbacPrincipal.Current.IsInRole("Set-MailboxAutoReplyConfiguration?InternalMessage"))
			{
				this.rbExternalAudience.Items[1].Text = OwaOptionStrings.SendToAllGalLessText;
				this.rteExternalMessage_label.Text = OwaOptionStrings.ExternalMessageGalLessInstruction;
				this.divExternalMessage.Attributes.Remove("class");
			}
		}

		// Token: 0x0400209F RID: 8351
		protected RadioButtonList rbExternalAudience;

		// Token: 0x040020A0 RID: 8352
		protected HtmlControl divExternalMessage;

		// Token: 0x040020A1 RID: 8353
		protected Label rteExternalMessage_label;
	}
}
