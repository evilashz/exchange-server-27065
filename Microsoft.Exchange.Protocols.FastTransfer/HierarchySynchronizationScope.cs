using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200001B RID: 27
	internal class HierarchySynchronizationScope : IHierarchySynchronizationScope
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000081B0 File Offset: 0x000063B0
		public HierarchySynchronizationScope(MapiFolder folder)
		{
			this.folder = folder;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000081BF File Offset: 0x000063BF
		public MapiLogon Logon
		{
			get
			{
				return this.folder.Logon;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000081CC File Offset: 0x000063CC
		public MapiFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000081D4 File Offset: 0x000063D4
		public ExchangeId GetExchangeId(long shortTermId)
		{
			return ExchangeId.CreateFromInt64(this.Logon.StoreMailbox.CurrentOperationContext, this.Logon.StoreMailbox.ReplidGuidMap, shortTermId);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000081FC File Offset: 0x000063FC
		public ReplId GuidToReplid(Guid guid)
		{
			return new ReplId(this.Logon.StoreMailbox.ReplidGuidMap.GetReplidFromGuid(this.Logon.StoreMailbox.CurrentOperationContext, guid));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008229 File Offset: 0x00006429
		public Guid ReplidToGuid(ReplId replid)
		{
			return this.Logon.StoreMailbox.ReplidGuidMap.GetGuidFromReplid(this.Logon.StoreMailbox.CurrentOperationContext, replid.Value);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008257 File Offset: 0x00006457
		public IdSet GetServerCnsetSeen(MapiContext operationContext)
		{
			return this.Logon.StoreMailbox.GetFolderCnsetIn(operationContext);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000826C File Offset: 0x0000646C
		public void GetChangedAndDeletedFolders(MapiContext context, SyncFlag syncFlags, IdSet cnsetSeen, IdSet idsetGiven, out IList<FolderChangeEntry> changedFolders, out IdSet idsetNewDeletes)
		{
			ReplidGuidMap replidGuidMap = this.folder.Logon.StoreMailbox.ReplidGuidMap;
			bool flag;
			Restriction restriction = ContentSynchronizationScopeBase.CreateCnsetSeenRestriction(context, replidGuidMap, PropTag.Folder.ChangeNumberBin, cnsetSeen, false, out flag);
			FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(this.folder.Logon.StoreMailbox.Database);
			int mailboxPartitionNumber = this.folder.Logon.StoreMailbox.MailboxPartitionNumber;
			IList<KeyRange> keyRanges;
			if (restriction != null)
			{
				SearchCriteria searchCriteria = restriction.ToSearchCriteria(this.folder.Logon.StoreMailbox.Database, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);
				keyRanges = QueryPlanner.BuildKeyRangesFromOrCriteria(new object[]
				{
					mailboxPartitionNumber
				}, folderTable.FolderChangeNumberIndex, ref searchCriteria, false, context.Culture);
			}
			else
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailboxPartitionNumber
				});
				keyRanges = new KeyRange[]
				{
					new KeyRange(startStopKey, startStopKey)
				};
			}
			List<FolderChangeEntry> list = new List<FolderChangeEntry>(100);
			List<int> list2 = new List<int>(100);
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, this.folder.Logon.StoreMailbox, this.folder.Fid.ToExchangeShortId(), FolderInformationType.Basic);
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.FolderChangeNumberIndex, new Column[]
			{
				folderTable.FolderId,
				folderTable.LcnCurrent
			}, null, null, 0, 0, keyRanges, false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						ExchangeId id = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(folderTable.LcnCurrent));
						if (!flag || !cnsetSeen.Contains(id))
						{
							int num;
							IFolderInformation folderInformation = folderHierarchy.Find(context, ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(folderTable.FolderId)).ToExchangeShortId(), out num);
							if (folderInformation != null && num != 0 && !folderInformation.IsSearchFolder && (!folderInformation.IsInternalAccess || context.HasInternalAccessRights))
							{
								list.Add(new FolderChangeEntry
								{
									FolderId = folderInformation.Fid,
									Cn = id.ToLong()
								});
								if ((ushort)(syncFlags & SyncFlag.CatchUp) == 0)
								{
									list2.Add(num);
								}
							}
						}
					}
				}
			}
			changedFolders = list;
			if ((ushort)(syncFlags & SyncFlag.CatchUp) == 0)
			{
				FolderChangeEntry[] array = list.ToArray();
				int[] keys = list2.ToArray();
				Array.Sort<int, FolderChangeEntry>(keys, array);
				changedFolders = array;
			}
			if ((ushort)(syncFlags & SyncFlag.NoDeletions) == 2 || (ushort)(syncFlags & SyncFlag.CatchUp) == 1024)
			{
				idsetNewDeletes = null;
				return;
			}
			if (idsetGiven.IsEmpty)
			{
				idsetNewDeletes = new IdSet();
				return;
			}
			idsetNewDeletes = IdSet.Subtract(idsetGiven, this.GetExistingFoldersSet(context));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008550 File Offset: 0x00006750
		private IdSet GetExistingFoldersSet(MapiContext context)
		{
			ExchangeId fid = this.folder.Fid;
			IdSet idSet = new IdSet();
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, this.folder.Logon.StoreMailbox, fid.ToExchangeShortId(), FolderInformationType.Basic);
			if (folderHierarchy != null && folderHierarchy.HierarchyRoots != null && folderHierarchy.HierarchyRoots.Count != 0)
			{
				IReplidGuidMap cacheForMailbox = ReplidGuidMap.GetCacheForMailbox(context, context.LockedMailboxState);
				foreach (ExchangeShortId exchangeShortId in folderHierarchy.HierarchyRoots[0].AllDescendantFolderIds())
				{
					Guid guid = cacheForMailbox.InternalGetGuidFromReplid(context, exchangeShortId.Replid);
					idSet.Insert(guid, exchangeShortId.Counter);
				}
			}
			return idSet;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008620 File Offset: 0x00006820
		public ExchangeId GetRootFid()
		{
			return this.folder.Fid;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000862D File Offset: 0x0000682D
		public MapiFolder OpenFolder(ExchangeId fid)
		{
			return MapiFolder.OpenFolder(this.folder.CurrentOperationContext, this.Logon, fid);
		}

		// Token: 0x0400007E RID: 126
		private MapiFolder folder;

		// Token: 0x0200001C RID: 28
		private class SyncFolderInformation
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00008646 File Offset: 0x00006846
			// (set) Token: 0x06000117 RID: 279 RVA: 0x0000864E File Offset: 0x0000684E
			internal ExchangeId FolderId
			{
				get
				{
					return this.folderId;
				}
				set
				{
					this.folderId = value;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000118 RID: 280 RVA: 0x00008657 File Offset: 0x00006857
			// (set) Token: 0x06000119 RID: 281 RVA: 0x0000865F File Offset: 0x0000685F
			internal ExchangeId ParentFolderId
			{
				get
				{
					return this.parentFolderId;
				}
				set
				{
					this.parentFolderId = value;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00008668 File Offset: 0x00006868
			// (set) Token: 0x0600011B RID: 283 RVA: 0x00008670 File Offset: 0x00006870
			internal ExchangeId Cn
			{
				get
				{
					return this.cn;
				}
				set
				{
					this.cn = value;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00008679 File Offset: 0x00006879
			internal List<HierarchySynchronizationScope.SyncFolderInformation> Children
			{
				get
				{
					return this.children;
				}
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00008681 File Offset: 0x00006881
			public void LinkToParent(HierarchySynchronizationScope.SyncFolderInformation parent)
			{
				parent.Children.Add(this);
			}

			// Token: 0x0400007F RID: 127
			private readonly List<HierarchySynchronizationScope.SyncFolderInformation> children = new List<HierarchySynchronizationScope.SyncFolderInformation>();

			// Token: 0x04000080 RID: 128
			private ExchangeId folderId;

			// Token: 0x04000081 RID: 129
			private ExchangeId parentFolderId;

			// Token: 0x04000082 RID: 130
			private ExchangeId cn;
		}
	}
}
