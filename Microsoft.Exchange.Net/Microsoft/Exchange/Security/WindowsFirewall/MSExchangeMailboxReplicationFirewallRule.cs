using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014F RID: 335
	[CLSCompliant(false)]
	public sealed class MSExchangeMailboxReplicationFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001902A File Offset: 0x0001722A
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMailboxReplication";
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00019031 File Offset: 0x00017231
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMAILBOXREPLICATION;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00019038 File Offset: 0x00017238
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeMailboxReplication.exe");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00019049 File Offset: 0x00017249
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMailboxReplication";
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00019050 File Offset: 0x00017250
		protected override string LocalPorts
		{
			get
			{
				return "808";
			}
		}

		// Token: 0x0400061C RID: 1564
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeMailboxReplication.exe";
	}
}
