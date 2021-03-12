using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000147 RID: 327
	[CLSCompliant(false)]
	public sealed class MSExchangeISServiceRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00018E96 File Offset: 0x00017096
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIS - RPC";
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00018E9D File Offset: 0x0001709D
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIS;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00018EA4 File Offset: 0x000170A4
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Store.Service.exe");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00018EB5 File Offset: 0x000170B5
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIS";
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00018EBC File Offset: 0x000170BC
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000614 RID: 1556
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Store.Service.exe";
	}
}
