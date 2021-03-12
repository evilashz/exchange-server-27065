using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000153 RID: 339
	[CLSCompliant(false)]
	public sealed class MSExchangeMonitoringCorrelationFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x000190FE File Offset: 0x000172FE
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMonitoringCorrelation - RPC";
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00019105 File Offset: 0x00017305
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMONITORINGCORRELATIONRPC;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001910C File Offset: 0x0001730C
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Monitoring.CorrelationEngine.exe");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001911D File Offset: 0x0001731D
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMonitoringCorrelation";
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00019124 File Offset: 0x00017324
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000620 RID: 1568
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Monitoring.CorrelationEngine.exe";
	}
}
