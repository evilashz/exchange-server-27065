using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x02000278 RID: 632
	internal class MailboxUtility
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0008B314 File Offset: 0x00089514
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0008B31C File Offset: 0x0008951C
		public MailboxSession MailboxSessionForUtility { get; set; }

		// Token: 0x06001767 RID: 5991 RVA: 0x0008B328 File Offset: 0x00089528
		public MailboxUtilityDeviceInfo GetDevice(DeviceIdentity deviceIdentity)
		{
			using (Folder folder = Folder.Bind(this.MailboxSessionForUtility, this.MailboxSessionForUtility.GetDefaultFolderId(DefaultFolderType.Configuration)))
			{
				StoreObjectId folderIdByDisplayName = MailboxUtility.GetFolderIdByDisplayName(folder, "Microsoft-Server-ActiveSync");
				if (folderIdByDisplayName == null)
				{
					return null;
				}
				using (Folder folder2 = Folder.Bind(this.MailboxSessionForUtility, folderIdByDisplayName))
				{
					StoreObjectId folderIdByDisplayName2 = MailboxUtility.GetFolderIdByDisplayName(folder2, deviceIdentity.DeviceType);
					if (folderIdByDisplayName2 == null)
					{
						return null;
					}
					using (QueryResult queryResult = folder2.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, MailboxUtility.folderProperties))
					{
						QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, deviceIdentity.DeviceId);
						queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
						object[] array = null;
						object[][] rows;
						while (array == null && (rows = queryResult.GetRows(10000)) != null && rows.Length > 0)
						{
							for (int i = 0; i < rows.Length; i++)
							{
								StoreObjectId storeObjectId = rows[i][1] as StoreObjectId;
								if (storeObjectId.CompareTo(folderIdByDisplayName2) == 0)
								{
									array = rows[i];
									break;
								}
							}
						}
						if (array != null)
						{
							StoreObjectId storeOrVersionedId = MailboxUtility.GetStoreOrVersionedId(array[0]);
							if (storeOrVersionedId == null)
							{
								return null;
							}
							using (Folder folder3 = Folder.Bind(this.MailboxSessionForUtility, storeOrVersionedId))
							{
								using (QueryResult queryResult2 = folder3.ItemQuery(ItemQueryType.None, null, null, MailboxUtility.itemSubjectProperty))
								{
									object[][] rows2 = queryResult2.GetRows(10000);
									HashSet<string> hashSet = new HashSet<string>();
									while (rows2 != null && rows2.Length > 0)
									{
										for (int j = 0; j < rows2.Length; j++)
										{
											string text = rows2[j][0] as string;
											if (text != null)
											{
												hashSet.TryAdd(text);
											}
										}
										rows2 = queryResult2.GetRows(10000);
									}
									return new MailboxUtilityDeviceInfo(deviceIdentity.DeviceId, deviceIdentity.DeviceType, storeOrVersionedId, hashSet);
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0008B584 File Offset: 0x00089784
		public void DeleteSyncStateStorage(DeviceIdentity deviceIdentity)
		{
			SyncStateStorage.DeleteSyncStateStorage(this.MailboxSessionForUtility, deviceIdentity, null);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0008B594 File Offset: 0x00089794
		internal static void ReclaimStream(MemoryStream stream)
		{
			MailboxUtility.MemoryStream50K memoryStream50K = stream as MailboxUtility.MemoryStream50K;
			if (memoryStream50K != null)
			{
				memoryStream50K.Seek(0L, SeekOrigin.Begin);
				stream.SetLength(0L);
				MailboxUtility.bufferPool.Release(memoryStream50K);
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0008B5C8 File Offset: 0x000897C8
		internal MemoryStream GetSyncState(StoreObjectId parentFolderId, string syncstateName)
		{
			if (!this.parentIdToSyncstates.ContainsKey(parentFolderId))
			{
				Dictionary<string, StoreObjectId> dictionary = new Dictionary<string, StoreObjectId>();
				using (Folder folder = Folder.Bind(this.MailboxSessionForUtility, parentFolderId))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, MailboxUtility.itemIdSubjectProperties))
					{
						object[][] rows = queryResult.GetRows(10000);
						while (rows != null && rows.Length > 0)
						{
							for (int i = 0; i < rows.Length; i++)
							{
								string text = rows[i][1] as string;
								StoreObjectId storeOrVersionedId = MailboxUtility.GetStoreOrVersionedId(rows[i][0]);
								if (text != null && storeOrVersionedId != null)
								{
									dictionary[text] = storeOrVersionedId;
								}
							}
							rows = queryResult.GetRows(10000);
						}
					}
				}
				this.parentIdToSyncstates[parentFolderId] = dictionary;
			}
			if (!this.parentIdToSyncstates[parentFolderId].ContainsKey(syncstateName))
			{
				return null;
			}
			StoreObjectId storeId = this.parentIdToSyncstates[parentFolderId][syncstateName];
			using (Item item = Item.Bind(this.MailboxSessionForUtility, storeId))
			{
				IList<AttachmentHandle> handles = item.AttachmentCollection.GetHandles();
				if (handles.Count > 0)
				{
					using (Attachment attachment = item.AttachmentCollection.Open(handles[0]))
					{
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						if (streamAttachment != null)
						{
							using (Stream contentStream = streamAttachment.GetContentStream())
							{
								MemoryStream memoryStream = MailboxUtility.bufferPool.Acquire(1000);
								StreamHelper.CopyStream(contentStream, memoryStream, 0, (int)contentStream.Length);
								return memoryStream;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0008B7A4 File Offset: 0x000899A4
		internal void DeleteFolder(StoreObjectId storeId, bool includeRoot)
		{
			if (!includeRoot)
			{
				this.MailboxSessionForUtility.DeleteAllObjects(DeleteItemFlags.SoftDelete, storeId);
				return;
			}
			this.MailboxSessionForUtility.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
			{
				storeId
			});
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0008B7DC File Offset: 0x000899DC
		private static StoreObjectId GetFolderIdByDisplayName(Folder parentFolder, string folderName)
		{
			object[][] array = null;
			using (QueryResult queryResult = parentFolder.FolderQuery(FolderQueryFlags.None, null, MailboxUtility.sortByDisplayName, new PropertyDefinition[]
			{
				FolderSchema.Id
			}))
			{
				ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, folderName);
				queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
				array = queryResult.GetRows(1);
			}
			if (array != null && array.Length == 1)
			{
				return MailboxUtility.GetStoreOrVersionedId(array[0][0]);
			}
			return null;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0008B858 File Offset: 0x00089A58
		private static StoreObjectId GetStoreOrVersionedId(object target)
		{
			StoreObjectId storeObjectId = target as StoreObjectId;
			if (storeObjectId == null)
			{
				VersionedId versionedId = target as VersionedId;
				if (versionedId == null)
				{
					return null;
				}
				storeObjectId = versionedId.ObjectId;
			}
			return storeObjectId;
		}

		// Token: 0x04000E52 RID: 3666
		private const string TiSyncFolderRootName = "Microsoft-Server-ActiveSync";

		// Token: 0x04000E53 RID: 3667
		private static readonly SortBy[] sortByDisplayName = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x04000E54 RID: 3668
		private static readonly StorePropertyDefinition[] folderProperties = new StorePropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.DisplayName
		};

		// Token: 0x04000E55 RID: 3669
		private static readonly PropertyDefinition[] itemSubjectProperty = new PropertyDefinition[]
		{
			ItemSchema.Subject
		};

		// Token: 0x04000E56 RID: 3670
		private static readonly PropertyDefinition[] itemIdSubjectProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Subject
		};

		// Token: 0x04000E57 RID: 3671
		private static ThrottlingObjectPool<MailboxUtility.MemoryStream50K> bufferPool = new ThrottlingObjectPool<MailboxUtility.MemoryStream50K>(1, 5);

		// Token: 0x04000E58 RID: 3672
		private Dictionary<StoreObjectId, Dictionary<string, StoreObjectId>> parentIdToSyncstates = new Dictionary<StoreObjectId, Dictionary<string, StoreObjectId>>(5);

		// Token: 0x02000279 RID: 633
		private class MemoryStream50K : MemoryStream
		{
			// Token: 0x06001770 RID: 6000 RVA: 0x0008B923 File Offset: 0x00089B23
			public MemoryStream50K() : base(51200)
			{
			}
		}
	}
}
