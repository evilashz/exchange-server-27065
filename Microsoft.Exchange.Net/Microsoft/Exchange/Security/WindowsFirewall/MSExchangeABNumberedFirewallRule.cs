using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000140 RID: 320
	[CLSCompliant(false)]
	public sealed class MSExchangeABNumberedFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00018D23 File Offset: 0x00016F23
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeAB-RpcHttp";
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00018D2A File Offset: 0x00016F2A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEAB;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00018D31 File Offset: 0x00016F31
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00018D42 File Offset: 0x00016F42
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00018D49 File Offset: 0x00016F49
		protected override string LocalPorts
		{
			get
			{
				return "6002,6004";
			}
		}

		// Token: 0x0400060D RID: 1549
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
