using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000149 RID: 329
	[CLSCompliant(false)]
	public sealed class MSExchangeISWorkerRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00018F00 File Offset: 0x00017100
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIS - RPC";
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00018F07 File Offset: 0x00017107
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIS;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00018F0E File Offset: 0x0001710E
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Store.Worker.exe");
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00018F1F File Offset: 0x0001711F
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIS";
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00018F26 File Offset: 0x00017126
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000616 RID: 1558
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Store.Worker.exe";
	}
}
