using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000167 RID: 359
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportLogSearchFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001950A File Offset: 0x0001770A
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportLogSearch - RPC";
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00019511 File Offset: 0x00017711
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTLOGSEARCH;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00019518 File Offset: 0x00017718
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeTransportLogSearch.exe");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00019529 File Offset: 0x00017729
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeTransportLogSearch";
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00019530 File Offset: 0x00017730
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000634 RID: 1588
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeTransportLogSearch.exe";
	}
}
