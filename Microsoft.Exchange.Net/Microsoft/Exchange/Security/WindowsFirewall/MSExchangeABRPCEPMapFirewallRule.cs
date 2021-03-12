using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200013F RID: 319
	[CLSCompliant(false)]
	public sealed class MSExchangeABRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00018CEE File Offset: 0x00016EEE
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeAB-RPCEPMap";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00018CF5 File Offset: 0x00016EF5
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEABRPCEPMAP;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00018CFC File Offset: 0x00016EFC
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00018D0D File Offset: 0x00016F0D
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00018D14 File Offset: 0x00016F14
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x0400060C RID: 1548
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
