using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000179 RID: 377
	[CLSCompliant(false)]
	public sealed class MSExchangeHealthManagerRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000198A8 File Offset: 0x00017AA8
		protected override string ComponentName
		{
			get
			{
				return "MSExchange Health Manager - RPC";
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000198AF File Offset: 0x00017AAF
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEHMRPC;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000198B6 File Offset: 0x00017AB6
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeHMHost.exe");
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000198C7 File Offset: 0x00017AC7
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeHM";
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000198CE File Offset: 0x00017ACE
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000646 RID: 1606
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeHMHost.exe";
	}
}
