using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000220 RID: 544
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UnifiedCustomSyncState : ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x00041B98 File Offset: 0x0003FD98
		internal UnifiedCustomSyncState()
		{
			this.cloudProperties = new Dictionary<string, string>(100);
			this.cloudItemTransientErrors = new Dictionary<string, short>(100);
			this.failedCloudItems = new Dictionary<string, string>(1);
			this.failedCloudFolders = new Dictionary<string, string>(1);
			this.cloudItemSyncConflictTransientErrors = new Dictionary<string, short>(1);
			this.nativeItemTransientErrors = new Dictionary<StoreObjectId, short>(100);
			this.nativeItemSyncConflictTransientErrors = new Dictionary<StoreObjectId, short>(1);
			this.cloudFolderTransientErrors = new Dictionary<string, short>(100);
			this.cloudFolderSyncConflictTransientErrors = new Dictionary<string, short>(1);
			this.nativeFolderTransientErrors = new Dictionary<StoreObjectId, short>(100);
			this.nativeFolderSyncConflictTransientErrors = new Dictionary<StoreObjectId, short>(1);
			this.nativeFolderMappingTable = new Dictionary<StoreObjectId, UnifiedCustomSyncStateItem>(100);
			this.cloudFolderMappingTable = new Dictionary<string, UnifiedCustomSyncStateItem>(100);
			this.nativeItemMappingTable = new Dictionary<StoreObjectId, UnifiedCustomSyncStateItem>(100);
			this.cloudItemMappingTable = new Dictionary<string, UnifiedCustomSyncStateItem>(100);
			this.version = 6;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00041C6F File Offset: 0x0003FE6F
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x00041C76 File Offset: 0x0003FE76
		public ushort TypeId
		{
			get
			{
				return UnifiedCustomSyncState.typeId;
			}
			set
			{
				UnifiedCustomSyncState.typeId = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00041C7E File Offset: 0x0003FE7E
		internal bool IsDirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x00041C86 File Offset: 0x0003FE86
		internal bool InitialSyncDone
		{
			get
			{
				return (this.syncEngineFlags & 1) != 0;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00041C96 File Offset: 0x0003FE96
		private static bool ShouldContinuePromotingTransientException(SyncTransientException exception)
		{
			return !SyncUtilities.VerifyNestedInnerExceptionType(exception, typeof(NonPromotableTransientException));
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00041CAB File Offset: 0x0003FEAB
		public ICustomSerializable BuildObject()
		{
			return new UnifiedCustomSyncState();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00041CB4 File Offset: 0x0003FEB4
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(6);
			new GenericDictionaryData<StringData, string, StringData, string>(this.cloudProperties).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>(this.nativeItemTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>(this.nativeFolderTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, Int16Data, short>(this.cloudItemTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, Int16Data, short>(this.cloudFolderTransientErrors).SerializeData(writer, componentDataPool);
			bool flag = UnifiedCustomSyncState.RemoveOrphanedEntries(this.cloudFolderMappingTable, this.cloudFolderMappingTable);
			UnifiedCustomSyncState.SerializeCollection(this.cloudFolderMappingTable, writer, componentDataPool);
			if (flag)
			{
				UnifiedCustomSyncState.RemoveOrphanedEntries(this.cloudFolderMappingTable, this.cloudItemMappingTable);
			}
			UnifiedCustomSyncState.SerializeCollection(this.cloudItemMappingTable, writer, componentDataPool);
			new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>(this.nativeItemSyncConflictTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>(this.nativeFolderSyncConflictTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, Int16Data, short>(this.cloudItemSyncConflictTransientErrors).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, Int16Data, short>(this.cloudFolderSyncConflictTransientErrors).SerializeData(writer, componentDataPool);
			writer.Write(this.syncEngineFlags);
			new GenericDictionaryData<StringData, string, StringData, string>(this.failedCloudItems).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, StringData, string>(this.failedCloudFolders).SerializeData(writer, componentDataPool);
			this.dirty = false;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00041DE4 File Offset: 0x0003FFE4
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			this.version = reader.ReadInt16();
			if (this.version > 6)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid Unified Custom Sync State version found in stream: {0}.", new object[]
				{
					this.version
				}));
			}
			GenericDictionaryData<StringData, string, StringData, string> genericDictionaryData = new GenericDictionaryData<StringData, string, StringData, string>();
			genericDictionaryData.DeserializeData(reader, componentDataPool);
			this.cloudProperties = genericDictionaryData.Data;
			GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short> genericDictionaryData2 = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>();
			genericDictionaryData2.DeserializeData(reader, componentDataPool);
			this.existingNativeItemTransientErrors = genericDictionaryData2.Data;
			GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short> genericDictionaryData3 = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>();
			genericDictionaryData3.DeserializeData(reader, componentDataPool);
			this.existingNativeFolderTransientErrors = genericDictionaryData3.Data;
			GenericDictionaryData<StringData, string, Int16Data, short> genericDictionaryData4 = new GenericDictionaryData<StringData, string, Int16Data, short>();
			genericDictionaryData4.DeserializeData(reader, componentDataPool);
			this.existingCloudItemTransientErrors = genericDictionaryData4.Data;
			GenericDictionaryData<StringData, string, Int16Data, short> genericDictionaryData5 = new GenericDictionaryData<StringData, string, Int16Data, short>();
			genericDictionaryData5.DeserializeData(reader, componentDataPool);
			this.existingCloudFolderTransientErrors = genericDictionaryData5.Data;
			UnifiedCustomSyncState.DeserializeCollections(this.version, reader, componentDataPool, true, out this.nativeFolderMappingTable, out this.cloudFolderMappingTable);
			UnifiedCustomSyncState.DeserializeCollections(this.version, reader, componentDataPool, false, out this.nativeItemMappingTable, out this.cloudItemMappingTable);
			if (this.version == 0)
			{
				reader.ReadBoolean();
				reader.ReadBoolean();
			}
			if (this.version >= 2)
			{
				GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short> genericDictionaryData6 = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>();
				genericDictionaryData6.DeserializeData(reader, componentDataPool);
				this.existingNativeItemSyncConflictTransientErrors = genericDictionaryData6.Data;
				GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short> genericDictionaryData7 = new GenericDictionaryData<StoreObjectIdData, StoreObjectId, Int16Data, short>();
				genericDictionaryData7.DeserializeData(reader, componentDataPool);
				this.existingNativeFolderSyncConflictTransientErrors = genericDictionaryData7.Data;
				GenericDictionaryData<StringData, string, Int16Data, short> genericDictionaryData8 = new GenericDictionaryData<StringData, string, Int16Data, short>();
				genericDictionaryData8.DeserializeData(reader, componentDataPool);
				this.existingCloudItemSyncConflictTransientErrors = genericDictionaryData8.Data;
				GenericDictionaryData<StringData, string, Int16Data, short> genericDictionaryData9 = new GenericDictionaryData<StringData, string, Int16Data, short>();
				genericDictionaryData9.DeserializeData(reader, componentDataPool);
				this.existingCloudFolderSyncConflictTransientErrors = genericDictionaryData9.Data;
			}
			if (this.version >= 4)
			{
				this.syncEngineFlags = reader.ReadByte();
			}
			if (this.version >= 5)
			{
				GenericDictionaryData<StringData, string, StringData, string> genericDictionaryData10 = new GenericDictionaryData<StringData, string, StringData, string>();
				genericDictionaryData10.DeserializeData(reader, componentDataPool);
				this.failedCloudItems = genericDictionaryData10.Data;
			}
			if (this.version >= 6)
			{
				GenericDictionaryData<StringData, string, StringData, string> genericDictionaryData11 = new GenericDictionaryData<StringData, string, StringData, string>();
				genericDictionaryData11.DeserializeData(reader, componentDataPool);
				this.failedCloudFolders = genericDictionaryData11.Data;
			}
			this.dirty = false;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00041FE8 File Offset: 0x000401E8
		internal void MarkInitialSyncDone()
		{
			this.syncEngineFlags |= 1;
			this.dirty = true;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00042000 File Offset: 0x00040200
		internal bool ContainsItem(string cloudId)
		{
			return this.cloudItemMappingTable.ContainsKey(cloudId);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004200E File Offset: 0x0004020E
		internal bool ContainsFailedItem(string cloudId)
		{
			return this.failedCloudItems.ContainsKey(cloudId);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004201C File Offset: 0x0004021C
		internal bool ContainsFolder(StoreObjectId nativeId)
		{
			return this.nativeFolderMappingTable.ContainsKey(nativeId);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004202A File Offset: 0x0004022A
		internal bool ContainsFailedFolder(string cloudId)
		{
			return this.failedCloudFolders.ContainsKey(cloudId);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00042038 File Offset: 0x00040238
		internal bool ContainsFolder(string cloudId)
		{
			return this.cloudFolderMappingTable.ContainsKey(cloudId);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00042046 File Offset: 0x00040246
		internal bool ContainsItem(StoreObjectId nativeId)
		{
			return this.nativeItemMappingTable.ContainsKey(nativeId);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00042054 File Offset: 0x00040254
		internal bool TryAddItem(string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> itemProperties)
		{
			if (cloudId == null)
			{
				throw new ArgumentNullException("cloudId");
			}
			if (nativeId != null && nativeFolderId == null)
			{
				throw new ArgumentNullException("nativeFolderId");
			}
			UnifiedCustomSyncStateItem value = new UnifiedCustomSyncStateItem(nativeId, changeKey, nativeFolderId, cloudId, cloudFolderId, cloudVersion, itemProperties, this.version);
			if (this.cloudItemMappingTable.ContainsKey(cloudId))
			{
				return false;
			}
			if (nativeId != null)
			{
				if (this.nativeItemMappingTable.ContainsKey(nativeId))
				{
					return false;
				}
				this.nativeItemMappingTable.Add(nativeId, value);
			}
			this.cloudItemMappingTable.Add(cloudId, value);
			this.dirty = true;
			return true;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000420DD File Offset: 0x000402DD
		internal bool TryAddFailedItem(string cloudId, string cloudFolderId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			if (this.failedCloudItems.ContainsKey(cloudId))
			{
				return false;
			}
			this.failedCloudItems.Add(cloudId, cloudFolderId);
			this.dirty = true;
			return true;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00042110 File Offset: 0x00040310
		internal bool TryFindItem(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties)
		{
			return UnifiedCustomSyncState.TryFindCollection<string>(this.cloudItemMappingTable, cloudId, out cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out itemProperties);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00042138 File Offset: 0x00040338
		internal bool TryFindItem(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties)
		{
			return UnifiedCustomSyncState.TryFindCollection<StoreObjectId>(this.nativeItemMappingTable, nativeId, out cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out itemProperties);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004215D File Offset: 0x0004035D
		internal bool TryUpdateItem(StoreObjectId nativeId, byte[] changeKey, string cloudVersion, Dictionary<string, string> itemProperties)
		{
			return this.TryUpdateCollection<StoreObjectId>(this.nativeItemMappingTable, nativeId, changeKey, cloudVersion, itemProperties);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00042170 File Offset: 0x00040370
		internal bool TryRemoveItem(string cloudId)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (this.cloudItemMappingTable.TryGetValue(cloudId, out unifiedCustomSyncStateItem) && unifiedCustomSyncStateItem.NativeId != null)
			{
				this.nativeItemMappingTable.Remove(unifiedCustomSyncStateItem.NativeId);
			}
			if (this.cloudItemMappingTable.Remove(cloudId))
			{
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000421BF File Offset: 0x000403BF
		internal bool TryRemoveFailedItem(string cloudId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			if (this.failedCloudItems.Remove(cloudId))
			{
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000421E4 File Offset: 0x000403E4
		internal bool TryRemoveItem(StoreObjectId nativeId)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (this.nativeItemMappingTable.TryGetValue(nativeId, out unifiedCustomSyncStateItem) && unifiedCustomSyncStateItem.CloudId != null)
			{
				this.cloudItemMappingTable.Remove(unifiedCustomSyncStateItem.CloudId);
			}
			if (this.nativeItemMappingTable.Remove(nativeId))
			{
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00042234 File Offset: 0x00040434
		internal bool TryAddFolder(bool isInbox, string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties)
		{
			if (cloudId == null)
			{
				throw new ArgumentNullException("cloudId");
			}
			UnifiedCustomSyncStateItem value = new UnifiedCustomSyncStateItem(nativeId, changeKey, nativeFolderId, cloudId, cloudFolderId, cloudVersion, folderProperties, this.version);
			if (this.cloudFolderMappingTable.ContainsKey(cloudId))
			{
				return false;
			}
			if (nativeId != null && !isInbox)
			{
				if (this.nativeFolderMappingTable.ContainsKey(nativeId))
				{
					return false;
				}
				this.nativeFolderMappingTable.Add(nativeId, value);
			}
			this.cloudFolderMappingTable.Add(cloudId, value);
			this.dirty = true;
			return true;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000422B2 File Offset: 0x000404B2
		internal bool TryAddFailedFolder(string cloudId, string cloudFolderId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			if (this.failedCloudFolders.ContainsKey(cloudId))
			{
				return false;
			}
			this.failedCloudFolders.Add(cloudId, cloudFolderId);
			this.dirty = true;
			return true;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000422E4 File Offset: 0x000404E4
		internal bool TryFindFolder(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties)
		{
			return UnifiedCustomSyncState.TryFindCollection<string>(this.cloudFolderMappingTable, cloudId, out cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out folderProperties);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004230C File Offset: 0x0004050C
		internal bool TryFindFolder(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties)
		{
			return UnifiedCustomSyncState.TryFindCollection<StoreObjectId>(this.nativeFolderMappingTable, nativeId, out cloudId, out cloudFolderId, out nativeId, out changeKey, out nativeFolderId, out cloudVersion, out folderProperties);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00042334 File Offset: 0x00040534
		internal bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId, string cloudId, string newCloudId, string newCloudFolderId, byte[] changeKey, StoreObjectId newNativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (!this.nativeFolderMappingTable.TryGetValue(nativeId, out unifiedCustomSyncStateItem) && !this.cloudFolderMappingTable.TryGetValue(cloudId, out unifiedCustomSyncStateItem))
			{
				return false;
			}
			this.dirty = true;
			unifiedCustomSyncStateItem.ChangeKey = changeKey;
			unifiedCustomSyncStateItem.CloudVersion = cloudVersion;
			unifiedCustomSyncStateItem.Properties = folderProperties;
			if (newNativeFolderId != null)
			{
				unifiedCustomSyncStateItem.NativeFolderId = newNativeFolderId;
			}
			if (newCloudFolderId != null)
			{
				unifiedCustomSyncStateItem.CloudFolderId = newCloudFolderId;
			}
			if (newCloudId != null)
			{
				int hashCode = unifiedCustomSyncStateItem.CloudId.GetHashCode();
				foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem2 in this.cloudFolderMappingTable.Values)
				{
					if (unifiedCustomSyncStateItem2.CloudFolderId != null && unifiedCustomSyncStateItem2.CloudFolderId.GetHashCode() == hashCode)
					{
						unifiedCustomSyncStateItem2.CloudFolderId = newCloudId;
					}
				}
				foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem3 in this.cloudItemMappingTable.Values)
				{
					if (unifiedCustomSyncStateItem3.CloudFolderId != null && unifiedCustomSyncStateItem3.CloudFolderId.GetHashCode() == hashCode)
					{
						unifiedCustomSyncStateItem3.CloudFolderId = newCloudId;
					}
				}
				this.cloudFolderMappingTable.Remove(unifiedCustomSyncStateItem.CloudId);
				unifiedCustomSyncStateItem.CloudId = newCloudId;
				this.cloudFolderMappingTable.Add(newCloudId, unifiedCustomSyncStateItem);
			}
			if (newNativeId != null)
			{
				this.UpdateFolderNativeId(isInbox, nativeId, newNativeId, unifiedCustomSyncStateItem);
			}
			return true;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000424A8 File Offset: 0x000406A8
		internal bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId)
		{
			UnifiedCustomSyncStateItem item;
			if (!this.nativeFolderMappingTable.TryGetValue(nativeId, out item))
			{
				return false;
			}
			this.dirty = true;
			this.UpdateFolderNativeId(isInbox, nativeId, newNativeId, item);
			return true;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000424DC File Offset: 0x000406DC
		private static bool ShouldPromoteTransientException<T>(Dictionary<T, short> currentTransientErrorCollection, Dictionary<T, short> previousTransientErrorCollection, T id, int maxTransientErrors)
		{
			short num;
			if (currentTransientErrorCollection.TryGetValue(id, out num))
			{
				num += 1;
				currentTransientErrorCollection[id] = num;
			}
			else
			{
				if (previousTransientErrorCollection != null && previousTransientErrorCollection.TryGetValue(id, out num))
				{
					num += 1;
				}
				else
				{
					num = 1;
				}
				currentTransientErrorCollection.Add(id, num);
			}
			return (int)num >= maxTransientErrors;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004252C File Offset: 0x0004072C
		private static void SerializeCollection(Dictionary<string, UnifiedCustomSyncStateItem> cloudCollection, BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write(cloudCollection != null && cloudCollection.Count > 0);
			if (cloudCollection != null && cloudCollection.Count > 0)
			{
				writer.Write(cloudCollection.Count);
				foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem in cloudCollection.Values)
				{
					unifiedCustomSyncStateItem.SerializeData(writer, componentDataPool);
				}
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000425B0 File Offset: 0x000407B0
		private static bool RemoveOrphanedEntries(Dictionary<string, UnifiedCustomSyncStateItem> folderCollection, Dictionary<string, UnifiedCustomSyncStateItem> itemCollection)
		{
			bool result = false;
			LinkedList<UnifiedCustomSyncStateItem> linkedList = null;
			foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem in itemCollection.Values)
			{
				if (unifiedCustomSyncStateItem.CloudFolderId != null && !folderCollection.ContainsKey(unifiedCustomSyncStateItem.CloudFolderId))
				{
					if (linkedList == null)
					{
						linkedList = new LinkedList<UnifiedCustomSyncStateItem>();
					}
					linkedList.AddLast(unifiedCustomSyncStateItem);
				}
			}
			if (linkedList != null)
			{
				foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem2 in linkedList)
				{
					itemCollection.Remove(unifiedCustomSyncStateItem2.CloudId);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00042674 File Offset: 0x00040874
		private static void DeserializeCollections(short version, BinaryReader reader, ComponentDataPool componentDataPool, bool skipMultipleReferencesToNativeId, out Dictionary<StoreObjectId, UnifiedCustomSyncStateItem> nativeCollection, out Dictionary<string, UnifiedCustomSyncStateItem> cloudCollection)
		{
			if (reader.ReadBoolean())
			{
				int num = reader.ReadInt32();
				nativeCollection = new Dictionary<StoreObjectId, UnifiedCustomSyncStateItem>(num);
				cloudCollection = new Dictionary<string, UnifiedCustomSyncStateItem>(num);
				for (int i = 0; i < num; i++)
				{
					UnifiedCustomSyncStateItem unifiedCustomSyncStateItem = new UnifiedCustomSyncStateItem(version);
					unifiedCustomSyncStateItem.DeserializeData(reader, componentDataPool);
					if (unifiedCustomSyncStateItem.NativeId != null && (!nativeCollection.ContainsKey(unifiedCustomSyncStateItem.NativeId) || !skipMultipleReferencesToNativeId))
					{
						nativeCollection.Add(unifiedCustomSyncStateItem.NativeId, unifiedCustomSyncStateItem);
					}
					cloudCollection.Add(unifiedCustomSyncStateItem.CloudId, unifiedCustomSyncStateItem);
				}
				return;
			}
			nativeCollection = new Dictionary<StoreObjectId, UnifiedCustomSyncStateItem>(100);
			cloudCollection = new Dictionary<string, UnifiedCustomSyncStateItem>(100);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004270C File Offset: 0x0004090C
		private static bool TryFindCollection<T>(Dictionary<T, UnifiedCustomSyncStateItem> collection, T id, out string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> properties)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (!collection.TryGetValue(id, out unifiedCustomSyncStateItem))
			{
				cloudId = null;
				cloudFolderId = null;
				nativeId = null;
				changeKey = null;
				nativeFolderId = null;
				cloudVersion = null;
				properties = null;
				return false;
			}
			cloudId = unifiedCustomSyncStateItem.CloudId;
			cloudFolderId = unifiedCustomSyncStateItem.CloudFolderId;
			nativeId = unifiedCustomSyncStateItem.NativeId;
			changeKey = unifiedCustomSyncStateItem.ChangeKey;
			nativeFolderId = unifiedCustomSyncStateItem.NativeFolderId;
			cloudVersion = unifiedCustomSyncStateItem.CloudVersion;
			properties = unifiedCustomSyncStateItem.Properties;
			return true;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00042780 File Offset: 0x00040980
		private void UpdateFolderNativeId(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId, UnifiedCustomSyncStateItem item)
		{
			int hashCode = nativeId.GetHashCode();
			foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem in this.nativeFolderMappingTable.Values)
			{
				if (unifiedCustomSyncStateItem.NativeFolderId != null && unifiedCustomSyncStateItem.NativeFolderId.GetHashCode() == hashCode)
				{
					unifiedCustomSyncStateItem.NativeFolderId = newNativeId;
				}
			}
			foreach (UnifiedCustomSyncStateItem unifiedCustomSyncStateItem2 in this.nativeItemMappingTable.Values)
			{
				if (unifiedCustomSyncStateItem2.NativeFolderId != null && unifiedCustomSyncStateItem2.NativeFolderId.GetHashCode() == hashCode)
				{
					unifiedCustomSyncStateItem2.NativeFolderId = newNativeId;
				}
			}
			this.nativeFolderMappingTable.Remove(nativeId);
			item.NativeId = newNativeId;
			if (newNativeId != null && !isInbox)
			{
				this.nativeFolderMappingTable.Add(newNativeId, item);
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004287C File Offset: 0x00040A7C
		internal bool TryRemoveFolder(string cloudId)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (this.cloudFolderMappingTable.TryGetValue(cloudId, out unifiedCustomSyncStateItem) && unifiedCustomSyncStateItem.NativeId != null)
			{
				this.nativeItemMappingTable.Remove(unifiedCustomSyncStateItem.NativeId);
			}
			if (this.cloudFolderMappingTable.Remove(cloudId))
			{
				UnifiedCustomSyncState.RemoveOrphanedEntries(this.cloudFolderMappingTable, this.cloudItemMappingTable);
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000428DD File Offset: 0x00040ADD
		internal bool TryRemoveFailedFolder(string cloudId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			if (this.failedCloudFolders.Remove(cloudId))
			{
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00042904 File Offset: 0x00040B04
		internal bool TryRemoveFolder(StoreObjectId nativeId)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (this.nativeFolderMappingTable.TryGetValue(nativeId, out unifiedCustomSyncStateItem) && unifiedCustomSyncStateItem.CloudId != null)
			{
				this.cloudFolderMappingTable.Remove(unifiedCustomSyncStateItem.CloudId);
			}
			if (this.nativeFolderMappingTable.Remove(nativeId))
			{
				UnifiedCustomSyncState.RemoveOrphanedEntries(this.cloudFolderMappingTable, this.cloudItemMappingTable);
				this.dirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00042965 File Offset: 0x00040B65
		internal IEnumerator<string> GetCloudItemEnumerator()
		{
			return this.cloudItemMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004297C File Offset: 0x00040B7C
		internal IEnumerator<string> GetFailedCloudItemEnumerator()
		{
			return this.failedCloudItems.Keys.GetEnumerator();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00042993 File Offset: 0x00040B93
		internal IEnumerator<string> GetFailedCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return new FailedItemEnumeratorInCloudFolder(this.failedCloudItems.GetEnumerator(), cloudFolderId);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000429AB File Offset: 0x00040BAB
		internal IEnumerator<string> GetCloudFolderEnumerator()
		{
			return this.cloudFolderMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000429C2 File Offset: 0x00040BC2
		internal IEnumerator<StoreObjectId> GetNativeItemEnumerator()
		{
			return this.nativeItemMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000429D9 File Offset: 0x00040BD9
		internal IEnumerator<StoreObjectId> GetNativeFolderEnumerator()
		{
			return this.nativeFolderMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000429F0 File Offset: 0x00040BF0
		internal IEnumerator<string> GetCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return new EnumeratorInCloudFolder(this.cloudItemMappingTable.Values.GetEnumerator(), cloudFolderId);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00042A0D File Offset: 0x00040C0D
		internal IEnumerator<string> GetCloudFolderFilteredByCloudFolderIdEnumerator(string cloudFolderId)
		{
			return new EnumeratorInCloudFolder(this.cloudFolderMappingTable.Values.GetEnumerator(), cloudFolderId);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00042A2A File Offset: 0x00040C2A
		internal IEnumerator<StoreObjectId> GetNativeItemFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId)
		{
			return new EnumeratorInNativeFolder(this.nativeItemMappingTable.Values.GetEnumerator(), nativeFolderId);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00042A47 File Offset: 0x00040C47
		internal IEnumerator<StoreObjectId> GetNativeFolderFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId)
		{
			return new EnumeratorInNativeFolder(this.nativeFolderMappingTable.Values.GetEnumerator(), nativeFolderId);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00042A64 File Offset: 0x00040C64
		internal bool TryUpdateItemCloudVersion(string cloudId, string cloudVersion)
		{
			return this.TryUpdateCollectionCloudVersion<string>(this.cloudItemMappingTable, cloudId, cloudVersion);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00042A74 File Offset: 0x00040C74
		internal bool TryUpdateFolderCloudVersion(string cloudId, string cloudVersion)
		{
			return this.TryUpdateCollectionCloudVersion<string>(this.cloudFolderMappingTable, cloudId, cloudVersion);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00042A84 File Offset: 0x00040C84
		internal void AddProperty(string property, string value)
		{
			this.cloudProperties.Add(property, value);
			this.dirty = true;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00042A9A File Offset: 0x00040C9A
		internal bool TryGetPropertyValue(string property, out string value)
		{
			return this.cloudProperties.TryGetValue(property, out value);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00042AAC File Offset: 0x00040CAC
		internal bool TryRemoveProperty(string property)
		{
			bool flag = this.cloudProperties.Remove(property);
			if (flag)
			{
				this.dirty = true;
			}
			return flag;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00042AD1 File Offset: 0x00040CD1
		internal void ChangePropertyValue(string property, string value)
		{
			this.cloudProperties[property] = value;
			this.dirty = true;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00042AE7 File Offset: 0x00040CE7
		internal bool ContainsProperty(string property)
		{
			return this.cloudProperties.ContainsKey(property);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00042AF8 File Offset: 0x00040CF8
		internal bool ShouldPromoteItemTransientException(string cloudId, SyncTransientException exception)
		{
			if (!UnifiedCustomSyncState.ShouldContinuePromotingTransientException(exception))
			{
				return false;
			}
			this.dirty = true;
			if (SyncUtilities.VerifyNestedInnerExceptionType(exception, typeof(SyncConflictException)))
			{
				return UnifiedCustomSyncState.ShouldPromoteTransientException<string>(this.cloudItemSyncConflictTransientErrors, this.existingCloudItemSyncConflictTransientErrors, cloudId, 24);
			}
			return UnifiedCustomSyncState.ShouldPromoteTransientException<string>(this.cloudItemTransientErrors, this.existingCloudItemTransientErrors, cloudId, UnifiedCustomSyncState.MaxTransientErrors);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00042B54 File Offset: 0x00040D54
		internal bool ShouldPromoteItemTransientException(StoreObjectId nativeId, SyncTransientException exception)
		{
			if (!UnifiedCustomSyncState.ShouldContinuePromotingTransientException(exception))
			{
				return false;
			}
			this.dirty = true;
			if (SyncUtilities.VerifyNestedInnerExceptionType(exception, typeof(SyncConflictException)))
			{
				return UnifiedCustomSyncState.ShouldPromoteTransientException<StoreObjectId>(this.nativeItemSyncConflictTransientErrors, this.existingNativeItemSyncConflictTransientErrors, nativeId, 24);
			}
			return UnifiedCustomSyncState.ShouldPromoteTransientException<StoreObjectId>(this.nativeItemTransientErrors, this.existingNativeItemTransientErrors, nativeId, UnifiedCustomSyncState.MaxTransientErrors);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00042BB0 File Offset: 0x00040DB0
		internal bool ShouldPromoteFolderTransientException(string cloudId, SyncTransientException exception)
		{
			if (!UnifiedCustomSyncState.ShouldContinuePromotingTransientException(exception))
			{
				return false;
			}
			this.dirty = true;
			if (SyncUtilities.VerifyNestedInnerExceptionType(exception, typeof(SyncConflictException)))
			{
				return UnifiedCustomSyncState.ShouldPromoteTransientException<string>(this.cloudFolderSyncConflictTransientErrors, this.existingCloudFolderSyncConflictTransientErrors, cloudId, 24);
			}
			return UnifiedCustomSyncState.ShouldPromoteTransientException<string>(this.cloudFolderTransientErrors, this.existingCloudFolderTransientErrors, cloudId, UnifiedCustomSyncState.MaxTransientErrors);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00042C0C File Offset: 0x00040E0C
		internal bool ShouldPromoteFolderTransientException(StoreObjectId nativeId, SyncTransientException exception)
		{
			if (!UnifiedCustomSyncState.ShouldContinuePromotingTransientException(exception))
			{
				return false;
			}
			this.dirty = true;
			if (SyncUtilities.VerifyNestedInnerExceptionType(exception, typeof(SyncConflictException)))
			{
				return UnifiedCustomSyncState.ShouldPromoteTransientException<StoreObjectId>(this.nativeFolderSyncConflictTransientErrors, this.existingNativeFolderSyncConflictTransientErrors, nativeId, 24);
			}
			return UnifiedCustomSyncState.ShouldPromoteTransientException<StoreObjectId>(this.nativeFolderTransientErrors, this.existingNativeFolderTransientErrors, nativeId, UnifiedCustomSyncState.MaxTransientErrors);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00042C68 File Offset: 0x00040E68
		internal IEnumerator<string> GetItemEnumerator()
		{
			return this.cloudItemMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00042C7F File Offset: 0x00040E7F
		internal IEnumerator<string> GetFolderEnumerator()
		{
			return this.cloudFolderMappingTable.Keys.GetEnumerator();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00042C98 File Offset: 0x00040E98
		private bool TryUpdateCollection<T>(Dictionary<T, UnifiedCustomSyncStateItem> collection, T id, byte[] changeKey, string cloudVersion, Dictionary<string, string> properties)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (!collection.TryGetValue(id, out unifiedCustomSyncStateItem))
			{
				return false;
			}
			this.dirty = true;
			unifiedCustomSyncStateItem.ChangeKey = changeKey;
			unifiedCustomSyncStateItem.CloudVersion = cloudVersion;
			unifiedCustomSyncStateItem.Properties = properties;
			return true;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00042CD4 File Offset: 0x00040ED4
		private bool TryUpdateCollectionCloudVersion<T>(Dictionary<T, UnifiedCustomSyncStateItem> collection, T id, string cloudVersion)
		{
			UnifiedCustomSyncStateItem unifiedCustomSyncStateItem;
			if (!collection.TryGetValue(id, out unifiedCustomSyncStateItem))
			{
				return false;
			}
			this.dirty = true;
			unifiedCustomSyncStateItem.CloudVersion = cloudVersion;
			return true;
		}

		// Token: 0x04000A3C RID: 2620
		internal const short InvalidVersion = -32768;

		// Token: 0x04000A3D RID: 2621
		private const short MaxSyncConflictTransientErrors = 24;

		// Token: 0x04000A3E RID: 2622
		private const short CurrentVersion = 6;

		// Token: 0x04000A3F RID: 2623
		private const int DefaultSyncMappingSize = 100;

		// Token: 0x04000A40 RID: 2624
		private const int DefaultSyncConflictErrorListSize = 1;

		// Token: 0x04000A41 RID: 2625
		private const int DefaultFailedItemsListSize = 1;

		// Token: 0x04000A42 RID: 2626
		private const int DefaultFailedFoldersListSize = 1;

		// Token: 0x04000A43 RID: 2627
		internal static readonly int MaxTransientErrors = AggregationConfiguration.Instance.MaxTransientErrorsPerItem;

		// Token: 0x04000A44 RID: 2628
		private static ushort typeId;

		// Token: 0x04000A45 RID: 2629
		private bool dirty;

		// Token: 0x04000A46 RID: 2630
		private short version;

		// Token: 0x04000A47 RID: 2631
		private Dictionary<string, string> cloudProperties;

		// Token: 0x04000A48 RID: 2632
		private Dictionary<string, short> cloudItemTransientErrors;

		// Token: 0x04000A49 RID: 2633
		private Dictionary<string, string> failedCloudItems;

		// Token: 0x04000A4A RID: 2634
		private Dictionary<string, string> failedCloudFolders;

		// Token: 0x04000A4B RID: 2635
		private Dictionary<string, short> cloudItemSyncConflictTransientErrors;

		// Token: 0x04000A4C RID: 2636
		private Dictionary<StoreObjectId, short> nativeItemTransientErrors;

		// Token: 0x04000A4D RID: 2637
		private Dictionary<StoreObjectId, short> nativeItemSyncConflictTransientErrors;

		// Token: 0x04000A4E RID: 2638
		private Dictionary<string, short> cloudFolderTransientErrors;

		// Token: 0x04000A4F RID: 2639
		private Dictionary<string, short> cloudFolderSyncConflictTransientErrors;

		// Token: 0x04000A50 RID: 2640
		private Dictionary<StoreObjectId, short> nativeFolderTransientErrors;

		// Token: 0x04000A51 RID: 2641
		private Dictionary<StoreObjectId, short> nativeFolderSyncConflictTransientErrors;

		// Token: 0x04000A52 RID: 2642
		private byte syncEngineFlags;

		// Token: 0x04000A53 RID: 2643
		private Dictionary<StoreObjectId, UnifiedCustomSyncStateItem> nativeFolderMappingTable;

		// Token: 0x04000A54 RID: 2644
		private Dictionary<string, UnifiedCustomSyncStateItem> cloudFolderMappingTable;

		// Token: 0x04000A55 RID: 2645
		private Dictionary<StoreObjectId, UnifiedCustomSyncStateItem> nativeItemMappingTable;

		// Token: 0x04000A56 RID: 2646
		private Dictionary<string, UnifiedCustomSyncStateItem> cloudItemMappingTable;

		// Token: 0x04000A57 RID: 2647
		private Dictionary<string, short> existingCloudItemTransientErrors;

		// Token: 0x04000A58 RID: 2648
		private Dictionary<string, short> existingCloudItemSyncConflictTransientErrors;

		// Token: 0x04000A59 RID: 2649
		private Dictionary<StoreObjectId, short> existingNativeItemTransientErrors;

		// Token: 0x04000A5A RID: 2650
		private Dictionary<StoreObjectId, short> existingNativeItemSyncConflictTransientErrors;

		// Token: 0x04000A5B RID: 2651
		private Dictionary<string, short> existingCloudFolderTransientErrors;

		// Token: 0x04000A5C RID: 2652
		private Dictionary<string, short> existingCloudFolderSyncConflictTransientErrors;

		// Token: 0x04000A5D RID: 2653
		private Dictionary<StoreObjectId, short> existingNativeFolderTransientErrors;

		// Token: 0x04000A5E RID: 2654
		private Dictionary<StoreObjectId, short> existingNativeFolderSyncConflictTransientErrors;

		// Token: 0x02000221 RID: 545
		[Flags]
		private enum SyncEngineFlagsMask : byte
		{
			// Token: 0x04000A60 RID: 2656
			InitialSyncDone = 1
		}
	}
}
