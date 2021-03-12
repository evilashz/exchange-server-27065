using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000172 RID: 370
	[CLSCompliant(false)]
	public sealed class MSExchangeUMServiceNumbered : ExchangeFirewallRule
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00019749 File Offset: 0x00017949
		protected override string ComponentName
		{
			get
			{
				return "UMService";
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00019750 File Offset: 0x00017950
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMSERVICENUMBERED;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00019757 File Offset: 0x00017957
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\UMService.exe");
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00019768 File Offset: 0x00017968
		protected override string ServiceName
		{
			get
			{
				return "UMService";
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001976F File Offset: 0x0001796F
		protected override string LocalPorts
		{
			get
			{
				return "5060,5061,5062,5063";
			}
		}

		// Token: 0x0400063F RID: 1599
		private const string RuleApplicationRelativePath = "Bin\\UMService.exe";
	}
}
