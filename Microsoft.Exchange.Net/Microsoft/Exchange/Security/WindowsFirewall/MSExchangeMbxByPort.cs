using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014B RID: 331
	[CLSCompliant(false)]
	public sealed class MSExchangeMbxByPort : ExchangeFirewallRule
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00018F6A File Offset: 0x0001716A
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIS (GFW)";
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00018F71 File Offset: 0x00017171
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIS;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00018F78 File Offset: 0x00017178
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00018F7F File Offset: 0x0001717F
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00018F86 File Offset: 0x00017186
		protected override string LocalPorts
		{
			get
			{
				return "6001,6002,6004,64327";
			}
		}

		// Token: 0x04000618 RID: 1560
		private const string RuleApplicationRelativePath = "";
	}
}
