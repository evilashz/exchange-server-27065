using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C4A RID: 3146
	[Cmdlet("Set", "ActiveSyncVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMobileSyncVirtualDirectory : SetMobileSyncVirtualDirectoryBase
	{
	}
}
