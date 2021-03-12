using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014C RID: 332
	[CLSCompliant(false)]
	public sealed class MSExchangeMailboxAssistantsRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00018F95 File Offset: 0x00017195
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMailboxAssistants - RPC";
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00018F9C File Offset: 0x0001719C
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMAILBOXASSISTANTSRPC;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00018FA3 File Offset: 0x000171A3
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeMailboxAssistants.exe");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00018FB4 File Offset: 0x000171B4
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMailboxAssistants";
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00018FBB File Offset: 0x000171BB
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000619 RID: 1561
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeMailboxAssistants.exe";
	}
}
