using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013C RID: 316
	[CLSCompliant(false)]
	public sealed class MSExchangePOPBeByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00018C63 File Offset: 0x00016E63
		protected override string ComponentName
		{
			get
			{
				return "MSExchange - POP3 (GFW)";
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00018C6A File Offset: 0x00016E6A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEPOP3;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00018C71 File Offset: 0x00016E71
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00018C78 File Offset: 0x00016E78
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00018C7F File Offset: 0x00016E7F
		protected override string LocalPorts
		{
			get
			{
				return "9955,1995";
			}
		}

		// Token: 0x04000609 RID: 1545
		private const string RuleApplicationRelativePath = "";
	}
}
