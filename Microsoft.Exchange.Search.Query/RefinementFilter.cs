using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000018 RID: 24
	internal class RefinementFilter
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006F38 File Offset: 0x00005138
		public RefinementFilter(IReadOnlyCollection<string> refinementQueries)
		{
			InstantSearch.ThrowOnNullOrEmptyArgument(refinementQueries, "refinementQueries");
			this.Filters = refinementQueries;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006F52 File Offset: 0x00005152
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006F5A File Offset: 0x0000515A
		internal IReadOnlyCollection<string> Filters { get; private set; }

		// Token: 0x0600013A RID: 314 RVA: 0x00006F64 File Offset: 0x00005164
		internal QueryFilter GetQueryFilter(CultureInfo cultureInfo, MailboxSession mailboxSession, IRecipientResolver recipientResolver, IPolicyTagProvider policyTagProvider)
		{
			QueryFilter[] array = new QueryFilter[this.Filters.Count];
			int num = 0;
			foreach (string text in this.Filters)
			{
				string[] array2 = text.Split(RefinementFilter.QuerySeparator, 2, StringSplitOptions.None);
				RefinementFilter.BuilderDelegate builderDelegate;
				QueryFilter queryFilter;
				if (array2.Length == 2 && RefinementFilter.FilterBuilders.TryGetValue(array2[0], out builderDelegate))
				{
					queryFilter = builderDelegate(RefinementFilter.RemoveQuotes(array2[1]), mailboxSession);
				}
				else
				{
					queryFilter = KqlParser.ParseAndBuildQuery(text, KqlParser.ParseOption.SuppressError | KqlParser.ParseOption.UseCiKeywordOnly, cultureInfo, RescopedAll.Default, recipientResolver, policyTagProvider);
				}
				array[num++] = queryFilter;
			}
			if (array.Length != 1)
			{
				return new AndFilter(array);
			}
			return array[0];
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007024 File Offset: 0x00005224
		private static QueryFilter GetHasAttachmentFilter(string arg, MailboxSession mailboxSession)
		{
			if (!(arg == "0"))
			{
				return RefinementFilter.HasAttachmentTrueFilter;
			}
			return RefinementFilter.HasAttachmentFalseFilter;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007040 File Offset: 0x00005240
		private static QueryFilter GetFolderIdFilter(string arg, MailboxSession mailboxSession)
		{
			StoreObjectId storeObjectIdFromHexString = FolderIdHelper.GetStoreObjectIdFromHexString(arg, mailboxSession);
			return new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ParentItemId, storeObjectIdFromHexString);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007061 File Offset: 0x00005261
		private static QueryFilter GetFromFilter(string arg, MailboxSession mailboxSession)
		{
			return RefinementFilter.GetParticipantFilter(arg, ItemSchema.SearchSender);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000706E File Offset: 0x0000526E
		private static QueryFilter GetSearchRecipientsFilter(string arg, MailboxSession mailboxSession)
		{
			return RefinementFilter.GetParticipantFilter(arg, ItemSchema.SearchRecipients);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000707B File Offset: 0x0000527B
		private static QueryFilter GetParticipantFilter(string arg, PropertyDefinition property)
		{
			return new TextFilter(property, arg, MatchOptions.ExactPhrase, MatchFlags.Loose);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007088 File Offset: 0x00005288
		private static string RemoveQuotes(string quotedString)
		{
			if (string.IsNullOrEmpty(quotedString) || quotedString.Length < 2 || quotedString[0] != '"' || quotedString[quotedString.Length - 1] != '"')
			{
				return quotedString;
			}
			return quotedString.Substring(1, quotedString.Length - 2);
		}

		// Token: 0x040000B7 RID: 183
		private const string HasAttachmentFalse = "0";

		// Token: 0x040000B8 RID: 184
		private static readonly string[] QuerySeparator = new string[]
		{
			":"
		};

		// Token: 0x040000B9 RID: 185
		private static readonly QueryFilter HasAttachmentTrueFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.HasAttachment, true);

		// Token: 0x040000BA RID: 186
		private static readonly QueryFilter HasAttachmentFalseFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.HasAttachment, false);

		// Token: 0x040000BB RID: 187
		private static readonly Dictionary<string, RefinementFilter.BuilderDelegate> FilterBuilders = new Dictionary<string, RefinementFilter.BuilderDelegate>(StringComparer.OrdinalIgnoreCase)
		{
			{
				FastIndexSystemSchema.FolderId.Name,
				new RefinementFilter.BuilderDelegate(RefinementFilter.GetFolderIdFilter)
			},
			{
				FastIndexSystemSchema.HasAttachment.Name,
				new RefinementFilter.BuilderDelegate(RefinementFilter.GetHasAttachmentFilter)
			},
			{
				FastIndexSystemSchema.Recipients.Name,
				new RefinementFilter.BuilderDelegate(RefinementFilter.GetSearchRecipientsFilter)
			},
			{
				FastIndexSystemSchema.From.Name,
				new RefinementFilter.BuilderDelegate(RefinementFilter.GetFromFilter)
			}
		};

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x06000143 RID: 323
		private delegate QueryFilter BuilderDelegate(string arg, MailboxSession mailboxSession);
	}
}
