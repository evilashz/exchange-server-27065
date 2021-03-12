using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015B RID: 347
	[CLSCompliant(false)]
	public sealed class MSExchangeReplRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000192A2 File Offset: 0x000174A2
		protected override string ComponentName
		{
			get
			{
				return "MSExchangerepl - RPC";
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000192A9 File Offset: 0x000174A9
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEREPLRPC;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000192B0 File Offset: 0x000174B0
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangerepl.exe");
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000192C1 File Offset: 0x000174C1
		protected override string ServiceName
		{
			get
			{
				return "MSExchangerepl";
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000192C8 File Offset: 0x000174C8
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000628 RID: 1576
		private const string RuleApplicationRelativePath = "Bin\\msexchangerepl.exe";
	}
}
