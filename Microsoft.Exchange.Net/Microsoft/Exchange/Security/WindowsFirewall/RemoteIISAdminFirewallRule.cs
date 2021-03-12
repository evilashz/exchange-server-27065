using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000138 RID: 312
	[CLSCompliant(false)]
	public sealed class RemoteIISAdminFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00018BA7 File Offset: 0x00016DA7
		protected override string ComponentName
		{
			get
			{
				return "inetinfo";
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00018BAE File Offset: 0x00016DAE
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_REMOTEIISADMIN;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00018BB5 File Offset: 0x00016DB5
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(Environment.SystemDirectory, "inetsrv\\inetinfo.exe");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00018BC6 File Offset: 0x00016DC6
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00018BCD File Offset: 0x00016DCD
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00018BD4 File Offset: 0x00016DD4
		protected override bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00018BD7 File Offset: 0x00016DD7
		protected override bool InhibitServiceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000605 RID: 1541
		private const string RuleApplicationRelativePath = "inetsrv\\inetinfo.exe";
	}
}
