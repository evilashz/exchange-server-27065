using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000240 RID: 576
	internal sealed class SearchPerformanceData
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x00077A64 File Offset: 0x00075C64
		internal static void WriteIndividualLog(OwaContext owaContext, string key, object value)
		{
			string text = string.Format("{0}={1}", key, value);
			if (Globals.CollectPerRequestPerformanceStats)
			{
				owaContext.OwaPerformanceData.TraceOther(text);
			}
			owaContext.HttpContext.Response.AppendToLog("&" + text);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00077AAC File Offset: 0x00075CAC
		private static bool GetSearchFolderData(UserContext userContext, out int searchFolderItemCount, out SearchState searchState, out bool isRemoteSession)
		{
			searchFolderItemCount = -1;
			searchState = SearchState.None;
			isRemoteSession = false;
			bool result = false;
			if (userContext.SearchFolderId != null)
			{
				try
				{
					using (SearchFolder searchFolder = SearchFolder.Bind(userContext.SearchFolderId.GetSession(userContext), userContext.SearchFolderId.StoreObjectId, SearchPerformanceData.searchFolderProperties))
					{
						object obj = searchFolder.TryGetProperty(FolderSchema.SearchFolderItemCount);
						if (obj is int)
						{
							searchFolderItemCount = (int)obj;
						}
						searchState = searchFolder.GetSearchCriteria().SearchState;
						isRemoteSession = searchFolder.Session.IsRemote;
					}
					result = true;
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00077B54 File Offset: 0x00075D54
		internal void StartSearch(string searchString)
		{
			this.searchId = Guid.NewGuid();
			this.searchString = searchString;
			this.firstPageLatency = new SearchPerformanceData.NotificationEventLatency("srchfp");
			this.completeLatency = new SearchPerformanceData.NotificationEventLatency("srchcomp");
			this.refreshLatency = new SearchPerformanceData.RefreshLatency("srchref");
			this.writeSearchFolderData = false;
			this.watch.Reset();
			this.watch.Start();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00077BC0 File Offset: 0x00075DC0
		internal void FirstPage(int itemCount)
		{
			this.firstPageLatency.Latency = this.watch.ElapsedMilliseconds;
			this.firstPageLatency.ItemCount = itemCount;
			this.writeSearchFolderData = true;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00077BEB File Offset: 0x00075DEB
		internal void Complete(bool isTimeout, bool isSync)
		{
			this.completeLatency.Latency = this.watch.ElapsedMilliseconds;
			this.isTimeout = isTimeout;
			this.isSync = isSync;
			this.writeSearchFolderData = true;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00077C18 File Offset: 0x00075E18
		internal void RefreshStart()
		{
			this.refreshLatency.RefreshStart = this.watch.ElapsedMilliseconds;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00077C30 File Offset: 0x00075E30
		internal void RefreshEnd()
		{
			this.refreshLatency.RefreshEnd = this.watch.ElapsedMilliseconds;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00077C48 File Offset: 0x00075E48
		internal void NotificationPickedUpByPendingGet(SearchNotificationType searchNotificationType)
		{
			if ((searchNotificationType & SearchNotificationType.FirstPage) != SearchNotificationType.None && this.firstPageLatency.PendingGetPickup == -1L)
			{
				this.firstPageLatency.PendingGetPickup = this.watch.ElapsedMilliseconds;
			}
			if ((searchNotificationType & SearchNotificationType.Complete) != SearchNotificationType.None)
			{
				this.completeLatency.PendingGetPickup = this.watch.ElapsedMilliseconds;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00077C9A File Offset: 0x00075E9A
		internal void SearchFailed()
		{
			this.searchFailed = true;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00077CA3 File Offset: 0x00075EA3
		internal void SlowSearchEnabled()
		{
			this.slowSearchEnabled = true;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00077CAC File Offset: 0x00075EAC
		internal void WriteLog()
		{
			OwaContext owaContext = OwaContext.Current;
			if (owaContext == null)
			{
				throw new InvalidOperationException("OwaContext.Current is null. SearchPerformanceData.WriteLog should be called in the context of a request.");
			}
			SearchPerformanceData.WriteIndividualLog(owaContext, "srchid", this.searchId);
			if (Globals.CollectSearchStrings && this.searchString != null)
			{
				SearchPerformanceData.WriteIndividualLog(owaContext, "srchstr", this.searchString);
				this.searchString = null;
			}
			if (this.refreshLatency != null)
			{
				this.refreshLatency.WriteLog(owaContext);
			}
			if (this.firstPageLatency != null)
			{
				this.firstPageLatency.WriteLog(owaContext);
			}
			if (this.completeLatency != null)
			{
				this.completeLatency.WriteLog(owaContext);
			}
			if (this.completeLatency.Latency >= 0L)
			{
				if (this.isTimeout)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, "srchto", 1);
					this.isTimeout = false;
				}
				if (this.isSync)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, "srchsync", 1);
					this.isSync = false;
				}
			}
			if (this.searchFailed)
			{
				SearchPerformanceData.WriteIndividualLog(owaContext, "srchfl", "1");
			}
			if (this.slowSearchEnabled)
			{
				SearchPerformanceData.WriteIndividualLog(owaContext, "srchslowenabled", "1");
			}
			if (this.writeSearchFolderData)
			{
				SearchState searchState = SearchState.None;
				int num = -1;
				bool flag = false;
				bool searchFolderData = SearchPerformanceData.GetSearchFolderData(owaContext.UserContext, out num, out searchState, out flag);
				if (searchFolderData)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, "srchsess", flag);
				}
				if (searchState != SearchState.None)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, "srchstate", string.Format(NumberFormatInfo.InvariantInfo, "0x{0:X8}", new object[]
					{
						(int)searchState
					}));
				}
				if (num != -1)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, "srchcount", num);
				}
				this.writeSearchFolderData = false;
			}
		}

		// Token: 0x04000D4D RID: 3405
		private const string FirstPageKey = "srchfp";

		// Token: 0x04000D4E RID: 3406
		private const string CompleteKey = "srchcomp";

		// Token: 0x04000D4F RID: 3407
		private const string RefreshKey = "srchref";

		// Token: 0x04000D50 RID: 3408
		private const string IsTimeoutKey = "srchto";

		// Token: 0x04000D51 RID: 3409
		private const string IsSyncKey = "srchsync";

		// Token: 0x04000D52 RID: 3410
		private const string SearchIdKey = "srchid";

		// Token: 0x04000D53 RID: 3411
		private const string SearchStringKey = "srchstr";

		// Token: 0x04000D54 RID: 3412
		private const string SearchFolderItemCountKey = "srchcount";

		// Token: 0x04000D55 RID: 3413
		private const string SearchStateKey = "srchstate";

		// Token: 0x04000D56 RID: 3414
		private const string IsRemoteSession = "srchsess";

		// Token: 0x04000D57 RID: 3415
		private const string SearchFailedKey = "srchfl";

		// Token: 0x04000D58 RID: 3416
		private const string SlowSearchEnabledKey = "srchslowenabled";

		// Token: 0x04000D59 RID: 3417
		private static readonly StorePropertyDefinition[] searchFolderProperties = new StorePropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.ItemCount,
			FolderSchema.SearchFolderItemCount
		};

		// Token: 0x04000D5A RID: 3418
		private Stopwatch watch = new Stopwatch();

		// Token: 0x04000D5B RID: 3419
		private Guid searchId;

		// Token: 0x04000D5C RID: 3420
		private SearchPerformanceData.NotificationEventLatency firstPageLatency;

		// Token: 0x04000D5D RID: 3421
		private SearchPerformanceData.NotificationEventLatency completeLatency;

		// Token: 0x04000D5E RID: 3422
		private SearchPerformanceData.RefreshLatency refreshLatency;

		// Token: 0x04000D5F RID: 3423
		private bool isTimeout;

		// Token: 0x04000D60 RID: 3424
		private bool isSync;

		// Token: 0x04000D61 RID: 3425
		private string searchString;

		// Token: 0x04000D62 RID: 3426
		private bool writeSearchFolderData;

		// Token: 0x04000D63 RID: 3427
		private bool searchFailed;

		// Token: 0x04000D64 RID: 3428
		private bool slowSearchEnabled;

		// Token: 0x02000241 RID: 577
		internal class NotificationEventLatency
		{
			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x06001365 RID: 4965 RVA: 0x00077E8D File Offset: 0x0007608D
			// (set) Token: 0x06001366 RID: 4966 RVA: 0x00077E95 File Offset: 0x00076095
			internal long Latency { get; set; }

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06001367 RID: 4967 RVA: 0x00077E9E File Offset: 0x0007609E
			// (set) Token: 0x06001368 RID: 4968 RVA: 0x00077EA6 File Offset: 0x000760A6
			internal long PendingGetPickup { get; set; }

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x00077EAF File Offset: 0x000760AF
			// (set) Token: 0x0600136A RID: 4970 RVA: 0x00077EB7 File Offset: 0x000760B7
			internal int ItemCount { get; set; }

			// Token: 0x0600136B RID: 4971 RVA: 0x00077EC0 File Offset: 0x000760C0
			internal NotificationEventLatency(string key)
			{
				this.Latency = -1L;
				this.ItemCount = -1;
				this.PendingGetPickup = -1L;
				this.key = key;
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x00077EE8 File Offset: 0x000760E8
			internal void WriteLog(OwaContext owaContext)
			{
				if (this.Latency >= 0L)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, this.key, this.Latency);
					this.Latency = -1L;
				}
				if (this.ItemCount >= 0)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, this.key + "ic", this.ItemCount);
					this.ItemCount = -1;
				}
				if (this.PendingGetPickup >= 0L)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, this.key + "pg", this.PendingGetPickup);
					this.PendingGetPickup = -1L;
				}
			}

			// Token: 0x04000D65 RID: 3429
			private const string ItemCountSuffix = "ic";

			// Token: 0x04000D66 RID: 3430
			private const string PendingGetPickupTimeSuffix = "pg";

			// Token: 0x04000D67 RID: 3431
			private readonly string key;
		}

		// Token: 0x02000242 RID: 578
		internal class RefreshLatency
		{
			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x0600136D RID: 4973 RVA: 0x00077F82 File Offset: 0x00076182
			// (set) Token: 0x0600136E RID: 4974 RVA: 0x00077F8A File Offset: 0x0007618A
			internal long RefreshStart { get; set; }

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x00077F93 File Offset: 0x00076193
			// (set) Token: 0x06001370 RID: 4976 RVA: 0x00077F9B File Offset: 0x0007619B
			internal long RefreshEnd { get; set; }

			// Token: 0x06001371 RID: 4977 RVA: 0x00077FA4 File Offset: 0x000761A4
			internal RefreshLatency(string key)
			{
				this.RefreshStart = -1L;
				this.RefreshEnd = -1L;
				this.key = key;
			}

			// Token: 0x06001372 RID: 4978 RVA: 0x00077FC4 File Offset: 0x000761C4
			internal void WriteLog(OwaContext owaContext)
			{
				if (this.RefreshStart >= 0L)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, this.key + "s", this.RefreshStart);
					this.RefreshStart = -1L;
				}
				if (this.RefreshEnd >= 0L)
				{
					SearchPerformanceData.WriteIndividualLog(owaContext, this.key + "e", this.RefreshEnd);
					this.RefreshEnd = -1L;
				}
			}

			// Token: 0x04000D6B RID: 3435
			private const string RequestStartSuffix = "s";

			// Token: 0x04000D6C RID: 3436
			private const string RequestEndSuffix = "e";

			// Token: 0x04000D6D RID: 3437
			private readonly string key;
		}
	}
}
