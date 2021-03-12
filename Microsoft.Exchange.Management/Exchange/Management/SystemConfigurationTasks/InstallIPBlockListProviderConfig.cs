using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9F RID: 2719
	[Cmdlet("Install", "IPBlockListProvidersConfig")]
	public sealed class InstallIPBlockListProviderConfig : InstallAntispamConfig<IPBlockListProviderConfig>
	{
		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x0600604D RID: 24653 RVA: 0x00191619 File Offset: 0x0018F819
		protected override string CanonicalName
		{
			get
			{
				return "IPBlockListProviderConfig";
			}
		}
	}
}
