using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3D RID: 3133
	[Cmdlet("Remove", "PushNotificationsVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePushNotificationsVirtualDirectory : RemoveExchangeVirtualDirectory<ADPushNotificationsVirtualDirectory>
	{
	}
}
