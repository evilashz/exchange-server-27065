using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C16 RID: 3094
	[Cmdlet("Get", "PushNotificationsVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetPushNotificationsVirtualDirectory : GetExchangeServiceVirtualDirectory<ADPushNotificationsVirtualDirectory>
	{
	}
}
