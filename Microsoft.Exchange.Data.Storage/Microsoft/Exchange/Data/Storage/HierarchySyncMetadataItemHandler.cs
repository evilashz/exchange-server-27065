﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200093C RID: 2364
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class HierarchySyncMetadataItemHandler : SingleInstanceItemHandler<HierarchySyncMetadataItem>
	{
		// Token: 0x0600582D RID: 22573 RVA: 0x0016A79E File Offset: 0x0016899E
		internal HierarchySyncMetadataItemHandler() : base(ItemQueryType.Associated, "IPM.HierarchySync.Metadata")
		{
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0016A7AC File Offset: 0x001689AC
		protected override Trace Tracer
		{
			get
			{
				return HierarchySyncMetadataItemHandler.PublicFolderTracer;
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0016A7B3 File Offset: 0x001689B3
		protected override HierarchySyncMetadataItem BindToItem(IStoreSession session, StoreId itemId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<HierarchySyncMetadataItem>(session as StoreSession, itemId, HierarchySyncMetadataItemSchema.Instance, propsToReturn);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x0016A7C8 File Offset: 0x001689C8
		protected override HierarchySyncMetadataItem CreateItem(IStoreSession session, IFolder folder)
		{
			HierarchySyncMetadataItem hierarchySyncMetadataItem = ItemBuilder.CreateNewItem<HierarchySyncMetadataItem>(session as StoreSession, folder.Id, ItemCreateInfo.HierarchySyncMetadataInfo, CreateMessageType.Associated);
			hierarchySyncMetadataItem[StoreObjectSchema.ItemClass] = "IPM.HierarchySync.Metadata";
			return hierarchySyncMetadataItem;
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x0016A800 File Offset: 0x00168A00
		protected override void InitializeNewItemData(IStoreSession session, IFolder folder, HierarchySyncMetadataItem newMetadataItem)
		{
			if (folder != null)
			{
				bool flag = false;
				HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Getting UserConfiguration from folder {0}", folder.Id);
				using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder as Folder, new UserConfigurationName("PublicFolderSyncInfo", ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary))
				{
					IDictionary dictionary = configuration.GetDictionary();
					ExDateTime exDateTime;
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<ExDateTime>(dictionary, "FirstFailedSyncTimeAfterLastSuccess", out exDateTime))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found FirstFailedSyncTimeAfterLastSuccess in UserConfiguration. Value {0}", exDateTime);
						newMetadataItem.FirstFailedSyncTimeAfterLastSuccess = exDateTime;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<ExDateTime>(dictionary, "LastAttemptedSyncTime", out exDateTime))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found LastAttemptedSyncTime in UserConfiguration. Value {0}", exDateTime);
						newMetadataItem.LastAttemptedSyncTime = exDateTime;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<ExDateTime>(dictionary, "LastFailedSyncTime", out exDateTime))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found LastFailedSyncTime in UserConfiguration. Value {0}", exDateTime);
						newMetadataItem.LastFailedSyncTime = exDateTime;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<ExDateTime>(dictionary, "LastSuccessfulSyncTime", out exDateTime))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found LastSuccessfulSyncTime in UserConfiguration. Value {0}", exDateTime);
						newMetadataItem.LastSuccessfulSyncTime = exDateTime;
						flag = true;
					}
					int num;
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<int>(dictionary, "NumberofAttemptsAfterLastSuccess", out num))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<int>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found NumberOfAttemptsAfterLastSuccess in UserConfiguration. Value {0}", num);
						newMetadataItem.NumberOfAttemptsAfterLastSuccess = num;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<int>(dictionary, "NumberOfBatchesExecuted", out num))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<int>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found NumberOfBatchesExecuted in UserConfiguration. Value {0}", num);
						newMetadataItem.NumberOfBatchesExecuted = num;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<int>(dictionary, "NumberOfFoldersSynced", out num))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<int>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found NumberOfFoldersSynced in UserConfiguration. Value {0}", num);
						newMetadataItem.NumberOfFoldersSynced = num;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<int>(dictionary, "NumberOfFoldersToBeSynced", out num))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<int>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found NumberOfFoldersToBeSynced in UserConfiguration. Value {0}", num);
						newMetadataItem.NumberOfFoldersToBeSynced = num;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<int>(dictionary, "BatchSize", out num))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<int>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found BatchSize in UserConfiguration. Value {0}", num);
						newMetadataItem.BatchSize = num;
						flag = true;
					}
					string text;
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<string>(dictionary, "LastSyncFailure", out text))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found LastSyncFailure in UserConfiguration. Value {0}", text);
						newMetadataItem.LastSyncFailure = text;
						flag = true;
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<string>(dictionary, "SyncState", out text) && !string.IsNullOrWhiteSpace(text))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found SyncState in UserConfiguration. Value {0}", text);
						using (Stream syncStateOverrideStream = newMetadataItem.GetSyncStateOverrideStream())
						{
							HierarchySyncMetadataItemHandler.CopyStringToMetadataAttachment(text, syncStateOverrideStream);
							flag = true;
						}
					}
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<string>(dictionary, "FinalJobSyncState", out text) && !string.IsNullOrWhiteSpace(text))
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found FinalJobSyncState in UserConfiguration. Value {0}", text);
						using (Stream finalJobSyncStateWriteStream = newMetadataItem.GetFinalJobSyncStateWriteStream(true))
						{
							HierarchySyncMetadataItemHandler.CopyStringToMetadataAttachment(text, finalJobSyncStateWriteStream);
							flag = true;
						}
					}
					byte[] array;
					if (HierarchySyncMetadataItemHandler.TryGetLegacyMetadataValue<byte[]>(dictionary, "PartiallyCommittedFolderIds", out array))
					{
						if (HierarchySyncMetadataItemHandler.PublicFolderTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Found PartiallyCommittedFolderIds in UserConfiguration. Value {0}", Convert.ToBase64String(array));
						}
						using (Reader reader = Reader.CreateBufferReader(array))
						{
							newMetadataItem.SetPartiallyCommittedFolderIds(IdSet.ParseWithReplGuids(reader));
							flag = true;
						}
					}
				}
				if (flag)
				{
					HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Metadata Item was updated. Saving and reloading properties.");
					newMetadataItem.Save(SaveMode.FailOnAnyConflict);
					newMetadataItem.Load();
					return;
				}
			}
			else
			{
				HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceDebug((long)this.GetHashCode(), "HierarchySyncMetadataItemHandler:InitializeNewItemData - Skipping import from UserConfiguration as no folder was provided.");
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x0016ABE4 File Offset: 0x00168DE4
		private static void CopyStringToMetadataAttachment(string syncStateValue, Stream attachmentStream)
		{
			using (StreamWriter streamWriter = new StreamWriter(attachmentStream))
			{
				streamWriter.Write(syncStateValue);
			}
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x0016AC1C File Offset: 0x00168E1C
		private static bool TryGetLegacyMetadataValue<T>(IDictionary dictionary, string name, out T propertyValue)
		{
			propertyValue = default(T);
			if (dictionary.Contains(name))
			{
				object obj = dictionary[name];
				if (obj != null)
				{
					try
					{
						propertyValue = (T)((object)obj);
					}
					catch (InvalidCastException)
					{
						HierarchySyncMetadataItemHandler.PublicFolderTracer.TraceError<Type, Type>(0L, "HierarchySyncMetadataItem:TryGetLegacyMetadataValue - Object found in the dictionary was not of the expected type. Found={0}. Expected={1}.", obj.GetType(), typeof(T));
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003008 RID: 12296
		private const string LegacyUserConfigurationName = "PublicFolderSyncInfo";

		// Token: 0x04003009 RID: 12297
		private static readonly Trace PublicFolderTracer = ExTraceGlobals.PublicFoldersTracer;
	}
}
