using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000437 RID: 1079
	public abstract class SetMailboxPolicyBase<T> : SetSystemConfigurationObjectTask<MailboxPolicyIdParameter, T> where T : MailboxPolicy, new()
	{
		// Token: 0x04001D7A RID: 7546
		protected IList<T> otherDefaultPolicies;

		// Token: 0x04001D7B RID: 7547
		protected bool updateOtherDefaultPolicies;
	}
}
