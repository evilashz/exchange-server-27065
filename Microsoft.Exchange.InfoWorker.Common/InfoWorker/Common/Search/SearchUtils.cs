using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.SearchService;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MailboxSearch;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000223 RID: 547
	internal static class SearchUtils
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x00042FC0 File Offset: 0x000411C0
		internal static MailboxSession OpenMailboxSessionForSubmission(ADUser adUser, bool catchStorageException)
		{
			try
			{
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser, RemotingOptions.AllowCrossSite);
				return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False", null, true);
			}
			catch (StoragePermanentException arg)
			{
				SearchUtils.Tracer.TraceError<StoragePermanentException>(0L, "SearchUtils.OpenMailboxSession error {0}", arg);
				if (!catchStorageException)
				{
					throw;
				}
			}
			catch (StorageTransientException arg2)
			{
				SearchUtils.Tracer.TraceError<StorageTransientException>(0L, "SearchUtils.OpenMailboxSession error {0}", arg2);
				if (!catchStorageException)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00043048 File Offset: 0x00041248
		internal static MailboxSession OpenMailboxSessionForCleanup(ADUser adUser, GenericIdentity actualExecutingIdentity)
		{
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser, RemotingOptions.AllowCrossSite);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False", actualExecutingIdentity, true);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0004307C File Offset: 0x0004127C
		internal static MailboxSession OpenMailboxSession(ADUser adUser, GenericIdentity actualExecutingIdentity)
		{
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser, RemotingOptions.AllowCrossSite);
			return SearchUtils.OpenMailboxSession(principal, actualExecutingIdentity);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000430A3 File Offset: 0x000412A3
		internal static MailboxSession OpenMailboxSession(ExchangePrincipal principal, GenericIdentity actualExecutingIdentity)
		{
			if (actualExecutingIdentity == null)
			{
				throw new ArgumentNullException("actualExecutingIdentity");
			}
			return MailboxSession.OpenAsAdmin(principal, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False", actualExecutingIdentity, true);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000430C8 File Offset: 0x000412C8
		internal static StoreSession OpenSession(ExchangePrincipal principal, GenericIdentity actualExecutingIdentity, bool publicFolderSession = false)
		{
			if (actualExecutingIdentity == null)
			{
				throw new ArgumentNullException("actualExecutingIdentity");
			}
			if (!publicFolderSession)
			{
				return MailboxSession.OpenAsAdmin(principal, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False", actualExecutingIdentity, true);
			}
			return PublicFolderSession.OpenAsAdmin(principal.MailboxInfo.OrganizationId, null, principal.MailboxInfo.MailboxGuid, null, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False", null);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00043124 File Offset: 0x00041324
		internal static void GetFolderItemsCountAndSize(MailboxSession mailboxSession, StoreId folderId, out int folderItemsCount, out ByteQuantifiedSize folderItemsSize)
		{
			folderItemsCount = 0;
			folderItemsSize = ByteQuantifiedSize.Zero;
			using (Folder folder = Folder.Bind(mailboxSession, folderId))
			{
				folderItemsCount = folder.ItemCount;
				folder.Load(new PropertyDefinition[]
				{
					FolderSchema.ExtendedSize
				});
				object obj = folder.TryGetProperty(FolderSchema.ExtendedSize);
				if (obj is long && (long)obj > 0L)
				{
					folderItemsSize = new ByteQuantifiedSize((ulong)((long)obj));
				}
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000431B0 File Offset: 0x000413B0
		internal static GenericIdentity GetExecutingIdentityFromRunspace(ExchangeRunspaceConfiguration runspaceConfig)
		{
			if (runspaceConfig == null)
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					return new GenericSidIdentity(string.Empty, string.Empty, current.User);
				}
			}
			SecurityIdentifier sid;
			if (runspaceConfig.TryGetExecutingUserSid(out sid))
			{
				return new GenericSidIdentity(string.Empty, string.Empty, sid);
			}
			if (!string.IsNullOrEmpty(runspaceConfig.ExecutingUserDisplayName))
			{
				return new GenericIdentity(runspaceConfig.ExecutingUserDisplayName);
			}
			return new GenericIdentity(runspaceConfig.IdentityName);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00043248 File Offset: 0x00041448
		internal static List<StoreId> GetSearchLogItems(Folder parentFolder)
		{
			List<StoreId> result;
			using (QueryResult queryResult = parentFolder.ItemQuery(ItemQueryType.None, new TextFilter(StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Exchange.Search.Log", MatchOptions.ExactPhrase, MatchFlags.IgnoreCase), null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				result = (from x in queryResult.Enumerator<StoreId>()
				where x != null
				select x).ToList<StoreId>();
			}
			return result;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000432CC File Offset: 0x000414CC
		internal static HashSet<StoreId> GetBccItemIds(MailboxSession mailbox, StoreId folderId)
		{
			HashSet<StoreId> bccItemIds;
			using (Folder folder = Folder.Bind(mailbox, folderId))
			{
				bccItemIds = SearchUtils.GetBccItemIds(folder);
			}
			return bccItemIds;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00043308 File Offset: 0x00041508
		internal static HashSet<StoreId> GetBccItemIds(Folder folder)
		{
			HashSet<StoreId> hashSet = new HashSet<StoreId>();
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.NotEqual, ItemSchema.DisplayBcc, ""),
				new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageBccMe, true)
			}), null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				for (;;)
				{
					object[][] rows = queryResult.GetRows(ResponseThrottler.MaxBulkSize);
					if (rows == null || rows.Length <= 0)
					{
						break;
					}
					for (int i = 0; i < rows.Length; i++)
					{
						hashSet.Add((StoreId)rows[i][0]);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000433DC File Offset: 0x000415DC
		internal static void MoveNonSearchLogItemsInMailbox(MailboxSession mailboxSession, Folder parentFolder, StoreId destFolderId)
		{
			using (QueryResult queryResult = parentFolder.ItemQuery(ItemQueryType.None, new NotFilter(new TextFilter(StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Exchange.Search.Log", MatchOptions.ExactPhrase, MatchFlags.IgnoreCase)), null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				for (;;)
				{
					object[][] rows = queryResult.GetRows(1024);
					if (rows == null || rows.Length <= 0)
					{
						break;
					}
					StoreId[] ids = (from x in rows
					where x[0] != null
					select x[0] as StoreId).ToArray<StoreId>();
					mailboxSession.Move(destFolderId, ids);
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000434A0 File Offset: 0x000416A0
		internal static void MoveObjectsInMailbox(MailboxSession mailboxSession, StoreId destFolderId, List<StoreId> storeIds)
		{
			StoreId[] array = null;
			while (storeIds.Count > 1024)
			{
				if (array == null)
				{
					array = new StoreId[1024];
				}
				storeIds.CopyTo(0, array, 0, array.Length);
				storeIds.RemoveRange(0, array.Length);
				mailboxSession.Move(destFolderId, array);
			}
			if (storeIds.Count > 0)
			{
				mailboxSession.Move(destFolderId, storeIds.ToArray());
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00043504 File Offset: 0x00041704
		internal static void ParseResultPath(IRecipientSession recipientSession, string resultPath, out ADUser adUser, out string folderName)
		{
			if (string.IsNullOrEmpty(resultPath))
			{
				throw new ArgumentNullException(resultPath);
			}
			string[] array = resultPath.Split(new char[]
			{
				'\\'
			});
			if (array.Length != 2)
			{
				throw new FormatException("Invalid resultPath format");
			}
			adUser = (ADUser)recipientSession.Read(new ADObjectId(array[0]));
			folderName = array[1];
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000435F8 File Offset: 0x000417F8
		internal static void ExWatsonWrappedCall(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						methodDelegate();
					});
				}
				catch (GrayException arg)
				{
					SearchUtils.Tracer.TraceError<GrayException>(0L, "GrayException {0} is thrown", arg);
				}
			}, delegate(object exception)
			{
				SearchUtils.Tracer.TraceError(0L, "ExWatsonWrappedCall: Unhandled exception {0}", new object[]
				{
					exception
				});
				return !(exception is GrayException);
			});
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000436F8 File Offset: 0x000418F8
		internal static void ExWatsonWrappedCall(ExWatson.MethodDelegate methodDelegate, ExWatson.MethodDelegate finallyDelegate)
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						methodDelegate();
					});
				}
				catch (GrayException arg)
				{
					SearchUtils.Tracer.TraceError<GrayException>(0L, "GrayException {0} is thrown", arg);
				}
				finally
				{
					finallyDelegate();
				}
			}, delegate(object exception)
			{
				SearchUtils.Tracer.TraceError(0L, "ExWatsonWrappedCall: Unhandled exception {0}", new object[]
				{
					exception
				});
				return !(exception is GrayException);
			});
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00043747 File Offset: 0x00041947
		internal static bool LegalHoldEnabled(MailboxSession mailboxSession)
		{
			if (mailboxSession.COWSettings != null)
			{
				return mailboxSession.COWSettings.HoldEnabled();
			}
			return COWSettings.HoldEnabled(mailboxSession);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00043764 File Offset: 0x00041964
		internal static IThrottlingPolicy GetDiscoveryThrottlingPolicy(IRecipientSession recipientSession)
		{
			DiscoveryTenantBudgetKey discoveryTenantBudgetKey = new DiscoveryTenantBudgetKey(recipientSession.SessionSettings.CurrentOrganizationId, BudgetType.PowerShell);
			return discoveryTenantBudgetKey.Lookup();
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0004378C File Offset: 0x0004198C
		internal static uint GetDiscoveryMaxMailboxes(IRecipientSession recipientSession)
		{
			Unlimited<uint> unlimited = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxMailboxes.Value;
			uint result = unlimited.IsUnlimited ? uint.MaxValue : unlimited.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(2437295421U, ref result);
			return result;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000437D8 File Offset: 0x000419D8
		internal static uint GetDiscoveryMaxKeywords(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxKeywords = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxKeywords;
			uint result = discoveryMaxKeywords.IsUnlimited ? uint.MaxValue : discoveryMaxKeywords.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(3511037245U, ref result);
			return result;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00043818 File Offset: 0x00041A18
		internal static uint GetDiscoveryMaxConcurrency(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxConcurrency = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxConcurrency;
			uint result = discoveryMaxConcurrency.IsUnlimited ? uint.MaxValue : discoveryMaxConcurrency.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(4047908157U, ref result);
			return result;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00043858 File Offset: 0x00041A58
		internal static uint GetDiscoveryMaxMailboxesForPreviewSearch(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxPreviewSearchMailboxes = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxPreviewSearchMailboxes;
			uint result = discoveryMaxPreviewSearchMailboxes.IsUnlimited ? uint.MaxValue : discoveryMaxPreviewSearchMailboxes.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(2831559997U, ref result);
			return result;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00043898 File Offset: 0x00041A98
		internal static uint GetDiscoveryMaxMailboxesForStatsSearch(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxStatsSearchMailboxes = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxStatsSearchMailboxes;
			uint result = discoveryMaxStatsSearchMailboxes.IsUnlimited ? uint.MaxValue : discoveryMaxStatsSearchMailboxes.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(2500209981U, ref result);
			return result;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000438D7 File Offset: 0x00041AD7
		internal static bool DiscoveryEnabled(IRecipientSession recipientSession)
		{
			return SearchUtils.GetDiscoveryMaxConcurrency(recipientSession) > 0U;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000438E4 File Offset: 0x00041AE4
		internal static uint GetDiscoveryPreviewSearchResultsPageSize(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryPreviewSearchResultsPageSize = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryPreviewSearchResultsPageSize;
			uint result = discoveryPreviewSearchResultsPageSize.IsUnlimited ? uint.MaxValue : discoveryPreviewSearchResultsPageSize.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(3590729021U, ref result);
			return result;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00043924 File Offset: 0x00041B24
		internal static uint GetDiscoveryMaxKeywordsPerPage(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxKeywordsPerPage = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxKeywordsPerPage;
			uint result = discoveryMaxKeywordsPerPage.IsUnlimited ? uint.MaxValue : discoveryMaxKeywordsPerPage.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(2516987197U, ref result);
			return result;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00043964 File Offset: 0x00041B64
		internal static uint GetDiscoveryMaxRefinerResults(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxRefinerResults = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxRefinerResults;
			uint result = discoveryMaxRefinerResults.IsUnlimited ? uint.MaxValue : discoveryMaxRefinerResults.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(4127599933U, ref result);
			return result;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000439A4 File Offset: 0x00041BA4
		internal static uint GetDiscoveryMaxSearchQueueDepth(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoveryMaxSearchQueueDepth = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoveryMaxSearchQueueDepth;
			uint result = discoveryMaxSearchQueueDepth.IsUnlimited ? uint.MaxValue : discoveryMaxSearchQueueDepth.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(2802199869U, ref result);
			return result;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000439E4 File Offset: 0x00041BE4
		internal static uint GetDiscoverySearchTimeoutPeriod(IRecipientSession recipientSession)
		{
			Unlimited<uint> discoverySearchTimeoutPeriod = SearchUtils.GetDiscoveryThrottlingPolicy(recipientSession).DiscoverySearchTimeoutPeriod;
			uint result = discoverySearchTimeoutPeriod.IsUnlimited ? uint.MaxValue : discoverySearchTimeoutPeriod.Value;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(3573951805U, ref result);
			return result;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00043A54 File Offset: 0x00041C54
		internal static void CheckDiscoveryBudget(ADUser arbitrationMailbox, IRecipientSession recipientSession)
		{
			uint discoveryMaxConcurrency = SearchUtils.GetDiscoveryMaxConcurrency(recipientSession);
			if (discoveryMaxConcurrency == 0U)
			{
				throw new SearchDisabledException();
			}
			MailboxDataProvider mailboxDataProvider = new MailboxDataProvider(arbitrationMailbox, recipientSession);
			IEnumerable<SearchObject> enumerable = mailboxDataProvider.FindPaged<SearchObject>(null, null, false, null, 0);
			if (enumerable != null)
			{
				Dictionary<ADObjectId, ADUser> dictionary = new Dictionary<ADObjectId, ADUser>();
				Dictionary<ADObjectId, string> dictionary2 = new Dictionary<ADObjectId, string>();
				ADUser aduser = null;
				string text = null;
				SearchStatus searchStatus = null;
				uint num = 0U;
				try
				{
					foreach (SearchObject searchObject in from so in enumerable
					where (so.SearchStatus != null && so.SearchStatus.Status == SearchState.InProgress) || (so.SearchStatus != null && so.SearchStatus.Status == SearchState.EstimateInProgress)
					select so)
					{
						if (!dictionary.TryGetValue(searchObject.TargetMailbox, out aduser))
						{
							aduser = (ADUser)recipientSession.Read(searchObject.TargetMailbox);
							if (aduser != null)
							{
								dictionary.Add(searchObject.TargetMailbox, aduser);
							}
						}
						if (aduser != null && !dictionary2.TryGetValue(searchObject.TargetMailbox, out text))
						{
							text = ExchangePrincipal.FromADUser(recipientSession.SessionSettings, aduser, RemotingOptions.AllowCrossSite).MailboxInfo.Location.ServerFqdn;
							if (!string.IsNullOrEmpty(text))
							{
								dictionary2.Add(searchObject.TargetMailbox, text);
							}
						}
						if (aduser != null && !string.IsNullOrEmpty(text))
						{
							SearchId searchId = new SearchId(mailboxDataProvider.ADUser.Id.DistinguishedName, mailboxDataProvider.ADUser.Id.ObjectGuid, searchObject.Id.Guid.ToString());
							try
							{
								using (MailboxSearchClient mailboxSearchClient = new MailboxSearchClient(text))
								{
									searchStatus = mailboxSearchClient.GetStatus(searchId);
								}
							}
							catch (RpcConnectionException arg)
							{
								SearchUtils.Tracer.TraceError<RpcConnectionException>(0L, "SearchUtils.CheckDiscoveryBudget error querying for status: {0}", arg);
								continue;
							}
							catch (RpcException arg2)
							{
								SearchUtils.Tracer.TraceError<RpcException>(0L, "SearchUtils.CheckDiscoveryBudget error querying for status: {0}", arg2);
								continue;
							}
							catch (SearchServerException arg3)
							{
								SearchUtils.Tracer.TraceError<SearchServerException>(0L, "SearchUtils.CheckDiscoveryBudget error querying for status: {0}", arg3);
								continue;
							}
							if (searchStatus != null && (searchStatus.Status == 0 || searchStatus.Status == 6))
							{
								num += 1U;
							}
						}
						if (num >= discoveryMaxConcurrency)
						{
							throw new SearchOverBudgetException((int)discoveryMaxConcurrency);
						}
					}
				}
				finally
				{
					dictionary.Clear();
					dictionary2.Clear();
				}
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00043D08 File Offset: 0x00041F08
		internal static bool CollectKeywordStats(SearchObject searchObject, int totalMailboxes, int discoveryMaxMailboxes)
		{
			if (totalMailboxes > discoveryMaxMailboxes)
			{
				SearchUtils.Tracer.TraceWarning<int, int>(0L, "Number of mailboxes ({0}) being search is greater than allowed max mailboxes ({1}), disable keyword statistics.", totalMailboxes, discoveryMaxMailboxes);
				return false;
			}
			return searchObject.EstimateOnly && searchObject.IncludeKeywordStatistics && !string.IsNullOrEmpty(searchObject.SearchQuery);
		}

		// Token: 0x04000A50 RID: 2640
		internal const int MoveBatchSize = 1024;

		// Token: 0x04000A51 RID: 2641
		private const uint LIDDiscoveryMaxSearch = 4047908157U;

		// Token: 0x04000A52 RID: 2642
		private const uint LIDDiscoveryMaxMailboxes = 2437295421U;

		// Token: 0x04000A53 RID: 2643
		private const uint LIDDiscoveryMaxKeywords = 3511037245U;

		// Token: 0x04000A54 RID: 2644
		private const uint LIDDiscoveryMaxPreviewSearchMailboxes = 2831559997U;

		// Token: 0x04000A55 RID: 2645
		private const uint LIDDiscoveryMaxStatsSearchMailboxes = 2500209981U;

		// Token: 0x04000A56 RID: 2646
		private const uint LIDDiscoveryPreviewSearchResultsPageSize = 3590729021U;

		// Token: 0x04000A57 RID: 2647
		private const uint LIDDiscoveryMaxKeywordsPerPage = 2516987197U;

		// Token: 0x04000A58 RID: 2648
		private const uint LIDDiscoveryMaxRefinerResults = 4127599933U;

		// Token: 0x04000A59 RID: 2649
		private const uint LIDDiscoveryMaxSearchQueueDepth = 2802199869U;

		// Token: 0x04000A5A RID: 2650
		private const uint LIDDiscoverySearchTimeoutPeriod = 3573951805U;

		// Token: 0x04000A5B RID: 2651
		internal static readonly Trace Tracer = ExTraceGlobals.SearchTracer;
	}
}
