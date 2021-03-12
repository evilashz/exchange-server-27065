using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015C RID: 348
	[CLSCompliant(false)]
	public sealed class MSExchangeReplRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000192D7 File Offset: 0x000174D7
		protected override string ComponentName
		{
			get
			{
				return "MSExchangerepl - RPCEPMap";
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000192DE File Offset: 0x000174DE
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEREPLRPCEPMAP;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000192E5 File Offset: 0x000174E5
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangerepl.exe");
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000192F6 File Offset: 0x000174F6
		protected override string ServiceName
		{
			get
			{
				return "MSExchangerepl";
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000192FD File Offset: 0x000174FD
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000629 RID: 1577
		private const string RuleApplicationRelativePath = "Bin\\msexchangerepl.exe";
	}
}
