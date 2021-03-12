using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014E RID: 334
	[CLSCompliant(false)]
	public sealed class MSExchangeMailboxReplicationByPort : ExchangeFirewallRule
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00018FFF File Offset: 0x000171FF
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMailboxReplication (GFW)";
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00019006 File Offset: 0x00017206
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMAILBOXREPLICATION;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001900D File Offset: 0x0001720D
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00019014 File Offset: 0x00017214
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001901B File Offset: 0x0001721B
		protected override string LocalPorts
		{
			get
			{
				return "808";
			}
		}

		// Token: 0x0400061B RID: 1563
		private const string RuleApplicationRelativePath = "";
	}
}
