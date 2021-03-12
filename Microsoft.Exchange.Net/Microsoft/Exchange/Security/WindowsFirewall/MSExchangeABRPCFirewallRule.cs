using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013E RID: 318
	[CLSCompliant(false)]
	public sealed class MSExchangeABRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00018CB9 File Offset: 0x00016EB9
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeAB-RPC";
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00018CC0 File Offset: 0x00016EC0
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEABRPCEPMAP;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00018CC7 File Offset: 0x00016EC7
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00018CD8 File Offset: 0x00016ED8
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00018CDF File Offset: 0x00016EDF
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400060B RID: 1547
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
