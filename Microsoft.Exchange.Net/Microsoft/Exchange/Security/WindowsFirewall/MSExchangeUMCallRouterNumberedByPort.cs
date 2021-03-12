using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000173 RID: 371
	[CLSCompliant(false)]
	public sealed class MSExchangeUMCallRouterNumberedByPort : ExchangeFirewallRule
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0001977E File Offset: 0x0001797E
		protected override string ComponentName
		{
			get
			{
				return "UMCallRouter (GFW)";
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00019785 File Offset: 0x00017985
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMCALLROUTERNUMBERED;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001978C File Offset: 0x0001798C
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00019793 File Offset: 0x00017993
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001979A File Offset: 0x0001799A
		protected override string LocalPorts
		{
			get
			{
				return "5060,5061";
			}
		}

		// Token: 0x04000640 RID: 1600
		private const string RuleApplicationRelativePath = "";
	}
}
