using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000164 RID: 356
	[CLSCompliant(false)]
	public sealed class MSExchangeServiceHostDiagnosticsAggregationFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001946B File Offset: 0x0001766B
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeServiceHost - Diagnostics Aggregation";
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00019472 File Offset: 0x00017672
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGESERVICEHOSTDIAGNOSTICSAGGREGATION;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00019479 File Offset: 0x00017679
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.ServiceHost.exe");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001948A File Offset: 0x0001768A
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeServiceHost";
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00019491 File Offset: 0x00017691
		protected override string LocalPorts
		{
			get
			{
				return "9710";
			}
		}

		// Token: 0x04000631 RID: 1585
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.ServiceHost.exe";
	}
}
