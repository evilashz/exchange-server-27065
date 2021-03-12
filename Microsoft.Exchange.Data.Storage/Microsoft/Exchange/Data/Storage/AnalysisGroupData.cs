using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000613 RID: 1555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnalysisGroupData
	{
		// Token: 0x06003FDB RID: 16347 RVA: 0x0010A135 File Offset: 0x00108335
		public AnalysisGroupData(AnalysisFolderItems parent, AnalysisGroupKey key)
		{
			this.parent = parent;
			this.key = key;
			this.allItemsItemId = null;
			this.newestItemId = null;
			this.newestItemLMT = ExDateTime.MinValue;
			this.clientInfoStats = new Dictionary<int, int>();
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0010A170 File Offset: 0x00108370
		public Item GetItemInAllItems(ICollection<PropertyDefinition> propsToReturn)
		{
			Item result = null;
			this.FindItemInAllItems();
			if (this.allItemsItemId == null)
			{
				return null;
			}
			try
			{
				result = Item.Bind(this.parent.MailboxSession, this.allItemsItemId, propsToReturn);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string>((long)this.parent.MailboxSession.GetHashCode(), "AnalysisGroupData.GetItemInAllItems failed to get item {0}, error {1}.", this.allItemsItemId.ToString(), ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0010A1F0 File Offset: 0x001083F0
		public Item GetItemInGroup(ICollection<PropertyDefinition> propsToReturn)
		{
			Item result = null;
			if (this.newestItemId == null)
			{
				return null;
			}
			try
			{
				result = Item.Bind(this.parent.MailboxSession, this.newestItemId, propsToReturn);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string>((long)this.parent.MailboxSession.GetHashCode(), "AnalysisGroupData.GetItemInGroup failed to get item {0}, error {1}.", this.newestItemId.ToString(), ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x0010A26C File Offset: 0x0010846C
		public StoreObjectId GroupItemId
		{
			get
			{
				return this.newestItemId;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0010A274 File Offset: 0x00108474
		public StoreObjectId AllItemsItemId
		{
			get
			{
				return this.allItemsItemId;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x0010A27C File Offset: 0x0010847C
		public int GroupCount
		{
			get
			{
				return this.groupCount;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0010A284 File Offset: 0x00108484
		public ByteQuantifiedSize GroupSize
		{
			get
			{
				return new ByteQuantifiedSize(this.groupSize);
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x0010A294 File Offset: 0x00108494
		public KeyValuePair<string, int> TopClientInfo
		{
			get
			{
				int num = -1;
				int num2 = -1;
				foreach (KeyValuePair<int, int> keyValuePair in this.clientInfoStats)
				{
					if (keyValuePair.Value > num)
					{
						num = keyValuePair.Value;
						num2 = keyValuePair.Key;
					}
				}
				if (num2 >= 0)
				{
					return new KeyValuePair<string, int>(this.parent.Clients[num2], num);
				}
				return new KeyValuePair<string, int>(string.Empty, 0);
			}
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0010A328 File Offset: 0x00108528
		public string GetItemInGroupFolderPath()
		{
			if (this.newestItemId == null)
			{
				return string.Empty;
			}
			return AnalysisGroupData.GetFolderPathForFolderId(this.parent.MailboxSession, IdConverter.GetParentIdFromMessageId(this.newestItemId));
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0010A353 File Offset: 0x00108553
		public string GetItemInAllItemsFolderPath()
		{
			if (this.allItemsItemId == null)
			{
				return string.Empty;
			}
			return AnalysisGroupData.GetFolderPathForFolderId(this.parent.MailboxSession, IdConverter.GetParentIdFromMessageId(this.allItemsItemId));
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0010A380 File Offset: 0x00108580
		internal static string GetFolderPathForFolderId(MailboxSession session, StoreObjectId folderId)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			StoreObjectId storeObjectId = folderId;
			int num = 0;
			int num2 = 10;
			while (storeObjectId != null && num < num2)
			{
				using (Folder folder = Folder.Bind(session, storeObjectId))
				{
					num++;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.Append(folder.DisplayName);
					}
					else
					{
						stringBuilder.Insert(0, "\\");
						stringBuilder.Insert(0, folder.DisplayName);
					}
					num++;
					if (folder.ParentId != null && storeObjectId.Equals(folder.ParentId))
					{
						break;
					}
					storeObjectId = folder.ParentId;
				}
			}
			if (num >= num2)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<int, string, string>((long)session.GetHashCode(), "AnalysisGroupData.GetFolderPathForFolderId hit the max {0} folder depth for folder if {1}, path {2}.", num, folderId.ToString(), stringBuilder.ToString());
				stringBuilder.Insert(0, "...\\");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0010A468 File Offset: 0x00108668
		internal void AddOneItem(AnalysisItemsQueryData item)
		{
			this.groupCount++;
			this.groupSize += (ulong)((long)item.Size);
			if (item.ClientInfo != null && !item.ClientInfo.Equals(string.Empty))
			{
				int clientInfoId = this.parent.GetClientInfoId(item.ClientInfo);
				if (!this.clientInfoStats.ContainsKey(clientInfoId))
				{
					this.clientInfoStats[clientInfoId] = 1;
				}
				else
				{
					this.clientInfoStats[clientInfoId] = this.clientInfoStats[clientInfoId] + 1;
				}
			}
			if (this.newestItemLMT < item.LastModifiedTime)
			{
				this.newestItemLMT = item.LastModifiedTime;
				this.newestItemId = item.Id;
			}
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0010A528 File Offset: 0x00108728
		private void FindItemInAllItems()
		{
			this.allItemsItemId = null;
			AllItemsFolderHelper.CheckAndCreateDefaultFolders(this.parent.MailboxSession);
			using (Folder folder = Folder.Bind(this.parent.MailboxSession, this.key.FolderToSearch()))
			{
				PropertyDefinition[] dataColumns = new PropertyDefinition[]
				{
					ItemSchema.Id
				};
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, this.key.Filter, null, dataColumns))
				{
					object[][] rows = queryResult.GetRows(1);
					if (rows == null)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<string, string>((long)this.parent.MailboxSession.GetHashCode(), "AnalysisGroupData.FindItemInAllItems found NO items in folder {0} for the key {1}", this.key.FolderToSearch().ToString(), this.key.ToString());
						this.allItemsItemId = null;
					}
					else if (rows.Length != 1)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<string, string, int>((long)this.parent.MailboxSession.GetHashCode(), "AnalysisGroupData.FindItemInAllItems found {2} items in folder {0} for the key {1}", this.key.FolderToSearch().ToString(), this.key.ToString(), rows.Length);
						this.allItemsItemId = null;
					}
					else
					{
						this.allItemsItemId = StoreId.GetStoreObjectId(rows[0][0] as StoreId);
					}
				}
			}
		}

		// Token: 0x04002352 RID: 9042
		private AnalysisFolderItems parent;

		// Token: 0x04002353 RID: 9043
		private AnalysisGroupKey key;

		// Token: 0x04002354 RID: 9044
		private int groupCount;

		// Token: 0x04002355 RID: 9045
		private ulong groupSize;

		// Token: 0x04002356 RID: 9046
		private StoreObjectId newestItemId;

		// Token: 0x04002357 RID: 9047
		private ExDateTime newestItemLMT;

		// Token: 0x04002358 RID: 9048
		private StoreObjectId allItemsItemId;

		// Token: 0x04002359 RID: 9049
		private Dictionary<int, int> clientInfoStats;
	}
}
