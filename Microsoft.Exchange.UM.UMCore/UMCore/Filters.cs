using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200013E RID: 318
	internal abstract class Filters
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00026959 File Offset: 0x00024B59
		internal static QueryFilter CreateNewVoiceMessageFilter()
		{
			return new ComparisonFilter(ComparisonOperator.NotEqual, MessageItemSchema.IsRead, true);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002696C File Offset: 0x00024B6C
		internal static QueryFilter CreateSavedVoiceMessageFilter()
		{
			return new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsRead, true);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00026980 File Offset: 0x00024B80
		internal static QueryFilter CreatePureVoiceFilter()
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Voicemail.UM");
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Voicemail.UM.CA");
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA");
			QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM");
			QueryFilter queryFilter5 = new OrFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2,
				queryFilter3,
				queryFilter4
			});
			QueryFilter queryFilter6 = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.HasAttachment, true);
			return new AndFilter(new QueryFilter[]
			{
				queryFilter5,
				queryFilter6
			});
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00026A24 File Offset: 0x00024C24
		internal static QueryFilter CreateVoicemailFindByNameFilter(ContactSearchItem searchResult, CultureInfo callerCulture)
		{
			List<QueryFilter> list = new List<QueryFilter>(6);
			if (!string.IsNullOrEmpty(searchResult.FullName))
			{
				list.Add(new TextFilter(ItemSchema.SentRepresentingDisplayName, searchResult.FullName, MatchOptions.FullString, MatchFlags.IgnoreCase));
				if (callerCulture != null)
				{
					list.Add(new TextFilter(ItemSchema.SentRepresentingDisplayName, Strings.NoEmailAddressDisplayName(searchResult.FullName).ToString(callerCulture), MatchOptions.FullString, MatchFlags.IgnoreCase));
				}
			}
			Filters.AddCommonFindByNameFilters(list, searchResult);
			return Filters.GenerateOrFilter(list);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00026A94 File Offset: 0x00024C94
		internal static QueryFilter CreateFindByNameFilter(ContactSearchItem searchResult)
		{
			List<QueryFilter> list = new List<QueryFilter>(6);
			if (!string.IsNullOrEmpty(searchResult.FullName))
			{
				list.Add(new TextFilter(ItemSchema.SentRepresentingDisplayName, searchResult.FullName, MatchOptions.FullString, MatchFlags.IgnoreCase));
			}
			Filters.AddCommonFindByNameFilters(list, searchResult);
			return Filters.GenerateOrFilter(list);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00026ADC File Offset: 0x00024CDC
		private static void AddCommonFindByNameFilters(List<QueryFilter> filterList, ContactSearchItem searchResult)
		{
			if (!string.IsNullOrEmpty(searchResult.PrimarySmtpAddress))
			{
				filterList.Add(new TextFilter(ItemSchema.SentRepresentingEmailAddress, searchResult.PrimarySmtpAddress, MatchOptions.FullString, MatchFlags.IgnoreCase));
			}
			foreach (string text in searchResult.ContactEmailAddresses)
			{
				if (!string.IsNullOrEmpty(text))
				{
					filterList.Add(new TextFilter(ItemSchema.SentRepresentingEmailAddress, text, MatchOptions.FullString, MatchFlags.IgnoreCase));
				}
			}
			if (searchResult.Recipient != null && !string.IsNullOrEmpty(searchResult.Recipient.LegacyExchangeDN))
			{
				filterList.Add(new TextFilter(ItemSchema.SentRepresentingEmailAddress, searchResult.Recipient.LegacyExchangeDN, MatchOptions.FullString, MatchFlags.IgnoreCase));
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00026BA0 File Offset: 0x00024DA0
		private static QueryFilter GenerateOrFilter(List<QueryFilter> filterList)
		{
			if (filterList.Count == 0)
			{
				return null;
			}
			return new OrFilter(filterList.ToArray());
		}
	}
}
