using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E2D RID: 3629
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SyncStateDiagnostics
	{
		// Token: 0x06007D8C RID: 32140 RVA: 0x00229D9C File Offset: 0x00227F9C
		public static GetSyncStateResult GetData(MailboxSession session, ParsedCallData callData)
		{
			GetSyncStateResult getSyncStateResult = new GetSyncStateResult();
			getSyncStateResult.LoggingEnabled = SyncStateStorage.GetMailboxLoggingEnabled(session, null);
			using (SyncStateStorage.GetSyncFolderRoot(session, null))
			{
				UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(session, null);
				List<DeviceSyncStateMetadata> allDevices = userSyncStateMetadata.GetAllDevices(session, true, null);
				getSyncStateResult.Devices = new List<DeviceData>(allDevices.Count);
				foreach (DeviceSyncStateMetadata deviceSyncStateMetadata in allDevices)
				{
					if (SyncStateDiagnostics.ShouldAddDevice(callData, deviceSyncStateMetadata.Id))
					{
						DeviceData deviceData = new DeviceData
						{
							Name = deviceSyncStateMetadata.Id.CompositeKey,
							SyncFolders = new List<SyncStateFolderData>(),
							FolderId = deviceSyncStateMetadata.DeviceFolderId
						};
						getSyncStateResult.Devices.Add(deviceData);
						foreach (KeyValuePair<string, SyncStateMetadata> keyValuePair in deviceSyncStateMetadata.SyncStates)
						{
							bool flag = string.Equals(keyValuePair.Key, callData.SyncStateName, StringComparison.OrdinalIgnoreCase);
							if (callData.SyncStateName == null || flag)
							{
								SyncStateFolderData syncStateFolderData = new SyncStateFolderData
								{
									Name = keyValuePair.Key,
									StorageType = keyValuePair.Value.StorageType.ToString()
								};
								if (flag)
								{
									SyncStateDiagnostics.GetSyncStateBlob(session, keyValuePair.Value, syncStateFolderData);
								}
								else
								{
									syncStateFolderData.SyncStateSize = -1;
								}
								deviceData.SyncFolders.Add(syncStateFolderData);
							}
						}
					}
				}
			}
			return getSyncStateResult;
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x00229F88 File Offset: 0x00228188
		private static void GetSyncStateBlob(MailboxSession session, SyncStateMetadata syncStateMetadata, SyncStateFolderData data)
		{
			switch (syncStateMetadata.StorageType)
			{
			case StorageType.Folder:
				using (Folder folder = Folder.Bind(session, syncStateMetadata.FolderSyncStateId, new PropertyDefinition[]
				{
					ItemSchema.SyncCustomState
				}))
				{
					object obj = folder.TryGetProperty(ItemSchema.SyncCustomState);
					byte[] array = obj as byte[];
					data.SyncStateBlob = Convert.ToBase64String(array);
					data.SyncStateSize = array.Length;
					data.Created = (DateTime)folder.CreationTime;
					return;
				}
				break;
			case StorageType.Item:
			case StorageType.DirectItem:
				break;
			default:
				return;
			}
			using (Item item = Item.Bind(session, syncStateMetadata.ItemSyncStateId, new PropertyDefinition[]
			{
				ItemSchema.SyncCustomState
			}))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Stream stream = item.OpenPropertyStream(ItemSchema.SyncCustomState, PropertyOpenMode.ReadOnly))
					{
						byte[] data2 = new byte[1024];
						Util.StreamHandler.CopyStreamData(stream, memoryStream, null, 0, data2);
						memoryStream.Flush();
					}
					data.Created = (DateTime)item.CreationTime;
					data.SyncStateSize = (int)memoryStream.Position;
					string syncStateBlob = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Position, Base64FormattingOptions.None);
					data.SyncStateBlob = syncStateBlob;
				}
			}
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x0022A114 File Offset: 0x00228314
		private static bool ShouldAddDevice(ParsedCallData callData, DeviceIdentity deviceIdentity)
		{
			return callData.DeviceId == null || deviceIdentity.Equals(callData.DeviceId, callData.DeviceType);
		}

		// Token: 0x04005598 RID: 21912
		private const int DisplayNameIndex = 0;

		// Token: 0x04005599 RID: 21913
		private const int CreationTimeIndex = 1;

		// Token: 0x0400559A RID: 21914
		private const int ParentItemIdIndex = 2;

		// Token: 0x0400559B RID: 21915
		private const int ItemCountIndex = 3;

		// Token: 0x0400559C RID: 21916
		private const int LastModifiedTimeIndex = 4;

		// Token: 0x0400559D RID: 21917
		private const int IdIndex = 5;

		// Token: 0x0400559E RID: 21918
		private static readonly StorePropertyDefinition[] FolderQueryProps = new StorePropertyDefinition[]
		{
			FolderSchema.DisplayName,
			StoreObjectSchema.CreationTime,
			StoreObjectSchema.ParentItemId,
			FolderSchema.ItemCount,
			StoreObjectSchema.LastModifiedTime,
			FolderSchema.Id
		};
	}
}
