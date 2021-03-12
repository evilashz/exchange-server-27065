using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000158 RID: 344
	[CLSCompliant(false)]
	public sealed class MSExchangeHighAvailabilityWebServiceFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001920D File Offset: 0x0001740D
		protected override string ComponentName
		{
			get
			{
				return "MSExchangerepl - High Availability Web Service";
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00019214 File Offset: 0x00017414
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEREPLPORTWEBSERVICE;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001921B File Offset: 0x0001741B
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangerepl.exe");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001922C File Offset: 0x0001742C
		protected override string ServiceName
		{
			get
			{
				return "MSExchangerepl";
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00019233 File Offset: 0x00017433
		protected override string LocalPorts
		{
			get
			{
				return "64337";
			}
		}

		// Token: 0x04000625 RID: 1573
		private const string RuleApplicationRelativePath = "Bin\\msexchangerepl.exe";
	}
}
