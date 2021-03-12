using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000611 RID: 1553
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnalysisFolderItems
	{
		// Token: 0x06003FCE RID: 16334 RVA: 0x00109CC4 File Offset: 0x00107EC4
		public AnalysisFolderItems(MailboxSession session, IEnumerable<StoreObjectId> folderIds)
		{
			this.session = session;
			this.folders = new List<StoreObjectId>(folderIds);
			this.itemGroups = new Dictionary<string, AnalysisGroupData>();
			this.clientsByString = new Dictionary<string, int>();
			this.clientsById = new Dictionary<int, string>();
			this.itemGroupsBySize = new List<AnalysisGroupData>(0);
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x00109D17 File Offset: 0x00107F17
		internal Dictionary<int, string> Clients
		{
			get
			{
				return this.clientsById;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x00109D1F File Offset: 0x00107F1F
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x00109D28 File Offset: 0x00107F28
		public int GetClientInfoId(string clientInfo)
		{
			int result = -1;
			if (!this.clientsByString.TryGetValue(clientInfo, out result))
			{
				int count = this.clientsByString.Count;
				this.clientsByString[clientInfo] = count;
				result = count;
			}
			return result;
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x00109D65 File Offset: 0x00107F65
		public List<AnalysisGroupData> GroupsBySize()
		{
			return this.itemGroupsBySize;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x00109D6D File Offset: 0x00107F6D
		public TimeSpan ExecuteTime()
		{
			return ExDateTime.TimeDiff(this.processEndTime, this.processStartTime);
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x00109D80 File Offset: 0x00107F80
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x00109D88 File Offset: 0x00107F88
		public ByteQuantifiedSize TotalSize
		{
			get
			{
				return new ByteQuantifiedSize(this.totalSize);
			}
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x00109D98 File Offset: 0x00107F98
		public void Execute()
		{
			this.processEndTime = (this.processStartTime = ExDateTime.Now);
			this.totalCount = 0;
			this.totalSize = 0UL;
			this.itemGroups = new Dictionary<string, AnalysisGroupData>();
			this.clientsByString = new Dictionary<string, int>();
			this.clientsById = new Dictionary<int, string>();
			foreach (StoreObjectId folderId in this.folders)
			{
				this.ProcessOneFolder(folderId);
			}
			this.itemGroupsBySize = new List<AnalysisGroupData>(this.itemGroups.Count);
			foreach (KeyValuePair<string, AnalysisGroupData> keyValuePair in this.itemGroups)
			{
				this.itemGroupsBySize.Add(keyValuePair.Value);
			}
			this.itemGroupsBySize.Sort(new Comparison<AnalysisGroupData>(AnalysisFolderItems.CompareGroupsBySize));
			foreach (KeyValuePair<string, int> keyValuePair2 in this.clientsByString)
			{
				this.clientsById[keyValuePair2.Value] = keyValuePair2.Key;
			}
			ExTraceGlobals.SessionTracer.TraceDebug<int, int, int>((long)this.session.GetHashCode(), "AnalysisFolderItems ended with {0} item, {1} groups, {2} client strings.", this.totalCount, this.itemGroups.Count, this.clientsByString.Count);
			this.processEndTime = ExDateTime.Now;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x00109F44 File Offset: 0x00108144
		private static int CompareGroupsBySize(AnalysisGroupData x, AnalysisGroupData y)
		{
			return -x.GroupSize.CompareTo(y.GroupSize);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00109F68 File Offset: 0x00108168
		private void ProcessOneFolder(StoreObjectId folderId)
		{
			using (Folder folder = Folder.Bind(this.session, folderId))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string, string>((long)this.session.GetHashCode(), "AnalysisFolderItems on folder {0}, id {1} starting.", folder.DisplayName ?? string.Empty, folderId.ToString());
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, AnalysisFolderItems.querySort, AnalysisFolderItems.queryProperties))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(AnalysisFolderItems.queryPageSize);
						if (rows == null || rows.Length <= 0)
						{
							break;
						}
						for (int i = 0; i < rows.Length; i++)
						{
							this.ProcessOneItem(new AnalysisItemsQueryData(rows[i]));
						}
					}
				}
			}
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0010A02C File Offset: 0x0010822C
		private void ProcessOneItem(AnalysisItemsQueryData item)
		{
			this.totalCount++;
			this.totalSize += (ulong)((long)item.Size);
			AnalysisGroupData analysisGroupData;
			if (!this.itemGroups.TryGetValue(item.Key.ToString(), out analysisGroupData))
			{
				analysisGroupData = new AnalysisGroupData(this, item.Key);
				this.itemGroups[item.Key.ToString()] = analysisGroupData;
			}
			analysisGroupData.AddOneItem(item);
		}

		// Token: 0x0400233A RID: 9018
		private static int queryPageSize = 500;

		// Token: 0x0400233B RID: 9019
		private static SortBy[] querySort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
		};

		// Token: 0x0400233C RID: 9020
		private static PropertyDefinition[] queryProperties = new PropertyDefinition[]
		{
			ItemSchema.NormalizedSubject,
			ItemSchema.ReceivedTime,
			InternalSchema.CleanGlobalObjectId,
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			ItemSchema.Size,
			StoreObjectSchema.LastModifiedTime,
			InternalSchema.ClientInfoString,
			InternalSchema.ClientProcessName,
			InternalSchema.ClientMachineName
		};

		// Token: 0x0400233D RID: 9021
		private MailboxSession session;

		// Token: 0x0400233E RID: 9022
		private List<StoreObjectId> folders;

		// Token: 0x0400233F RID: 9023
		private int totalCount;

		// Token: 0x04002340 RID: 9024
		private ulong totalSize;

		// Token: 0x04002341 RID: 9025
		private ExDateTime processStartTime;

		// Token: 0x04002342 RID: 9026
		private ExDateTime processEndTime;

		// Token: 0x04002343 RID: 9027
		private Dictionary<string, AnalysisGroupData> itemGroups;

		// Token: 0x04002344 RID: 9028
		private List<AnalysisGroupData> itemGroupsBySize;

		// Token: 0x04002345 RID: 9029
		private Dictionary<string, int> clientsByString;

		// Token: 0x04002346 RID: 9030
		private Dictionary<int, string> clientsById;

		// Token: 0x02000612 RID: 1554
		public enum QueryIndex
		{
			// Token: 0x04002348 RID: 9032
			NormalizedSubject,
			// Token: 0x04002349 RID: 9033
			ReceivedTime,
			// Token: 0x0400234A RID: 9034
			CleanGlobalObjectId,
			// Token: 0x0400234B RID: 9035
			ItemClass,
			// Token: 0x0400234C RID: 9036
			Id,
			// Token: 0x0400234D RID: 9037
			Size,
			// Token: 0x0400234E RID: 9038
			LastModifiedTime,
			// Token: 0x0400234F RID: 9039
			ClientInfoString,
			// Token: 0x04002350 RID: 9040
			ClientProcessName,
			// Token: 0x04002351 RID: 9041
			ClientMachineName
		}
	}
}
