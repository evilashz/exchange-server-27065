using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker.Health;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Throttling
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncStoreLoadManager : DisposeTrackableBase
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		protected SyncStoreLoadManager(SyncLogSession syncLogSession)
		{
			ResourceLoadDelayInfo.Initialize();
			this.resourceMonitoringDictionary = new Dictionary<Guid, SyncResource>();
			this.monitoredResourcesResourcesKey = new Dictionary<string, ResourceKey[]>();
			this.syncLogSession = syncLogSession;
			this.sleepWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
			this.cloudLatencyAverage = new RunningAverageFloat(SyncStoreLoadManager.NumberOfPercentWIInStoreSamples);
			this.storeLatencyAverage = new RunningAverageFloat(SyncStoreLoadManager.NumberOfPercentWIInStoreSamples);
			this.storeCloudRatioAverage = new RunningAverageFloat(SyncStoreLoadManager.NumberOfPercentWIInStoreSamples);
			this.syncBudget = this.AcquireUnthrottledBudget(SyncStoreLoadManager.TransportSyncBudgetKey);
			this.isRunning = true;
			this.cloudLatencyAverage.Update(100f);
			this.storeLatencyAverage.Update(10f);
			this.storeCloudRatioAverage.Update(1f);
			this.randomDelay = new Random();
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000CCCD File Offset: 0x0000AECD
		internal static SyncStoreLoadManager Singleton
		{
			get
			{
				return SyncStoreLoadManager.singleton;
			}
			set
			{
				SyncStoreLoadManager.singleton = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		internal static SyncStoreLoadManager Instance
		{
			get
			{
				SyncStoreLoadManager result;
				lock (SyncStoreLoadManager.syncObject)
				{
					if (SyncStoreLoadManager.singleton == null)
					{
						throw new InvalidOperationException("Instance can be accessed only after Create.");
					}
					result = SyncStoreLoadManager.singleton;
				}
				return result;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		internal static float StoreLatencyAverage
		{
			get
			{
				return SyncStoreLoadManager.Instance.storeLatencyAverage.Value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000CD3D File Offset: 0x0000AF3D
		internal static float CloudLatencyAverage
		{
			get
			{
				return SyncStoreLoadManager.Instance.cloudLatencyAverage.Value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000CD4E File Offset: 0x0000AF4E
		internal static float StoreCloudRatioAverage
		{
			get
			{
				return SyncStoreLoadManager.Instance.storeCloudRatioAverage.Value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000CD5F File Offset: 0x0000AF5F
		protected SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000CD67 File Offset: 0x0000AF67
		internal static bool Sleep(int sleepTime)
		{
			return SyncStoreLoadManager.Instance.sleepWaitHandle.WaitOne(sleepTime);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		internal static void StartExecution()
		{
			SyncStoreLoadManager.Instance.isRunning = true;
			SyncStoreLoadManager.Instance.sleepWaitHandle.Reset();
			SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1241UL, ExTraceGlobals.SchedulerTracer, "StartExection of SyncStoreLoadManager!", new object[0]);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		internal static void StopExecution()
		{
			SyncStoreLoadManager.Instance.isRunning = false;
			SyncStoreLoadManager.Instance.sleepWaitHandle.Set();
			SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1242UL, ExTraceGlobals.SchedulerTracer, "StopExecution of SyncStoreLoadManager!", new object[0]);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000CE24 File Offset: 0x0000B024
		internal static void RecordPercentTimeInStore(float storeLatency, float cloudLatency, float storeCloudRatio)
		{
			SyncStoreLoadManager.Instance.storeLatencyAverage.Update(storeLatency);
			SyncStoreLoadManager.Instance.cloudLatencyAverage.Update(cloudLatency);
			SyncStoreLoadManager.Instance.storeCloudRatioAverage.Update(storeCloudRatio);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000CE5C File Offset: 0x0000B05C
		internal static void TrackMailItem(TransportMailItem transportMailItem, Guid userMailboxGuid, Guid subscriptionGuid, string cloudId)
		{
			SyncTransportServer orCreateSyncTransportServer = SyncStoreLoadManager.Instance.GetOrCreateSyncTransportServer();
			orCreateSyncTransportServer.TrackMailItem(transportMailItem, userMailboxGuid, subscriptionGuid, cloudId);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000CE7E File Offset: 0x0000B07E
		internal static void ThrottleAndExecuteStoreCall(MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete, string methodName, Action method)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, methodName, method, roundtripComplete);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		internal static Folder FolderBind(MailboxSession mailboxSession, StoreId storeId, PropertyDefinition[] properties, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Folder folder = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "FolderBind", delegate()
			{
				folder = Folder.Bind(mailboxSession, storeId, properties);
			}, roundtripComplete);
			return folder;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000CF18 File Offset: 0x0000B118
		internal static void FolderSave(Folder folder, MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "FolderSave", delegate()
			{
				folder.Save();
			}, roundtripComplete);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000CF68 File Offset: 0x0000B168
		internal static void FolderLoad(Folder folder, MailboxSession mailboxSession, PropertyDefinition[] properties, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "FolderLoad", delegate()
			{
				folder.Load(properties);
			}, roundtripComplete);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		internal static Folder FolderCreate(MailboxSession mailboxSession, StoreObjectId storeId, StoreObjectType objectType, string objectName, CreateMode createMode, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Folder folder = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "FolderCreate", delegate()
			{
				folder = Folder.Create(mailboxSession, storeId, objectType, objectName, createMode);
			}, roundtripComplete);
			return folder;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D060 File Offset: 0x0000B260
		internal static Item ItemBind(MailboxSession mailboxSession, StoreId storeId, PropertyDefinition[] properties, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Item item = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ItemBind", delegate()
			{
				item = Item.Bind(mailboxSession, storeId, properties);
			}, roundtripComplete);
			return item;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		internal static ConflictResolutionResult ItemSave(Item item, MailboxSession mailboxSession, SaveMode saveMode, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			ConflictResolutionResult result = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ItemSave", delegate()
			{
				result = item.Save(saveMode);
			}, roundtripComplete);
			return result;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D138 File Offset: 0x0000B338
		internal static void ItemLoad(Item item, MailboxSession mailboxSession, PropertyDefinition[] properties, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ItemLoad", delegate()
			{
				item.Load(properties);
			}, roundtripComplete);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D194 File Offset: 0x0000B394
		internal static MessageItem MessageItemCreateAggregated(MailboxSession mailboxSession, StoreId storeId, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			MessageItem item = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "MessageItemCreateAggregated", delegate()
			{
				item = MessageItem.CreateAggregated(mailboxSession, storeId);
			}, roundtripComplete);
			return item;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D200 File Offset: 0x0000B400
		internal static ConflictResolutionResult ContactSave(Contact contact, MailboxSession mailboxSession, SaveMode saveMode, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			ConflictResolutionResult result = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ContactSave", delegate()
			{
				result = contact.Save(saveMode);
			}, roundtripComplete);
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D264 File Offset: 0x0000B464
		internal static void ContactLoad(Contact contact, MailboxSession mailboxSession, PropertyDefinition[] properties, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ContactLoad", delegate()
			{
				contact.Load(properties);
			}, roundtripComplete);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal static Contact ContactCreate(MailboxSession mailboxSession, StoreId storeId, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Contact contact = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "ContactCreate", delegate()
			{
				contact = Contact.Create(mailboxSession, storeId);
			}, roundtripComplete);
			return contact;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D334 File Offset: 0x0000B534
		internal static AggregationSubscription LoadSubscription(MailboxSession mailboxSession, StoreId messageId, AggregationSubscriptionType subscriptionType, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			AggregationSubscription subscription = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "LoadSubscription", delegate()
			{
				subscription = SubscriptionManager.LoadSubscription(mailboxSession, messageId, subscriptionType);
			}, roundtripComplete);
			return subscription;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		internal static bool TrySaveSubscription(MailboxSession mailboxSession, AggregationSubscription subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete, out Exception exception)
		{
			SubscriptionMailboxSession subMailboxSession = new SubscriptionMailboxSession(mailboxSession);
			Exception saveException = null;
			if (SyncStoreLoadManager.TryPerformXsoOperation(mailboxSession, "SaveSubscription", delegate
			{
				SubscriptionManager.Instance.TrySaveSubscription(subMailboxSession, subscription, out saveException);
			}, roundtripComplete, out exception))
			{
				exception = saveException;
			}
			return exception != null;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D42C File Offset: 0x0000B62C
		internal static bool TryDeleteSubscription(MailboxSession mailboxSession, Guid subscriptionGuid, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			bool deleted = true;
			Exception ex;
			bool flag = SyncStoreLoadManager.TryPerformXsoOperation(mailboxSession, "TryDeleteSubscription", delegate
			{
				deleted = SubscriptionManager.TryDeleteSubscription(mailboxSession, subscriptionGuid);
			}, roundtripComplete, out ex);
			return flag && deleted;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		internal static bool TryDeleteSubscription(MailboxSession mailboxSession, AggregationSubscription subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			bool deleted = true;
			Exception ex;
			bool flag = SyncStoreLoadManager.TryPerformXsoOperation(mailboxSession, "TryDeleteSubscription", delegate
			{
				deleted = SubscriptionManager.TryDeleteSubscription(mailboxSession, subscription);
			}, roundtripComplete, out ex);
			return flag && deleted;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D52C File Offset: 0x0000B72C
		internal static SyncStateStorage SyncStateStorageCreate(MailboxSession mailboxSession, string protocol, string deviceType, string deviceId, StateStorageFeatures features, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStateStorage syncStateStorage = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "SyncStateStorageCreate", delegate()
			{
				syncStateStorage = SyncStateStorage.Create(mailboxSession, new DeviceIdentity(deviceId, deviceType, protocol), features, null);
			}, roundtripComplete);
			return syncStateStorage;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		internal static SyncStateStorage SyncStateStorageBind(MailboxSession mailboxSession, string protocol, string deviceType, string deviceId, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStateStorage syncStateStorage = null;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "SyncStateStorageBind", delegate()
			{
				syncStateStorage = SyncStateStorage.Bind(mailboxSession, new DeviceIdentity(deviceId, deviceType, protocol), null);
			}, roundtripComplete);
			return syncStateStorage;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D654 File Offset: 0x0000B854
		internal static bool SyncStateStorageDelete(MailboxSession mailboxSession, string protocol, string deviceType, string deviceId, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			bool deleteResult = false;
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "SyncStateStorageDelete", delegate()
			{
				deleteResult = SyncStateStorage.DeleteSyncStateStorage(mailboxSession, new DeviceIdentity(deviceId, deviceType, protocol), null);
			}, roundtripComplete);
			return deleteResult;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		internal static CustomSyncState SyncStateStorageGetCustomSyncState(Guid databaseGuid, SyncStateStorage syncStateStorage, SyncStateInfo syncStateInfo, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			CustomSyncState customSyncState = null;
			SyncStoreLoadManager.PerformXsoOperation(databaseGuid, syncStateStorage, "SyncStateStorageGetCustomSyncState", delegate()
			{
				customSyncState = syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
			}, roundtripComplete);
			return customSyncState;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D748 File Offset: 0x0000B948
		internal static CustomSyncState SyncStateStorageCreateCustomSyncState(Guid databaseGuid, SyncStateStorage syncStateStorage, SyncStateInfo syncStateInfo, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			CustomSyncState customSyncState = null;
			SyncStoreLoadManager.PerformXsoOperation(databaseGuid, syncStateStorage, "SyncStateStorageCreateCustomSyncState", delegate()
			{
				customSyncState = syncStateStorage.CreateCustomSyncState(syncStateInfo);
			}, roundtripComplete);
			return customSyncState;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		internal static void Link(MailboxSession mailboxSession, BulkAutomaticLink bulkAutomaticLink, Contact contact, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "Link", delegate()
			{
				bulkAutomaticLink.Link(contact);
			}, roundtripComplete);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000D804 File Offset: 0x0000BA04
		internal static void NotifyContactSaved(MailboxSession mailboxSession, BulkAutomaticLink bulkAutomaticLink, Contact contact, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession, "NotifyContactSaved", delegate()
			{
				bulkAutomaticLink.NotifyContactSaved(contact);
			}, roundtripComplete);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000D83D File Offset: 0x0000BA3D
		internal static void Create(SyncLogSession syncLogSession)
		{
			if (SyncStoreLoadManager.singleton != null)
			{
				throw new InvalidOperationException("Create should be called only once.");
			}
			SyncStoreLoadManager.singleton = new SyncStoreLoadManager(syncLogSession);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000D85C File Offset: 0x0000BA5C
		internal virtual void EnableLoadTrackingOnSession(MailboxSession mailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			mailboxSession.AccountingObject = this.syncBudget;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D878 File Offset: 0x0000BA78
		internal bool TryAcceptWorkItem(AggregationWorkItem workItem, out SubscriptionSubmissionResult result)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			result = SubscriptionSubmissionResult.Success;
			IEnumerable<SyncResource> orCreateSyncResources = this.GetOrCreateSyncResources(workItem);
			List<SyncResource> list = new List<SyncResource>();
			foreach (SyncResource syncResource in orCreateSyncResources)
			{
				if (!syncResource.TryAcceptWorkItem(workItem, out result))
				{
					this.SyncLogSession.LogVerbose((TSLID)1351UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI could not be accepted on Resource {0} - {1}.", new object[]
					{
						syncResource.ResourceId,
						syncResource.GetType().ToString()
					});
					foreach (SyncResource syncResource2 in list)
					{
						syncResource2.RemoveWorkItem(workItem);
					}
					return false;
				}
				list.Add(syncResource);
				this.SyncLogSession.LogVerbose((TSLID)1022UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI can be accepted on Resource {0} - {1}.", new object[]
				{
					syncResource.ResourceId,
					syncResource.GetType().ToString()
				});
			}
			this.SyncLogSession.LogVerbose((TSLID)1525UL, ExTraceGlobals.SchedulerTracer, "AcceptCheck: WI Accepted on all Resources.", new object[0]);
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		internal void RemoveWorkItem(AggregationWorkItem workItem)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			IEnumerable<SyncResource> orCreateSyncResources = this.GetOrCreateSyncResources(workItem);
			foreach (SyncResource syncResource in orCreateSyncResources)
			{
				syncResource.RemoveWorkItem(workItem);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000DA44 File Offset: 0x0000BC44
		protected SyncDB GetOrCreateSyncDB(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncResource syncResource;
			if (!this.resourceMonitoringDictionary.TryGetValue(databaseGuid, out syncResource))
			{
				lock (SyncStoreLoadManager.syncObject)
				{
					if (!this.resourceMonitoringDictionary.TryGetValue(databaseGuid, out syncResource))
					{
						syncResource = this.CreateSyncDB(databaseGuid);
						this.resourceMonitoringDictionary.Add(databaseGuid, syncResource);
					}
				}
			}
			return (SyncDB)syncResource;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		protected virtual SyncDB CreateSyncDB(Guid databaseGuid)
		{
			return SyncDB.CreateSyncDB(databaseGuid, this.SyncLogSession);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		protected SyncMailboxServer GetOrCreateSyncMailboxServer(Guid mailboxServerGuid, string mailboxServer)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxServerGuid", mailboxServerGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			SyncResource syncResource;
			if (!this.resourceMonitoringDictionary.TryGetValue(mailboxServerGuid, out syncResource))
			{
				lock (SyncStoreLoadManager.syncObject)
				{
					if (!this.resourceMonitoringDictionary.TryGetValue(mailboxServerGuid, out syncResource))
					{
						syncResource = this.CreateSyncMailboxServer(mailboxServerGuid, mailboxServer);
						this.resourceMonitoringDictionary.Add(mailboxServerGuid, syncResource);
					}
				}
			}
			return (SyncMailboxServer)syncResource;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000DB60 File Offset: 0x0000BD60
		protected virtual SyncMailboxServer CreateSyncMailboxServer(Guid mailboxServerGuid, string mailboxServer)
		{
			return SyncMailboxServer.CreateSyncMailboxServer(mailboxServerGuid, mailboxServer, this.SyncLogSession);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000DB70 File Offset: 0x0000BD70
		protected SyncTransportServer GetOrCreateSyncTransportServer()
		{
			SyncResource syncResource;
			if (!this.resourceMonitoringDictionary.TryGetValue(SyncStoreLoadManager.TransportServerMonitorKey, out syncResource))
			{
				lock (SyncStoreLoadManager.syncObject)
				{
					if (!this.resourceMonitoringDictionary.TryGetValue(SyncStoreLoadManager.TransportServerMonitorKey, out syncResource))
					{
						syncResource = this.CreateSyncTransportServer(AggregationConfiguration.Instance.MaxPendingMessagesInTransportQueueForTheServer, AggregationConfiguration.Instance.MaxPendingMessagesInTransportQueuePerUser);
						this.resourceMonitoringDictionary.Add(SyncStoreLoadManager.TransportServerMonitorKey, syncResource);
					}
				}
			}
			return (SyncTransportServer)syncResource;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000DC04 File Offset: 0x0000BE04
		protected virtual SyncTransportServer CreateSyncTransportServer(int maxPendingMessages, int maxPendingMessagesPerUser)
		{
			return SyncTransportServer.CreateSyncTransportServer(this.SyncLogSession, maxPendingMessages, maxPendingMessagesPerUser);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000DC14 File Offset: 0x0000BE14
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (SyncStoreLoadManager.syncObject)
				{
					this.syncBudget.Dispose();
					this.syncBudget = null;
					this.resourceMonitoringDictionary.Clear();
					this.resourceMonitoringDictionary = null;
					this.sleepWaitHandle.Close();
					this.sleepWaitHandle = null;
					SyncStoreLoadManager.singleton = null;
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncStoreLoadManager>(this);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000DC94 File Offset: 0x0000BE94
		private static ThrottlingInfo ThrottleStoreCall(IExchangePrincipal exchangePrincipal, Guid databaseGuid)
		{
			return SyncStoreLoadManager.ThrottleStoreCall(exchangePrincipal.MailboxInfo.Location.ServerGuid, exchangePrincipal.MailboxInfo.Location.ServerFqdn, databaseGuid);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		private static ThrottlingInfo ThrottleStoreCall(Guid mailboxServerGuid, string mailboxServer, Guid databaseGuid)
		{
			IEnumerable<SyncResource> orCreateSyncResources = SyncStoreLoadManager.Instance.GetOrCreateSyncResources(mailboxServerGuid, mailboxServer, databaseGuid);
			ResourceKey[] orCreateSyncResourceKeys = SyncStoreLoadManager.Instance.GetOrCreateSyncResourceKeys(orCreateSyncResources);
			ResourceKey resourceKey = null;
			SyncResourceMonitorType monitor = SyncResourceMonitorType.Unknown;
			int num = 0;
			bool flag = true;
			TimeSpan minValue = TimeSpan.MinValue;
			ThrottlingInfo throttlingInfo = new ThrottlingInfo();
			while (flag)
			{
				if (!SyncStoreLoadManager.Instance.isRunning)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1244UL, ExTraceGlobals.SchedulerTracer, "Work item stopping retry for unhealthy resource due to SyncStoreLoadManager StopExecution!", new object[0]);
					return throttlingInfo;
				}
				num = SyncStoreLoadManager.Instance.GetDelay(orCreateSyncResourceKeys, out resourceKey);
				if (num > 0)
				{
					SyncStoreLoadManager.Instance.syncBudget.ResetWorkAccomplished();
				}
				bool flag2;
				bool flag3;
				monitor = SyncStoreLoadManager.Instance.IsAnyResourceUnhealthyOrUnknown(orCreateSyncResources, out flag2, out flag3);
				if (minValue >= AggregationConfiguration.Instance.SyncDurationThreshold)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1522UL, ExTraceGlobals.SchedulerTracer, "Work Item has exceeded the SyncDuration Threshold!", new object[0]);
					throw new SyncStoreUnhealthyException(databaseGuid, num);
				}
				Stopwatch stopwatch = Stopwatch.StartNew();
				if (num == 0)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1531UL, ExTraceGlobals.SchedulerTracer, "All Resources Healthy!", new object[]
					{
						num
					});
					stopwatch.Stop();
					break;
				}
				if (resourceKey != null)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1467UL, ExTraceGlobals.SchedulerTracer, "The offending resource {0} has delay of {1} milliseconds.", new object[]
					{
						resourceKey,
						num
					});
				}
				if (num == 2147483647)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1532UL, ExTraceGlobals.SchedulerTracer, "Delay for DB {0} and MailboxServer {1} is Int32.MaxValue.", new object[]
					{
						databaseGuid,
						mailboxServerGuid
					});
				}
				if (num > SyncStoreLoadManager.MaxBackOffValue)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1533UL, ExTraceGlobals.SchedulerTracer, "Backoff Delay is {0}. The value will be adjusted to {1}", new object[]
					{
						num,
						SyncStoreLoadManager.MaxBackOffValue
					});
					num = SyncStoreLoadManager.MaxBackOffValue;
				}
				SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1352UL, ExTraceGlobals.SchedulerTracer, "Budget snapshot for DB {0} and MailboxServer {1}: {2}", new object[]
				{
					databaseGuid,
					mailboxServerGuid,
					SyncStoreLoadManager.Instance.syncBudget.ToString()
				});
				if (SyncStoreLoadManager.BackOffOverride > 0)
				{
					num = SyncStoreLoadManager.BackOffOverride;
				}
				else if (flag3)
				{
					num = SyncStoreLoadManager.BackOffForUnknownHealth;
				}
				ResourceKey resourceKey2 = null;
				foreach (SyncResource syncResource in orCreateSyncResources)
				{
					SyncResourceMonitor[] resourceMonitors = syncResource.GetResourceMonitors();
					foreach (SyncResourceMonitor syncResourceMonitor in resourceMonitors)
					{
						if (syncResourceMonitor.ResourceKey.Equals(resourceKey))
						{
							int suggestedDelay = syncResource.GetSuggestedDelay(num);
							int num2 = SyncStoreLoadManager.Instance.GetRandomDelay(suggestedDelay);
							SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1534UL, ExTraceGlobals.SchedulerTracer, "Current delay for {0} is {1}. Suggested Delay is {2}. Random delay is {3}", new object[]
							{
								resourceKey,
								num,
								suggestedDelay,
								num2
							});
							num = num2;
							syncResource.UpdateDelay(num);
							resourceKey2 = syncResourceMonitor.ResourceKey;
							monitor = syncResourceMonitor.SyncResourceMonitorType;
							break;
						}
						SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1535UL, ExTraceGlobals.SchedulerTracer, "Delay not updated for {0}", new object[]
						{
							syncResourceMonitor.ResourceKey
						});
					}
				}
				if (resourceKey2 == null)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1536UL, ExTraceGlobals.SchedulerTracer, "No backoff applied. Could not find the offending resource!", new object[0]);
					stopwatch.Stop();
					break;
				}
				ResourceLoadState health;
				if (!flag2 && !flag3)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1537UL, ExTraceGlobals.SchedulerTracer, "Resource Fair {0} : Sleeping for {1} milliseconds, then accepting!", new object[]
					{
						resourceKey2,
						num
					});
					health = ResourceLoadState.Overloaded;
					flag = false;
				}
				else if (flag3)
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1538UL, ExTraceGlobals.SchedulerTracer, "Resource Unknown {0} : Sleeping for {1} milliseconds, then accepting!", new object[]
					{
						resourceKey2,
						num
					});
					health = ResourceLoadState.Unknown;
					flag = false;
				}
				else
				{
					SyncStoreLoadManager.Instance.SyncLogSession.LogVerbose((TSLID)1539UL, ExTraceGlobals.SchedulerTracer, "Resource Unhealthy {0} : Sleeping for {1} milliseconds, then retrying", new object[]
					{
						resourceKey2,
						num
					});
					health = ResourceLoadState.Critical;
				}
				SyncStoreLoadManager.Sleep(num);
				stopwatch.Stop();
				minValue.Add(stopwatch.Elapsed);
				throttlingInfo.Add(monitor, health, num);
			}
			return throttlingInfo;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		private static bool TryPerformXsoOperation(MailboxSession mailboxSession, string xsoOperationName, Action xsoOperation, EventHandler<RoundtripCompleteEventArgs> roundtripComplete, out Exception exception)
		{
			exception = null;
			try
			{
				SyncStoreLoadManager.PerformXsoOperation(mailboxSession, xsoOperationName, xsoOperation, roundtripComplete);
			}
			catch (SyncStoreUnhealthyException ex)
			{
				exception = ex;
			}
			return exception != null;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E238 File Offset: 0x0000C438
		private static void PerformXsoOperation(MailboxSession mailboxSession, string xsoOperationName, Action xsoOperation, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(mailboxSession.MdbGuid, mailboxSession.MailboxOwner, xsoOperationName, xsoOperation, roundtripComplete);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E24E File Offset: 0x0000C44E
		private static void PerformXsoOperation(Guid databaseGuid, SyncStateStorage syncStateStorage, string xsoOperationName, Action xsoOperation, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncStoreLoadManager.PerformXsoOperation(databaseGuid, syncStateStorage.MailboxOwner, xsoOperationName, xsoOperation, roundtripComplete);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E260 File Offset: 0x0000C460
		private static void PerformXsoOperation(Guid databaseGuid, IExchangePrincipal exchangePrincipal, string xsoOperationName, Action xsoOperation, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool roundtripSuccessful = false;
			try
			{
				xsoOperation();
				roundtripSuccessful = true;
			}
			finally
			{
				stopwatch.Stop();
				ThrottlingInfo throttlingInfo = SyncStoreLoadManager.ThrottleStoreCall(exchangePrincipal, databaseGuid);
				if (roundtripComplete != null)
				{
					SyncDB orCreateSyncDB = SyncStoreLoadManager.Instance.GetOrCreateSyncDB(databaseGuid);
					orCreateSyncDB.NotifyStoreRoundtripComplete(xsoOperationName, roundtripComplete, new RoundtripCompleteEventArgs(stopwatch.Elapsed, throttlingInfo, roundtripSuccessful));
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		private IBudget AcquireUnthrottledBudget(string budgetKey)
		{
			return StandardBudget.Acquire(new UnthrottledBudgetKey(budgetKey, BudgetType.ResourceTracking));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		private int GetRandomDelay(int backoffDelay)
		{
			int result;
			lock (SyncStoreLoadManager.syncObject)
			{
				result = this.randomDelay.Next(1, backoffDelay);
			}
			return result;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E31C File Offset: 0x0000C51C
		private SyncResourceMonitorType IsAnyResourceUnhealthyOrUnknown(IEnumerable<SyncResource> syncResources, out bool isAnyResourceUnhealthy, out bool isAnyResourceUnknown)
		{
			isAnyResourceUnhealthy = false;
			isAnyResourceUnknown = false;
			List<SyncResourceMonitor> list = new List<SyncResourceMonitor>();
			foreach (SyncResource syncResource in syncResources)
			{
				list.AddRange(syncResource.GetResourceMonitors());
			}
			return SyncResourceMonitor.IsAnyResourceUnhealthyOrUnknown(null, list, out isAnyResourceUnhealthy, out isAnyResourceUnknown);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E380 File Offset: 0x0000C580
		private IEnumerable<SyncResource> GetOrCreateSyncResources(AggregationWorkItem workItem)
		{
			List<SyncResource> list = (List<SyncResource>)this.GetOrCreateSyncResources(workItem.MailboxServerGuid, workItem.MailboxServer, workItem.DatabaseGuid);
			if (workItem.AggregationType == AggregationType.Aggregation)
			{
				list.Add(this.GetOrCreateSyncTransportServer());
			}
			return list;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		private IEnumerable<SyncResource> GetOrCreateSyncResources(Guid mailboxServerGuid, string mailboxServer, Guid databaseGuid)
		{
			return new List<SyncResource>
			{
				this.GetOrCreateSyncMailboxServer(mailboxServerGuid, mailboxServer),
				this.GetOrCreateSyncDB(databaseGuid)
			};
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		private ResourceKey[] GetOrCreateSyncResourceKeys(IEnumerable<SyncResource> syncResources)
		{
			ResourceKey[] array = null;
			string combinedResourcesId = this.GetCombinedResourcesId(syncResources);
			lock (SyncStoreLoadManager.syncObject)
			{
				if (this.monitoredResourcesResourcesKey.ContainsKey(combinedResourcesId))
				{
					array = this.monitoredResourcesResourcesKey[combinedResourcesId];
				}
				else
				{
					array = this.GetSyncResourcesKey(syncResources);
					this.monitoredResourcesResourcesKey.Add(combinedResourcesId, array);
				}
			}
			return array;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E468 File Offset: 0x0000C668
		private string GetCombinedResourcesId(IEnumerable<SyncResource> syncResources)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SyncResource syncResource in syncResources)
			{
				string resourceId = syncResource.ResourceId;
				stringBuilder.Append(resourceId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		private ResourceKey[] GetSyncResourcesKey(IEnumerable<SyncResource> syncResources)
		{
			List<ResourceKey> list = new List<ResourceKey>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SyncResource syncResource in syncResources)
			{
				SyncResourceMonitor[] resourceMonitors = syncResource.GetResourceMonitors();
				foreach (SyncResourceMonitor syncResourceMonitor in resourceMonitors)
				{
					ResourceKey resourceKey = syncResourceMonitor.ResourceKey;
					if (resourceKey != null)
					{
						list.Add(resourceKey);
						stringBuilder.Append(resourceKey);
						stringBuilder.Append("/");
					}
				}
			}
			SyncStoreLoadManager.Instance.SyncLogSession.LogDebugging((TSLID)1524UL, ExTraceGlobals.SchedulerTracer, "GetSyncResourcesKey Count {0} - value {1}", new object[]
			{
				list.Count,
				stringBuilder
			});
			return list.ToArray();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		private int GetDelay(ResourceKey[] syncResourceKeys, out ResourceKey offendingResource)
		{
			offendingResource = null;
			DelayInfo delay = ResourceLoadDelayInfo.GetDelay(this.syncBudget, SyncStoreLoadManager.workloadSettings, syncResourceKeys, true);
			if (delay.Delay > TimeSpan.Zero)
			{
				ResourceLoadDelayInfo resourceLoadDelayInfo = delay as ResourceLoadDelayInfo;
				if (resourceLoadDelayInfo != null)
				{
					offendingResource = resourceLoadDelayInfo.ResourceKey;
				}
			}
			if (delay.Delay.TotalMilliseconds < 2147483647.0)
			{
				return (int)delay.Delay.TotalMilliseconds;
			}
			return int.MaxValue;
		}

		// Token: 0x04000185 RID: 389
		private static readonly Guid TransportServerMonitorKey = Guid.NewGuid();

		// Token: 0x04000186 RID: 390
		private static readonly string TransportSyncBudgetKey = "TransportSyncBudget";

		// Token: 0x04000187 RID: 391
		private static readonly WorkloadSettings workloadSettings = new WorkloadSettings(WorkloadType.TransportSync, true);

		// Token: 0x04000188 RID: 392
		private static readonly object syncObject = new object();

		// Token: 0x04000189 RID: 393
		private static readonly ushort NumberOfPercentWIInStoreSamples = 100;

		// Token: 0x0400018A RID: 394
		private static readonly int BackOffForUnknownHealth = 10;

		// Token: 0x0400018B RID: 395
		private static readonly int MaxBackOffValue = 1000;

		// Token: 0x0400018C RID: 396
		private static readonly int BackOffOverride = 0;

		// Token: 0x0400018D RID: 397
		private static SyncStoreLoadManager singleton = null;

		// Token: 0x0400018E RID: 398
		private SyncLogSession syncLogSession;

		// Token: 0x0400018F RID: 399
		private RunningAverageFloat storeLatencyAverage;

		// Token: 0x04000190 RID: 400
		private RunningAverageFloat cloudLatencyAverage;

		// Token: 0x04000191 RID: 401
		private RunningAverageFloat storeCloudRatioAverage;

		// Token: 0x04000192 RID: 402
		private Dictionary<string, ResourceKey[]> monitoredResourcesResourcesKey;

		// Token: 0x04000193 RID: 403
		private EventWaitHandle sleepWaitHandle;

		// Token: 0x04000194 RID: 404
		private Dictionary<Guid, SyncResource> resourceMonitoringDictionary;

		// Token: 0x04000195 RID: 405
		private IBudget syncBudget;

		// Token: 0x04000196 RID: 406
		private Random randomDelay;

		// Token: 0x04000197 RID: 407
		private volatile bool isRunning;
	}
}
