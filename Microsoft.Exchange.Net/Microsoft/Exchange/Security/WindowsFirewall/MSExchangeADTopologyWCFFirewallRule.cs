using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000141 RID: 321
	[CLSCompliant(false)]
	public sealed class MSExchangeADTopologyWCFFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00018D58 File Offset: 0x00016F58
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeADTopology - WCF";
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00018D5F File Offset: 0x00016F5F
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEADTOPOLOGY_WCF;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00018D66 File Offset: 0x00016F66
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Directory.Topologyservice.exe");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00018D77 File Offset: 0x00016F77
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeADTopology";
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00018D7E File Offset: 0x00016F7E
		protected override string LocalPorts
		{
			get
			{
				return "890";
			}
		}

		// Token: 0x0400060E RID: 1550
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Directory.Topologyservice.exe";
	}
}
