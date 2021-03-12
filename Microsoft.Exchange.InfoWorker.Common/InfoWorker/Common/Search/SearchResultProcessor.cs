using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000240 RID: 576
	internal class SearchResultProcessor
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x0004BCDC File Offset: 0x00049EDC
		public SearchResultProcessor(MailboxSession source, MailboxSession target, StoreId targetRootId, string[] targetSubfolders, List<SearchMailboxAction> actions, ref HashSet<StoreObjectId> unsearchableItemSet, SearchCommunicator communicator, SearchMailboxWorker worker)
		{
			if (worker == null || source == null || communicator == null)
			{
				return;
			}
			this.searchWorker = worker;
			this.searchCommunicator = communicator;
			this.sourceMailbox = source;
			this.targetMailbox = target;
			this.searchActions = actions;
			this.unsearchableItemSet = unsearchableItemSet;
			this.sourceThrottler = new ResponseThrottler(this.searchCommunicator.AbortEvent);
			this.targetThrottler = new ResponseThrottler(this.searchCommunicator.AbortEvent);
			this.InitializeFolderMap(targetRootId, targetSubfolders);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0004BD7C File Offset: 0x00049F7C
		internal void Process(Folder[] folders)
		{
			double mailboxProgress = this.searchWorker.MailboxProgress;
			int totalItemCount = folders.Aggregate(0, (int s, Folder f) => s + f.ItemCount) + ((this.unsearchableItemSet == null) ? 0 : this.unsearchableItemSet.Count);
			int num = 0;
			int num2 = 0;
			while (num2 < folders.Length && !this.searchCommunicator.IsAborted)
			{
				double maxProgress = this.CalcProgress(num + folders[num2].ItemCount, totalItemCount, mailboxProgress, 100.0);
				this.ProcessFolderItems(folders[num2], this.sourceMailbox, this.targetMailbox, maxProgress);
				num += folders[num2].ItemCount;
				num2++;
			}
			this.AfterEnumerate();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004BE33 File Offset: 0x0004A033
		internal void ReportActionException(Exception e)
		{
			this.errorDuringAction = true;
			this.searchWorker.ReportActionException(e);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004BE48 File Offset: 0x0004A048
		internal bool IsAborted()
		{
			return this.searchCommunicator.IsAborted;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004BE58 File Offset: 0x0004A058
		internal void ReportLogs(StreamLogItem.LogItem logItem)
		{
			lock (this.searchCommunicator)
			{
				this.searchCommunicator.ReportLogs(logItem);
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004BEA0 File Offset: 0x0004A0A0
		internal string GetSourceUserName()
		{
			return this.searchWorker.SourceUser.Id.DistinguishedName;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004BEB7 File Offset: 0x0004A0B7
		internal void BackOffFromSourceStore()
		{
			if (this.sourceMailbox != null)
			{
				this.sourceThrottler.BackOffFromStore(this.sourceMailbox);
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0004BED2 File Offset: 0x0004A0D2
		internal void BackOffFromTargetStore()
		{
			if (this.targetMailbox != null)
			{
				this.targetThrottler.BackOffFromStore(this.targetMailbox);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0004BEED File Offset: 0x0004A0ED
		internal int WorkerId
		{
			get
			{
				return this.searchWorker.WorkerId;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004BEFC File Offset: 0x0004A0FC
		private void ProcessFolderItems(Folder folder, MailboxSession sourceMailbox, MailboxSession targetMailbox, double maxProgress)
		{
			double mailboxProgress = this.searchWorker.MailboxProgress;
			int resultItemsCount = this.searchWorker.SearchResult.ResultItemsCount;
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(StoreObjectSchema.ParentEntryId, SortOrder.Ascending)
			}, SearchResultProcessor.ItemPreloadProperties))
			{
				while (!this.searchCommunicator.IsAborted)
				{
					this.BackOffFromSourceStore();
					object[][] rows = queryResult.GetRows(this.batchedItemBuffer.Length);
					if (rows == null || rows.Length <= 0)
					{
						break;
					}
					for (int i = 0; i < rows.Length; i++)
					{
						this.ProcessSingleResult(rows[i]);
						this.searchWorker.SearchResult.ResultItemsCount++;
						StoreId storeId = (StoreId)rows[i][0];
						StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
						if (this.unsearchableItemSet != null && storeObjectId != null && this.unsearchableItemSet.Contains(storeObjectId))
						{
							this.unsearchableItemSet.Remove(storeObjectId);
						}
						if (SearchResultProcessor.PropertyExists(rows[i][1]))
						{
							this.searchWorker.SearchResult.ResultItemsSize += (int)rows[i][1];
						}
						else
						{
							SearchResultProcessor.Tracer.TraceDebug<StoreId>((long)this.GetHashCode(), "Unable to retrieve message size for message {0}", storeId);
						}
						int num = this.searchWorker.SearchResult.ResultItemsCount - resultItemsCount;
						double progress = this.CalcProgress(num, Math.Max(queryResult.EstimatedRowCount, num), mailboxProgress, maxProgress);
						this.UpdateProgress(progress, 10.0);
					}
				}
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0004C0B4 File Offset: 0x0004A2B4
		public void AfterEnumerate()
		{
			if (this.fetchedItemCount > 0)
			{
				this.ProcessCurrentBatch();
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0004C0C8 File Offset: 0x0004A2C8
		internal void CheckTargetMailboxAvailableSpace()
		{
			Unlimited<ByteQuantifiedSize> availableSpace = this.GetAvailableSpace();
			ByteQuantifiedSize value = this.CalculateBatchSize(this.batchedItemBuffer, this.fetchedItemCount);
			if (SearchResultProcessor.MinimumSpaceRequired > value)
			{
				value = SearchResultProcessor.MinimumSpaceRequired;
			}
			if (!availableSpace.IsUnlimited && availableSpace.Value < value)
			{
				throw new SearchMailboxException(Strings.TargetMailboxOutOfSpace);
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004C128 File Offset: 0x0004A328
		private void ProcessSingleResult(object[] result)
		{
			if (SearchResultProcessor.PropertyExists(result[0]) && SearchResultProcessor.PropertyExists(result[2]))
			{
				StoreId storeId = (StoreId)result[0];
				StoreId id = (StoreId)result[2];
				if (this.currentFolderId == null)
				{
					this.currentFolderId = id;
				}
				if (this.fetchedItemCount >= this.batchedItemBuffer.Length || !this.currentFolderId.Equals(id))
				{
					this.ProcessCurrentBatch();
					this.fetchedItemCount = 0;
					this.currentFolderId = id;
					if (this.searchCommunicator.IsAborted)
					{
						return;
					}
				}
				this.batchedItemBuffer[this.fetchedItemCount++] = result;
				return;
			}
			SearchResultProcessor.Tracer.TraceDebug((long)this.GetHashCode(), "Item is skipped because the itemId or ParentItemId is unavailable");
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004C1E0 File Offset: 0x0004A3E0
		public void InitializeFolderMap(StoreId targetRootId, string[] targetSubfolders)
		{
			StoreId defaultFolderId = this.sourceMailbox.GetDefaultFolderId(DefaultFolderType.Root);
			StoreId defaultFolderId2 = this.sourceMailbox.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot);
			FolderNode folderNode2;
			if (this.targetMailbox != null)
			{
				FolderNode folderNode = new FolderNode(null, targetRootId, null, null);
				folderNode2 = folderNode;
				foreach (string displayName in targetSubfolders)
				{
					FolderNode folderNode3 = new FolderNode(null, null, displayName, folderNode2);
					folderNode2 = folderNode3;
				}
			}
			else
			{
				folderNode2 = new FolderNode(defaultFolderId, null, Strings.PrimaryMailbox, null);
			}
			FolderNode folderNode4 = folderNode2;
			folderNode4.SourceFolderId = defaultFolderId;
			this.folderNodeMap = new Dictionary<StoreId, FolderNode>();
			this.folderNodeMap.Add(folderNode4.SourceFolderId, folderNode4);
			string displayName2 = null;
			if (this.searchWorker.SearchDumpster || this.searchWorker.IncludeUnsearchableItems)
			{
				string[] uniqueFolderName = this.GetUniqueFolderName(this.sourceMailbox, defaultFolderId, new string[]
				{
					Strings.RecoverableItems,
					Strings.Unsearchable
				});
				displayName2 = uniqueFolderName[0];
				string text = uniqueFolderName[1];
			}
			if (this.searchWorker.SearchDumpster && defaultFolderId2 != null)
			{
				this.folderNodeMap.Add(defaultFolderId2, new FolderNode(defaultFolderId2, null, displayName2, folderNode4));
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004C35C File Offset: 0x0004A55C
		private string[] GetUniqueFolderName(MailboxSession mailboxStore, StoreId folderId, string[] suggestedNames)
		{
			List<string> subFolderNames = new List<string>();
			using (Folder folder = Folder.Bind(mailboxStore, folderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					ElcMailboxHelper.ForeachQueryResult(queryResult, delegate(object[] rowProps, ref bool breakLoop)
					{
						if (SearchResultProcessor.PropertyExists(rowProps[0]))
						{
							subFolderNames.Add((string)rowProps[0]);
						}
					});
				}
			}
			string[] array = new string[suggestedNames.Length];
			for (int i = 0; i < suggestedNames.Length; i++)
			{
				string folderName = suggestedNames[i];
				List<string> list = (from x in subFolderNames
				where x.StartsWith(folderName, StringComparison.OrdinalIgnoreCase)
				select x).ToList<string>();
				for (int j = 0; j < list.Count + 1; j++)
				{
					if (list.Find((string x) => x.Equals(folderName, StringComparison.OrdinalIgnoreCase)) == null)
					{
						break;
					}
					folderName = string.Format("{0}-{1}", suggestedNames[i], j + 1);
				}
				array[i] = folderName;
			}
			return array;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004C4AC File Offset: 0x0004A6AC
		private void ProcessCurrentBatch()
		{
			this.UpdateFolderMap(this.currentFolderId);
			this.errorDuringAction = false;
			foreach (SearchMailboxAction searchMailboxAction in this.searchActions)
			{
				if (!this.errorDuringAction)
				{
					searchMailboxAction.PerformBatchOperation(this.batchedItemBuffer, this.fetchedItemCount, this.currentFolderId, this.sourceMailbox, this.targetMailbox, this.folderNodeMap, this);
				}
			}
			this.errorDuringAction = false;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004C548 File Offset: 0x0004A748
		private void UpdateFolderMap(StoreId sourceFolderId)
		{
			Stack<FolderNode> stack = new Stack<FolderNode>();
			StoreId storeId = sourceFolderId;
			while (!this.folderNodeMap.ContainsKey(storeId))
			{
				using (Folder folder = Folder.Bind(this.sourceMailbox, storeId, new PropertyDefinition[]
				{
					StoreObjectSchema.ParentItemId,
					FolderSchema.DisplayName,
					StoreObjectSchema.IsSoftDeleted
				}))
				{
					bool valueOrDefault = folder.GetValueOrDefault<bool>(StoreObjectSchema.IsSoftDeleted);
					stack.Push(new FolderNode(storeId, null, folder.DisplayName, valueOrDefault, null));
					if (storeId.Equals(folder.ParentId))
					{
						throw new CorruptDataException(Strings.CorruptedFolder(this.sourceMailbox.MailboxOwner.MailboxInfo.DisplayName));
					}
					storeId = folder.ParentId;
				}
			}
			FolderNode parent = this.folderNodeMap[storeId];
			while (stack.Count > 0)
			{
				FolderNode folderNode = stack.Pop();
				folderNode.Parent = parent;
				this.folderNodeMap.Add(folderNode.SourceFolderId, folderNode);
				parent = folderNode;
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004C65C File Offset: 0x0004A85C
		private static bool PropertyExists(object property)
		{
			return property != null && !(property is PropertyError);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004C670 File Offset: 0x0004A870
		private double CalcProgress(int copiedItemCount, int totalItemCount, double startProgress, double maxProgress)
		{
			if (totalItemCount == 0)
			{
				return maxProgress;
			}
			double val = startProgress + (maxProgress - startProgress) * (double)copiedItemCount / (double)totalItemCount;
			return Math.Min(Math.Min(val, startProgress), maxProgress);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004C69E File Offset: 0x0004A89E
		private void UpdateProgress(double progress, double minstep)
		{
			if (progress >= this.searchWorker.MailboxProgress + minstep)
			{
				this.UpdateProgress(progress);
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		private void UpdateProgress(double progress)
		{
			this.searchWorker.MailboxProgress = progress;
			lock (this.searchCommunicator)
			{
				this.searchCommunicator.UpdateProgress(this.searchWorker);
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004C710 File Offset: 0x0004A910
		private Unlimited<ByteQuantifiedSize> GetAvailableSpace()
		{
			if (this.targetMailbox == null)
			{
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
			this.targetMailbox.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.QuotaUsedExtended
			});
			ulong num = (ulong)((long)this.targetMailbox.Mailbox.TryGetProperty(MailboxSchema.QuotaUsedExtended));
			Unlimited<ByteQuantifiedSize> result;
			if (this.searchWorker.TargetMailboxQuota.IsUnlimited)
			{
				result = this.searchWorker.TargetMailboxQuota;
			}
			else if (this.searchWorker.TargetMailboxQuota.Value < new ByteQuantifiedSize(num))
			{
				result = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.Zero);
			}
			else
			{
				result = this.searchWorker.TargetMailboxQuota - num;
			}
			return result;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004C7D0 File Offset: 0x0004A9D0
		private ByteQuantifiedSize CalculateBatchSize(object[][] batchedItemBuffer, int fetchedItemCount)
		{
			ByteQuantifiedSize byteQuantifiedSize = ByteQuantifiedSize.Zero;
			for (int i = 0; i < fetchedItemCount; i++)
			{
				if (SearchResultProcessor.PropertyExists(batchedItemBuffer[i][1]))
				{
					byteQuantifiedSize += (int)batchedItemBuffer[i][1];
				}
			}
			return byteQuantifiedSize;
		}

		// Token: 0x04000B40 RID: 2880
		private static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000B41 RID: 2881
		private object[][] batchedItemBuffer = new object[ResponseThrottler.MaxBulkSize][];

		// Token: 0x04000B42 RID: 2882
		private static readonly ByteQuantifiedSize MinimumSpaceRequired = new ByteQuantifiedSize(1048576UL);

		// Token: 0x04000B43 RID: 2883
		private StoreId currentFolderId;

		// Token: 0x04000B44 RID: 2884
		private int fetchedItemCount;

		// Token: 0x04000B45 RID: 2885
		private Dictionary<StoreId, FolderNode> folderNodeMap;

		// Token: 0x04000B46 RID: 2886
		private MailboxSession sourceMailbox;

		// Token: 0x04000B47 RID: 2887
		private MailboxSession targetMailbox;

		// Token: 0x04000B48 RID: 2888
		private ResponseThrottler sourceThrottler;

		// Token: 0x04000B49 RID: 2889
		private ResponseThrottler targetThrottler;

		// Token: 0x04000B4A RID: 2890
		private SearchCommunicator searchCommunicator;

		// Token: 0x04000B4B RID: 2891
		private SearchMailboxWorker searchWorker;

		// Token: 0x04000B4C RID: 2892
		private bool errorDuringAction;

		// Token: 0x04000B4D RID: 2893
		private List<SearchMailboxAction> searchActions;

		// Token: 0x04000B4E RID: 2894
		private HashSet<StoreObjectId> unsearchableItemSet;

		// Token: 0x04000B4F RID: 2895
		private static readonly PropertyDefinition[] ItemPreloadProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Subject,
			MessageItemSchema.IsRead,
			ItemSchema.SentTime,
			ItemSchema.ReceivedTime,
			ItemSchema.Sender,
			MessageItemSchema.SenderSmtpAddress
		};

		// Token: 0x02000241 RID: 577
		internal enum ItemPropertyIndex
		{
			// Token: 0x04000B52 RID: 2898
			Id,
			// Token: 0x04000B53 RID: 2899
			Size,
			// Token: 0x04000B54 RID: 2900
			ParentItemId,
			// Token: 0x04000B55 RID: 2901
			Subject,
			// Token: 0x04000B56 RID: 2902
			IsRead,
			// Token: 0x04000B57 RID: 2903
			SentTime,
			// Token: 0x04000B58 RID: 2904
			ReceivedTime,
			// Token: 0x04000B59 RID: 2905
			Sender,
			// Token: 0x04000B5A RID: 2906
			SenderSmtpAddress
		}
	}
}
