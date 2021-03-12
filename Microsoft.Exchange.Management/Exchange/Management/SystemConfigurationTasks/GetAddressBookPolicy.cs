using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007E8 RID: 2024
	[Cmdlet("Get", "AddressBookPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetAddressBookPolicy : GetMailboxPolicyBase<AddressBookMailboxPolicy>
	{
	}
}
