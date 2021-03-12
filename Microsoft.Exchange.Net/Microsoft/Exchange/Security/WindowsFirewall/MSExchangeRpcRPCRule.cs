using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015D RID: 349
	[CLSCompliant(false)]
	public sealed class MSExchangeRpcRPCRule : ExchangeFirewallRule
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001930C File Offset: 0x0001750C
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeRPC - RPC";
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00019313 File Offset: 0x00017513
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGERPCRPC;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001931A File Offset: 0x0001751A
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001932B File Offset: 0x0001752B
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00019332 File Offset: 0x00017532
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400062A RID: 1578
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
