using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000178 RID: 376
	[CLSCompliant(false)]
	public sealed class MSExchangeDeliveryNumberedPortFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00019873 File Offset: 0x00017A73
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeDelivery";
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001987A File Offset: 0x00017A7A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTDELIVERYNUMBERED;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00019881 File Offset: 0x00017A81
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeDelivery.exe");
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00019892 File Offset: 0x00017A92
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeDelivery";
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00019899 File Offset: 0x00017A99
		protected override string LocalPorts
		{
			get
			{
				return "475";
			}
		}

		// Token: 0x04000645 RID: 1605
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeDelivery.exe";
	}
}
