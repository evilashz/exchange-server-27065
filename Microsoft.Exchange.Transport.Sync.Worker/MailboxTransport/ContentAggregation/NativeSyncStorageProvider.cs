using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000207 RID: 519
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class NativeSyncStorageProvider : ISyncStorageProviderItemRetriever
	{
		// Token: 0x0600117E RID: 4478
		public abstract NativeSyncStorageProviderState Bind(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, INativeStateStorage stateStorage, MailSubmitter mailSubmitter, SyncLogSession syncLogSession, bool underRecovery);

		// Token: 0x0600117F RID: 4479
		public abstract void Unbind(NativeSyncStorageProviderState state);

		// Token: 0x06001180 RID: 4480 RVA: 0x000387C4 File Offset: 0x000369C4
		public IAsyncResult BeginEnumerateChanges(NativeSyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.BeginEnumerateChanges is no-longer supported");
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000387D0 File Offset: 0x000369D0
		public AsyncOperationResult<SyncProviderResultData> EndEnumerateChanges(IAsyncResult asyncResult)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.EndEnumerateChanges is no-longer supported");
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000387DC File Offset: 0x000369DC
		public IAsyncResult BeginAcknowledgeChanges(NativeSyncStorageProviderState state, IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.BeginAcknowledgeChanges is no-longer supported");
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000387E8 File Offset: 0x000369E8
		public AsyncOperationResult<SyncProviderResultData> EndAcknowledgeChanges(IAsyncResult asyncResult)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.EndAcknowledgeChanges is no-longer supported");
		}

		// Token: 0x06001184 RID: 4484
		public abstract IAsyncResult BeginApplyChanges(NativeSyncStorageProviderState state, IList<SyncChangeEntry> changeList, ISyncStorageProviderItemRetriever itemRetriever, object itemRetrieverState, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x06001185 RID: 4485
		public abstract AsyncOperationResult<SyncProviderResultData> EndApplyChanges(IAsyncResult asyncResult);

		// Token: 0x06001186 RID: 4486
		public abstract void Cancel(IAsyncResult asyncResult);

		// Token: 0x06001187 RID: 4487 RVA: 0x000387F4 File Offset: 0x000369F4
		public IAsyncResult BeginGetItem(object itemRetrieverState, SyncChangeEntry item, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.BeginGetItem is no-longer supported");
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00038800 File Offset: 0x00036A00
		public AsyncOperationResult<SyncChangeEntry> EndGetItem(IAsyncResult asyncResult)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.EndGetItem is no-longer supported");
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003880C File Offset: 0x00036A0C
		public void CancelGetItem(IAsyncResult asyncResult)
		{
			throw new NotSupportedException("NativeSyncStorageProvider.CancelGetItem is no-longer supported");
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00038818 File Offset: 0x00036A18
		protected static void SetRecoverySync(NativeSyncStorageProviderState providerState)
		{
			providerState.SyncLogSession.LogError((TSLID)932UL, NativeSyncStorageProvider.Tracer, "Setting recovery sync for subsequent changes and next sync.", new object[0]);
			providerState.UnderRecovery = true;
			providerState.StateStorage.SetForceRecoverySyncNext(true);
		}

		// Token: 0x0600118B RID: 4491
		protected abstract void UpdateMapping(NativeSyncStorageProviderState providerState, SyncChangeEntry change);

		// Token: 0x0600118C RID: 4492 RVA: 0x00038853 File Offset: 0x00036A53
		protected void PopulateFolderProperties(Folder folder, SyncFolder syncFolder)
		{
			folder.DisplayName = syncFolder.DisplayName;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00038861 File Offset: 0x00036A61
		protected virtual void RecordNativeFolderOperation(NativeSyncStorageProviderState providerState, SyncChangeEntry change, StoreObjectId nativeId, ChangeType changeType, Folder folder)
		{
			if (folder != null && folder.DisplayName != null)
			{
				this.RecordNativeFolderName(change, folder.DisplayName);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00038880 File Offset: 0x00036A80
		protected void RecordNativeFolderName(SyncChangeEntry change, string folderDisplayName)
		{
			if (change.ChangeType == ChangeType.Add || change.ChangeType == ChangeType.Change)
			{
				if (change.Properties.ContainsKey(NativeSyncStorageProvider.FolderNameProperty))
				{
					change.Properties[NativeSyncStorageProvider.FolderNameProperty] = folderDisplayName;
					return;
				}
				change.Properties.Add(NativeSyncStorageProvider.FolderNameProperty, folderDisplayName);
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000388D4 File Offset: 0x00036AD4
		protected virtual void VerifyNativeFolderExists(NativeSyncStorageProviderState providerState, SyncChangeEntry change)
		{
			StoreObjectId nativeFolderId = change.NativeFolderId;
			using (SyncStoreLoadManager.FolderBind(providerState.SyncMailboxSession.MailboxSession, nativeFolderId, null, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete)))
			{
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00038924 File Offset: 0x00036B24
		protected bool TryEnsureFullParentFolderHierarchy(NativeSyncStorageProviderState providerState, SyncChangeEntry itemChange)
		{
			LinkedList<SyncChangeEntry> linkedList = new LinkedList<SyncChangeEntry>();
			SyncFolder syncFolder = itemChange.SyncObject as SyncFolder;
			if (syncFolder != null && syncFolder.DefaultFolderType == DefaultFolderType.Root)
			{
				return true;
			}
			try
			{
				if (itemChange.CloudFolderId == null)
				{
					itemChange.NativeFolderId = providerState.GetDefaultFolderId(itemChange);
					return true;
				}
				if (itemChange.NativeFolderId == null)
				{
					Exception ex;
					if (!this.ContinueWithParentFolder(providerState, itemChange, out ex))
					{
						itemChange.Exception = SyncTransientException.CreateItemLevelException(new SyncFailedDependencyException("Failed to create parent folder hierarchy.", ex.InnerException));
						return false;
					}
					return true;
				}
				else
				{
					SyncChangeEntry syncChangeEntry = itemChange;
					while (syncChangeEntry.NativeFolderId != null)
					{
						try
						{
							this.VerifyNativeFolderExists(providerState, syncChangeEntry);
							break;
						}
						catch (ObjectNotFoundException)
						{
							providerState.SyncLogSession.LogVerbose((TSLID)1195UL, NativeSyncStorageProvider.Tracer, "Folder {0} doesn't exist and will attempt to search/re-create it.", new object[]
							{
								syncChangeEntry.NativeFolderId
							});
							if (syncChangeEntry.CloudFolderId == null)
							{
								itemChange.NativeFolderId = providerState.GetDefaultFolderId(syncChangeEntry);
								break;
							}
							string cloudFolderId;
							StoreObjectId nativeId;
							byte[] array;
							StoreObjectId nativeFolderId;
							string cloudVersion;
							Dictionary<string, string> properties;
							if (!providerState.StateStorage.TryFindFolder(syncChangeEntry.CloudFolderId, out cloudFolderId, out nativeId, out array, out nativeFolderId, out cloudVersion, out properties))
							{
								providerState.SyncLogSession.LogError((TSLID)1196UL, NativeSyncStorageProvider.Tracer, "We must not get an item for a folder that we do not know about. {0}", new object[]
								{
									itemChange
								});
								itemChange.Exception = SyncPermanentException.CreateItemLevelException(new SyncPoisonItemFoundException(itemChange.ToString(), providerState.Subscription.SubscriptionGuid));
								return false;
							}
							SyncChangeEntry syncChangeEntry2 = new SyncChangeEntry(ChangeType.Add, SchemaType.Folder, syncChangeEntry.CloudFolderId);
							syncChangeEntry2.CloudFolderId = cloudFolderId;
							syncChangeEntry2.CloudId = syncChangeEntry.CloudFolderId;
							syncChangeEntry2.NativeFolderId = nativeFolderId;
							syncChangeEntry2.NativeId = nativeId;
							syncChangeEntry2.Properties = properties;
							syncChangeEntry2.CloudVersion = cloudVersion;
							syncChangeEntry2.Persist = itemChange.Persist;
							syncChangeEntry2.Recovered = itemChange.Recovered;
							linkedList.AddFirst(syncChangeEntry2);
							syncChangeEntry = syncChangeEntry2;
						}
					}
					for (LinkedListNode<SyncChangeEntry> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
					{
						Exception ex2;
						this.ContinueWithFolder(providerState, linkedListNode.Value, out ex2);
						if (ex2 != null)
						{
							itemChange.Exception = SyncTransientException.CreateItemLevelException(new SyncFailedDependencyException("Failed to create parent folder hierarchy.", ex2.InnerException));
							providerState.SyncLogSession.LogError((TSLID)1062UL, NativeSyncStorageProvider.Tracer, "Failed to recreate folder: {0} due to {1}", new object[]
							{
								linkedListNode.Value,
								ex2
							});
							return false;
						}
						if (linkedListNode.Value.ChangeType == ChangeType.Add)
						{
							try
							{
								this.RecreateFolder(providerState, itemChange, linkedListNode.Value);
							}
							catch (IOException innerException)
							{
								itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException);
							}
							catch (ExchangeDataException innerException2)
							{
								itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException2);
							}
							catch (ADTransientException innerException3)
							{
								itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException3);
							}
							catch (StorageTransientException innerException4)
							{
								itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException4);
							}
							catch (StoragePermanentException ex3)
							{
								if (ExceptionUtilities.ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(providerState.SyncLogSession, ex3))
								{
									itemChange.Exception = SyncTransientException.CreateItemLevelException(ex3);
								}
								else
								{
									itemChange.Exception = SyncPermanentException.CreateItemLevelException(ex3);
								}
							}
							catch (SyncStoreUnhealthyException innerException5)
							{
								itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException5);
							}
						}
						if (itemChange.Exception != null)
						{
							providerState.AddFailedFolderCreation(linkedListNode.Value.CloudId, itemChange.Exception);
							providerState.SyncLogSession.LogError((TSLID)1198UL, NativeSyncStorageProvider.Tracer, "Item {0} being skipped due to exception {1}", new object[]
							{
								itemChange,
								itemChange.Exception
							});
							return false;
						}
						if (linkedListNode.Next != null)
						{
							linkedListNode.Next.Value.NativeFolderId = linkedListNode.Value.NewNativeId;
						}
						else
						{
							itemChange.NativeFolderId = linkedListNode.Value.NewNativeId;
						}
					}
				}
			}
			catch (StorageTransientException innerException6)
			{
				itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException6);
			}
			catch (StoragePermanentException ex4)
			{
				if (ExceptionUtilities.ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(providerState.SyncLogSession, ex4))
				{
					itemChange.Exception = SyncTransientException.CreateItemLevelException(ex4);
				}
				else
				{
					itemChange.Exception = SyncPermanentException.CreateItemLevelException(ex4);
				}
			}
			catch (SyncStoreUnhealthyException innerException7)
			{
				itemChange.Exception = SyncTransientException.CreateItemLevelException(innerException7);
			}
			if (itemChange.Exception != null)
			{
				providerState.SyncLogSession.LogError((TSLID)1197UL, NativeSyncStorageProvider.Tracer, "Item {0} being skipped due to exception {1}", new object[]
				{
					itemChange,
					itemChange.Exception
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00038E68 File Offset: 0x00037068
		protected bool ContinueWithFolder(NativeSyncStorageProviderState providerState, SyncChangeEntry folderEntry, out Exception exception)
		{
			if (!providerState.TryGetFailedFolderCreation(folderEntry.CloudId, out exception))
			{
				return true;
			}
			if (exception is SyncTransientException)
			{
				return false;
			}
			providerState.SyncLogSession.LogVerbose((TSLID)1063UL, NativeSyncStorageProvider.Tracer, "Ignoring permanent exception {0} for folder entry {1} and mapping to default folder.", new object[]
			{
				exception,
				folderEntry
			});
			exception = null;
			bool result = folderEntry.NativeId != null;
			this.MapToDefaultFolder(providerState, folderEntry);
			return result;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00038EE0 File Offset: 0x000370E0
		private bool ContinueWithParentFolder(NativeSyncStorageProviderState providerState, SyncChangeEntry change, out Exception exception)
		{
			exception = null;
			SyncChangeEntry syncChangeEntry = new SyncChangeEntry(ChangeType.Change, SchemaType.Folder, change.CloudFolderId);
			syncChangeEntry.NativeId = change.NativeFolderId;
			this.ContinueWithFolder(providerState, syncChangeEntry, out exception);
			if (exception != null)
			{
				return false;
			}
			change.NativeFolderId = (syncChangeEntry.NewNativeId ?? syncChangeEntry.NativeId);
			if (change.NativeFolderId == null)
			{
				this.MapToDefaultFolder(providerState, syncChangeEntry);
				change.NativeFolderId = syncChangeEntry.NativeId;
			}
			return true;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00038F50 File Offset: 0x00037150
		protected virtual void ApplyAddFolderChange(NativeSyncStorageProviderState providerState, SyncChangeEntry change)
		{
			SyncFolder syncFolder = (SyncFolder)change.SyncObject;
			if (syncFolder.DefaultFolderType != DefaultFolderType.None)
			{
				StoreObjectId storeObjectId = providerState.EnsureDefaultFolder(syncFolder.DefaultFolderType);
				if (!providerState.StateStorage.ContainsFolder(storeObjectId))
				{
					change.NativeId = storeObjectId;
					using (Folder folder = SyncStoreLoadManager.FolderBind(providerState.SyncMailboxSession.MailboxSession, storeObjectId, NativeSyncStorageProvider.FolderPropertiesStartingWithDisplayName, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete)))
					{
						this.RecordNativeFolderName(change, folder.DisplayName);
					}
					if (DefaultFolderType.Root != syncFolder.DefaultFolderType)
					{
						change.NativeFolderId = providerState.SyncMailboxSession.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
					}
					providerState.SyncLogSession.LogDebugging((TSLID)933UL, NativeSyncStorageProvider.Tracer, "Mapped Folder: {0}", new object[]
					{
						change
					});
					return;
				}
				providerState.SyncLogSession.LogDebugging((TSLID)934UL, NativeSyncStorageProvider.Tracer, "Default folder is already mapped, so we will create a regular folder for this change: {0}", new object[]
				{
					change
				});
			}
			if (change.NativeFolderId == null)
			{
				providerState.SyncLogSession.LogError((TSLID)935UL, NativeSyncStorageProvider.Tracer, "Could not create a folder with no parent folder specified. This generally means a provider bug or malicious content.", new object[0]);
				change.Exception = SyncPermanentException.CreateItemLevelException(new SyncPoisonItemFoundException(change.ToString(), providerState.Subscription.SubscriptionGuid));
				return;
			}
			using (Folder folder2 = this.EnsureNonDefaultFolder(providerState, change, syncFolder))
			{
				if (folder2 == null)
				{
					providerState.SyncLogSession.LogError((TSLID)936UL, NativeSyncStorageProvider.Tracer, "Unresolveable folder name: {0} for change {1}.", new object[]
					{
						syncFolder.DisplayName,
						change
					});
					change.Exception = SyncPermanentException.CreateItemLevelException(new UnresolveableFolderNameException(syncFolder.DisplayName));
				}
				else
				{
					providerState.SyncLogSession.LogDebugging((TSLID)937UL, NativeSyncStorageProvider.Tracer, "Added or mapped folder: {0}", new object[]
					{
						change
					});
					bool flag = false;
					try
					{
						change.NativeId = folder2.Id.ObjectId;
						change.ChangeKey = folder2.Id.ChangeKeyAsByteArray();
						this.RecordNativeFolderOperation(providerState, change, change.NativeId, change.ChangeType, folder2);
						PermissionSet permissionSet = folder2.GetPermissionSet();
						Permission permission = null;
						foreach (Permission permission2 in permissionSet)
						{
							if (permission2.Principal != null && permission2.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal)
							{
								permission = permission2;
								break;
							}
						}
						if (permission != null)
						{
							permissionSet.RemoveEntry(permission.Principal);
							SyncStoreLoadManager.FolderSave(folder2, providerState.SyncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
							SyncStoreLoadManager.FolderLoad(folder2, providerState.SyncMailboxSession.MailboxSession, NativeSyncStorageProvider.FolderProperties, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
							change.NativeId = folder2.Id.ObjectId;
							change.ChangeKey = folder2.Id.ChangeKeyAsByteArray();
							this.RecordNativeFolderOperation(providerState, change, change.NativeId, ChangeType.Change, folder2);
						}
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							NativeSyncStorageProvider.SetRecoverySync(providerState);
						}
					}
				}
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000392D0 File Offset: 0x000374D0
		protected bool IsNativeIdMappedToADifferentFolder(NativeSyncStorageProviderState providerState, StoreObjectId nativeId, string expectedCloudId, out string otherCloudId)
		{
			string text;
			byte[] array;
			StoreObjectId storeObjectId;
			string text2;
			Dictionary<string, string> dictionary;
			bool flag = providerState.StateStorage.TryFindFolder(nativeId, out otherCloudId, out text, out array, out storeObjectId, out text2, out dictionary);
			return flag && expectedCloudId != otherCloudId;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003930C File Offset: 0x0003750C
		private void RecreateFolder(NativeSyncStorageProviderState providerState, SyncChangeEntry itemChange, SyncChangeEntry folderReAdd)
		{
			string displayName = null;
			if (folderReAdd.Properties == null || !folderReAdd.Properties.TryGetValue(NativeSyncStorageProvider.FolderNameProperty, out displayName))
			{
				providerState.SyncLogSession.LogError((TSLID)1199UL, NativeSyncStorageProvider.Tracer, "The Subscription would have been probably created prior to R4 or targeted towards default folder to not track the folder name. We will drop the item to INBOX.", new object[]
				{
					itemChange
				});
				this.MapToDefaultFolder(providerState, folderReAdd);
				return;
			}
			StoreObjectId nativeId = folderReAdd.NativeId;
			using (SyncFolder syncFolder = new SyncFolder(displayName))
			{
				folderReAdd.SyncObject = syncFolder;
				this.ApplyAddFolderChange(providerState, folderReAdd);
				if (folderReAdd.HasException)
				{
					providerState.SyncLogSession.LogError((TSLID)1200UL, NativeSyncStorageProvider.Tracer, "Add Folder Change: {0} failed with error: {1}, so failing the item change: {2} with the same error.", new object[]
					{
						folderReAdd,
						folderReAdd.Exception,
						itemChange
					});
					itemChange.Exception = folderReAdd.Exception;
					return;
				}
			}
			folderReAdd.ChangeType = ChangeType.Change;
			folderReAdd.NewNativeId = folderReAdd.NativeId;
			folderReAdd.NativeId = nativeId;
			providerState.SyncLogSession.LogDebugging((TSLID)1072UL, NativeSyncStorageProvider.Tracer, "Updating the mapping table entry as it is recreated. {0}", new object[]
			{
				folderReAdd
			});
			this.UpdateMapping(providerState, folderReAdd);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003944C File Offset: 0x0003764C
		private Folder EnsureNonDefaultFolder(NativeSyncStorageProviderState providerState, SyncChangeEntry change, SyncFolder syncFolder)
		{
			bool flag = false;
			Folder folder = null;
			string text = syncFolder.DisplayName;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3638963517U);
					folder = SyncStoreLoadManager.FolderCreate(providerState.SyncMailboxSession.MailboxSession, change.NativeFolderId, StoreObjectType.Folder, text, CreateMode.OpenIfExists, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
					SyncStoreLoadManager.FolderSave(folder, providerState.SyncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
					SyncStoreLoadManager.FolderLoad(folder, providerState.SyncMailboxSession.MailboxSession, NativeSyncStorageProvider.FolderProperties, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
					string text2;
					bool flag2 = this.IsNativeIdMappedToADifferentFolder(providerState, folder.Id.ObjectId, change.CloudId, out text2);
					if (!flag2)
					{
						this.PopulateFolderProperties(folder, syncFolder);
						folder.DisplayName = text;
						SyncStoreLoadManager.FolderSave(folder, providerState.SyncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
						SyncStoreLoadManager.FolderLoad(folder, providerState.SyncMailboxSession.MailboxSession, NativeSyncStorageProvider.FolderProperties, new EventHandler<RoundtripCompleteEventArgs>(providerState.OnRoundtripComplete));
						providerState.SyncLogSession.LogDebugging((TSLID)939UL, NativeSyncStorageProvider.Tracer, "Successfully create folder {0}.", new object[]
						{
							text
						});
						flag = true;
						return folder;
					}
					providerState.SyncLogSession.LogError((TSLID)938UL, NativeSyncStorageProvider.Tracer, "Folder {0} is already mapped to cloud id {1} and cannot be used for cloud id {2}. Attempting to create another folder.", new object[]
					{
						text,
						text2,
						change.CloudId
					});
					text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						syncFolder.DisplayName,
						i
					});
				}
				finally
				{
					if (!flag && folder != null)
					{
						folder.Dispose();
						folder = null;
					}
				}
			}
			providerState.SyncLogSession.LogError((TSLID)940UL, NativeSyncStorageProvider.Tracer, "Could not create folder {0} and any variants as there were too many collisions.", new object[]
			{
				syncFolder.DisplayName
			});
			return null;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003966C File Offset: 0x0003786C
		private void MapToDefaultFolder(NativeSyncStorageProviderState providerState, SyncChangeEntry folderEntry)
		{
			StoreObjectId defaultFolderId = providerState.GetDefaultFolderId(folderEntry);
			if (folderEntry.NativeId == null)
			{
				folderEntry.ChangeType = ChangeType.Add;
				folderEntry.NativeId = defaultFolderId;
				this.UpdateMapping(providerState, folderEntry);
			}
			else
			{
				folderEntry.ChangeType = ChangeType.Change;
				folderEntry.NewNativeId = defaultFolderId;
				this.UpdateMapping(providerState, folderEntry);
			}
			providerState.TryRemoveFailedFolderCreation(folderEntry.CloudId);
		}

		// Token: 0x04000995 RID: 2453
		internal static readonly string FolderNameProperty = "Name";

		// Token: 0x04000996 RID: 2454
		protected static readonly PropertyDefinition[] FolderPropertiesStartingWithDisplayName = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId
		};

		// Token: 0x04000997 RID: 2455
		protected static readonly PropertyDefinition[] FolderProperties = NativeSyncStorageProvider.FolderPropertiesStartingWithDisplayName;

		// Token: 0x04000998 RID: 2456
		private static readonly Trace Tracer = ExTraceGlobals.NativeSyncStorageProviderTracer;
	}
}
