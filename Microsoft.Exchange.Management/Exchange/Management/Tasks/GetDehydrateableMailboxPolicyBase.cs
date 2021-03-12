using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000440 RID: 1088
	public abstract class GetDehydrateableMailboxPolicyBase<T> : GetDeepSearchMailboxPolicyBase<DehydrateableMailboxPolicyIdParameter, T> where T : MailboxPolicy, new()
	{
	}
}
