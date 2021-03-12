using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000168 RID: 360
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportLogSearchRPCEPMapperFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001953F File Offset: 0x0001773F
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportLogSearch - RPCEPMap";
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00019546 File Offset: 0x00017746
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTLOGSEARCHRPCEPMAP;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001954D File Offset: 0x0001774D
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeTransportLogSearch.exe");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001955E File Offset: 0x0001775E
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeTransportLogSearch";
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00019565 File Offset: 0x00017765
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000635 RID: 1589
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeTransportLogSearch.exe";
	}
}
