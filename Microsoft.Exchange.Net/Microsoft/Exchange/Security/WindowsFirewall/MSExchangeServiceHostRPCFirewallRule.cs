using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000162 RID: 354
	[CLSCompliant(false)]
	public sealed class MSExchangeServiceHostRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00019401 File Offset: 0x00017601
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeServiceHost - RPC";
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00019408 File Offset: 0x00017608
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGESERVICEHOSTRPC;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001940F File Offset: 0x0001760F
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.ServiceHost.exe");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00019420 File Offset: 0x00017620
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeServiceHost";
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00019427 File Offset: 0x00017627
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400062F RID: 1583
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.ServiceHost.exe";
	}
}
