using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013B RID: 315
	[CLSCompliant(false)]
	public sealed class MSExchangeOWAByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00018C38 File Offset: 0x00016E38
		protected override string ComponentName
		{
			get
			{
				return "MSExchange - OWA (GFW)";
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00018C3F File Offset: 0x00016E3F
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEOWA;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00018C46 File Offset: 0x00016E46
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00018C4D File Offset: 0x00016E4D
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00018C54 File Offset: 0x00016E54
		protected override string LocalPorts
		{
			get
			{
				return "5075,5076,5077";
			}
		}

		// Token: 0x04000608 RID: 1544
		private const string RuleApplicationRelativePath = "";
	}
}
