using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043F RID: 1087
	[Cmdlet("Get", "OwaMailboxPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetOwaMailboxPolicy : GetMailboxPolicyBase<OwaMailboxPolicy>
	{
	}
}
