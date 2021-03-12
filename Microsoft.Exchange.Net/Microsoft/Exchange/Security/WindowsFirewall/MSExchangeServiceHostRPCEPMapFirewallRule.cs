using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000163 RID: 355
	[CLSCompliant(false)]
	public sealed class MSExchangeServiceHostRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00019436 File Offset: 0x00017636
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeServiceHost - RPCEPMap";
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001943D File Offset: 0x0001763D
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGESERVICEHOSTRPCEPMAP;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00019444 File Offset: 0x00017644
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.ServiceHost.exe");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00019455 File Offset: 0x00017655
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeServiceHost";
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001945C File Offset: 0x0001765C
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000630 RID: 1584
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.ServiceHost.exe";
	}
}
