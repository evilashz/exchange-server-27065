using System;
using System.Globalization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search
{
	// Token: 0x02000488 RID: 1160
	internal static class SearchStoreHelper
	{
		// Token: 0x06001D3F RID: 7487 RVA: 0x000AD310 File Offset: 0x000AB510
		internal static void CreateMessage(MailboxSession session, Folder folder, string subject)
		{
			using (MessageItem messageItem = MessageItem.Create(session, folder.Id))
			{
				messageItem.Subject = subject;
				ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success && conflictResolutionResult.SaveStatus != SaveResult.SuccessWithConflictResolution)
				{
					throw new LocalizedException(Strings.SearchFailToSaveMessage);
				}
			}
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000AD374 File Offset: 0x000AB574
		internal static VersionedId GetMessageBySubject(Folder folder, string subject, out ExDateTime creationTime)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, subject);
			return SearchStoreHelper.GetMessageByComparisonFilter(folder, filter, out creationTime);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000AD398 File Offset: 0x000AB598
		internal static VersionedId GetMessageByInternetMessageId(Folder folder, string internetMessageId, out ExDateTime creationTime)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InternetMessageId, internetMessageId);
			return SearchStoreHelper.GetMessageByComparisonFilter(folder, filter, out creationTime);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000AD3BC File Offset: 0x000AB5BC
		internal static VersionedId GetMessageByComparisonFilter(Folder folder, ComparisonFilter filter, out ExDateTime creationTime)
		{
			creationTime = ExDateTime.MinValue;
			SortBy[] sortColumns = new SortBy[]
			{
				new SortBy(StoreObjectSchema.CreationTime, SortOrder.Descending)
			};
			VersionedId result;
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, sortColumns, new PropertyDefinition[]
			{
				ItemSchema.Id,
				StoreObjectSchema.CreationTime
			}))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, filter))
				{
					object[][] rows = queryResult.GetRows(1);
					if (rows.Length == 0)
					{
						result = null;
					}
					else
					{
						VersionedId versionedId = (VersionedId)rows[0][0];
						creationTime = (ExDateTime)rows[0][1];
						result = versionedId;
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000AD474 File Offset: 0x000AB674
		internal static int GetQueryHitCount(MailboxSession session, string query, int maxResultsCount)
		{
			SearchStoreHelper.BuildQueryFilter(query, CultureInfo.CurrentCulture);
			return SearchStoreHelper.GetQueryHitCount(session, query, maxResultsCount);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000AD48C File Offset: 0x000AB68C
		internal static int GetQueryHitCount(MailboxSession session, QueryFilter query, int maxResultsCount)
		{
			SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(query, new StoreObjectId[]
			{
				session.GetDefaultFolderId(DefaultFolderType.Inbox)
			});
			searchFolderCriteria.MaximumResultsCount = new int?(maxResultsCount);
			int result;
			using (SearchFolder searchFolder = SearchFolder.Create(session, session.GetDefaultFolderId(DefaultFolderType.SearchFolders), "SearchQueryStx" + DateTime.UtcNow.Ticks, CreateMode.InstantSearch))
			{
				searchFolder[FolderSchema.SearchFolderAllowAgeout] = true;
				searchFolder.Save();
				searchFolder.Load();
				VersionedId id = searchFolder.Id;
				IAsyncResult asyncResult = searchFolder.BeginApplyOneTimeSearch(searchFolderCriteria, null, null);
				if (!asyncResult.AsyncWaitHandle.WaitOne(180000))
				{
					throw new TimeoutException();
				}
				searchFolder.EndApplyOneTimeSearch(asyncResult);
				using (QueryResult queryResult = searchFolder.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
				}, new PropertyDefinition[]
				{
					ItemSchema.Id
				}))
				{
					object[][] rows = queryResult.GetRows(queryResult.EstimatedRowCount);
					result = rows.Length;
				}
			}
			return result;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000AD5C0 File Offset: 0x000AB7C0
		internal static MailboxSession GetMailboxSession(string smtpAddress, bool allowCrossSiteServer = false, string client = "Monitoring")
		{
			if (!SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				throw new ArgumentException("smtpAddress");
			}
			string domain = smtpAddress.Split(new char[]
			{
				'@'
			})[1];
			ADSessionSettings adSettings;
			if (Datacenter.GetExchangeSku() == Datacenter.ExchangeSku.ExchangeDatacenter)
			{
				adSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
			}
			else
			{
				adSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			ExchangePrincipal mailboxOwner;
			if (allowCrossSiteServer)
			{
				mailboxOwner = ExchangePrincipal.FromProxyAddress(adSettings, smtpAddress, RemotingOptions.AllowCrossSite);
			}
			else
			{
				mailboxOwner = ExchangePrincipal.FromProxyAddress(adSettings, smtpAddress);
			}
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, string.Format("Client={0};Action=Monitoring Search", client));
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000AD63B File Offset: 0x000AB83B
		internal static Folder GetInboxFolder(MailboxSession session)
		{
			return Folder.Bind(session, DefaultFolderType.Inbox);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000AD644 File Offset: 0x000AB844
		private static QueryFilter BuildQueryFilter(string aqsQuery, CultureInfo culture)
		{
			QueryFilter queryFilter = AqsParser.ParseAndBuildQuery(aqsQuery, AqsParser.ParseOption.None, culture, null, null);
			OrFilter orFilter = new OrFilter(new QueryFilter[]
			{
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.Note", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.Schedule.Meeting", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.OCTEL.VOICE", MatchOptions.PrefixOnWords, MatchFlags.Loose),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.VOICENOTES", MatchOptions.PrefixOnWords, MatchFlags.Loose)
			});
			return new AndFilter(new QueryFilter[]
			{
				queryFilter,
				orFilter
			});
		}

		// Token: 0x04001422 RID: 5154
		internal const int SearchTimeOutSeconds = 180;
	}
}
