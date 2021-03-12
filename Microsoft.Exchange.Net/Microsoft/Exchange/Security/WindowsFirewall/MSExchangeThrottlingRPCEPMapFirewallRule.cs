using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000166 RID: 358
	[CLSCompliant(false)]
	public sealed class MSExchangeThrottlingRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000194D5 File Offset: 0x000176D5
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeThrottling - RPCEPMap";
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000194DC File Offset: 0x000176DC
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETHROTTLINGRPCEPMAP;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x000194E3 File Offset: 0x000176E3
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeThrottling.exe");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000194F4 File Offset: 0x000176F4
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeThrottling";
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000194FB File Offset: 0x000176FB
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000633 RID: 1587
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeThrottling.exe";
	}
}
