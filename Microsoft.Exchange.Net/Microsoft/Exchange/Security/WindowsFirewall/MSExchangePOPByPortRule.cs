using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013D RID: 317
	[CLSCompliant(false)]
	public sealed class MSExchangePOPByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00018C8E File Offset: 0x00016E8E
		protected override string ComponentName
		{
			get
			{
				return "MSExchange - POP3 (GFW)";
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00018C95 File Offset: 0x00016E95
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEPOP3;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00018C9C File Offset: 0x00016E9C
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00018CA3 File Offset: 0x00016EA3
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00018CAA File Offset: 0x00016EAA
		protected override string LocalPorts
		{
			get
			{
				return "110,995";
			}
		}

		// Token: 0x0400060A RID: 1546
		private const string RuleApplicationRelativePath = "";
	}
}
