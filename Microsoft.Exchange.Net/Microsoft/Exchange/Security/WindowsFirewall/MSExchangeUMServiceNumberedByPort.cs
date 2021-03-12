using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000171 RID: 369
	[CLSCompliant(false)]
	public sealed class MSExchangeUMServiceNumberedByPort : ExchangeFirewallRule
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001971E File Offset: 0x0001791E
		protected override string ComponentName
		{
			get
			{
				return "UMService (GFW)";
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00019725 File Offset: 0x00017925
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMSERVICENUMBERED;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0001972C File Offset: 0x0001792C
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00019733 File Offset: 0x00017933
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001973A File Offset: 0x0001793A
		protected override string LocalPorts
		{
			get
			{
				return "5060,5061,5062,5063";
			}
		}

		// Token: 0x0400063E RID: 1598
		private const string RuleApplicationRelativePath = "";
	}
}
