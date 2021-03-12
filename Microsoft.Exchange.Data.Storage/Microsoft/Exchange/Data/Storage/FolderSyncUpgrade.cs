using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E1F RID: 3615
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderSyncUpgrade
	{
		// Token: 0x06007CFE RID: 31998 RVA: 0x00228A6E File Offset: 0x00226C6E
		public FolderSyncUpgrade(StoreObjectId rootIdIn)
		{
			this.rootId = rootIdIn;
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x00228A7D File Offset: 0x00226C7D
		private FolderSyncUpgrade()
		{
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x00228A88 File Offset: 0x00226C88
		public Dictionary<string, StoreObjectId> Upgrade(FolderHierarchySync folderHierarchySyncIn, SyncState syncState, Dictionary<string, FolderNode> nodesOldInfo, out Dictionary<string, StoreObjectType> contentTypeTable)
		{
			contentTypeTable = new Dictionary<string, StoreObjectType>();
			this.syncState = syncState;
			this.folderHierarchySync = folderHierarchySyncIn;
			Dictionary<string, StoreObjectId> result = this.ProcessCommand(nodesOldInfo, contentTypeTable);
			this.folderHierarchySync.AcknowledgeServerOperations();
			return result;
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x00228AC4 File Offset: 0x00226CC4
		private static string ConvertByteArrayToHex(byte[] id)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			for (int i = 0; i < id.Length; i++)
			{
				stringBuilder.Append(id[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x00228B0C File Offset: 0x00226D0C
		private static string GetOldIdFromNewId(byte[] id)
		{
			string text = FolderSyncUpgrade.ConvertByteArrayToHex(id);
			if (text.Length < 48)
			{
				return string.Empty;
			}
			return text.Substring(text.Length - 48, 44);
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x00228B44 File Offset: 0x00226D44
		private static void StoreType(FolderNode foldernode, StoreObjectId storeObjectId, Dictionary<string, StoreObjectType> contentTypeTable)
		{
			if (foldernode.ContentClass.CompareTo(FolderSyncUpgrade.folderType) == 0)
			{
				contentTypeTable[foldernode.ServerId] = StoreObjectType.Folder;
				return;
			}
			if (foldernode.ContentClass.CompareTo(FolderSyncUpgrade.emailType) == 0)
			{
				contentTypeTable[foldernode.ServerId] = StoreObjectType.Folder;
				return;
			}
			if (foldernode.ContentClass.CompareTo(FolderSyncUpgrade.calendarType) == 0)
			{
				contentTypeTable[foldernode.ServerId] = StoreObjectType.CalendarFolder;
				return;
			}
			if (foldernode.ContentClass.CompareTo(FolderSyncUpgrade.taskType) == 0)
			{
				contentTypeTable[foldernode.ServerId] = StoreObjectType.TasksFolder;
				return;
			}
			if (foldernode.ContentClass.CompareTo(FolderSyncUpgrade.contactType) == 0)
			{
				contentTypeTable[foldernode.ServerId] = StoreObjectType.ContactsFolder;
				return;
			}
			contentTypeTable[foldernode.ServerId] = StoreObjectType.Unknown;
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x00228C00 File Offset: 0x00226E00
		private string DepadNewId(string newId)
		{
			if (newId.Length != 44)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(newId, 0, 32, 40);
			stringBuilder.Append("-");
			int num = 38;
			while (num < 44 && newId[num] == '0')
			{
				num++;
			}
			stringBuilder.Append(newId.Substring(num));
			return stringBuilder.ToString();
		}

		// Token: 0x06007D05 RID: 32005 RVA: 0x00228C60 File Offset: 0x00226E60
		private string PadOldId(string oldId)
		{
			string result;
			if (oldId.CompareTo("0") == 0)
			{
				result = FolderSyncUpgrade.GetOldIdFromNewId(this.rootId.ProviderLevelItemId);
			}
			else
			{
				int num = oldId.IndexOf("-");
				result = oldId;
				if (num >= 0)
				{
					result = oldId.Substring(0, num) + "0000000000000000000000000000000000000000".Substring(0, 44 - oldId.Length + 1) + oldId.Substring(num + 1, oldId.Length - num - 1);
				}
			}
			return result;
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x00228CD8 File Offset: 0x00226ED8
		private Dictionary<string, StoreObjectId> ProcessCommand(Dictionary<string, FolderNode> folders, Dictionary<string, StoreObjectType> contentTypeTable)
		{
			HierarchySyncOperations hierarchySyncOperations = this.folderHierarchySync.EnumerateServerOperations();
			if (hierarchySyncOperations == null)
			{
				throw new ApplicationException("EnumerateServerOperations returned null!");
			}
			this.serverManifest = ((GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderManifestEntry>)this.syncState[SyncStateProp.CurServerManifest]).Data;
			if (this.serverManifest == null)
			{
				throw new ApplicationException("Server Manifest returned null!");
			}
			Dictionary<string, StoreObjectId> dictionary = new Dictionary<string, StoreObjectId>();
			if (folders != null)
			{
				for (int i = 0; i < hierarchySyncOperations.Count; i++)
				{
					HierarchySyncOperation hierarchySyncOperation = hierarchySyncOperations[i];
					using (Folder folder = hierarchySyncOperation.GetFolder())
					{
						string oldIdFromNewId = FolderSyncUpgrade.GetOldIdFromNewId(folder.Id.ObjectId.ProviderLevelItemId);
						if (oldIdFromNewId == null)
						{
							throw new ApplicationException("The new Id is invalid!");
						}
						FolderNode folderNode;
						if (folders.TryGetValue(oldIdFromNewId, out folderNode))
						{
							FolderSyncUpgrade.StoreType(folderNode, folder.Id.ObjectId, contentTypeTable);
							folders.Remove(oldIdFromNewId);
							dictionary.Add(folderNode.ServerId, folder.Id.ObjectId);
							FolderManifestEntry folderManifestEntry = this.serverManifest[folder.Id.ObjectId];
							if (FolderSyncUpgrade.GetOldIdFromNewId(folderManifestEntry.ParentId.ProviderLevelItemId).CompareTo(this.PadOldId(folderNode.ParentId)) != 0 || folder.DisplayName.CompareTo(folderNode.DisplayName) != 0)
							{
								folderManifestEntry.ChangeKey = new byte[]
								{
									1
								};
							}
						}
						else
						{
							if (FolderSyncUpgrade.GetOldIdFromNewId(folder.Id.ObjectId.ProviderLevelItemId) == null)
							{
								throw new ApplicationException("The new Id is invalid!");
							}
							string key = this.DepadNewId(FolderSyncUpgrade.GetOldIdFromNewId(folder.Id.ObjectId.ProviderLevelItemId));
							dictionary.Add(key, folder.Id.ObjectId);
							if (folder.ClassName.Equals("IPF.Appointment"))
							{
								contentTypeTable[key] = StoreObjectType.CalendarFolder;
							}
							else if (folder.ClassName.Equals("IPF.Contact"))
							{
								contentTypeTable[key] = StoreObjectType.ContactsFolder;
							}
							else
							{
								contentTypeTable[key] = StoreObjectType.Folder;
							}
							this.serverManifest.Remove(folder.Id.ObjectId);
						}
					}
				}
				IEnumerator enumerator = folders.GetEnumerator();
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					FolderNode value = ((KeyValuePair<string, FolderNode>)obj).Value;
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					byte[] bytes = unicodeEncoding.GetBytes(this.PadOldId(value.ServerId));
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(bytes, StoreObjectType.Mailbox);
					dictionary.Add(value.ServerId, storeObjectId);
					FolderSyncUpgrade.StoreType(value, storeObjectId, contentTypeTable);
					FolderManifestEntry folderManifestEntry2 = new FolderManifestEntry(storeObjectId);
					folderManifestEntry2.ChangeKey = new byte[]
					{
						1
					};
					folderManifestEntry2.ChangeType = ChangeType.Add;
					StoreObjectId storeObjectId2;
					dictionary.TryGetValue(value.ParentId, out storeObjectId2);
					if (storeObjectId2 == null)
					{
						if (value.ParentId.CompareTo("0") == 0)
						{
							folderManifestEntry2.ParentId = this.rootId;
						}
						else
						{
							folderManifestEntry2.ParentId = StoreObjectId.FromProviderSpecificId(unicodeEncoding.GetBytes(this.PadOldId(value.ParentId)));
						}
					}
					else
					{
						folderManifestEntry2.ParentId = storeObjectId2;
					}
					this.serverManifest[storeObjectId] = folderManifestEntry2;
				}
			}
			else
			{
				for (int j = 0; j < hierarchySyncOperations.Count; j++)
				{
					HierarchySyncOperation hierarchySyncOperation2 = hierarchySyncOperations[j];
					using (Folder folder2 = hierarchySyncOperation2.GetFolder())
					{
						string oldIdFromNewId2 = FolderSyncUpgrade.GetOldIdFromNewId(folder2.Id.ObjectId.ProviderLevelItemId);
						if (oldIdFromNewId2 == null)
						{
							throw new ApplicationException("The new Id is invalid!");
						}
						string key2 = this.DepadNewId(oldIdFromNewId2);
						dictionary.Add(key2, folder2.Id.ObjectId);
						if (folder2.ClassName.Equals("IPF.Appointment"))
						{
							contentTypeTable[key2] = StoreObjectType.CalendarFolder;
						}
						else if (folder2.ClassName.Equals("IPF.Contact"))
						{
							contentTypeTable[key2] = StoreObjectType.ContactsFolder;
						}
						else
						{
							contentTypeTable[key2] = StoreObjectType.Folder;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04005569 RID: 21865
		private const int NumberOfDigitsOfDavId = 44;

		// Token: 0x0400556A RID: 21866
		private const int NumberOfTrailingDigits = 4;

		// Token: 0x0400556B RID: 21867
		private const string ZeroString = "0000000000000000000000000000000000000000";

		// Token: 0x0400556C RID: 21868
		private static string calendarType = "calendarfolder";

		// Token: 0x0400556D RID: 21869
		private static string contactType = "contactfolder";

		// Token: 0x0400556E RID: 21870
		private static string emailType = "mailfolder";

		// Token: 0x0400556F RID: 21871
		private static string folderType = "folder";

		// Token: 0x04005570 RID: 21872
		private static string taskType = "taskfolder";

		// Token: 0x04005571 RID: 21873
		private FolderHierarchySync folderHierarchySync;

		// Token: 0x04005572 RID: 21874
		private StoreObjectId rootId;

		// Token: 0x04005573 RID: 21875
		private Dictionary<StoreObjectId, FolderManifestEntry> serverManifest;

		// Token: 0x04005574 RID: 21876
		private SyncState syncState;
	}
}
