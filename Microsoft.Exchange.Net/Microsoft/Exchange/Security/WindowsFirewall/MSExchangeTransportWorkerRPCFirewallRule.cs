using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000169 RID: 361
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportWorkerRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00019574 File Offset: 0x00017774
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportWorker - RPC";
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001957B File Offset: 0x0001777B
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTWORKERRPC;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00019582 File Offset: 0x00017782
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\edgetransport.exe");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00019593 File Offset: 0x00017793
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeTransportWorker";
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001959A File Offset: 0x0001779A
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000636 RID: 1590
		private const string RuleApplicationRelativePath = "Bin\\edgetransport.exe";
	}
}
