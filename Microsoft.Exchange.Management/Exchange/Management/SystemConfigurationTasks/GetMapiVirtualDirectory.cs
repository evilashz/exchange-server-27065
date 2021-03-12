using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0E RID: 3086
	[Cmdlet("Get", "MapiVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetMapiVirtualDirectory : GetExchangeVirtualDirectory<ADMapiVirtualDirectory>
	{
		// Token: 0x06007494 RID: 29844 RVA: 0x001DBE94 File Offset: 0x001DA094
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}
	}
}
