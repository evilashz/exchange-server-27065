using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000446 RID: 1094
	internal class DefaultOwaMailboxPolicyUtility : DefaultMailboxPolicyUtility<OwaMailboxPolicy>
	{
		// Token: 0x060026B1 RID: 9905 RVA: 0x000994DC File Offset: 0x000976DC
		public static IList<OwaMailboxPolicy> GetDefaultPolicies(IConfigurationSession session)
		{
			return DefaultMailboxPolicyUtility<OwaMailboxPolicy>.GetDefaultPolicies(session, DefaultOwaMailboxPolicyUtility.filter, null);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000994EA File Offset: 0x000976EA
		public static IList<OwaMailboxPolicy> GetDefaultPolicies(IConfigurationSession session, QueryFilter extraFilter)
		{
			return DefaultMailboxPolicyUtility<OwaMailboxPolicy>.GetDefaultPolicies(session, DefaultOwaMailboxPolicyUtility.filter, extraFilter);
		}

		// Token: 0x04001D80 RID: 7552
		private static readonly QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, OwaMailboxPolicySchema.IsDefault, true);
	}
}
