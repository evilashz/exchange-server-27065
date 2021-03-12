using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000142 RID: 322
	[CLSCompliant(false)]
	public sealed class MSExchangeCAMOMConnectorFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00018D8D File Offset: 0x00016F8D
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeCAMOMConnector - RPC";
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00018D94 File Offset: 0x00016F94
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGECAMOMCONNECTORRPC;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00018D9B File Offset: 0x00016F9B
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Management.CentralAdmin.MomConnector.exe");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00018DAC File Offset: 0x00016FAC
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeCAMOMConnector";
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00018DB3 File Offset: 0x00016FB3
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400060F RID: 1551
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Management.CentralAdmin.MomConnector.exe";
	}
}
