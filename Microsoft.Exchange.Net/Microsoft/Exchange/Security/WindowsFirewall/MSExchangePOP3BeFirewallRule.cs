using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000155 RID: 341
	[CLSCompliant(false)]
	public sealed class MSExchangePOP3BeFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001916E File Offset: 0x0001736E
		protected override string ComponentName
		{
			get
			{
				return "MSExchangePOP3BE";
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00019175 File Offset: 0x00017375
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEPOP3;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001917C File Offset: 0x0001737C
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "ClientAccess\\PopImap\\Microsoft.Exchange.Pop3Service.exe");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001918D File Offset: 0x0001738D
		protected override string ServiceName
		{
			get
			{
				return "MSExchangePOP3BE";
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00019194 File Offset: 0x00017394
		protected override string LocalPorts
		{
			get
			{
				return "9955,1995";
			}
		}

		// Token: 0x04000622 RID: 1570
		private const string RuleApplicationRelativePath = "ClientAccess\\PopImap\\Microsoft.Exchange.Pop3Service.exe";
	}
}
