using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000148 RID: 328
	[CLSCompliant(false)]
	public sealed class MSExchangeISServiceRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00018ECB File Offset: 0x000170CB
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIS - RPCEPMap";
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00018ED2 File Offset: 0x000170D2
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIS;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00018ED9 File Offset: 0x000170D9
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Store.Service.exe");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00018EEA File Offset: 0x000170EA
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIS";
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00018EF1 File Offset: 0x000170F1
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000615 RID: 1557
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Store.Service.exe";
	}
}
