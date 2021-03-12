using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000144 RID: 324
	[CLSCompliant(false)]
	public sealed class MSExchangeEdgesyncRPCEPMapRule : ExchangeFirewallRule
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00018DF7 File Offset: 0x00016FF7
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeEdgesync - RPCEPMap";
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00018DFE File Offset: 0x00016FFE
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEEDGESYNCRPCEPMAP;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00018E05 File Offset: 0x00017005
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.EdgeSyncSvc.exe");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00018E16 File Offset: 0x00017016
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeEdgesync";
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00018E1D File Offset: 0x0001701D
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000611 RID: 1553
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.EdgeSyncSvc.exe";
	}
}
