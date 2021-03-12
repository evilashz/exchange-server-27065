using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000453 RID: 1107
	internal static class RoleAssignmentPolicyHelper
	{
		// Token: 0x06002732 RID: 10034 RVA: 0x0009B1EC File Offset: 0x000993EC
		public static IList<RoleAssignmentPolicy> GetDefaultPolicies(IConfigurationSession session, QueryFilter extraFilter)
		{
			QueryFilter queryFilter = RoleAssignmentPolicyHelper.filter;
			if (extraFilter != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					extraFilter,
					RoleAssignmentPolicyHelper.filter
				});
			}
			return RoleAssignmentPolicyHelper.GetPolicies(session, queryFilter);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0009B224 File Offset: 0x00099424
		public static IList<RoleAssignmentPolicy> GetPolicies(IConfigurationSession session, QueryFilter filter)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			ADPagedReader<RoleAssignmentPolicy> adpagedReader = session.FindPaged<RoleAssignmentPolicy>(null, QueryScope.SubTree, filter, null, 0);
			List<RoleAssignmentPolicy> list = new List<RoleAssignmentPolicy>();
			foreach (RoleAssignmentPolicy item in adpagedReader)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0009B290 File Offset: 0x00099490
		public static void ClearIsDefaultOnPolicies(IConfigurationSession session, IList<RoleAssignmentPolicy> defaultPolicies)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (defaultPolicies != null && defaultPolicies.Count > 0)
			{
				foreach (RoleAssignmentPolicy roleAssignmentPolicy in defaultPolicies)
				{
					roleAssignmentPolicy.IsDefault = false;
					session.Save(roleAssignmentPolicy);
				}
			}
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x0009B2FC File Offset: 0x000994FC
		public static bool RoleAssignmentsForPolicyExist(IConfigurationSession session, RoleAssignmentPolicy policy)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.User, policy.Id);
			ExchangeRoleAssignment[] array = session.Find<ExchangeRoleAssignment>(null, QueryScope.SubTree, queryFilter, null, 1);
			return array.Length > 0;
		}

		// Token: 0x04001D99 RID: 7577
		private static readonly QueryFilter filter = new BitMaskAndFilter(RoleAssignmentPolicySchema.Flags, 1UL);
	}
}
