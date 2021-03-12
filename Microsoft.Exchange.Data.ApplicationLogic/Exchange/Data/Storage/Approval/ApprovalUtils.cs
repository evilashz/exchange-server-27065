using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Approval.Common;

namespace Microsoft.Exchange.Data.Storage.Approval
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ApprovalUtils
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x00017510 File Offset: 0x00015710
		public static bool TryGetDecisionMakers(string decisionMakers, out RoutingAddress[] addresses)
		{
			addresses = null;
			if (string.IsNullOrEmpty(decisionMakers))
			{
				ApprovalUtils.diag.TraceDebug(0L, "null or empty decisionMakers string");
				return false;
			}
			if (decisionMakers.Length > 4096)
			{
				ApprovalUtils.diag.TraceDebug<string>(0L, "decision makers too long {0}", decisionMakers);
				return false;
			}
			string[] array = decisionMakers.Split(ApprovalUtils.decisionMakerSeparator, 26, StringSplitOptions.RemoveEmptyEntries);
			addresses = new RoutingAddress[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				addresses[i] = (RoutingAddress)array[i].Trim();
				if (!addresses[i].IsValid || addresses[i] == RoutingAddress.NullReversePath)
				{
					addresses = null;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000175CC File Offset: 0x000157CC
		public static IList<RetentionPolicyTag> GetDefaultRetentionPolicyTag(IConfigurationSession scopedSession, ApprovalApplicationId appType, int resultSize)
		{
			SortBy sortBy = new SortBy(ADObjectSchema.WhenChanged, SortOrder.Descending);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicyTagSchema.IsDefaultAutoGroupPolicyTag, true);
			QueryFilter filter2 = new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicyTagSchema.IsDefaultModeratedRecipientsPolicyTag, true);
			switch (appType)
			{
			case ApprovalApplicationId.AutoGroup:
				return ApprovalUtils.GetDefaultRetentionPolicyTag(scopedSession, filter, sortBy, resultSize);
			case ApprovalApplicationId.ModeratedRecipient:
				return ApprovalUtils.GetDefaultRetentionPolicyTag(scopedSession, filter2, sortBy, resultSize);
			default:
				return null;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00017630 File Offset: 0x00015830
		public static IList<RetentionPolicyTag> GetDefaultRetentionPolicyTag(IConfigurationSession scopedSession, QueryFilter filter, SortBy sortBy, int resultSize)
		{
			return scopedSession.Find<RetentionPolicyTag>(null, QueryScope.SubTree, filter, sortBy, resultSize);
		}

		// Token: 0x040002B4 RID: 692
		public const int MaxSupportedDecisionMakers = 25;

		// Token: 0x040002B5 RID: 693
		private const int DecisionMakerStringLimit = 4096;

		// Token: 0x040002B6 RID: 694
		private static readonly char[] decisionMakerSeparator = new char[]
		{
			';'
		};

		// Token: 0x040002B7 RID: 695
		private static readonly Trace diag = ExTraceGlobals.GeneralTracer;
	}
}
