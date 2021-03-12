using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000152 RID: 338
	[CLSCompliant(false)]
	public sealed class MSExchangeMonitoringFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000190C9 File Offset: 0x000172C9
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMonitoring - RPC";
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000190D0 File Offset: 0x000172D0
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMONITORINGRPC;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x000190D7 File Offset: 0x000172D7
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Management.Monitoring.exe");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000190E8 File Offset: 0x000172E8
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMonitoring";
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000190EF File Offset: 0x000172EF
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400061F RID: 1567
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Management.Monitoring.exe";
	}
}
