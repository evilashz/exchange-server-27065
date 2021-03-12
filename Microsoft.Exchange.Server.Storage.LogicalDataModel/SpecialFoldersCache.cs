using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000D3 RID: 211
	public class SpecialFoldersCache
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0005DB0C File Offset: 0x0005BD0C
		internal static void Initialize()
		{
			if (SpecialFoldersCache.cacheMailboxDataSlot == -1)
			{
				SpecialFoldersCache.cacheMailboxDataSlot = MailboxState.AllocateComponentDataSlot(true);
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0005DB24 File Offset: 0x0005BD24
		public static ExchangeId[] GetSpecialFolders(Context context, Mailbox mailbox)
		{
			ExchangeId[] array = (ExchangeId[])mailbox.SharedState.GetComponentData(SpecialFoldersCache.cacheMailboxDataSlot);
			if (array != null)
			{
				return array;
			}
			bool flag;
			array = SpecialFoldersCache.BuildSpecialFoldersListFromFolderTable(context, mailbox, out flag);
			mailbox.SharedState.SetComponentData(SpecialFoldersCache.cacheMailboxDataSlot, array);
			return array;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0005DB68 File Offset: 0x0005BD68
		public static void Reset(Context context, Mailbox mailbox)
		{
			mailbox.SharedState.SetComponentData(SpecialFoldersCache.cacheMailboxDataSlot, null);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0005DB7C File Offset: 0x0005BD7C
		[Conditional("DEBUG")]
		internal static void AssertSpecialFoldersValueIsValid(Context context, Mailbox mailbox, Folder parentFolder, SpecialFolders specialFolder)
		{
			if (specialFolder == SpecialFolders.Regular)
			{
				return;
			}
			SpecialFolders[] array;
			SpecialFolders[] array2;
			SpecialFoldersCache.GetSpecialFolderTypesForMailbox(mailbox, out array, out array2);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0005DB9C File Offset: 0x0005BD9C
		internal static ExchangeId[] BuildSpecialFoldersListFromFolderTable(Context context, Mailbox mailbox, out bool slowPathExecuted)
		{
			FolderTable folderTable = DatabaseSchema.FolderTable(mailbox.Database);
			SpecialFolders[] collection;
			SpecialFolders[] source;
			SpecialFoldersCache.GetSpecialFolderTypesForMailbox(mailbox, out collection, out source);
			List<SpecialFolders> list = new List<SpecialFolders>(collection);
			ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
			Column lhs = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.MailboxNum);
			SearchCriteria restriction = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(folderTable.SpecialFolderNumber, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(0)),
				Factory.CreateSearchCriteriaOr(new SearchCriteria[]
				{
					Factory.CreateSearchCriteriaCompare(lhs, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxNumber)),
					Factory.CreateSearchCriteriaCompare(lhs, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(-1))
				})
			});
			Column[] columnsToFetch = new Column[]
			{
				folderTable.FolderId,
				folderTable.SpecialFolderNumber
			};
			ExchangeId[] array = new ExchangeId[22];
			Queue<ExchangeId> queue = new Queue<ExchangeId>(10);
			queue.Enqueue(ExchangeId.Zero);
			while (queue.Count != 0)
			{
				ExchangeId exchangeId = queue.Dequeue();
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber,
					exchangeId.To26ByteArray()
				});
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.FolderByParentIndex, columnsToFetch, restriction, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							short @int = reader.GetInt16(folderTable.SpecialFolderNumber);
							if (@int < 22)
							{
								SpecialFolders specialFolders = (SpecialFolders)@int;
								ExchangeId exchangeId2 = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(folderTable.FolderId));
								array[(int)@int] = exchangeId2;
								list.Remove(specialFolders);
								if (source.Contains(specialFolders))
								{
									queue.Enqueue(exchangeId2);
								}
							}
						}
					}
				}
			}
			if (list.Count == 0)
			{
				for (int i = 1; i < 22; i++)
				{
					if (array[i].IsNull)
					{
						array[i] = ExchangeId.Zero;
					}
				}
				slowPathExecuted = false;
				return array;
			}
			slowPathExecuted = true;
			return SpecialFoldersCache.SlowBuildSpecialFoldersListFromFolderTable(context, mailbox);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0005DDF8 File Offset: 0x0005BFF8
		internal static ExchangeId[] SlowBuildSpecialFoldersListFromFolderTable(Context context, Mailbox mailbox)
		{
			FolderTable folderTable = DatabaseSchema.FolderTable(mailbox.Database);
			ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber
			});
			Column lhs = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.MailboxNum);
			SearchCriteria restriction = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
			{
				Factory.CreateSearchCriteriaCompare(folderTable.SpecialFolderNumber, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(0)),
				Factory.CreateSearchCriteriaOr(new SearchCriteria[]
				{
					Factory.CreateSearchCriteriaCompare(lhs, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxNumber)),
					Factory.CreateSearchCriteriaCompare(lhs, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(-1))
				})
			});
			Column[] columnsToFetch = new Column[]
			{
				folderTable.FolderId,
				folderTable.SpecialFolderNumber
			};
			ExchangeId[] result;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.Table.PrimaryKeyIndex, columnsToFetch, restriction, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				ExchangeId[] array = new ExchangeId[22];
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						short @int = reader.GetInt16(folderTable.SpecialFolderNumber);
						byte[] binary = reader.GetBinary(folderTable.FolderId);
						if (@int < 22)
						{
							array[(int)@int] = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, binary);
						}
					}
				}
				for (int i = 1; i < 22; i++)
				{
					if (array[i].IsNull)
					{
						array[i] = ExchangeId.Zero;
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0005DFD8 File Offset: 0x0005C1D8
		private static void GetSpecialFolderTypesForMailbox(Mailbox mailbox, out SpecialFolders[] expectedSpecialFolders, out SpecialFolders[] expectedSpecialFolderParents)
		{
			if (mailbox.SharedState.MailboxType == MailboxInfo.MailboxType.Private)
			{
				expectedSpecialFolders = SpecialFoldersCache.privateMailboxExpectedFolders;
				expectedSpecialFolderParents = SpecialFoldersCache.privateMailboxExpectedParentFolders;
				return;
			}
			expectedSpecialFolders = SpecialFoldersCache.publicFolderMailboxExpectedFolders;
			expectedSpecialFolderParents = SpecialFoldersCache.publicFolderMailboxExpectedParentFolders;
		}

		// Token: 0x04000561 RID: 1377
		private static int cacheMailboxDataSlot = -1;

		// Token: 0x04000562 RID: 1378
		private static SpecialFolders[] privateMailboxExpectedFolders = new SpecialFolders[]
		{
			SpecialFolders.MailboxRoot,
			SpecialFolders.DeferredAction,
			SpecialFolders.SpoolerQueue,
			SpecialFolders.Shortcuts,
			SpecialFolders.Finder,
			SpecialFolders.Views,
			SpecialFolders.CommonViews,
			SpecialFolders.Schedule,
			SpecialFolders.TopofInformationStore,
			SpecialFolders.SentItems,
			SpecialFolders.DeletedItems,
			SpecialFolders.Outbox,
			SpecialFolders.Inbox,
			SpecialFolders.Conversations
		};

		// Token: 0x04000563 RID: 1379
		private static SpecialFolders[] privateMailboxExpectedParentFolders = new SpecialFolders[]
		{
			SpecialFolders.MailboxRoot,
			SpecialFolders.TopofInformationStore
		};

		// Token: 0x04000564 RID: 1380
		private static SpecialFolders[] publicFolderMailboxExpectedFolders = new SpecialFolders[]
		{
			SpecialFolders.MailboxRoot,
			SpecialFolders.DeferredAction,
			SpecialFolders.SpoolerQueue,
			SpecialFolders.TopofInformationStore,
			SpecialFolders.DeletedItems,
			SpecialFolders.Finder,
			SpecialFolders.SentItems,
			SpecialFolders.Outbox
		};

		// Token: 0x04000565 RID: 1381
		private static SpecialFolders[] publicFolderMailboxExpectedParentFolders = new SpecialFolders[]
		{
			SpecialFolders.MailboxRoot,
			SpecialFolders.SpoolerQueue
		};

		// Token: 0x04000566 RID: 1382
		private static SpecialFolders[] optionalFolders = new SpecialFolders[]
		{
			SpecialFolders.MaterializedRestrictionRoot
		};
	}
}
