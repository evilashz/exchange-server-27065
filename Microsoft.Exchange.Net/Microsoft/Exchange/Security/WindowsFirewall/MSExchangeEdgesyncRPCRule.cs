using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000143 RID: 323
	[CLSCompliant(false)]
	public sealed class MSExchangeEdgesyncRPCRule : ExchangeFirewallRule
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00018DC2 File Offset: 0x00016FC2
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeEdgesync - RPC";
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00018DC9 File Offset: 0x00016FC9
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEEDGESYNCRPC;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00018DD0 File Offset: 0x00016FD0
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.EdgeSyncSvc.exe");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00018DE1 File Offset: 0x00016FE1
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeEdgesync";
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00018DE8 File Offset: 0x00016FE8
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000610 RID: 1552
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.EdgeSyncSvc.exe";
	}
}
