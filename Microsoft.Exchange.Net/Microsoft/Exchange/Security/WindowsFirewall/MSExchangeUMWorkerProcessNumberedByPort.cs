using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000176 RID: 374
	[CLSCompliant(false)]
	public sealed class MSExchangeUMWorkerProcessNumberedByPort : ExchangeFirewallRule
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00019813 File Offset: 0x00017A13
		protected override string ComponentName
		{
			get
			{
				return "UMWorkerProcess (GFW)";
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001981A File Offset: 0x00017A1A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMWORKERPROCESSNUMBERED;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00019821 File Offset: 0x00017A21
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00019828 File Offset: 0x00017A28
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001982F File Offset: 0x00017A2F
		protected override string LocalPorts
		{
			get
			{
				return "5065,5066,5067,5068";
			}
		}

		// Token: 0x04000643 RID: 1603
		private const string RuleApplicationRelativePath = "";
	}
}
