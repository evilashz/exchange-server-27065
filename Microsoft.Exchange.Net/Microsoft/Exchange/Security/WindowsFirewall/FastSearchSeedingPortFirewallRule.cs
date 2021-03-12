using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016E RID: 366
	[CLSCompliant(false)]
	public sealed class FastSearchSeedingPortFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00019673 File Offset: 0x00017873
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeSearch - Seeding";
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001967A File Offset: 0x0001787A
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_FASTSEARCHSEEDINGPORT_IN;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00019681 File Offset: 0x00017881
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\Search\\Ceres\\Runtime\\1.0\\NodeRunner.exe");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00019692 File Offset: 0x00017892
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00019699 File Offset: 0x00017899
		protected override string LocalPorts
		{
			get
			{
				return "3870,3871,3872,3873,3874,3875,3876,3877,3878,3879,3880,3881,3882,3883,3884,3885,3886,3887,3888,3889";
			}
		}

		// Token: 0x0400063B RID: 1595
		private const string RuleApplicationRelativePath = "Bin\\Search\\Ceres\\Runtime\\1.0\\NodeRunner.exe";
	}
}
