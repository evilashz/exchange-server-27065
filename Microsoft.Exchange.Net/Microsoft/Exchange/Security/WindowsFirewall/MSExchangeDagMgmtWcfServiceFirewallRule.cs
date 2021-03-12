using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015A RID: 346
	[CLSCompliant(false)]
	public sealed class MSExchangeDagMgmtWcfServiceFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001926D File Offset: 0x0001746D
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeDagMgmt WCF Monitoring Service";
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00019274 File Offset: 0x00017474
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEDAGMGMTWEBSERVICE;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001927B File Offset: 0x0001747B
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeDagMgmt.exe");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001928C File Offset: 0x0001748C
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeDagMgmt";
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00019293 File Offset: 0x00017493
		protected override string LocalPorts
		{
			get
			{
				return "808";
			}
		}

		// Token: 0x04000627 RID: 1575
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeDagMgmt.exe";
	}
}
