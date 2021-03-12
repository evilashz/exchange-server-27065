using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000139 RID: 313
	[CLSCompliant(false)]
	public sealed class MSExchangeIMAPBeByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00018BE2 File Offset: 0x00016DE2
		protected override string ComponentName
		{
			get
			{
				return "MSExchange - IMAP4 (GFW)";
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00018BE9 File Offset: 0x00016DE9
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIMAP4;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00018BF0 File Offset: 0x00016DF0
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00018BF7 File Offset: 0x00016DF7
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00018BFE File Offset: 0x00016DFE
		protected override string LocalPorts
		{
			get
			{
				return "9933,1993";
			}
		}

		// Token: 0x04000606 RID: 1542
		private const string RuleApplicationRelativePath = "";
	}
}
