using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE0 RID: 2784
	[Cmdlet("Set", "OfflineAddressBook", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOfflineAddressBook : SetOfflineAddressBookInternal
	{
	}
}
