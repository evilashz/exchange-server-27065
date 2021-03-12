using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200004F RID: 79
	public class Main : EcpContentPage
	{
		// Token: 0x060019D3 RID: 6611 RVA: 0x00052E24 File Offset: 0x00051024
		protected override void OnLoad(EventArgs e)
		{
			string a;
			if ((a = this.Provider.ToLowerInvariant()) != null)
			{
				if (a == "facebook")
				{
					base.Server.Transfer("~/Connect/FacebookSetup.aspx");
					return;
				}
				if (a == "linkedin")
				{
					base.Server.Transfer("~/Connect/LinkedInSetup.aspx");
					return;
				}
			}
			ErrorHandlingUtil.TransferToErrorPage("badrequesttopeopleconnectmainbadproviderparameter");
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00052E88 File Offset: 0x00051088
		private string Provider
		{
			get
			{
				return base.Request.QueryString["Provider"] ?? string.Empty;
			}
		}

		// Token: 0x04001AE4 RID: 6884
		private const string FacebookLowerCase = "facebook";

		// Token: 0x04001AE5 RID: 6885
		private const string LinkedInLowerCase = "linkedin";
	}
}
