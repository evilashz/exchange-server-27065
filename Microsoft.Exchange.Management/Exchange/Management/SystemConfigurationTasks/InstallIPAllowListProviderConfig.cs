using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9D RID: 2717
	[Cmdlet("Install", "IPAllowListProvidersConfig")]
	public sealed class InstallIPAllowListProviderConfig : InstallAntispamConfig<IPAllowListProviderConfig>
	{
		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06006049 RID: 24649 RVA: 0x001915FB File Offset: 0x0018F7FB
		protected override string CanonicalName
		{
			get
			{
				return "IPAllowListProviderConfig";
			}
		}
	}
}
