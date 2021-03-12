using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020E RID: 526
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class StateStorage : DisposeTrackableBase, IStateStorage, INativeStateStorage, ISimpleStateStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0003A038 File Offset: 0x00038238
		internal StateStorage(MailboxSession mailboxSession, ISyncWorkerData subscription, SyncLogSession syncLogSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.subscriptionGuid = subscription.SubscriptionGuid;
			this.mailboxSession = mailboxSession;
			this.syncLogSession = syncLogSession;
			string deviceId = SubscriptionManager.GenerateDeviceId(subscription.SubscriptionGuid);
			bool flag = false;
			try
			{
				this.syncStateStorage = SyncStoreLoadManager.SyncStateStorageBind(mailboxSession, SubscriptionManager.Protocol, subscription.SubscriptionType.ToString(), deviceId, roundtripComplete);
				if (this.syncStateStorage == null)
				{
					this.syncStateStorage = SyncStoreLoadManager.SyncStateStorageCreate(mailboxSession, SubscriptionManager.Protocol, subscription.SubscriptionType.ToString(), deviceId, StateStorageFeatures.ContentState, roundtripComplete);
				}
				if (this.syncStateStorage == null)
				{
					syncLogSession.LogError((TSLID)1488UL, StateStorage.Tracer, "Constructor: Could not create XSO SyncStateStorage.", new object[0]);
					throw new ArgumentNullException("syncStateStorage");
				}
				StateStorage.EnsureSyncStateTypesRegistered();
				this.LoadCustomSyncState(roundtripComplete);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (this.storageCustomSyncState != null)
					{
						this.storageCustomSyncState.Dispose();
						this.storageCustomSyncState = null;
					}
					if (this.syncStateStorage != null)
					{
						this.syncStateStorage.Dispose();
						this.syncStateStorage = null;
					}
				}
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0003A174 File Offset: 0x00038374
		public SyncStateStorage SyncStateStorage
		{
			get
			{
				return this.syncStateStorage;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0003A17C File Offset: 0x0003837C
		public bool IsDirty
		{
			get
			{
				return this.customSyncState.IsDirty;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0003A189 File Offset: 0x00038389
		public SyncProgress SyncProgress
		{
			get
			{
				return this.syncProgress;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0003A191 File Offset: 0x00038391
		public bool ForceRecoverySyncNext
		{
			get
			{
				return this.forceRecoverySyncNext;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003A199 File Offset: 0x00038399
		public bool InitialSyncDone
		{
			get
			{
				return this.customSyncState.InitialSyncDone;
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003A1A8 File Offset: 0x000383A8
		public static bool TryDelete(MailboxSession mailboxSession, ISyncWorkerData subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			bool result;
			try
			{
				SyncStoreLoadManager.SyncStateStorageDelete(mailboxSession, SubscriptionManager.Protocol, subscription.SubscriptionType.ToString(), SubscriptionManager.GenerateDeviceId(subscription.SubscriptionGuid), roundtripComplete);
				result = true;
			}
			catch (StoragePermanentException)
			{
				result = false;
			}
			catch (StorageTransientException)
			{
				result = false;
			}
			catch (SerializationException)
			{
				result = false;
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			catch (SyncStoreUnhealthyException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003A23C File Offset: 0x0003843C
		public void MarkInitialSyncDone()
		{
			this.customSyncState.MarkInitialSyncDone();
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003A249 File Offset: 0x00038449
		public void SetSyncProgress(SyncProgress progress)
		{
			this.syncProgress = progress;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003A252 File Offset: 0x00038452
		public void SetForceRecoverySyncNext(bool forceRecoverySyncNext)
		{
			this.forceRecoverySyncNext = forceRecoverySyncNext;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003A25B File Offset: 0x0003845B
		public void AddProperty(string property, string value)
		{
			this.customSyncState.AddProperty(property, value);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003A26A File Offset: 0x0003846A
		public bool TryGetPropertyValue(string property, out string value)
		{
			return this.customSyncState.TryGetPropertyValue(property, out value);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003A279 File Offset: 0x00038479
		public bool TryRemoveProperty(string property)
		{
			return this.customSyncState.TryRemoveProperty(property);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003A287 File Offset: 0x00038487
		public void ChangePropertyValue(string property, string value)
		{
			this.customSyncState.ChangePropertyValue(property, value);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003A296 File Offset: 0x00038496
		public bool ContainsProperty(string property)
		{
			return this.customSyncState.ContainsProperty(property);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public bool ContainsItem(string cloudId)
		{
			return this.customSyncState.ContainsItem(cloudId);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003A2B2 File Offset: 0x000384B2
		public bool ContainsFailedItem(string cloudId)
		{
			return this.customSyncState.ContainsFailedItem(cloudId);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003A2C0 File Offset: 0x000384C0
		public bool ContainsFolder(StoreObjectId nativeId)
		{
			return this.customSyncState.ContainsFolder(nativeId);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003A2CE File Offset: 0x000384CE
		public bool ContainsFolder(string cloudId)
		{
			return this.customSyncState.ContainsFolder(cloudId);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003A2DC File Offset: 0x000384DC
		public bool ContainsFailedFolder(string cloudId)
		{
			return this.customSyncState.ContainsFailedFolder(cloudId);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003A2EA File Offset: 0x000384EA
		public bool ContainsItem(StoreObjectId nativeId)
		{
			return this.customSyncState.ContainsItem(nativeId);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003A2F8 File Offset: 0x000384F8
		public bool TryAddItem(string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> itemProperties)
		{
			return this.customSyncState.TryAddItem(cloudId, cloudFolderId, nativeId, changeKey, nativeFolderId, cloudVersion, itemProperties);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003A310 File Offset: 0x00038510
		public bool TryAddFailedItem(string cloudId, string cloudFolderId)
		{
			return this.customSyncState.TryAddFailedItem(cloudId, cloudFolderId);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003A320 File Offset: 0x00038520
		public bool TryFindItem(string cloudId, out string cloudFolderId, out string cloudVersion)
		{
			Dictionary<string, string> dictionary;
			return this.TryFindItem(cloudId, out cloudFolderId, out cloudVersion, out dictionary);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0003A338 File Offset: 0x00038538
		public bool TryFindItem(string cloudId, out string cloudFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties)
		{
			StoreObjectId storeObjectId;
			byte[] array;
			StoreObjectId storeObjectId2;
			return this.TryFindItem(cloudId, out cloudFolderId, out storeObjectId, out array, out storeObjectId2, out cloudVersion, out itemProperties);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003A356 File Offset: 0x00038556
		public bool TryFindItem(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties)
		{
			return this.customSyncState.TryFindItem(cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out itemProperties);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003A36E File Offset: 0x0003856E
		public bool TryFindItem(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties)
		{
			return this.customSyncState.TryFindItem(nativeId, out cloudId, out cloudFolderId, out changeKey, out nativeFolderId, out cloudVersion, out itemProperties);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003A386 File Offset: 0x00038586
		public bool TryUpdateItem(StoreObjectId nativeId, byte[] changeKey, string cloudVersion, Dictionary<string, string> itemProperties)
		{
			return this.customSyncState.TryUpdateItem(nativeId, changeKey, cloudVersion, itemProperties);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003A398 File Offset: 0x00038598
		public bool TryRemoveItem(string cloudId)
		{
			return this.customSyncState.TryRemoveItem(cloudId);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0003A3A6 File Offset: 0x000385A6
		public bool TryRemoveItem(StoreObjectId nativeId)
		{
			return this.customSyncState.TryRemoveItem(nativeId);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public bool TryRemoveFailedItem(string cloudId)
		{
			return this.customSyncState.TryRemoveFailedItem(cloudId);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0003A3C4 File Offset: 0x000385C4
		public bool TryAddFolder(bool isInbox, string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties)
		{
			return this.customSyncState.TryAddFolder(isInbox, cloudId, cloudFolderId, nativeId, changeKey, nativeFolderId, cloudVersion, folderProperties);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003A3E9 File Offset: 0x000385E9
		public bool TryAddFailedFolder(string cloudId, string cloudFolderId)
		{
			return this.customSyncState.TryAddFailedFolder(cloudId, cloudFolderId);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0003A3F8 File Offset: 0x000385F8
		public bool TryFindFolder(string cloudId, out string cloudFolderId, out string cloudVersion)
		{
			Dictionary<string, string> dictionary;
			return this.TryFindFolder(cloudId, out cloudFolderId, out cloudVersion, out dictionary);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003A410 File Offset: 0x00038610
		public bool TryFindFolder(string cloudId, out string cloudFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties)
		{
			StoreObjectId storeObjectId;
			byte[] array;
			StoreObjectId storeObjectId2;
			return this.TryFindFolder(cloudId, out cloudFolderId, out storeObjectId, out array, out storeObjectId2, out cloudVersion, out folderProperties);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003A42E File Offset: 0x0003862E
		public bool TryFindFolder(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties)
		{
			return this.customSyncState.TryFindFolder(cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out folderProperties);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0003A446 File Offset: 0x00038646
		public bool TryFindFolder(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties)
		{
			return this.customSyncState.TryFindFolder(nativeId, out cloudId, out cloudFolderId, out changeKey, out nativeFolderId, out cloudVersion, out folderProperties);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003A460 File Offset: 0x00038660
		public bool TryUpdateFolder(ISyncWorkerData subscription, string cloudId, string newCloudId, string cloudVersion)
		{
			if ((subscription.SyncQuirks & SyncQuirks.AllowDirectCloudFolderUpdates) == SyncQuirks.None)
			{
				throw new InvalidOperationException("Direct update of folders by cloud ID is not allowed for this subscription. You should not be using this API.");
			}
			string text;
			StoreObjectId nativeId;
			byte[] changeKey;
			StoreObjectId storeObjectId;
			string text2;
			Dictionary<string, string> folderProperties;
			if (!this.TryFindFolder(cloudId, out text, out nativeId, out changeKey, out storeObjectId, out text2, out folderProperties))
			{
				return false;
			}
			bool isInbox = false;
			return this.TryUpdateFolder(isInbox, nativeId, null, cloudId, newCloudId, null, changeKey, null, cloudVersion, folderProperties);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003A4B0 File Offset: 0x000386B0
		public bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId, string cloudId, string newCloudId, string newCloudFolderId, byte[] changeKey, StoreObjectId newNativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties)
		{
			return this.customSyncState.TryUpdateFolder(isInbox, nativeId, newNativeId, cloudId, newCloudId, newCloudFolderId, changeKey, newNativeFolderId, cloudVersion, folderProperties);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003A4D9 File Offset: 0x000386D9
		public bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId)
		{
			return this.customSyncState.TryUpdateFolder(isInbox, nativeId, newNativeId);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003A4E9 File Offset: 0x000386E9
		public bool TryRemoveFolder(string cloudId)
		{
			return this.customSyncState.TryRemoveFolder(cloudId);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003A4F7 File Offset: 0x000386F7
		public bool TryRemoveFailedFolder(string cloudId)
		{
			return this.customSyncState.TryRemoveFailedFolder(cloudId);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003A505 File Offset: 0x00038705
		public bool TryRemoveFolder(StoreObjectId nativeId)
		{
			return this.customSyncState.TryRemoveFolder(nativeId);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003A513 File Offset: 0x00038713
		public IEnumerator<string> GetCloudItemEnumerator()
		{
			return this.customSyncState.GetCloudItemEnumerator();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003A520 File Offset: 0x00038720
		public IEnumerator<string> GetFailedCloudItemEnumerator()
		{
			return this.customSyncState.GetFailedCloudItemEnumerator();
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003A52D File Offset: 0x0003872D
		public IEnumerator<string> GetCloudFolderEnumerator()
		{
			return this.customSyncState.GetCloudFolderEnumerator();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003A53A File Offset: 0x0003873A
		public IEnumerator<StoreObjectId> GetNativeItemEnumerator()
		{
			return this.customSyncState.GetNativeItemEnumerator();
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003A547 File Offset: 0x00038747
		public IEnumerator<StoreObjectId> GetNativeFolderEnumerator()
		{
			return this.customSyncState.GetNativeFolderEnumerator();
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003A554 File Offset: 0x00038754
		public IEnumerator<string> GetCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return this.customSyncState.GetCloudItemFilteredByCloudFolderIdEnumerator(cloudFolderId);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0003A562 File Offset: 0x00038762
		public IEnumerator<string> GetCloudFolderFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return this.customSyncState.GetCloudFolderFilteredByCloudFolderIdEnumerator(cloudFolderId);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003A570 File Offset: 0x00038770
		public IEnumerator<StoreObjectId> GetNativeItemFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId)
		{
			return this.customSyncState.GetNativeItemFilteredByNativeFolderIdEnumerator(nativeFolderId);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003A57E File Offset: 0x0003877E
		public IEnumerator<StoreObjectId> GetNativeFolderFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId)
		{
			return this.customSyncState.GetNativeFolderFilteredByNativeFolderIdEnumerator(nativeFolderId);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003A58C File Offset: 0x0003878C
		public IEnumerator<string> GetFailedCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return this.customSyncState.GetFailedCloudItemFilteredByCloudFolderIdEnumerator(cloudFolderId);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003A59A File Offset: 0x0003879A
		public bool TryUpdateItemCloudVersion(string cloudId, string cloudVersion)
		{
			return this.customSyncState.TryUpdateItemCloudVersion(cloudId, cloudVersion);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003A5A9 File Offset: 0x000387A9
		public bool TryUpdateFolderCloudVersion(string cloudId, string cloudVersion)
		{
			return this.customSyncState.TryUpdateFolderCloudVersion(cloudId, cloudVersion);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003A5B8 File Offset: 0x000387B8
		public bool ShouldPromoteItemTransientException(string cloudId, SyncTransientException exception)
		{
			return this.customSyncState.ShouldPromoteItemTransientException(cloudId, exception);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003A5C7 File Offset: 0x000387C7
		public bool ShouldPromoteItemTransientException(StoreObjectId nativeId, SyncTransientException exception)
		{
			return this.customSyncState.ShouldPromoteItemTransientException(nativeId, exception);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0003A5D6 File Offset: 0x000387D6
		public bool ShouldPromoteFolderTransientException(string cloudId, SyncTransientException exception)
		{
			return this.customSyncState.ShouldPromoteFolderTransientException(cloudId, exception);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003A5E5 File Offset: 0x000387E5
		public bool ShouldPromoteFolderTransientException(StoreObjectId nativeId, SyncTransientException exception)
		{
			return this.customSyncState.ShouldPromoteFolderTransientException(nativeId, exception);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003A5F4 File Offset: 0x000387F4
		public Exception Commit(bool commitState, MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			Exception ex = null;
			try
			{
				if (this.requiresReload)
				{
					this.syncLogSession.LogDebugging((TSLID)1489UL, StateStorage.Tracer, "Loading State Storage as reload was set.", new object[0]);
					this.storageCustomSyncState.KeepCachedDataOnReload = true;
					this.storageCustomSyncState.Load();
					this.requiresReload = false;
					this.storageCustomSyncState.KeepCachedDataOnReload = false;
				}
				if (this.IsDirty && commitState)
				{
					this.storageCustomSyncState.Commit();
					long lastUncompressedSize = this.storageCustomSyncState.GetLastUncompressedSize();
					UncompressedSyncStateSizeExceededException innerException;
					if (StateStorage.mappingTableSizeChecker.IsUncompressedSyncStateExceededBounds(this.subscriptionGuid, StateStorage.UnifiedStateTypeInfo.UniqueName, lastUncompressedSize, out innerException))
					{
						ex = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.SyncStateSizeError, innerException);
					}
					else
					{
						this.syncLogSession.LogDebugging((TSLID)1449UL, StateStorage.Tracer, "MappingTable syncState size in memory {0}", new object[]
						{
							lastUncompressedSize
						});
						this.storageCustomSyncState.Dispose();
						this.storageCustomSyncState = null;
					}
				}
			}
			catch (StoragePermanentException ex2)
			{
				CompressedSyncStateSizeExceededException innerException2;
				if (StateStorage.mappingTableSizeChecker.IsCompressedSyncStateExceededBounds(this.subscriptionGuid, StateStorage.UnifiedStateTypeInfo.UniqueName, ex2, out innerException2))
				{
					ex = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.SyncStateSizeError, innerException2);
				}
				else
				{
					ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex2, true);
				}
			}
			catch (StorageTransientException ex3)
			{
				this.requiresReload = true;
				this.syncLogSession.LogDebugging((TSLID)977UL, StateStorage.Tracer, "Marking state storage as requiring reload before commit as a transient exception was hit when reloading: {0}", new object[]
				{
					ex3
				});
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex3);
			}
			catch (SerializationException innerException3)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException3);
			}
			catch (InvalidOperationException innerException4)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException4);
			}
			catch (SyncStoreUnhealthyException ex4)
			{
				this.requiresReload = true;
				this.syncLogSession.LogDebugging((TSLID)1354UL, StateStorage.Tracer, "Marking state storage as requiring reload before commit as a store unhealthy exception was hit when reloading: {0}", new object[]
				{
					ex4
				});
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex4);
			}
			if (ex != null)
			{
				return ex;
			}
			try
			{
				this.SaveSyncProgress(mailboxSession, roundtripComplete);
			}
			catch (StoragePermanentException innerException5)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException5, true);
			}
			catch (StorageTransientException innerException6)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException6);
			}
			catch (SyncStoreUnhealthyException innerException7)
			{
				ex = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException7);
			}
			return ex;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0003A870 File Offset: 0x00038A70
		public void ReloadForRetry(EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			if (this.storageCustomSyncState != null)
			{
				return;
			}
			this.LoadCustomSyncState(roundtripComplete);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003A882 File Offset: 0x00038A82
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.syncStateStorage != null)
				{
					this.syncStateStorage.Dispose();
					this.syncStateStorage = null;
				}
				if (this.storageCustomSyncState != null)
				{
					this.storageCustomSyncState.Dispose();
					this.storageCustomSyncState = null;
				}
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003A8BB File Offset: 0x00038ABB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StateStorage>(this);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		private static void EnsureSyncStateTypesRegistered()
		{
			if (!StateStorage.initialized)
			{
				lock (StateStorage.globalLock)
				{
					if (!StateStorage.initialized)
					{
						SyncStateTypeFactory.GetInstance().RegisterBuilder(new UnifiedCustomSyncState());
						SyncStateTypeFactory.GetInstance().RegisterBuilder(new UnifiedCustomSyncStateItem());
						StateStorage.initialized = true;
					}
				}
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0003A930 File Offset: 0x00038B30
		private void LoadSyncProgress(MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				FolderSchema.AggregationSyncProgress
			};
			using (Folder folder = SyncStoreLoadManager.FolderBind(mailboxSession, this.syncStateStorage.FolderId, properties, roundtripComplete))
			{
				int? num = folder.TryGetProperty(FolderSchema.AggregationSyncProgress) as int?;
				if (num != null)
				{
					this.lastSavedSyncProgress = (SyncProgress)num.Value;
				}
				else
				{
					this.lastSavedSyncProgress = SyncProgress.None;
				}
			}
			this.syncProgress = this.lastSavedSyncProgress;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003A9C0 File Offset: 0x00038BC0
		private void SaveSyncProgress(MailboxSession mailboxSession, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			if (this.lastSavedSyncProgress == this.syncProgress)
			{
				this.syncLogSession.LogDebugging((TSLID)299UL, "Skipping save of SyncProgress as it is same as earlier: {0}", new object[]
				{
					this.syncProgress
				});
				return;
			}
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				FolderSchema.AggregationSyncProgress
			};
			using (Folder folder = SyncStoreLoadManager.FolderBind(mailboxSession, this.syncStateStorage.FolderId, properties, roundtripComplete))
			{
				folder[FolderSchema.AggregationSyncProgress] = (int)this.syncProgress;
				SyncStoreLoadManager.FolderSave(folder, mailboxSession, roundtripComplete);
				this.lastSavedSyncProgress = this.syncProgress;
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003AA78 File Offset: 0x00038C78
		private void LoadCustomSyncState(EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			this.storageCustomSyncState = SyncStoreLoadManager.SyncStateStorageGetCustomSyncState(this.mailboxSession.MdbGuid, this.syncStateStorage, StateStorage.UnifiedStateTypeInfo, roundtripComplete);
			if (this.storageCustomSyncState == null)
			{
				this.syncLogSession.LogVerbose((TSLID)978UL, StateStorage.Tracer, "There is no custom sync state. We'll create one.", new object[0]);
				this.storageCustomSyncState = SyncStoreLoadManager.SyncStateStorageCreateCustomSyncState(this.mailboxSession.MdbGuid, this.syncStateStorage, StateStorage.UnifiedStateTypeInfo, roundtripComplete);
			}
			else
			{
				this.LoadSyncProgress(this.mailboxSession, roundtripComplete);
			}
			this.customSyncState = (this.storageCustomSyncState[StateStorage.UnifiedStateTypeInfo.UniqueName] as UnifiedCustomSyncState);
			if (this.customSyncState == null)
			{
				this.customSyncState = new UnifiedCustomSyncState();
				this.storageCustomSyncState[StateStorage.UnifiedStateTypeInfo.UniqueName] = this.customSyncState;
			}
		}

		// Token: 0x040009AD RID: 2477
		internal static readonly Trace Tracer = ExTraceGlobals.StateStorageTracer;

		// Token: 0x040009AE RID: 2478
		private static readonly SyncStateSizeLimitChecker mappingTableSizeChecker = new SyncStateSizeLimitChecker(Convert.ToInt64(AggregationConfiguration.Instance.MaxMappingTableSizeInMemory.ToBytes()));

		// Token: 0x040009AF RID: 2479
		private static readonly SyncStateInfo UnifiedStateTypeInfo = new UnifiedCustomSyncStateInfo();

		// Token: 0x040009B0 RID: 2480
		private static object globalLock = new object();

		// Token: 0x040009B1 RID: 2481
		private static bool initialized;

		// Token: 0x040009B2 RID: 2482
		private readonly Guid subscriptionGuid;

		// Token: 0x040009B3 RID: 2483
		private SyncStateStorage syncStateStorage;

		// Token: 0x040009B4 RID: 2484
		private CustomSyncState storageCustomSyncState;

		// Token: 0x040009B5 RID: 2485
		private UnifiedCustomSyncState customSyncState;

		// Token: 0x040009B6 RID: 2486
		private SyncProgress syncProgress;

		// Token: 0x040009B7 RID: 2487
		private SyncProgress lastSavedSyncProgress;

		// Token: 0x040009B8 RID: 2488
		private bool forceRecoverySyncNext;

		// Token: 0x040009B9 RID: 2489
		private MailboxSession mailboxSession;

		// Token: 0x040009BA RID: 2490
		private SyncLogSession syncLogSession;

		// Token: 0x040009BB RID: 2491
		private bool requiresReload;
	}
}
