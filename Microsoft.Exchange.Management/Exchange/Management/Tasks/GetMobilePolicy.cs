using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000443 RID: 1091
	[Cmdlet("get", "MobileDeviceMailboxPolicy", DefaultParameterSetName = "Identity")]
	public class GetMobilePolicy : GetMailboxPolicyBase<MobileMailboxPolicy>
	{
	}
}
