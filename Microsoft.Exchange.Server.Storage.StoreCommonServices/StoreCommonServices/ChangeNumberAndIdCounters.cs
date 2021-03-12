using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000024 RID: 36
	public class ChangeNumberAndIdCounters : LockableMailboxComponent
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00010B8E File Offset: 0x0000ED8E
		public ChangeNumberAndIdCounters(MailboxState mailboxState)
		{
			this.mailboxLockName = mailboxState;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public override LockManager.LockType ReaderLockType
		{
			get
			{
				return LockManager.LockType.ChangeNumberAndIdCountersShared;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00010BAC File Offset: 0x0000EDAC
		public override LockManager.LockType WriterLockType
		{
			get
			{
				return LockManager.LockType.ChangeNumberAndIdCountersExclusive;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public override MailboxComponentId MailboxComponentId
		{
			get
			{
				return ChangeNumberAndIdCounters.ComponentId;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00010BB7 File Offset: 0x0000EDB7
		public override Guid DatabaseGuid
		{
			get
			{
				return this.mailboxLockName.DatabaseGuid;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		public override int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxLockName.MailboxPartitionNumber;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		public override bool Committable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public static void Initialize()
		{
			if (ChangeNumberAndIdCounters.changeNumberAndIdCountersCacheSlot == -1)
			{
				ChangeNumberAndIdCounters.changeNumberAndIdCountersCacheSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public static ChangeNumberAndIdCounters GetCacheForMailbox(Mailbox mailbox)
		{
			ChangeNumberAndIdCounters changeNumberAndIdCounters = (ChangeNumberAndIdCounters)mailbox.SharedState.GetComponentData(ChangeNumberAndIdCounters.changeNumberAndIdCountersCacheSlot);
			if (changeNumberAndIdCounters == null)
			{
				changeNumberAndIdCounters = new ChangeNumberAndIdCounters(mailbox.SharedState);
				ChangeNumberAndIdCounters changeNumberAndIdCounters2 = (ChangeNumberAndIdCounters)mailbox.SharedState.CompareExchangeComponentData(ChangeNumberAndIdCounters.changeNumberAndIdCountersCacheSlot, null, changeNumberAndIdCounters);
				if (changeNumberAndIdCounters2 != null)
				{
					changeNumberAndIdCounters = changeNumberAndIdCounters2;
				}
			}
			return changeNumberAndIdCounters;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (table.Equals(DatabaseSchema.MailboxIdentityTable(context.Database).Table))
			{
				if (operationType == Connection.OperationType.Query)
				{
					return this.TestSharedLock() || this.TestExclusiveLock();
				}
				if (operationType == Connection.OperationType.Update)
				{
					return this.TestExclusiveLock();
				}
			}
			return false;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00010C77 File Offset: 0x0000EE77
		public void InitializeCounterCaches(Context context, Mailbox mailbox)
		{
			this.AllocateChangeNumberCounterRange(context, mailbox, 0U, false);
			this.AllocateObjectIdCounterRange(context, mailbox, 0U, false);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00010C90 File Offset: 0x0000EE90
		public Guid GetLocalIdGuid(Context context, Mailbox mailbox)
		{
			Guid result;
			using (context.MailboxComponentReadOperation(this))
			{
				if (this.localIdGuid == Guid.Empty)
				{
					using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, mailbox.MailboxIdentityTable.Table, true, new ColumnValue[]
					{
						new ColumnValue(mailbox.MailboxIdentityTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
					}))
					{
						this.localIdGuid = (Guid)dataRow.GetValue(context, mailbox.MailboxIdentityTable.LocalIdGuid);
					}
				}
				result = this.localIdGuid;
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public void GetGlobalCounters(Context context, Mailbox mailbox, out ulong idCounter, out ulong cnCounter)
		{
			using (context.MailboxComponentReadOperation(this))
			{
				ulong num;
				ulong num2;
				using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, mailbox.MailboxIdentityTable.Table, true, new ColumnValue[]
				{
					new ColumnValue(mailbox.MailboxIdentityTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
				}))
				{
					num = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.IdCounter));
					num2 = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.CnCounter));
				}
				idCounter = num;
				cnCounter = num2;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00010E2C File Offset: 0x0000F02C
		public ulong AllocateObjectIdCounter(Context context, Mailbox mailbox)
		{
			return this.AllocateObjectIdCounterRange(context, mailbox, 1U, true);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00010E38 File Offset: 0x0000F038
		public ulong AllocateChangeNumberCounter(Context context, Mailbox mailbox)
		{
			return this.AllocateChangeNumberCounterRange(context, mailbox, 1U, true);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00010E44 File Offset: 0x0000F044
		public ulong GetLastChangeNumber(Context context, Mailbox mailbox)
		{
			return this.AllocateChangeNumberCounterRange(context, mailbox, 0U, true);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00010E50 File Offset: 0x0000F050
		public ulong GetNextIdCounterAndReserveRange(Context context, Mailbox mailbox, uint reservedRange)
		{
			return this.AllocateObjectIdCounterRange(context, mailbox, reservedRange, true);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00010E5C File Offset: 0x0000F05C
		public ulong GetNextIdCounterAndReserveRange(Context context, Mailbox mailbox, uint reservedRange, bool separateTransaction)
		{
			return this.AllocateObjectIdCounterRange(context, mailbox, reservedRange, separateTransaction);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00010E69 File Offset: 0x0000F069
		public ulong GetNextCnCounterAndReserveRange(Context context, Mailbox mailbox, uint reservedRange)
		{
			return this.AllocateChangeNumberCounterRange(context, mailbox, reservedRange, true);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00010E78 File Offset: 0x0000F078
		public void SetGlobalCounters(Context context, Mailbox mailbox, ulong newIdCounter, ulong newCnCounter)
		{
			this.SetGlobalCounters(context, mailbox, newIdCounter, newCnCounter, null);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00010E9C File Offset: 0x0000F09C
		public void SetGlobalCounters(Context context, Mailbox mailbox, ulong newIdCounter, ulong newCnCounter, Guid? newLocalIdGuid)
		{
			bool flag = false;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
			{
				context.PushConnection();
				try
				{
					using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, mailbox.MailboxIdentityTable.Table, true, new ColumnValue[]
					{
						new ColumnValue(mailbox.MailboxIdentityTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
					}))
					{
						if (newLocalIdGuid != null)
						{
							Guid guid = (Guid)dataRow.GetValue(context, mailbox.MailboxIdentityTable.LocalIdGuid);
							dataRow.SetValue(context, mailbox.MailboxIdentityTable.LocalIdGuid, newLocalIdGuid.Value);
						}
						else
						{
							ulong num = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.IdCounter));
							ulong num2 = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.CnCounter));
							if (num > newIdCounter)
							{
								throw new CorruptDataException((LID)62536U, "New ID counter out of range.");
							}
							if (num2 > newCnCounter)
							{
								throw new CorruptDataException((LID)37960U, "New CN counter out of range.");
							}
						}
						dataRow.SetValue(context, mailbox.MailboxIdentityTable.IdCounter, (long)newIdCounter);
						dataRow.SetValue(context, mailbox.MailboxIdentityTable.CnCounter, (long)newCnCounter);
						dataRow.SetValue(context, mailbox.MailboxIdentityTable.LastCounterPatchingTime, mailbox.SharedState.UtcNow);
						dataRow.Flush(context);
					}
					context.Commit();
					flag = true;
					mailboxComponentOperationFrame.Success();
					mailbox.SharedState.GlobalIdLowWatermark = newIdCounter;
					mailbox.SharedState.GlobalCnLowWatermark = newCnCounter;
					this.cnAllocationCache = null;
					this.idAllocationCache = null;
				}
				finally
				{
					if (!flag)
					{
						context.Abort();
					}
					context.PopConnection();
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public void UpdateMailboxGlobalIDs(Context context, Mailbox mailbox, StoreDatabase database, bool separateTransaction)
		{
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
			{
				if (!mailbox.SharedState.CountersAlreadyPatched && !mailbox.SharedState.IsRemoved)
				{
					if (mailbox.GetMRSPreservingMailboxSignature(context) || mailbox.GetPreservingMailboxSignature(context))
					{
						mailbox.SharedState.CountersAlreadyPatched = true;
					}
					else
					{
						bool flag = false;
						if (separateTransaction)
						{
							context.PushConnection();
						}
						try
						{
							ulong globalIdLowWatermark;
							ulong globalCnLowWatermark;
							using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, mailbox.MailboxIdentityTable.Table, true, new ColumnValue[]
							{
								new ColumnValue(mailbox.MailboxIdentityTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
							}))
							{
								ulong num = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.IdCounter));
								ulong num2 = (ulong)((long)dataRow.GetValue(context, mailbox.MailboxIdentityTable.CnCounter));
								if (database.IsReadOnly)
								{
									globalIdLowWatermark = num;
									globalCnLowWatermark = num2;
								}
								else
								{
									DateTime utcNow = mailbox.SharedState.UtcNow;
									DateTime dateTime = (DateTime)dataRow.GetValue(context, mailbox.MailboxIdentityTable.LastCounterPatchingTime);
									uint num3;
									if (utcNow > dateTime)
									{
										num3 = (uint)(utcNow - dateTime).TotalSeconds;
									}
									else
									{
										num3 = 0U;
									}
									ulong num4 = (ulong)Math.Max(num3 * 128U, 3840U);
									long num5;
									long num6;
									if (num < 281474976645120UL - num4 && num2 < 281474976645120UL - num4)
									{
										num5 = (long)(num + num4);
										num6 = (long)(num2 + num4);
									}
									else
									{
										num5 = 281474976645120L;
										num6 = 281474976645120L;
									}
									dataRow.SetValue(context, mailbox.MailboxIdentityTable.IdCounter, num5);
									dataRow.SetValue(context, mailbox.MailboxIdentityTable.CnCounter, num6);
									dataRow.SetValue(context, mailbox.MailboxIdentityTable.LastCounterPatchingTime, utcNow);
									dataRow.Flush(context);
									globalIdLowWatermark = (ulong)num5;
									globalCnLowWatermark = (ulong)num6;
								}
							}
							if (separateTransaction)
							{
								context.Commit();
								flag = true;
							}
							mailbox.SharedState.CountersAlreadyPatched = true;
							mailbox.SharedState.GlobalIdLowWatermark = globalIdLowWatermark;
							mailbox.SharedState.GlobalCnLowWatermark = globalCnLowWatermark;
							mailboxComponentOperationFrame.Success();
						}
						finally
						{
							if (separateTransaction)
							{
								if (!flag)
								{
									context.Abort();
								}
								context.PopConnection();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00011378 File Offset: 0x0000F578
		private void AllocateCounterRange(Context context, Mailbox mailbox, uint rangeSize, bool separateTransaction, StorePropTag propTagCounterRangeUpperLimit, PhysicalColumn nextUnusedCounterPhysicalColumn, ref GlobcntAllocationCache allocationCache, out bool needNewGlobCountSet, out ulong globCount)
		{
			needNewGlobCountSet = false;
			globCount = 0UL;
			uint num = (allocationCache == null) ? 0U : allocationCache.CountAvailable;
			if (allocationCache == null || num < rangeSize)
			{
				bool flag = false;
				ulong num2 = 0UL;
				bool preservingMailboxSignature = mailbox.GetPreservingMailboxSignature(context);
				bool mrspreservingMailboxSignature = mailbox.GetMRSPreservingMailboxSignature(context);
				if (ExTraceGlobals.MailboxCountersTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxCountersTracer.TraceDebug(59680L, "Request counter range for mailbox {0} : {1} : Store preserving mailbox signature {2} : MRS preserving mailbox signature {3} : counter {4} : available range {5}, requested range {6} : stack {7}", new object[]
					{
						mailbox.GetDisplayName(context),
						mailbox.MailboxGuid,
						preservingMailboxSignature,
						mrspreservingMailboxSignature,
						nextUnusedCounterPhysicalColumn,
						num,
						rangeSize,
						new StackTrace()
					});
				}
				if (!preservingMailboxSignature && mrspreservingMailboxSignature)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_InvalidMailboxGlobcntAllocation, new object[]
					{
						mailbox.MailboxGuid,
						mailbox.Database.MdbGuid,
						context.Diagnostics.OpSource,
						context.Diagnostics.OpNumber,
						new StackTrace()
					});
					throw new StoreException((LID)43296U, ErrorCodeValue.NotSupported, "Invalid counter range allocation at this time.");
				}
				if (preservingMailboxSignature)
				{
					object propertyValue = mailbox.GetPropertyValue(context, propTagCounterRangeUpperLimit);
					num2 = (ulong)((long)propertyValue);
				}
				if (separateTransaction)
				{
					context.PushConnection();
				}
				try
				{
					uint num3 = rangeSize - num;
					ulong num4 = (ulong)Math.Max(num3, 500U);
					ulong nextUnallocated;
					ulong num7;
					using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, mailbox.MailboxIdentityTable.Table, true, new ColumnValue[]
					{
						new ColumnValue(mailbox.MailboxIdentityTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
					}))
					{
						ulong num5 = (ulong)((long)dataRow.GetValue(context, nextUnusedCounterPhysicalColumn));
						if (allocationCache == null)
						{
							nextUnallocated = num5;
						}
						else
						{
							nextUnallocated = allocationCache.Allocate(0U);
						}
						if (preservingMailboxSignature)
						{
							ulong num6 = num2 - num5;
							if (num6 <= (ulong)num3)
							{
								if (ExTraceGlobals.MailboxCountersTracer.IsTraceEnabled(TraceType.ErrorTrace))
								{
									ExTraceGlobals.MailboxCountersTracer.TraceDebug(35104L, "Failed with GlobalCounterRangeExceeded the request for counter range for mailbox {0} : {1} : Store preserving mailbox signature {2} : MRS preserving mailbox signature {3} : counter {4} : available range {5}, requested range {6} : stack {7}", new object[]
									{
										mailbox.GetDisplayName(context),
										mailbox.MailboxGuid,
										preservingMailboxSignature,
										mrspreservingMailboxSignature,
										nextUnusedCounterPhysicalColumn,
										num,
										rangeSize,
										new StackTrace()
									});
								}
								throw new StoreException((LID)35912U, ErrorCodeValue.GlobalCounterRangeExceeded, (PropTag.Mailbox.ReservedIdCounterRangeUpperLimit == propTagCounterRangeUpperLimit) ? "Out of IDs." : "Out of CNs.");
							}
							num4 = Math.Min(num6, num4);
						}
						num7 = num5 + num4;
						if (num7 > 281474976645120UL)
						{
							needNewGlobCountSet = true;
							return;
						}
						dataRow.SetValue(context, nextUnusedCounterPhysicalColumn, (long)num7);
						dataRow.Flush(context);
						if (ExTraceGlobals.MailboxCountersTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MailboxCountersTracer.TraceDebug<Guid, PhysicalColumn, ulong>(0L, "Updated counter for mailbox {0} : counter {1} : updated value {2}", mailbox.MailboxGuid, nextUnusedCounterPhysicalColumn, num7);
						}
					}
					if (separateTransaction)
					{
						context.Commit();
						flag = true;
					}
					if (allocationCache == null)
					{
						allocationCache = new GlobcntAllocationCache(nextUnallocated, num7);
					}
					else
					{
						allocationCache.SetMax(num7);
					}
				}
				finally
				{
					if (separateTransaction)
					{
						if (!flag)
						{
							context.Abort();
						}
						context.PopConnection();
					}
				}
			}
			needNewGlobCountSet = false;
			globCount = allocationCache.Allocate(rangeSize);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00011734 File Offset: 0x0000F934
		private ulong AllocateCounterRange(Context context, Mailbox mailbox, uint rangeSize, bool separateTransaction, StorePropTag propTagCounterRangeUpperLimit, PhysicalColumn nextUnusedCounterPhysicalColumn, ref GlobcntAllocationCache allocationCache)
		{
			ulong result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
			{
				bool flag;
				ulong num;
				this.AllocateCounterRange(context, mailbox, rangeSize, separateTransaction, propTagCounterRangeUpperLimit, nextUnusedCounterPhysicalColumn, ref allocationCache, out flag, out num);
				if (flag)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxGlobcntRolledOver, new object[]
					{
						mailbox.MailboxGuid
					});
					this.GetNewGlobCountSets(context, mailbox);
					this.AllocateCounterRange(context, mailbox, rangeSize, separateTransaction, propTagCounterRangeUpperLimit, nextUnusedCounterPhysicalColumn, ref allocationCache, out flag, out num);
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!flag, "Cannot exhaust the new global counter set in one allocation.");
				}
				mailboxComponentOperationFrame.Success();
				result = num;
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000117DC File Offset: 0x0000F9DC
		private ulong AllocateObjectIdCounterRange(Context context, Mailbox mailbox, uint rangeSize, bool separateTransaction)
		{
			return this.AllocateCounterRange(context, mailbox, rangeSize, separateTransaction, PropTag.Mailbox.ReservedIdCounterRangeUpperLimit, mailbox.MailboxIdentityTable.IdCounter, ref this.idAllocationCache);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000117FF File Offset: 0x0000F9FF
		private ulong AllocateChangeNumberCounterRange(Context context, Mailbox mailbox, uint rangeSize, bool separateTransaction)
		{
			return this.AllocateCounterRange(context, mailbox, rangeSize, separateTransaction, PropTag.Mailbox.ReservedCnCounterRangeUpperLimit, mailbox.MailboxIdentityTable.CnCounter, ref this.cnAllocationCache);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00011824 File Offset: 0x0000FA24
		private void GetNewGlobCountSets(Context context, Mailbox mailbox)
		{
			ReplidGuidMap cacheForMailbox = ReplidGuidMap.GetCacheForMailbox(context, mailbox.SharedState);
			Guid guid = cacheForMailbox.RegisterNewLocalIdGuid(context, mailbox, Guid.Empty);
			this.idAllocationCache = null;
			this.cnAllocationCache = null;
			this.localIdGuid = guid;
		}

		// Token: 0x040001EE RID: 494
		internal const uint DefaultIdReserveChunk = 500U;

		// Token: 0x040001EF RID: 495
		private static readonly MailboxComponentId ComponentId = MailboxComponentId.ChangeNumberAndIdCounters;

		// Token: 0x040001F0 RID: 496
		private static int changeNumberAndIdCountersCacheSlot = -1;

		// Token: 0x040001F1 RID: 497
		private MailboxLockNameBase mailboxLockName;

		// Token: 0x040001F2 RID: 498
		private GlobcntAllocationCache idAllocationCache;

		// Token: 0x040001F3 RID: 499
		private GlobcntAllocationCache cnAllocationCache;

		// Token: 0x040001F4 RID: 500
		private Guid localIdGuid = Guid.Empty;
	}
}
