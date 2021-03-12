using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016D RID: 365
	[CLSCompliant(false)]
	public sealed class MSExchangeFrontEndTransportNumberedPortsFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001963E File Offset: 0x0001783E
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeFrontendTransport";
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00019645 File Offset: 0x00017845
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTWORKERNUMBERED;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001964C File Offset: 0x0001784C
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeFrontendTransport.exe");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001965D File Offset: 0x0001785D
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeFrontendTransport";
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00019664 File Offset: 0x00017864
		protected override string LocalPorts
		{
			get
			{
				return "25,587,717";
			}
		}

		// Token: 0x0400063A RID: 1594
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeFrontendTransport.exe";
	}
}
