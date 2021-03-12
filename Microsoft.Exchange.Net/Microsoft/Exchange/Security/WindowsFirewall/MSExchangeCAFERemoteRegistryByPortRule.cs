using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200017A RID: 378
	[CLSCompliant(false)]
	public sealed class MSExchangeCAFERemoteRegistryByPortRule : ExchangeFirewallRule
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000198DD File Offset: 0x00017ADD
		protected override string ComponentName
		{
			get
			{
				return "MSExchange CAS";
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000198E4 File Offset: 0x00017AE4
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGECAFEREMOTEREGISTRY;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000198EB File Offset: 0x00017AEB
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000198F2 File Offset: 0x00017AF2
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000198F9 File Offset: 0x00017AF9
		protected override string LocalPorts
		{
			get
			{
				return "139";
			}
		}

		// Token: 0x04000647 RID: 1607
		private const string RuleApplicationRelativePath = "";
	}
}
