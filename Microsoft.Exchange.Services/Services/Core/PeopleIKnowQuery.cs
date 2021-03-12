using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PeopleIKnowQuery
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0001410D File Offset: 0x0001230D
		public static QueryFilter GetConversationQueryFilter(string senderDisplayName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("senderDisplayName", senderDisplayName);
			return new MultivaluedInstanceComparisonFilter(ComparisonOperator.Equal, PeopleIKnowQuery.ConversationQuerySenderProperty, senderDisplayName);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014126 File Offset: 0x00012326
		public static QueryFilter GetItemQueryFilter(string senderSmtpAddress)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("senderSmtpAddress", senderSmtpAddress);
			return new ComparisonFilter(ComparisonOperator.Equal, PeopleIKnowQuery.ItemQuerySenderProperty, senderSmtpAddress);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001413F File Offset: 0x0001233F
		public static SortBy[] GetConversationQuerySortBy(SortBy[] originalSortBy)
		{
			ArgumentValidator.ThrowIfNull("originalSortBy", originalSortBy);
			return PeopleIKnowQuery.MergeSortBys(new SortBy(PeopleIKnowQuery.ConversationQuerySenderProperty, SortOrder.Ascending), originalSortBy);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001415D File Offset: 0x0001235D
		public static SortBy[] GetItemQuerySortBy(SortBy[] originalSortBy)
		{
			ArgumentValidator.ThrowIfNull("originalSortBy", originalSortBy);
			return PeopleIKnowQuery.MergeSortBys(new SortBy(PeopleIKnowQuery.ItemQuerySenderProperty, SortOrder.Ascending), originalSortBy);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001417C File Offset: 0x0001237C
		private static SortBy[] MergeSortBys(SortBy sortByToMerge, SortBy[] originalSortBy)
		{
			List<SortBy> list = new List<SortBy>
			{
				sortByToMerge
			};
			foreach (SortBy sortBy in originalSortBy)
			{
				if (!sortBy.ColumnDefinition.Equals(sortByToMerge.ColumnDefinition))
				{
					list.Add(sortBy);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400062F RID: 1583
		private static readonly StorePropertyDefinition ItemQuerySenderProperty = MessageItemSchema.SenderSmtpAddress;

		// Token: 0x04000630 RID: 1584
		private static readonly StorePropertyDefinition ConversationQuerySenderProperty = ConversationItemSchema.ConversationMVFrom;
	}
}
