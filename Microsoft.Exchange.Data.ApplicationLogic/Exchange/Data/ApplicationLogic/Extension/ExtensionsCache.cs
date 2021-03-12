using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000FB RID: 251
	internal class ExtensionsCache
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00029249 File Offset: 0x00027449
		internal static ExtensionsCache Singleton
		{
			get
			{
				return ExtensionsCache.singleton;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00029250 File Offset: 0x00027450
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x00029258 File Offset: 0x00027458
		internal bool SkipSubmitUpdateQueryForTest { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00029261 File Offset: 0x00027461
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00029269 File Offset: 0x00027469
		internal int SubmitCount { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00029272 File Offset: 0x00027472
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0002927A File Offset: 0x0002747A
		internal int Size { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00029283 File Offset: 0x00027483
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0002928B File Offset: 0x0002748B
		internal DateTime LastCacheCleanupTime { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00029294 File Offset: 0x00027494
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0002929C File Offset: 0x0002749C
		internal int GetUpdatesCount { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000292A5 File Offset: 0x000274A5
		internal TokenRenewSubmitter TokenRenewSubmitter
		{
			get
			{
				return this.tokenRenewSubmitter;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000292AD File Offset: 0x000274AD
		internal int QueryQueueCount
		{
			get
			{
				return this.queryQueue.Count;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000292BC File Offset: 0x000274BC
		internal ExtensionsCache()
		{
			this.LastCacheCleanupTime = DateTime.UtcNow;
			this.extensionsDictionary.OnReplaced += this.OnReplaced;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00029322 File Offset: 0x00027522
		internal ExtensionsCache(OmexWebServiceUrlsCache urlsCache) : this()
		{
			if (urlsCache == null)
			{
				throw new ArgumentNullException("urlsCache");
			}
			this.urlsCache = urlsCache;
			this.tokenRenewSubmitter = new TokenRenewSubmitter(urlsCache);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002934C File Offset: 0x0002754C
		internal bool TryGetEntry(string marketplaceAssetID, out ExtensionsCacheEntry extensionCacheEntry)
		{
			ExtensionsCacheEntry extensionsCacheEntry = null;
			if (this.extensionsDictionary.TryGetValue(marketplaceAssetID, out extensionsCacheEntry) && !InstalledExtensionTable.IsUpdateCheckTimeExpired(extensionsCacheEntry.LastUpdateCheckTime))
			{
				extensionCacheEntry = extensionsCacheEntry;
			}
			else
			{
				extensionCacheEntry = null;
			}
			return extensionCacheEntry != null;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00029388 File Offset: 0x00027588
		internal int Count
		{
			get
			{
				return this.extensionsDictionary.Count;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00029398 File Offset: 0x00027598
		private void OnReplaced(object sender, MruDictionaryElementReplacedEventArgs<string, ExtensionsCacheEntry> eventArgs)
		{
			if (eventArgs.OldKeyValuePair.Value != null && eventArgs.NewKeyValuePair.Value != null)
			{
				lock (this.extensionsDictionary.SyncRoot)
				{
					this.Size -= eventArgs.OldKeyValuePair.Value.Size;
				}
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00029418 File Offset: 0x00027618
		private void CleanupCache()
		{
			lock (this.extensionsDictionary.SyncRoot)
			{
				if (this.LastCacheCleanupTime.AddDays(1.0) < DateTime.UtcNow)
				{
					List<ExtensionsCacheEntry> list = new List<ExtensionsCacheEntry>(this.extensionsDictionary.Count);
					this.Size = 0;
					foreach (KeyValuePair<string, ExtensionsCacheEntry> keyValuePair in this.extensionsDictionary)
					{
						ExtensionsCacheEntry value = keyValuePair.Value;
						if (value.LastUpdateCheckTime.AddDays(14.0) < DateTime.UtcNow)
						{
							list.Add(value);
						}
						else
						{
							this.Size += value.Size;
						}
					}
					foreach (ExtensionsCacheEntry extensionsCacheEntry in list)
					{
						ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.CleanupCache: Removing Extension {0}.", extensionsCacheEntry.MarketplaceAssetID);
						this.extensionsDictionary.Remove(extensionsCacheEntry.MarketplaceAssetID);
					}
					ExtensionsCache.Tracer.TraceDebug<int>(0L, "ExtensionsCache.CleanupCache: Current cache size {0}.", this.Size);
				}
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000295B8 File Offset: 0x000277B8
		internal bool TryGetExtensionUpdate(ExtensionData extensionData, out byte[] manifestBytes)
		{
			manifestBytes = null;
			this.CleanupCache();
			ExtensionsCacheEntry extensionsCacheEntry = null;
			if (this.extensionsDictionary.TryGetValue(extensionData.MarketplaceAssetID, out extensionsCacheEntry))
			{
				if (extensionData.ExtensionId != extensionsCacheEntry.ExtensionID)
				{
					ExtensionsCache.Tracer.TraceError<string, string, string>(0L, "ExtensionsCache.TryGetExtensionUpdate: Extension {0} ExtensionID property {1} does not match cache entry value {2}.", extensionData.MarketplaceAssetID, extensionData.ExtensionId, extensionsCacheEntry.ExtensionID);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_MismatchedCacheMailboxExtensionId, extensionData.MarketplaceAssetID, new object[]
					{
						"ProcessUpdates",
						extensionData.MarketplaceAssetID,
						extensionData.ExtensionId,
						extensionsCacheEntry.ExtensionID
					});
				}
				else if (extensionData.Version != null && extensionsCacheEntry.RequestedCapabilities != null && extensionsCacheEntry.Manifest != null && extensionData.Version < extensionsCacheEntry.Version && ExtensionData.CompareCapabilities(extensionsCacheEntry.RequestedCapabilities.Value, extensionData.RequestedCapabilities.Value) <= 0 && GetUpdates.IsValidUpdateState(new OmexConstants.AppState?(extensionsCacheEntry.State)))
				{
					manifestBytes = extensionsCacheEntry.Manifest;
				}
			}
			return manifestBytes != null;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000296E4 File Offset: 0x000278E4
		internal void SubmitUpdateQuery(ICollection<ExtensionData> extensions, UpdateQueryContext queryContext)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			if (extensions.Count == 0)
			{
				throw new ArgumentException("extensions must contain one or more extensions");
			}
			if (this.SkipSubmitUpdateQueryForTest)
			{
				this.SubmitCount = 0;
			}
			Dictionary<string, UpdateRequestAsset> dictionary = new Dictionary<string, UpdateRequestAsset>(extensions.Count);
			foreach (ExtensionData extensionData in extensions)
			{
				if (extensionData.Version == null)
				{
					ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.SubmitUpdateQuery: Extension {0} not added to query list because version is invalid", extensionData.MarketplaceAssetID);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_InvalidVersionSubmitUpdateQuery, extensionData.MarketplaceAssetID, new object[]
					{
						"ProcessUpdates",
						ExtensionDiagnostics.GetLoggedMailboxIdentifier(queryContext.ExchangePrincipal),
						extensionData.MarketplaceAssetID
					});
				}
				else
				{
					if (extensionData.Scope == null)
					{
						throw new ArgumentNullException("extensionData.Scope");
					}
					if (extensionData.RequestedCapabilities == null)
					{
						throw new ArgumentNullException("extensionData.RequestedCapabilities");
					}
					ExtensionsCacheEntry extensionsCacheEntry = null;
					if (this.extensionsDictionary.TryGetValue(extensionData.MarketplaceAssetID, out extensionsCacheEntry) && !InstalledExtensionTable.IsUpdateCheckTimeExpired(extensionsCacheEntry.LastUpdateCheckTime) && extensionsCacheEntry.Version == extensionData.Version)
					{
						ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.SubmitUpdateQuery: Extension {0} not added to query list because version matches recent cache entry", extensionData.MarketplaceAssetID);
					}
					else
					{
						UpdateRequestAsset updateRequestAsset = null;
						if (dictionary.TryGetValue(extensionData.MarketplaceAssetID, out updateRequestAsset))
						{
							ExtensionsCache.Tracer.TraceDebug<string, string, string>(0L, "ExtensionsCache.SubmitUpdateQuery: Extension {0} not added to query list because asset with same MarketplaceAssetID is already in list. ExtensionIds with same asset id: {1} {2}", extensionData.MarketplaceAssetID, extensionData.ExtensionId, updateRequestAsset.ExtensionID);
						}
						else
						{
							dictionary.Add(extensionData.MarketplaceAssetID, new UpdateRequestAsset
							{
								MarketplaceContentMarket = extensionData.MarketplaceContentMarket,
								ExtensionID = extensionData.ExtensionId,
								MarketplaceAssetID = extensionData.MarketplaceAssetID,
								RequestedCapabilities = extensionData.RequestedCapabilities.Value,
								Version = extensionData.Version,
								DisableReason = extensionData.DisableReason,
								Enabled = extensionData.Enabled,
								Scope = extensionData.Scope.Value,
								Etoken = extensionData.Etoken
							});
						}
					}
				}
			}
			if (dictionary.Count == 0)
			{
				ExtensionsCache.Tracer.TraceDebug(0L, "ExtensionsCache.SubmitUpdateQuery: UpdateRequestAssets count is 0. Updates query will not be started.");
				return;
			}
			queryContext.UpdateRequestAssets = dictionary;
			queryContext.DeploymentId = ExtensionDataHelper.GetDeploymentId(queryContext.Domain);
			this.QueueQueryItem(queryContext);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00029980 File Offset: 0x00027B80
		internal void QueueQueryItem(UpdateQueryContext queryContext)
		{
			GetUpdates getUpdates = null;
			lock (this.queryQueueLockObject)
			{
				if (this.queryQueue.Count > 500)
				{
					ExtensionsCache.Tracer.TraceError<IExchangePrincipal>(0L, "Query for {0} not added to the query queue because queue is full.", queryContext.ExchangePrincipal);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ExtensionUpdateQueryMaxExceeded, null, new object[]
					{
						"ProcessUpdates",
						ExtensionDiagnostics.GetLoggedMailboxIdentifier(queryContext.ExchangePrincipal)
					});
					return;
				}
				ExtensionsCache.Tracer.TraceDebug<IExchangePrincipal>(0L, "Adding query for {0} to the query queue.", queryContext.ExchangePrincipal);
				this.queryQueue.Enqueue(queryContext);
				if (this.GetUpdatesCount < 50)
				{
					getUpdates = new GetUpdates(this.urlsCache, this);
					this.GetUpdatesCount++;
					ExtensionsCache.Tracer.TraceDebug<int>(0L, "Creating a new instance of GetUpdates. GetUpdates Count {0}", this.GetUpdatesCount);
				}
				else
				{
					ExtensionsCache.Tracer.TraceDebug<int>(0L, "Too many GetUpdates commands. Query will be handled from pool. GetUpdates Count {0}", this.GetUpdatesCount);
				}
			}
			if (getUpdates != null)
			{
				this.ExecuteUpdateQuery(getUpdates);
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00029A9C File Offset: 0x00027C9C
		internal void ExecuteUpdateQuery(GetUpdates getUpdates)
		{
			UpdateQueryContext updateQueryContext = null;
			lock (this.queryQueueLockObject)
			{
				if (this.queryQueue.Count > 0)
				{
					updateQueryContext = this.queryQueue.Dequeue();
				}
				else
				{
					this.GetUpdatesCount--;
					if (this.GetUpdatesCount < 0)
					{
						throw new InvalidOperationException("GetUpdatesCount can't be less than 0.");
					}
					ExtensionsCache.Tracer.TraceDebug<int>(0L, "Query queue is empty. GetUpdates Count {0}", this.GetUpdatesCount);
				}
			}
			if (updateQueryContext != null)
			{
				ExtensionsCache.Tracer.TraceDebug<IExchangePrincipal>(0L, "Starting query for {0}.", updateQueryContext.ExchangePrincipal);
				if (this.SkipSubmitUpdateQueryForTest)
				{
					this.SubmitCount = updateQueryContext.UpdateRequestAssets.Count;
					return;
				}
				getUpdates.Execute(updateQueryContext);
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00029B68 File Offset: 0x00027D68
		internal void Update(AppStateResponseAsset appStateResponseAsset)
		{
			if (appStateResponseAsset == null)
			{
				throw new ArgumentNullException("appStateResponseAsset");
			}
			if (appStateResponseAsset.State == null)
			{
				throw new ArgumentNullException("appStateResponseAsset.State");
			}
			if (appStateResponseAsset.Version == null)
			{
				throw new ArgumentNullException("appStateResponseAsset.Version");
			}
			byte[] manifest = null;
			RequestedCapabilities? requestedCapabilities = null;
			ExtensionsCacheEntry extensionsCacheEntry = null;
			if (this.extensionsDictionary.TryGetValue(appStateResponseAsset.MarketplaceAssetID, out extensionsCacheEntry) && extensionsCacheEntry.Version == appStateResponseAsset.Version)
			{
				ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.Update: Since version is unchanged, getting properties from extension entry {0} for add", appStateResponseAsset.MarketplaceAssetID);
				requestedCapabilities = extensionsCacheEntry.RequestedCapabilities;
				if (GetUpdates.IsValidUpdateState(new OmexConstants.AppState?(appStateResponseAsset.State.Value)))
				{
					manifest = extensionsCacheEntry.Manifest;
				}
				else
				{
					manifest = null;
				}
			}
			ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.Update: Adding extension {0} from AppStateResponse", appStateResponseAsset.MarketplaceAssetID);
			ExtensionsCacheEntry entry = new ExtensionsCacheEntry(appStateResponseAsset.MarketplaceAssetID, appStateResponseAsset.ExtensionID, appStateResponseAsset.Version, requestedCapabilities, appStateResponseAsset.State.Value, manifest);
			this.AddExtension(entry);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00029C78 File Offset: 0x00027E78
		internal void Add(ExtensionData extensionData, OmexConstants.AppState state)
		{
			byte[] manifestBytes = extensionData.GetManifestBytes();
			if (manifestBytes == null || manifestBytes.Length == 0)
			{
				throw new ArgumentNullException("extensionData Manifest");
			}
			if (extensionData.Version == null)
			{
				throw new ArgumentNullException("extensionData Version");
			}
			if (extensionData.RequestedCapabilities == null)
			{
				throw new ArgumentNullException("extensionData RequestedCapabilities");
			}
			ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.Add: Adding Extension {0} from ExtensionData", extensionData.MarketplaceAssetID);
			ExtensionsCacheEntry entry = new ExtensionsCacheEntry(extensionData.MarketplaceAssetID, extensionData.ExtensionId, extensionData.Version, new RequestedCapabilities?(extensionData.RequestedCapabilities.Value), state, manifestBytes);
			this.AddExtension(entry);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00029D20 File Offset: 0x00027F20
		private void AddExtension(ExtensionsCacheEntry entry)
		{
			lock (this.extensionsDictionary.SyncRoot)
			{
				this.MaintainCacheSize(entry);
				this.extensionsDictionary.Add(entry.MarketplaceAssetID, entry);
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00029D78 File Offset: 0x00027F78
		private void MaintainCacheSize(ExtensionsCacheEntry entry)
		{
			int num = this.Size + entry.Size;
			if (num > 512000)
			{
				List<ExtensionsCacheEntry> list = new List<ExtensionsCacheEntry>();
				foreach (KeyValuePair<string, ExtensionsCacheEntry> keyValuePair in this.extensionsDictionary)
				{
					ExtensionsCacheEntry value = keyValuePair.Value;
					list.Add(value);
					num -= value.Size;
					if (num <= 460800)
					{
						break;
					}
				}
				foreach (ExtensionsCacheEntry extensionsCacheEntry in list)
				{
					ExtensionsCache.Tracer.TraceDebug<string>(0L, "ExtensionsCache.MaintainCacheSize: Removing Extension {0}.", extensionsCacheEntry.MarketplaceAssetID);
					this.extensionsDictionary.Remove(extensionsCacheEntry.MarketplaceAssetID);
				}
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ExtensionsCacheReachedMaxSize, null, new object[]
				{
					"ProcessUpdates"
				});
			}
			this.Size = num;
			ExtensionsCache.Tracer.TraceDebug<int>(0L, "ExtensionsCache.MaintainCacheSize: Current cache size {0}.", this.Size);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00029EA8 File Offset: 0x000280A8
		internal static string BuildClientInfoString(string clientInfoStringPrefix)
		{
			return clientInfoStringPrefix + "ExtensionUpdate";
		}

		// Token: 0x0400052B RID: 1323
		private const string ExtensionUpdateClientInfoPart = "ExtensionUpdate";

		// Token: 0x0400052C RID: 1324
		private const string ScenarioProcessUpdates = "ProcessUpdates";

		// Token: 0x0400052D RID: 1325
		private const int MaxEntryCount = 500000;

		// Token: 0x0400052E RID: 1326
		private const int QueryQueueMaxCount = 500;

		// Token: 0x0400052F RID: 1327
		internal const int GetUpdatesMaxCount = 50;

		// Token: 0x04000530 RID: 1328
		internal const int MaxCacheSize = 512000;

		// Token: 0x04000531 RID: 1329
		internal const int ReduceCacheSize = 460800;

		// Token: 0x04000532 RID: 1330
		internal const int CleanupFrequencyDays = 1;

		// Token: 0x04000533 RID: 1331
		internal const int CleanupLastUpdateCheckThresholdDays = 14;

		// Token: 0x04000534 RID: 1332
		private static ExtensionsCache singleton = new ExtensionsCache(OmexWebServiceUrlsCache.Singleton);

		// Token: 0x04000535 RID: 1333
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x04000536 RID: 1334
		private MruDictionary<string, ExtensionsCacheEntry> extensionsDictionary = new MruDictionary<string, ExtensionsCacheEntry>(500000, StringComparer.Ordinal, null);

		// Token: 0x04000537 RID: 1335
		private OmexWebServiceUrlsCache urlsCache;

		// Token: 0x04000538 RID: 1336
		private TokenRenewSubmitter tokenRenewSubmitter;

		// Token: 0x04000539 RID: 1337
		private object queryQueueLockObject = new object();

		// Token: 0x0400053A RID: 1338
		private Queue<UpdateQueryContext> queryQueue = new Queue<UpdateQueryContext>(500);
	}
}
