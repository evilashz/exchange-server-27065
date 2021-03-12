using System;
using System.IO;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2C RID: 3372
	public abstract class UMCallRouterTask : ManageUMService
	{
		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x0600814C RID: 33100 RVA: 0x00210E11 File Offset: 0x0020F011
		protected override string ServiceExeName
		{
			get
			{
				return "Microsoft.Exchange.UM.CallRouter.exe";
			}
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x0600814D RID: 33101 RVA: 0x00210E18 File Offset: 0x0020F018
		protected override string ServiceShortName
		{
			get
			{
				return "MSExchangeUMCR";
			}
		}

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x0600814E RID: 33102 RVA: 0x00210E1F File Offset: 0x0020F01F
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.UmCallRouterName;
			}
		}

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x0600814F RID: 33103 RVA: 0x00210E2B File Offset: 0x0020F02B
		protected override string ServiceDescription
		{
			get
			{
				return Strings.UmCallRouterDescription;
			}
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x06008150 RID: 33104 RVA: 0x00210E37 File Offset: 0x0020F037
		protected override ExchangeFirewallRule FirewallRule
		{
			get
			{
				return new MSExchangeUMCallRouterNumbered();
			}
		}

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x06008151 RID: 33105 RVA: 0x00210E3E File Offset: 0x0020F03E
		protected override string RelativeInstallPath
		{
			get
			{
				return Path.Combine("FrontEnd", "CallRouter");
			}
		}

		// Token: 0x04003F23 RID: 16163
		private const string FrontEndFolderName = "FrontEnd";

		// Token: 0x04003F24 RID: 16164
		private const string CallRouterFolderName = "CallRouter";
	}
}
