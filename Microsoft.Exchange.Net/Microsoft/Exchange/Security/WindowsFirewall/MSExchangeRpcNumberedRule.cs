using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000160 RID: 352
	[CLSCompliant(false)]
	public sealed class MSExchangeRpcNumberedRule : ExchangeFirewallRule
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x000193A1 File Offset: 0x000175A1
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000193A8 File Offset: 0x000175A8
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGERPCNUMBERED;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000193AF File Offset: 0x000175AF
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000193C0 File Offset: 0x000175C0
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000193C7 File Offset: 0x000175C7
		protected override string LocalPorts
		{
			get
			{
				return "6001";
			}
		}

		// Token: 0x0400062D RID: 1581
		private const string RuleApplicationRelativePath = "Bin\\Microsoft.Exchange.RpcClientAccess.Service.exe";
	}
}
