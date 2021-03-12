using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A61 RID: 2657
	[Cmdlet("Get", "IPBlockListEntry", DefaultParameterSetName = "Identity")]
	public sealed class GetIPBlockListEntry : GetIPListEntry<IPBlockListEntry>
	{
	}
}
