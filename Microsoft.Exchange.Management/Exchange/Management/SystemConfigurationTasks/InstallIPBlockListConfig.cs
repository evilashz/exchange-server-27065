using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9E RID: 2718
	[Cmdlet("Install", "IPBlockListConfig")]
	public sealed class InstallIPBlockListConfig : InstallAntispamConfig<IPBlockListConfig>
	{
		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x0019160A File Offset: 0x0018F80A
		protected override string CanonicalName
		{
			get
			{
				return "IPBlockListConfig";
			}
		}
	}
}
