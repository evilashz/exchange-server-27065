using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013A RID: 314
	[CLSCompliant(false)]
	public sealed class MSExchangeIMAPByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00018C0D File Offset: 0x00016E0D
		protected override string ComponentName
		{
			get
			{
				return "MSExchange - IMAP4 (GFW)";
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00018C14 File Offset: 0x00016E14
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIMAP4;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00018C1B File Offset: 0x00016E1B
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00018C22 File Offset: 0x00016E22
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00018C29 File Offset: 0x00016E29
		protected override string LocalPorts
		{
			get
			{
				return "143,993";
			}
		}

		// Token: 0x04000607 RID: 1543
		private const string RuleApplicationRelativePath = "";
	}
}
