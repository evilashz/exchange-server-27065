using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015E RID: 350
	[CLSCompliant(false)]
	public sealed class MSExchangeRpcRPCEPMapRule : ExchangeFirewallRule
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00019341 File Offset: 0x00017541
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeRPC - RPCEPMap";
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00019348 File Offset: 0x00017548
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGERPCRPCEPMAP;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001934F File Offset: 0x0001754F
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00019360 File Offset: 0x00017560
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00019367 File Offset: 0x00017567
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x0400062B RID: 1579
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
