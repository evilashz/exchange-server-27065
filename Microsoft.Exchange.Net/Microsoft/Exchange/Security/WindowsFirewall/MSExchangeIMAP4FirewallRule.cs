using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000146 RID: 326
	[CLSCompliant(false)]
	public sealed class MSExchangeIMAP4FirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00018E61 File Offset: 0x00017061
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIMAP4";
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00018E68 File Offset: 0x00017068
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIMAP4;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00018E6F File Offset: 0x0001706F
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "FrontEnd\\PopImap\\Microsoft.Exchange.Imap4Service.exe");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00018E80 File Offset: 0x00017080
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIMAP4";
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00018E87 File Offset: 0x00017087
		protected override string LocalPorts
		{
			get
			{
				return "143,993";
			}
		}

		// Token: 0x04000613 RID: 1555
		private const string RuleApplicationRelativePath = "FrontEnd\\PopImap\\Microsoft.Exchange.Imap4Service.exe";
	}
}
