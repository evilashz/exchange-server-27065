using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000225 RID: 549
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecipientCacheSignalSource : IAnalyticsSignalSource
	{
		// Token: 0x060014BB RID: 5307 RVA: 0x0007768C File Offset: 0x0007588C
		public RecipientCacheSignalSource(IMailboxSession mailboxSession)
		{
			this.GetRecipientCache = (() => RecipientCacheSignalSource.GetRecipientCacheFromFolder(mailboxSession));
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000776C5 File Offset: 0x000758C5
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x000776CD File Offset: 0x000758CD
		public Func<IEnumerable<RecipientCacheSignalSource.RecipientInCache>> GetRecipientCache { get; set; }

		// Token: 0x060014BE RID: 5310 RVA: 0x000776D6 File Offset: 0x000758D6
		public string GetSourceName()
		{
			return "Recipient Cache";
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000776E8 File Offset: 0x000758E8
		public IEnumerable<AnalyticsSignal> GetSignals()
		{
			return (from r in this.GetRecipientCache()
			where r.IsPerson
			select r).Take(50).Select(new Func<RecipientCacheSignalSource.RecipientInCache, AnalyticsSignal>(RecipientCacheSignalSource.CreateSignal));
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00077774 File Offset: 0x00075974
		private static IEnumerable<RecipientCacheSignalSource.RecipientInCache> GetRecipientCacheFromFolder(IMailboxSession session)
		{
			IEnumerable<RecipientCacheSignalSource.RecipientInCache> result;
			using (Folder folder = Folder.Bind((MailboxSession)session, DefaultFolderType.RecipientCache))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, RecipientCacheSignalSource.RecipientCacheQueryFilter, RecipientCacheSignalSource.RecipientCacheQuerySortColumns, RecipientCacheSignalSource.RecipientCacheQueryPropertyDefinitions))
				{
					result = from bag in queryResult.GetPropertyBags(1000)
					select new RecipientCacheSignalSource.RecipientInCache((string)bag[ContactSchema.Email1EmailAddress], (int)bag[ContactSchema.RelevanceScore], (PersonType)bag[ContactSchema.PersonType]);
				}
			}
			return result;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00077808 File Offset: 0x00075A08
		private static AnalyticsSignal CreateSignal(RecipientCacheSignalSource.RecipientInCache recipient)
		{
			Dictionary<string, string> properties = new Dictionary<string, string>
			{
				{
					"Weight",
					recipient.RecipientRank.ToString(CultureInfo.InvariantCulture)
				}
			};
			AnalyticsSignal.AnalyticsActor analyticsActor = new AnalyticsSignal.AnalyticsActor();
			analyticsActor.Id = null;
			analyticsActor.Properties = SharePointSignalRestDataProvider.CreateSignalProperties(null);
			AnalyticsSignal.AnalyticsAction analyticsAction = new AnalyticsSignal.AnalyticsAction();
			analyticsAction.ActionType = "RecipientCache";
			analyticsAction.Properties = SharePointSignalRestDataProvider.CreateSignalProperties(properties);
			analyticsAction.ExpireTime = DateTime.UtcNow.AddDays(42.0);
			AnalyticsSignal.AnalyticsItem analyticsItem = new AnalyticsSignal.AnalyticsItem();
			analyticsItem.Id = recipient.SmtpAddress;
			analyticsItem.Properties = SharePointSignalRestDataProvider.CreateSignalProperties(null);
			return new AnalyticsSignal
			{
				Actor = analyticsActor,
				Action = analyticsAction,
				Item = analyticsItem,
				Source = "Exchange"
			};
		}

		// Token: 0x04000C81 RID: 3201
		internal const int NumberOfDaysToExpire = 42;

		// Token: 0x04000C82 RID: 3202
		private const int MaxRecipientRank = 50;

		// Token: 0x04000C83 RID: 3203
		private const string SignalSource = "Exchange";

		// Token: 0x04000C84 RID: 3204
		private const string SignalActionType = "RecipientCache";

		// Token: 0x04000C85 RID: 3205
		private const string SignalSourceName = "Recipient Cache";

		// Token: 0x04000C86 RID: 3206
		private const string SignalWeightProperty = "Weight";

		// Token: 0x04000C87 RID: 3207
		private static readonly PropertyDefinition[] RecipientCacheQueryPropertyDefinitions = new PropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.RelevanceScore,
			ContactSchema.PersonType
		};

		// Token: 0x04000C88 RID: 3208
		private static readonly SortBy[] RecipientCacheQuerySortColumns = new SortBy[]
		{
			new SortBy(ContactSchema.RelevanceScore, SortOrder.Ascending)
		};

		// Token: 0x04000C89 RID: 3209
		private static readonly AndFilter RecipientCacheQueryFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.NotEqual, ContactSchema.RelevanceScore, int.MaxValue),
			new ComparisonFilter(ComparisonOperator.NotEqual, ContactSchema.Email1EmailAddress, string.Empty),
			new ComparisonFilter(ComparisonOperator.NotEqual, ContactSchema.Email1EmailAddress, null)
		});

		// Token: 0x02000226 RID: 550
		internal class RecipientInCache
		{
			// Token: 0x060014C5 RID: 5317 RVA: 0x00077979 File Offset: 0x00075B79
			public RecipientInCache(string emailAddress, int relevanceScore, PersonType personType)
			{
				this.SmtpAddress = emailAddress;
				this.RecipientRank = relevanceScore;
				this.IsPerson = (personType == PersonType.Person);
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00077999 File Offset: 0x00075B99
			// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000779A1 File Offset: 0x00075BA1
			public string SmtpAddress { get; private set; }

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000779AA File Offset: 0x00075BAA
			// (set) Token: 0x060014C9 RID: 5321 RVA: 0x000779B2 File Offset: 0x00075BB2
			public int RecipientRank { get; private set; }

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060014CA RID: 5322 RVA: 0x000779BB File Offset: 0x00075BBB
			// (set) Token: 0x060014CB RID: 5323 RVA: 0x000779C3 File Offset: 0x00075BC3
			public bool IsPerson { get; private set; }
		}
	}
}
