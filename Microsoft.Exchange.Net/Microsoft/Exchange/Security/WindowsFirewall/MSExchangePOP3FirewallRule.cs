using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000156 RID: 342
	[CLSCompliant(false)]
	public sealed class MSExchangePOP3FirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000191A3 File Offset: 0x000173A3
		protected override string ComponentName
		{
			get
			{
				return "MSExchangePOP3";
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000191AA File Offset: 0x000173AA
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEPOP3;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x000191B1 File Offset: 0x000173B1
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "FrontEnd\\PopImap\\Microsoft.Exchange.Pop3Service.exe");
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000191C2 File Offset: 0x000173C2
		protected override string ServiceName
		{
			get
			{
				return "MSExchangePOP3";
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x000191C9 File Offset: 0x000173C9
		protected override string LocalPorts
		{
			get
			{
				return "110,995";
			}
		}

		// Token: 0x04000623 RID: 1571
		private const string RuleApplicationRelativePath = "FrontEnd\\PopImap\\Microsoft.Exchange.Pop3Service.exe";
	}
}
