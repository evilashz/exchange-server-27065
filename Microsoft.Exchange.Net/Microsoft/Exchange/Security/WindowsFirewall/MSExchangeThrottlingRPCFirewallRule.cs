using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000165 RID: 357
	[CLSCompliant(false)]
	public sealed class MSExchangeThrottlingRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000194A0 File Offset: 0x000176A0
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeThrottling - RPC";
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x000194A7 File Offset: 0x000176A7
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETHROTTLINGRPC;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000194AE File Offset: 0x000176AE
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeThrottling.exe");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000194BF File Offset: 0x000176BF
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeThrottling";
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000194C6 File Offset: 0x000176C6
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000632 RID: 1586
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeThrottling.exe";
	}
}
