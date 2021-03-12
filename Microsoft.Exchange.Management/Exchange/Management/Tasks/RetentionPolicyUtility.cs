using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044E RID: 1102
	internal class RetentionPolicyUtility : DefaultMailboxPolicyUtility<RetentionPolicy>
	{
		// Token: 0x060026FD RID: 9981 RVA: 0x0009A604 File Offset: 0x00098804
		public static IList<RetentionPolicy> GetDefaultPolicies(IConfigurationSession session, bool isArbitrationMailbox)
		{
			return SharedConfiguration.GetDefaultRetentionPolicy(session, isArbitrationMailbox, RetentionPolicyUtility.sortBy, int.MaxValue);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0009A617 File Offset: 0x00098817
		public static void ClearDefaultPolicies(IConfigurationSession session, IList<RetentionPolicy> defaultPolicies, bool isArbitrationMailbox)
		{
			if (isArbitrationMailbox)
			{
				RetentionPolicyUtility.ClearDefaultArbitrationMailboxPolicies(session, defaultPolicies);
				return;
			}
			DefaultMailboxPolicyUtility<RetentionPolicy>.ClearDefaultPolicies(session, defaultPolicies);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0009A62C File Offset: 0x0009882C
		private static void ClearDefaultArbitrationMailboxPolicies(IConfigurationSession session, IList<RetentionPolicy> defaultPolicies)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session is null probably due to not of IConfigurationSession type");
			}
			if (defaultPolicies != null && defaultPolicies.Count > 0)
			{
				foreach (RetentionPolicy retentionPolicy in defaultPolicies)
				{
					retentionPolicy.IsDefaultArbitrationMailbox = false;
					session.Save(retentionPolicy);
				}
			}
		}

		// Token: 0x04001D91 RID: 7569
		private static readonly SortBy sortBy = new SortBy(ADObjectSchema.WhenChanged, SortOrder.Descending);
	}
}
