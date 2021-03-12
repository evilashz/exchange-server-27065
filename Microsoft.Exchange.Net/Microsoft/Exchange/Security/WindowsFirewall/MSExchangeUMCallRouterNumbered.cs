using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000174 RID: 372
	[CLSCompliant(false)]
	public sealed class MSExchangeUMCallRouterNumbered : ExchangeFirewallRule
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000197A9 File Offset: 0x000179A9
		protected override string ComponentName
		{
			get
			{
				return "UMCallRouter";
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000197B0 File Offset: 0x000179B0
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMCALLROUTERNUMBERED;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x000197B7 File Offset: 0x000179B7
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "FrontEnd\\CallRouter\\Microsoft.Exchange.UM.CallRouter.exe");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000197C8 File Offset: 0x000179C8
		protected override string ServiceName
		{
			get
			{
				return "UMCallRouter";
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000197CF File Offset: 0x000179CF
		protected override string LocalPorts
		{
			get
			{
				return "5060,5061";
			}
		}

		// Token: 0x04000641 RID: 1601
		private const string RuleApplicationRelativePath = "FrontEnd\\CallRouter\\Microsoft.Exchange.UM.CallRouter.exe";
	}
}
