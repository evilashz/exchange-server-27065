using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000161 RID: 353
	[CLSCompliant(false)]
	public sealed class MSExchangeRPCEPMapByPortRule : ExchangeFirewallRule
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000193D6 File Offset: 0x000175D6
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeRPCEPMap (GFW)";
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000193DD File Offset: 0x000175DD
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGERPCRPCEPMAP;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x000193E4 File Offset: 0x000175E4
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000193EB File Offset: 0x000175EB
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000193F2 File Offset: 0x000175F2
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x0400062E RID: 1582
		private const string RuleApplicationRelativePath = "";
	}
}
