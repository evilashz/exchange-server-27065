using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016B RID: 363
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportWorkerByPortRule : ExchangeFirewallRule
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000195DE File Offset: 0x000177DE
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportWorker (GFW)";
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x000195E5 File Offset: 0x000177E5
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTWORKERNUMBERED;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000195EC File Offset: 0x000177EC
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000195F3 File Offset: 0x000177F3
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000195FA File Offset: 0x000177FA
		protected override string LocalPorts
		{
			get
			{
				return "25,587";
			}
		}

		// Token: 0x04000638 RID: 1592
		private const string RuleApplicationRelativePath = "";
	}
}
