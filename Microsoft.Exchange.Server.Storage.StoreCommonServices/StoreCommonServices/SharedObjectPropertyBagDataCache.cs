using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E5 RID: 229
	public class SharedObjectPropertyBagDataCache : IComponentData
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0002A490 File Offset: 0x00028690
		internal SharedObjectPropertyBagDataCache(MailboxState mailboxState, int capacity, TimeSpan timeToLive)
		{
			this.mailboxState = mailboxState;
			this.dataCache = new Dictionary<ExchangeId, SharedObjectPropertyBagData>(capacity);
			this.evictionPolicy = new LRU2WithTimeToLiveExpirationPolicy<ExchangeId>(capacity, timeToLive, false);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002A4B9 File Offset: 0x000286B9
		public MailboxState MailboxState
		{
			get
			{
				return this.mailboxState;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002A4E5 File Offset: 0x000286E5
		internal static void Initialize()
		{
			if (SharedObjectPropertyBagDataCache.mailboxStateSlot == -1)
			{
				SharedObjectPropertyBagDataCache.mailboxStateSlot = MailboxState.AllocateComponentDataSlot(false);
			}
			Mailbox.RegisterOnDisconnectAction(delegate(Context context, Mailbox mailbox)
			{
				SharedObjectPropertyBagDataCache cacheForMailbox = SharedObjectPropertyBagDataCache.GetCacheForMailbox(mailbox.SharedState);
				cacheForMailbox.OnMailboxDisconnect(context, mailbox);
			});
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002A51C File Offset: 0x0002871C
		internal static SharedObjectPropertyBagDataCache GetCacheForMailboxNoCreate(MailboxState mailboxState)
		{
			return (SharedObjectPropertyBagDataCache)mailboxState.GetComponentData(SharedObjectPropertyBagDataCache.mailboxStateSlot);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002A530 File Offset: 0x00028730
		internal static SharedObjectPropertyBagDataCache GetCacheForMailbox(MailboxState mailboxState)
		{
			SharedObjectPropertyBagDataCache sharedObjectPropertyBagDataCache = SharedObjectPropertyBagDataCache.GetCacheForMailboxNoCreate(mailboxState);
			if (sharedObjectPropertyBagDataCache == null)
			{
				int capacity = (mailboxState.MailboxType == MailboxInfo.MailboxType.Private) ? ConfigurationSchema.DefaultMailboxSharedObjectPropertyBagDataCacheSize.Value : ConfigurationSchema.PublicFolderMailboxSharedObjectPropertyBagDataCacheSize.Value;
				sharedObjectPropertyBagDataCache = new SharedObjectPropertyBagDataCache(mailboxState, capacity, ConfigurationSchema.SharedObjectPropertyBagDataCacheTimeToLive.Value);
				try
				{
					mailboxState.AddReference();
					SharedObjectPropertyBagDataCache sharedObjectPropertyBagDataCache2 = (SharedObjectPropertyBagDataCache)mailboxState.CompareExchangeComponentData(SharedObjectPropertyBagDataCache.mailboxStateSlot, null, sharedObjectPropertyBagDataCache);
					if (sharedObjectPropertyBagDataCache2 != null)
					{
						sharedObjectPropertyBagDataCache = sharedObjectPropertyBagDataCache2;
					}
				}
				finally
				{
					mailboxState.ReleaseReference();
				}
			}
			return sharedObjectPropertyBagDataCache;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002A5B0 File Offset: 0x000287B0
		internal static DataRow LoadDataRow(Context context, bool newBag, Table table, bool writeThrough, ColumnValue[] initialValues)
		{
			if (newBag)
			{
				return Factory.CreateDataRow(context.Culture, context, table, writeThrough, initialValues);
			}
			return Factory.OpenDataRow(context.Culture, context, table, writeThrough, initialValues);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002A5D8 File Offset: 0x000287D8
		bool IComponentData.DoCleanup(Context context)
		{
			bool result;
			using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
			{
				List<ExchangeId> list = new List<ExchangeId>(this.dataCache.Count);
				foreach (KeyValuePair<ExchangeId, SharedObjectPropertyBagData> keyValuePair in this.dataCache)
				{
					if (!keyValuePair.Value.IsInUse)
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (ExchangeId exchangeId in list)
				{
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Dropping unused folder {0} from cache", exchangeId);
					}
					SharedObjectPropertyBagData sharedData = this.dataCache[exchangeId];
					this.RemoveDataFromTheCache(sharedData, exchangeId);
				}
				result = (this.dataCache.Count == 0);
			}
			return result;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002A700 File Offset: 0x00028900
		internal SharedObjectPropertyBagData LoadPropertyBagData(Context context, Mailbox mailbox, ExchangeId propertyBagId, bool newBag, Table table, bool writeThrough, params ColumnValue[] initialValues)
		{
			SharedObjectPropertyBagData sharedObjectPropertyBagData = null;
			if (!newBag)
			{
				using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
				{
					if (this.dataCache.TryGetValue(propertyBagId, out sharedObjectPropertyBagData))
					{
						if (sharedObjectPropertyBagData.DataRow != null && !sharedObjectPropertyBagData.DataRow.IsDead)
						{
							this.MarkAsActiveInTheCacheNoLock(sharedObjectPropertyBagData, propertyBagId);
							sharedObjectPropertyBagData.IncrementUsage();
							return sharedObjectPropertyBagData;
						}
						sharedObjectPropertyBagData = null;
					}
				}
			}
			DataRow dataRow = null;
			SharedObjectPropertyBagData sharedObjectPropertyBagData2 = null;
			SharedObjectPropertyBagData result;
			try
			{
				dataRow = SharedObjectPropertyBagDataCache.LoadDataRow(context, newBag, table, writeThrough, initialValues);
				using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
				{
					if (this.dataCache.TryGetValue(propertyBagId, out sharedObjectPropertyBagData))
					{
						if (sharedObjectPropertyBagData.DataRow == null || sharedObjectPropertyBagData.DataRow.IsDead)
						{
							if (sharedObjectPropertyBagData.DataRow != null)
							{
								sharedObjectPropertyBagData.DataRow.Dispose();
								sharedObjectPropertyBagData.DataRow = null;
							}
							sharedObjectPropertyBagData.DataRow = dataRow;
							dataRow = null;
						}
					}
					else
					{
						sharedObjectPropertyBagData2 = new SharedObjectPropertyBagData(context, mailbox, this, propertyBagId, dataRow);
						dataRow = null;
						this.dataCache.Add(propertyBagId, sharedObjectPropertyBagData2);
						sharedObjectPropertyBagData = sharedObjectPropertyBagData2;
						sharedObjectPropertyBagData2 = null;
					}
					this.MarkAsActiveInTheCacheNoLock(sharedObjectPropertyBagData, propertyBagId);
					sharedObjectPropertyBagData.IncrementUsage();
					result = sharedObjectPropertyBagData;
				}
			}
			finally
			{
				if (dataRow != null)
				{
					dataRow.Dispose();
				}
				if (sharedObjectPropertyBagData2 != null)
				{
					sharedObjectPropertyBagData2.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002A860 File Offset: 0x00028A60
		internal bool IsCleanupRequired()
		{
			bool result;
			using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock))
			{
				result = (this.evictionPolicy.CountOfKeysToCleanup > ConfigurationSchema.SharedObjectPropertyBagDataCacheCleanupMultiplier.Value * this.evictionPolicy.Capacity);
			}
			return result;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		internal void MarkAsActiveInTheCache(SharedObjectPropertyBagData sharedData, ExchangeId propertyBagId)
		{
			using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock))
			{
				this.MarkAsActiveInTheCacheNoLock(sharedData, propertyBagId);
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002A904 File Offset: 0x00028B04
		internal void ReleasePropertyBagData(SharedObjectPropertyBagData sharedData, ExchangeId propertyBagId)
		{
			using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock))
			{
				sharedData.DecrementUsage();
				if (!sharedData.IsInUse && (sharedData.DataRow == null || sharedData.DataRow.IsDead || sharedData.DataRow.IsDirty || sharedData.DataRow.IsNew))
				{
					this.RemoveDataFromTheCache(sharedData, propertyBagId);
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002A984 File Offset: 0x00028B84
		private void MarkAsActiveInTheCacheNoLock(SharedObjectPropertyBagData sharedData, ExchangeId propertyBagId)
		{
			if (sharedData.IsActiveInTheCache)
			{
				this.evictionPolicy.KeyAccess(propertyBagId);
				return;
			}
			this.evictionPolicy.Insert(propertyBagId);
			sharedData.IsActiveInTheCache = true;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		private void OnMailboxDisconnect(Context context, Mailbox mailbox)
		{
			if (!mailbox.SharedState.IsMailboxLockedExclusively())
			{
				return;
			}
			using (LockManager.Lock(this.dataCache, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
			{
				this.evictionPolicy.EvictionCheckpoint();
				IList<ExchangeId> keysToCleanup = this.evictionPolicy.GetKeysToCleanup(true);
				for (int i = 0; i < keysToCleanup.Count; i++)
				{
					SharedObjectPropertyBagData sharedObjectPropertyBagData;
					if (this.dataCache.TryGetValue(keysToCleanup[i], out sharedObjectPropertyBagData))
					{
						if (object.ReferenceEquals(mailbox.DataRow, sharedObjectPropertyBagData.DataRow))
						{
							this.evictionPolicy.Insert(keysToCleanup[i]);
						}
						else
						{
							sharedObjectPropertyBagData.IsActiveInTheCache = false;
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Discarding folder {0} cache", keysToCleanup[i]);
							}
							sharedObjectPropertyBagData.DiscardCache(context);
						}
					}
				}
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002AA98 File Offset: 0x00028C98
		private void RemoveDataFromTheCache(SharedObjectPropertyBagData sharedData, ExchangeId propertyBagId)
		{
			this.dataCache.Remove(propertyBagId);
			if (sharedData.IsActiveInTheCache)
			{
				this.evictionPolicy.Remove(propertyBagId);
			}
			sharedData.Dispose();
		}

		// Token: 0x0400052F RID: 1327
		private static int mailboxStateSlot = -1;

		// Token: 0x04000530 RID: 1328
		private MailboxState mailboxState;

		// Token: 0x04000531 RID: 1329
		private Dictionary<ExchangeId, SharedObjectPropertyBagData> dataCache;

		// Token: 0x04000532 RID: 1330
		private EvictionPolicy<ExchangeId> evictionPolicy;
	}
}
