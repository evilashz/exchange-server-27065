using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200002E RID: 46
	public class PhysicalIndexCache
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00014443 File Offset: 0x00012643
		private PhysicalIndexCache(StoreDatabase database)
		{
			this.cache = new Dictionary<int, PhysicalIndex>(255);
			this.lockName = new LockName<Guid>(database.MdbGuid, LockManager.LockLevel.PhysicalIndexCache);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0001446E File Offset: 0x0001266E
		public static void Initialize()
		{
			if (PhysicalIndexCache.physicalIndexCacheDataSlot == -1)
			{
				PhysicalIndexCache.physicalIndexCacheDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00014484 File Offset: 0x00012684
		public static void MountEventHandler(Context context)
		{
			PhysicalIndexCache physicalIndexCache = new PhysicalIndexCache(context.Database);
			context.Database.ComponentData[PhysicalIndexCache.physicalIndexCacheDataSlot] = physicalIndexCache;
			List<int> listOfPhysicalIndexesFromDatabase = PhysicalIndexCache.GetListOfPhysicalIndexesFromDatabase(context);
			for (int i = 0; i < listOfPhysicalIndexesFromDatabase.Count; i++)
			{
				PhysicalIndex physicalIndex = PhysicalIndex.GetPhysicalIndex(context, listOfPhysicalIndexesFromDatabase[i]);
				physicalIndexCache.cache[listOfPhysicalIndexesFromDatabase[i]] = physicalIndex;
			}
			physicalIndexCache.CleanupUnusedPhysicalIndexesHelper(context);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000144F3 File Offset: 0x000126F3
		public static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[PhysicalIndexCache.physicalIndexCacheDataSlot] = null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00014508 File Offset: 0x00012708
		internal static PhysicalIndexCache GetPhysicalIndexCache(StoreDatabase database)
		{
			return database.ComponentData[PhysicalIndexCache.physicalIndexCacheDataSlot] as PhysicalIndexCache;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001452C File Offset: 0x0001272C
		internal static PhysicalIndex GetPhysicalIndex(Context context, int physicalIndexNumber)
		{
			PhysicalIndexCache physicalIndexCache = PhysicalIndexCache.GetPhysicalIndexCache(context.Database);
			return physicalIndexCache.GetPhysicalIndexHelper(context, physicalIndexNumber);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00014550 File Offset: 0x00012750
		internal static PhysicalIndex GetPhysicalIndex(Context context, int keyColumnCount, short identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings, bool permitReverseOrder)
		{
			PhysicalIndexCache physicalIndexCache = PhysicalIndexCache.GetPhysicalIndexCache(context.Database);
			return physicalIndexCache.GetPhysicalIndexHelper(context, keyColumnCount, identityColumnIndex, columnTypes, columnMaxLengths, columnFixedLengths, columnAscendings, permitReverseOrder);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001457C File Offset: 0x0001277C
		internal static PhysicalIndex FindExistingPhysicalIndex(Context context, int keyColumnCount, int lcid, short identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings)
		{
			PhysicalIndexCache physicalIndexCache = PhysicalIndexCache.GetPhysicalIndexCache(context.Database);
			return physicalIndexCache.FindExistingPhysicalIndexHelper(context, keyColumnCount, lcid, identityColumnIndex, columnTypes, columnMaxLengths, columnFixedLengths, columnAscendings);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000145A8 File Offset: 0x000127A8
		internal static void CleanupUnusedPhysicalIndexes(Context context)
		{
			PhysicalIndexCache physicalIndexCache = PhysicalIndexCache.GetPhysicalIndexCache(context.Database);
			physicalIndexCache.CleanupUnusedPhysicalIndexesHelper(context);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000145C8 File Offset: 0x000127C8
		internal static void DeletePhysicalIndex(Context context, int indexNum)
		{
			PhysicalIndexCache physicalIndexCache = PhysicalIndexCache.GetPhysicalIndexCache(context.Database);
			physicalIndexCache.DeletePhysicalIndexHelper(context, indexNum);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000145EC File Offset: 0x000127EC
		private static List<int> GetListOfPhysicalIndexesFromDatabase(Context context)
		{
			List<int> list = new List<int>(255);
			PseudoIndexDefinitionTable pseudoIndexDefinitionTable = DatabaseSchema.PseudoIndexDefinitionTable(context.Database);
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, pseudoIndexDefinitionTable.Table, pseudoIndexDefinitionTable.Table.PrimaryKeyIndex, new Column[]
			{
				pseudoIndexDefinitionTable.PhysicalIndexNumber
			}, null, null, 0, 0, KeyRange.AllRows, false, false))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						list.Add(reader.GetInt32(pseudoIndexDefinitionTable.PhysicalIndexNumber));
					}
				}
			}
			return list;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000146A8 File Offset: 0x000128A8
		private PhysicalIndex GetPhysicalIndexHelper(Context context, int physicalIndexNumber)
		{
			PhysicalIndex result;
			using (LockManager.Lock(this.lockName, LockManager.LockType.PhysicalIndexCache, context.Diagnostics))
			{
				this.cache.TryGetValue(physicalIndexNumber, out result);
			}
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000146FC File Offset: 0x000128FC
		private PhysicalIndex GetPhysicalIndexHelper(Context context, int keyColumnCount, short identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings, bool permitReverseOrder)
		{
			PhysicalIndex physicalIndex = null;
			bool flag = context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet;
			Connection connection = context.GetConnection();
			bool databaseTransactionStarted = context.DatabaseTransactionStarted;
			long num = databaseTransactionStarted ? connection.TransactionTimeStamp : long.MaxValue;
			using (LockManager.Lock(this.lockName, LockManager.LockType.PhysicalIndexCache, context.Diagnostics))
			{
				foreach (PhysicalIndex physicalIndex2 in this.cache.Values)
				{
					if (physicalIndex2.IndexIsVisibleForConnection(connection) && (!flag || !databaseTransactionStarted || num > physicalIndex2.CreationTimeStamp) && physicalIndex2.IndexMatch(CultureHelper.GetLcidFromCulture(context.Culture), keyColumnCount, (int)identityColumnIndex, columnTypes, columnMaxLengths, columnFixedLengths, columnAscendings, permitReverseOrder))
					{
						physicalIndex = physicalIndex2;
						break;
					}
				}
				if (physicalIndex == null)
				{
					bool flag2 = false;
					if (!flag || !databaseTransactionStarted)
					{
						context.PushConnection();
					}
					try
					{
						physicalIndex = PhysicalIndex.CreatePhysicalIndex(context, keyColumnCount, identityColumnIndex, columnTypes, columnMaxLengths, columnFixedLengths, columnAscendings);
						if (!flag || !databaseTransactionStarted)
						{
							context.Commit();
							flag2 = true;
							physicalIndex.CreationTimeStamp = context.GetConnection().TransactionTimeStamp;
							this.cache.Add(physicalIndex.PhysicalIndexNumber, physicalIndex);
						}
						else
						{
							physicalIndex.ConnectionLimitVisibility = connection;
							this.cache.Add(physicalIndex.PhysicalIndexNumber, physicalIndex);
							PhysicalIndexCache.PhysicalIndexTableCommitCallback stateObject = new PhysicalIndexCache.PhysicalIndexTableCommitCallback(this, physicalIndex);
							context.RegisterStateObject(stateObject);
						}
					}
					finally
					{
						if (!flag || !databaseTransactionStarted)
						{
							if (!flag2)
							{
								context.Abort();
							}
							context.PopConnection();
						}
					}
				}
			}
			return physicalIndex;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000148C8 File Offset: 0x00012AC8
		private PhysicalIndex FindExistingPhysicalIndexHelper(Context context, int keyColumnCount, int lcid, short identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings)
		{
			PhysicalIndex result = null;
			using (LockManager.Lock(this.lockName, LockManager.LockType.PhysicalIndexCache, context.Diagnostics))
			{
				foreach (PhysicalIndex physicalIndex in this.cache.Values)
				{
					if (physicalIndex.IndexIsVisibleForConnection(context.GetConnection()) && physicalIndex.IndexMatch(lcid, keyColumnCount, (int)identityColumnIndex, columnTypes, columnMaxLengths, columnFixedLengths, columnAscendings, true))
					{
						result = physicalIndex;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00014974 File Offset: 0x00012B74
		private void CleanupUnusedPhysicalIndexesHelper(Context context)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00014978 File Offset: 0x00012B78
		private void DeletePhysicalIndexHelper(Context context, int indexNum)
		{
			if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				this.cache.Remove(indexNum);
				PseudoIndexDefinitionTable pseudoIndexDefinitionTable = DatabaseSchema.PseudoIndexDefinitionTable(context.Database);
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					indexNum
				});
				using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, pseudoIndexDefinitionTable.Table, pseudoIndexDefinitionTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false), false))
				{
					int num = (int)deleteOperator.ExecuteScalar();
				}
				string tableName = PhysicalIndex.GetTableName(indexNum);
				Factory.DeleteTable(context, tableName);
				context.Database.PhysicalDatabase.RemoveTableMetadata(tableName);
			}
		}

		// Token: 0x0400015A RID: 346
		private const int AvgPhysicalIndexPerMDB = 255;

		// Token: 0x0400015B RID: 347
		private static int physicalIndexCacheDataSlot = -1;

		// Token: 0x0400015C RID: 348
		private readonly LockName<Guid> lockName;

		// Token: 0x0400015D RID: 349
		private Dictionary<int, PhysicalIndex> cache;

		// Token: 0x0200002F RID: 47
		private class PhysicalIndexTableCommitCallback : IStateObject
		{
			// Token: 0x0600026A RID: 618 RVA: 0x00014A58 File Offset: 0x00012C58
			public PhysicalIndexTableCommitCallback(PhysicalIndexCache state, PhysicalIndex indexDef)
			{
				this.state = state;
				this.indexDef = indexDef;
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00014A6E File Offset: 0x00012C6E
			void IStateObject.OnBeforeCommit(Context context)
			{
			}

			// Token: 0x0600026C RID: 620 RVA: 0x00014A70 File Offset: 0x00012C70
			void IStateObject.OnCommit(Context context)
			{
				this.OnEndTransaction(context, true);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00014A7A File Offset: 0x00012C7A
			void IStateObject.OnAbort(Context context)
			{
				this.OnEndTransaction(context, false);
			}

			// Token: 0x0600026E RID: 622 RVA: 0x00014A84 File Offset: 0x00012C84
			public void OnEndTransaction(Context context, bool committed)
			{
				using (LockManager.Lock(this.state.lockName, LockManager.LockType.PhysicalIndexCache, context.Diagnostics))
				{
					if (committed)
					{
						this.indexDef.CreationTimeStamp = context.GetConnection().TransactionTimeStamp;
						this.indexDef.ConnectionLimitVisibility = null;
					}
					else
					{
						this.state.cache.Remove(this.indexDef.PhysicalIndexNumber);
						context.Database.PhysicalDatabase.RemoveTableMetadata(this.indexDef.Table.Name);
					}
				}
			}

			// Token: 0x0400015E RID: 350
			private PhysicalIndexCache state;

			// Token: 0x0400015F RID: 351
			private PhysicalIndex indexDef;
		}
	}
}
