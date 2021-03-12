using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A60 RID: 2656
	[Cmdlet("Get", "IPAllowListEntry", DefaultParameterSetName = "Identity")]
	public sealed class GetIPAllowListEntry : GetIPListEntry<IPAllowListEntry>
	{
	}
}
