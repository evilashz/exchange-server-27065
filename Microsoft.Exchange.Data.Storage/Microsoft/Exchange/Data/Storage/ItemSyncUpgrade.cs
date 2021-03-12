using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E25 RID: 3621
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemSyncUpgrade
	{
		// Token: 0x06007D5E RID: 32094 RVA: 0x0022930C File Offset: 0x0022750C
		public Dictionary<StoreObjectId, string> Upgrade(FolderSyncState syncStateState, Dictionary<string, CommonNode> mappingOldInfo, Folder sourceFolder, MailboxSession mailboxSession)
		{
			this.syncState = syncStateState;
			return this.ProcessCommand(mappingOldInfo, sourceFolder, mailboxSession);
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x0022932C File Offset: 0x0022752C
		private static string ConvertByteArrayToHex(byte[] id)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			for (int i = 0; i < id.Length; i++)
			{
				stringBuilder.Append(id[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x00229374 File Offset: 0x00227574
		private static string GetOldIdFromNewId(byte[] id)
		{
			string text = ItemSyncUpgrade.ConvertByteArrayToHex(id);
			if (text.Length < 48)
			{
				return null;
			}
			return text.Substring(text.Length - 48, 44);
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x002293A8 File Offset: 0x002275A8
		private static bool HasChanged(string oldVersionId, VersionedId newVersionId)
		{
			string text = oldVersionId.Substring(44);
			string strB = ItemSyncUpgrade.ConvertByteArrayToHex(newVersionId.ChangeKeyAsByteArray());
			return text.CompareTo(strB) != 0;
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x002293D8 File Offset: 0x002275D8
		private Dictionary<StoreObjectId, string> ProcessCommand(Dictionary<string, CommonNode> oldItems, Folder sourceFolder, MailboxSession mailboxSession)
		{
			if (oldItems == null)
			{
				throw new ArgumentNullException("items");
			}
			Dictionary<StoreObjectId, string> dictionary = new Dictionary<StoreObjectId, string>();
			if (oldItems.Count == 0)
			{
				return dictionary;
			}
			GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, ClientManifestEntry> genericDictionaryData = (GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, ClientManifestEntry>)this.syncState[SyncStateProp.CumulativeClientManifest];
			if (genericDictionaryData != null)
			{
				return null;
			}
			genericDictionaryData = new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, ClientManifestEntry>();
			this.syncState[SyncStateProp.CumulativeClientManifest] = genericDictionaryData;
			this.clientManifest = genericDictionaryData.Data;
			this.clientManifest = new Dictionary<ISyncItemId, ClientManifestEntry>();
			genericDictionaryData.Data = this.clientManifest;
			GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation> genericDictionaryData2 = (GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation>)this.syncState[SyncStateProp.ClientState];
			if (genericDictionaryData2 != null)
			{
				return null;
			}
			genericDictionaryData2 = new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation>();
			this.syncState[SyncStateProp.ClientState] = genericDictionaryData2;
			genericDictionaryData2.Data = new Dictionary<ISyncItemId, FolderSync.ClientStateInformation>();
			MailboxSyncProviderFactory mailboxSyncProviderFactory = new MailboxSyncProviderFactory(mailboxSession, sourceFolder.StoreObjectId);
			ISyncProvider syncProvider = mailboxSyncProviderFactory.CreateSyncProvider(null);
			FolderSync folderSync = new FolderSync(syncProvider, this.syncState, ConflictResolutionPolicy.ServerWins, false);
			using (QueryResult queryResult = sourceFolder.ItemQuery(ItemQueryType.None, null, null, ItemSyncUpgrade.queryColumns))
			{
				bool flag = false;
				while (!flag && oldItems.Count > 0)
				{
					object[][] rows = queryResult.GetRows(10000);
					flag = (rows.Length == 0);
					int num = 0;
					while (num < rows.Length && oldItems.Count > 0)
					{
						if (!(rows[num][0] is VersionedId) || !(rows[num][1] is int) || !(rows[num][2] is bool))
						{
							throw new ApplicationException("The data returned from the query is unusable!");
						}
						VersionedId versionedId = (VersionedId)rows[num][0];
						StoreObjectId objectId = versionedId.ObjectId;
						int changeNumber = (int)rows[num][1];
						string messageClass = rows[num][3] as string;
						string oldIdFromNewId = ItemSyncUpgrade.GetOldIdFromNewId(objectId.ProviderLevelItemId);
						if (oldIdFromNewId == null)
						{
							throw new ApplicationException("The new Id is invalid!");
						}
						if (oldItems.ContainsKey(oldIdFromNewId))
						{
							CommonNode commonNode = oldItems[oldIdFromNewId];
							oldItems.Remove(oldIdFromNewId);
							dictionary.Add(objectId, commonNode.ServerId);
							ISyncItemId syncItemId = MailboxSyncItemId.CreateForNewItem(objectId);
							FolderSync.ClientStateInformation clientStateInformation = new FolderSync.ClientStateInformation();
							clientStateInformation.ClientHasItem = true;
							genericDictionaryData2.Data[syncItemId] = clientStateInformation;
							if (!ItemSyncUpgrade.HasChanged(commonNode.VersionId, versionedId))
							{
								bool read;
								if (commonNode.IsEmail && commonNode.Read != (bool)rows[num][2])
								{
									folderSync.QueueDelayedServerOperation(new ServerManifestEntry(syncItemId)
									{
										ChangeType = ChangeType.ReadFlagChange
									});
									read = commonNode.Read;
								}
								else
								{
									read = (bool)rows[num][2];
								}
								ClientManifestEntry clientManifestEntry = new ClientManifestEntry(syncItemId);
								clientManifestEntry.Watermark = MailboxSyncWatermark.CreateForSingleItem();
								clientManifestEntry.ClientAddId = "TiSyncStateUpgrade";
								clientManifestEntry.ChangeType = ChangeType.Add;
								clientManifestEntry.MessageClass = messageClass;
								((MailboxSyncWatermark)clientManifestEntry.Watermark).UpdateWithChangeNumber(changeNumber, read);
								this.clientManifest.Add(syncItemId, clientManifestEntry);
							}
							else
							{
								folderSync.QueueDelayedServerOperation(new ServerManifestEntry(syncItemId)
								{
									ChangeType = ChangeType.Change,
									MessageClass = messageClass
								});
							}
						}
						num++;
					}
				}
			}
			if (oldItems.Count > 0)
			{
				foreach (KeyValuePair<string, CommonNode> keyValuePair in oldItems)
				{
					string key = keyValuePair.Key;
					CommonNode value = keyValuePair.Value;
					byte[] bytes = Encoding.Unicode.GetBytes(value.ServerId);
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(bytes, StoreObjectType.Mailbox);
					dictionary.Add(storeObjectId, value.ServerId);
					MailboxSyncItemId mailboxSyncItemId = MailboxSyncItemId.CreateForNewItem(storeObjectId);
					folderSync.QueueDelayedServerOperation(new ServerManifestEntry(mailboxSyncItemId)
					{
						ChangeType = ChangeType.Delete
					});
					FolderSync.ClientStateInformation clientStateInformation2 = new FolderSync.ClientStateInformation();
					clientStateInformation2.ClientHasItem = true;
					genericDictionaryData2.Data[mailboxSyncItemId] = clientStateInformation2;
				}
			}
			return dictionary;
		}

		// Token: 0x04005579 RID: 21881
		private const int NumberOfDigitsOfDavId = 44;

		// Token: 0x0400557A RID: 21882
		private const int NumberOfTrailingDigits = 4;

		// Token: 0x0400557B RID: 21883
		private const int IdxItemId = 0;

		// Token: 0x0400557C RID: 21884
		private const int IdxArticleId = 1;

		// Token: 0x0400557D RID: 21885
		private const int IdxIsRead = 2;

		// Token: 0x0400557E RID: 21886
		private const int IdxItemClass = 3;

		// Token: 0x0400557F RID: 21887
		private static readonly PropertyDefinition[] queryColumns = new PropertyDefinition[]
		{
			InternalSchema.ItemId,
			ItemSchema.ArticleId,
			MessageItemSchema.IsRead,
			InternalSchema.ItemClass
		};

		// Token: 0x04005580 RID: 21888
		private FolderSyncState syncState;

		// Token: 0x04005581 RID: 21889
		private Dictionary<ISyncItemId, ClientManifestEntry> clientManifest;
	}
}
