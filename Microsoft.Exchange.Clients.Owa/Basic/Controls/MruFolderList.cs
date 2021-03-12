using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000072 RID: 114
	internal class MruFolderList
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0001B67C File Offset: 0x0001987C
		public MruFolderList(UserContext userContext)
		{
			PropertyDefinition[] propsToReturn = new PropertyDefinition[]
			{
				FolderSchema.DisplayName,
				FolderSchema.ItemCount,
				FolderSchema.UnreadCount,
				FolderSchema.ExtendedFolderFlags
			};
			FolderMruCache cacheInstance = FolderMruCache.GetCacheInstance(userContext);
			if (cacheInstance != null)
			{
				cacheInstance.Sort();
				int num = 0;
				while (num < cacheInstance.CacheLength && num < this.folderItems.Length)
				{
					StoreObjectId folderId = cacheInstance.CacheEntries[num].FolderId;
					Folder folder = null;
					try
					{
						folder = Folder.Bind(userContext.MailboxSession, folderId, propsToReturn);
					}
					catch (ObjectNotFoundException)
					{
						FolderMruCache.DeleteFromCache(folderId, userContext);
						cacheInstance = FolderMruCache.GetCacheInstance(userContext);
						cacheInstance.Sort();
						int entryIndexByFolderId = cacheInstance.GetEntryIndexByFolderId(folderId);
						if (entryIndexByFolderId != -1)
						{
							num++;
						}
						continue;
					}
					int i;
					for (i = 0; i < this.folderItemCount; i++)
					{
						if (string.CompareOrdinal(folder.DisplayName, this.folderItems[i].DisplayName) < 0)
						{
							for (int j = this.folderItemCount - 1; j >= i; j--)
							{
								if (j + 1 < this.folderItems.Length)
								{
									this.folderItems[j + 1] = this.folderItems[j];
								}
							}
							break;
						}
					}
					this.folderItems[i] = new MruFolderItem(folderId, folder.DisplayName, folder.ItemCount, (int)folder.TryGetProperty(FolderSchema.UnreadCount), folder.TryGetProperty(FolderSchema.ExtendedFolderFlags));
					this.folderItemCount++;
					folder.Dispose();
					num++;
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001B81C File Offset: 0x00019A1C
		public int Count
		{
			get
			{
				return this.folderItemCount;
			}
		}

		// Token: 0x170000AD RID: 173
		public MruFolderItem this[int i]
		{
			get
			{
				if (i < 0 || i >= this.Count)
				{
					throw new ArgumentOutOfRangeException("i", "Indexer of MruFolderList");
				}
				return this.folderItems[i];
			}
		}

		// Token: 0x0400024D RID: 589
		public const int MaxMruFolderNum = 10;

		// Token: 0x0400024E RID: 590
		private MruFolderItem[] folderItems = new MruFolderItem[10];

		// Token: 0x0400024F RID: 591
		private int folderItemCount;
	}
}
