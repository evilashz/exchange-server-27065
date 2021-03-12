using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000145 RID: 325
	[CLSCompliant(false)]
	public sealed class MSExchangeIMAP4BeFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00018E2C File Offset: 0x0001702C
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIMAP4BE";
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00018E33 File Offset: 0x00017033
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIMAP4;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00018E3A File Offset: 0x0001703A
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "ClientAccess\\PopImap\\Microsoft.Exchange.Imap4Service.exe");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00018E4B File Offset: 0x0001704B
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIMAP4BE";
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00018E52 File Offset: 0x00017052
		protected override string LocalPorts
		{
			get
			{
				return "9933,1993";
			}
		}

		// Token: 0x04000612 RID: 1554
		private const string RuleApplicationRelativePath = "ClientAccess\\PopImap\\Microsoft.Exchange.Imap4Service.exe";
	}
}
