using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016C RID: 364
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportWorkerNumberedPortsFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00019609 File Offset: 0x00017809
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportWorker";
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00019610 File Offset: 0x00017810
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTWORKERNUMBERED;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00019617 File Offset: 0x00017817
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\edgetransport.exe");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00019628 File Offset: 0x00017828
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeTransportWorker";
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0001962F File Offset: 0x0001782F
		protected override string LocalPorts
		{
			get
			{
				return "25,465,587,2525";
			}
		}

		// Token: 0x04000639 RID: 1593
		private const string RuleApplicationRelativePath = "Bin\\edgetransport.exe";
	}
}
