using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000439 RID: 1081
	internal class DefaultMailboxPolicyUtility<T> where T : MailboxPolicy, new()
	{
		// Token: 0x06002613 RID: 9747 RVA: 0x000982A0 File Offset: 0x000964A0
		public static IList<T> GetDefaultPolicies(IConfigurationSession session, QueryFilter defaultFilter, QueryFilter extraFilter)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session is null probably due to not of IConfigurationSession type");
			}
			QueryFilter filter;
			if (extraFilter != null)
			{
				filter = new AndFilter(new QueryFilter[]
				{
					extraFilter,
					defaultFilter
				});
			}
			else
			{
				filter = defaultFilter;
			}
			return session.Find<T>(null, QueryScope.SubTree, filter, DefaultMailboxPolicyUtility<T>.sortBy, int.MaxValue);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000982F0 File Offset: 0x000964F0
		public static void ClearDefaultPolicies(IConfigurationSession session, IList<T> defaultPolicies)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session is null probably due to not of IConfigurationSession type");
			}
			if (defaultPolicies != null && defaultPolicies.Count > 0)
			{
				foreach (T t in defaultPolicies)
				{
					t.IsDefault = false;
					session.Save(t);
				}
			}
		}

		// Token: 0x04001D7C RID: 7548
		private static readonly SortBy sortBy = new SortBy(ADObjectSchema.WhenChanged, SortOrder.Descending);
	}
}
