using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000154 RID: 340
	[CLSCompliant(false)]
	public sealed class MSExchangeOWAFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00019133 File Offset: 0x00017333
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeOWAAppPool";
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001913A File Offset: 0x0001733A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEOWA;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00019141 File Offset: 0x00017341
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(Environment.SystemDirectory, "inetsrv\\w3wp.exe");
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00019152 File Offset: 0x00017352
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeOWAAppPool";
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00019159 File Offset: 0x00017359
		protected override string LocalPorts
		{
			get
			{
				return "5075,5076,5077";
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00019160 File Offset: 0x00017360
		protected override bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00019163 File Offset: 0x00017363
		protected override bool InhibitServiceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000621 RID: 1569
		private const string RuleApplicationRelativePath = "inetsrv\\w3wp.exe";
	}
}
