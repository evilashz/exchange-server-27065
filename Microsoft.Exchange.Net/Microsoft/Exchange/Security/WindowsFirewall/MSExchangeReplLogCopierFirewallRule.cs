using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000157 RID: 343
	[CLSCompliant(false)]
	public sealed class MSExchangeReplLogCopierFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000191D8 File Offset: 0x000173D8
		protected override string ComponentName
		{
			get
			{
				return "MSExchangerepl - Log Copier";
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000191DF File Offset: 0x000173DF
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEREPLPORTCOPIER;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000191E6 File Offset: 0x000173E6
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangerepl.exe");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000191F7 File Offset: 0x000173F7
		protected override string ServiceName
		{
			get
			{
				return "MSExchangerepl";
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000191FE File Offset: 0x000173FE
		protected override string LocalPorts
		{
			get
			{
				return "64327";
			}
		}

		// Token: 0x04000624 RID: 1572
		private const string RuleApplicationRelativePath = "Bin\\msexchangerepl.exe";
	}
}
