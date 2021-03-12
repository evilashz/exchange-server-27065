using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000459 RID: 1113
	internal class DefaultTeamMailboxProvisioningPolicyUtility : DefaultMailboxPolicyUtility<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x06002765 RID: 10085 RVA: 0x0009BA7C File Offset: 0x00099C7C
		public static IList<TeamMailboxProvisioningPolicy> GetDefaultPolicies(IConfigurationSession session)
		{
			return DefaultMailboxPolicyUtility<TeamMailboxProvisioningPolicy>.GetDefaultPolicies(session, DefaultTeamMailboxProvisioningPolicyUtility.filter, null);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0009BA8A File Offset: 0x00099C8A
		public static IList<TeamMailboxProvisioningPolicy> GetDefaultPolicies(IConfigurationSession session, QueryFilter additionalFilter)
		{
			return DefaultMailboxPolicyUtility<TeamMailboxProvisioningPolicy>.GetDefaultPolicies(session, DefaultTeamMailboxProvisioningPolicyUtility.filter, additionalFilter);
		}

		// Token: 0x04001D9E RID: 7582
		private static readonly QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, TeamMailboxProvisioningPolicySchema.IsDefault, true);
	}
}
