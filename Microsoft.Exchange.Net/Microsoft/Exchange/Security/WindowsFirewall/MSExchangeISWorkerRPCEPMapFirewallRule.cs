using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014A RID: 330
	[CLSCompliant(false)]
	public sealed class MSExchangeISWorkerRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00018F35 File Offset: 0x00017135
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeIS - RPCEPMap";
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00018F3C File Offset: 0x0001713C
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEIS;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00018F43 File Offset: 0x00017143
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.Store.Worker.exe");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00018F54 File Offset: 0x00017154
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeIS";
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00018F5B File Offset: 0x0001715B
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000617 RID: 1559
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.Store.Worker.exe";
	}
}
