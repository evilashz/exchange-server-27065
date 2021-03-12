using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9C RID: 2716
	[Cmdlet("Install", "IPAllowListConfig")]
	public sealed class InstallIPAllowListConfig : InstallAntispamConfig<IPAllowListConfig>
	{
		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06006047 RID: 24647 RVA: 0x001915EC File Offset: 0x0018F7EC
		protected override string CanonicalName
		{
			get
			{
				return "IPAllowListConfig";
			}
		}
	}
}
