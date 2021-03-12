using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000177 RID: 375
	[CLSCompliant(false)]
	public sealed class MSExchangeUMWorkerProcessNumbered : ExchangeFirewallRule
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001983E File Offset: 0x00017A3E
		protected override string ComponentName
		{
			get
			{
				return "UMWorkerProcess";
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00019845 File Offset: 0x00017A45
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMWORKERPROCESSNUMBERED;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001984C File Offset: 0x00017A4C
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\UMWorkerProcess.exe");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001985D File Offset: 0x00017A5D
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00019864 File Offset: 0x00017A64
		protected override string LocalPorts
		{
			get
			{
				return "5065,5066,5067,5068";
			}
		}

		// Token: 0x04000644 RID: 1604
		private const string RuleApplicationRelativePath = "Bin\\UMWorkerProcess.exe";
	}
}
