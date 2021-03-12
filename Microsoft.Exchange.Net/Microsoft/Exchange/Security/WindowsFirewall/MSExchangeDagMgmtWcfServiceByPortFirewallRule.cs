using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000159 RID: 345
	[CLSCompliant(false)]
	public sealed class MSExchangeDagMgmtWcfServiceByPortFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00019242 File Offset: 0x00017442
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeDagMgmt WCF Monitoring Service (GFW)";
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00019249 File Offset: 0x00017449
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEDAGMGMTWEBSERVICE;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00019250 File Offset: 0x00017450
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00019257 File Offset: 0x00017457
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001925E File Offset: 0x0001745E
		protected override string LocalPorts
		{
			get
			{
				return "808";
			}
		}

		// Token: 0x04000626 RID: 1574
		private const string RuleApplicationRelativePath = "";
	}
}
