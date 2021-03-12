using System;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2E RID: 3374
	public abstract class UMServiceTask : ManageUMService
	{
		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06008155 RID: 33109 RVA: 0x00210E7D File Offset: 0x0020F07D
		protected override string ServiceExeName
		{
			get
			{
				return "umservice.exe";
			}
		}

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06008156 RID: 33110 RVA: 0x00210E84 File Offset: 0x0020F084
		protected override string ServiceShortName
		{
			get
			{
				return "MSExchangeUM";
			}
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x06008157 RID: 33111 RVA: 0x00210E8B File Offset: 0x0020F08B
		protected override string ServiceDisplayName
		{
			get
			{
				return Strings.UmServiceName;
			}
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x06008158 RID: 33112 RVA: 0x00210E97 File Offset: 0x0020F097
		protected override string ServiceDescription
		{
			get
			{
				return Strings.UmServiceDescription;
			}
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x06008159 RID: 33113 RVA: 0x00210EA3 File Offset: 0x0020F0A3
		protected override ExchangeFirewallRule FirewallRule
		{
			get
			{
				return new MSExchangeUMServiceNumbered();
			}
		}

		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x0600815A RID: 33114 RVA: 0x00210EAA File Offset: 0x0020F0AA
		protected override string RelativeInstallPath
		{
			get
			{
				return "Bin";
			}
		}

		// Token: 0x04003F25 RID: 16165
		private const string BinFolderName = "Bin";
	}
}
