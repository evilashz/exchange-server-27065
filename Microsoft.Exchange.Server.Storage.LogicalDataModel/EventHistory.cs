using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000040 RID: 64
	public class EventHistory : IStateObject
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00038C88 File Offset: 0x00036E88
		private EventHistory(Context context)
		{
			this.database = context.Database;
			this.mdbVersionGuid = Guid.NewGuid();
			this.watermarkTableLockName = EventHistory.WatermarkTableLockName(context.Database.MdbGuid);
			this.eventsTable = DatabaseSchema.EventsTable(context.Database);
			this.watermarksTable = DatabaseSchema.WatermarksTable(context.Database);
			this.eventsFetchList = new Column[]
			{
				this.eventsTable.EventCounter,
				this.eventsTable.CreateTime,
				this.eventsTable.TransactionId,
				this.eventsTable.EventType,
				this.eventsTable.MailboxNumber,
				this.eventsTable.ClientType,
				this.eventsTable.Flags,
				this.eventsTable.ObjectClass,
				this.eventsTable.Fid,
				this.eventsTable.Mid,
				this.eventsTable.ParentFid,
				this.eventsTable.OldFid,
				this.eventsTable.OldMid,
				this.eventsTable.OldParentFid,
				this.eventsTable.ItemCount,
				this.eventsTable.UnreadCount,
				this.eventsTable.ExtendedFlags,
				this.eventsTable.Sid,
				this.eventsTable.DocumentId
			};
			this.lastEventFetchList = new Column[]
			{
				this.eventsTable.EventCounter,
				this.eventsTable.CreateTime
			};
			this.watermarksFetchList = new Column[]
			{
				this.watermarksTable.ConsumerGuid,
				this.watermarksTable.MailboxNumber,
				this.watermarksTable.EventCounter
			};
			this.lastEventCounter = this.ComputeEventCounterBound(context, true);
			this.highestCommittedEventCounter = this.lastEventCounter;
			this.eventCounterUpperBound = this.lastEventCounter + 1L;
			this.eventCounterLowerBound = this.ComputeEventCounterBound(context, false);
			this.eventCounterAllocationPotentiallyLost = true;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00038EC3 File Offset: 0x000370C3
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00038ECB File Offset: 0x000370CB
		public Guid MdbVersionGuid
		{
			get
			{
				return this.mdbVersionGuid;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00038ED3 File Offset: 0x000370D3
		internal long LastEventCounter
		{
			get
			{
				return this.lastEventCounter;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00038EDB File Offset: 0x000370DB
		internal long EventCounterUpperBound
		{
			get
			{
				return this.eventCounterUpperBound;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00038EE3 File Offset: 0x000370E3
		internal bool EventCounterAllocationPotentiallyLost
		{
			get
			{
				return this.eventCounterAllocationPotentiallyLost;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00038EEB File Offset: 0x000370EB
		internal bool IsEventCounterUpperBoundFlushNeeded
		{
			get
			{
				return this.flushEventCounterUpperBoundTaskManager.IsFlushNeeded;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00038EF8 File Offset: 0x000370F8
		public static void Initialize()
		{
			if (EventHistory.eventHistoryDataSlot == -1)
			{
				EventHistory.eventHistoryDataSlot = StoreDatabase.AllocateComponentDataSlot();
				EventHistory.eventHistoryCleanupMaintenance = MaintenanceHandler.RegisterDatabaseMaintenance(EventHistory.EventHistoryCleanupMaintenanceId, RequiredMaintenanceResourceType.Store, new MaintenanceHandler.DatabaseMaintenanceDelegate(EventHistory.EventHistoryCleanupMaintenance), "EventHistory.EventHistoryCleanupMaintenance");
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00038F30 File Offset: 0x00037130
		internal static void MountEventHandler(Context context, bool readOnly)
		{
			EventHistory eventHistory = new EventHistory(context);
			if (AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database) && context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				try
				{
					context.Database.GetSharedLock();
					eventHistory.SyncEventCounterBounds(context, readOnly);
				}
				finally
				{
					context.Database.ReleaseSharedLock();
				}
			}
			context.Database.ComponentData[EventHistory.eventHistoryDataSlot] = eventHistory;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00038FAC File Offset: 0x000371AC
		internal static void MountedEventHandler(Context context)
		{
			EventHistory.eventHistoryCleanupMaintenance.ScheduleMarkForMaintenance(context, TimeSpan.FromDays(1.0));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00038FC7 File Offset: 0x000371C7
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[EventHistory.eventHistoryDataSlot] = null;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00038FDA File Offset: 0x000371DA
		internal static IDisposable SetEventHistoryCleanupChunkSizeForTest(int chunkSize)
		{
			return EventHistory.eventHistoryCleanupChunkSize.SetTestHook(chunkSize);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00038FE7 File Offset: 0x000371E7
		internal static IDisposable SetEventHistoryCleanupRowsDeletedTestHook(Action<int> testDelegate)
		{
			return EventHistory.eventHistoryCleanupRowsDeletedTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00038FF4 File Offset: 0x000371F4
		internal static IDisposable SetSimulateReadEventsFromPassiveTestHook(bool simulateReadEventsFromPassive)
		{
			return EventHistory.simulateReadEventsFromPassiveTestHook.SetTestHook(simulateReadEventsFromPassive);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00039001 File Offset: 0x00037201
		internal static IDisposable SetInsertedEventHistoryRecordTestHook(Action<long> testDelegate)
		{
			return EventHistory.insertedEventHistoryRecordTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0003900E File Offset: 0x0003720E
		internal static IDisposable SetEventCounterAllocatedTestHook(Action<int, EventType, long> testDelegate)
		{
			return EventHistory.eventCounterAllocatedTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0003901C File Offset: 0x0003721C
		public static EventHistory GetEventHistory(StoreDatabase database)
		{
			return database.ComponentData[EventHistory.eventHistoryDataSlot] as EventHistory;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00039040 File Offset: 0x00037240
		private long ComputeEventCounterBound(Context context, bool upperBound)
		{
			Column[] columnsToFetch = new Column[]
			{
				this.eventsTable.EventCounter
			};
			long result;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, KeyRange.AllRows, upperBound, false))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						result = reader.GetInt64(this.eventsTable.EventCounter);
					}
					else
					{
						result = 0L;
					}
				}
			}
			return result;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000390F8 File Offset: 0x000372F8
		private void SyncEventCounterBounds(Context context, bool readOnly)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(context.Database);
			Column[] columnsToFetch = new Column[]
			{
				globalsTable.EventCounterLowerBound,
				globalsTable.EventCounterUpperBound
			};
			using (TableOperator globalsTableRow = GlobalsTableHelper.GetGlobalsTableRow(context, columnsToFetch))
			{
				using (Reader reader = globalsTableRow.ExecuteReader(false))
				{
					reader.Read();
					long @int = reader.GetInt64(globalsTable.EventCounterLowerBound);
					long int2 = reader.GetInt64(globalsTable.EventCounterUpperBound);
					using (LockManager.Lock(this, LockManager.LockType.EventCounterBounds, context.Diagnostics))
					{
						if (@int > this.eventCounterLowerBound)
						{
							this.eventCounterLowerBound = @int;
						}
						else if (@int < this.eventCounterLowerBound)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(@int == 0L, "Persisted lower bound should never be lower than the actual EventHistory table contents.");
							if (!readOnly)
							{
								GlobalsTableHelper.UpdateGlobalsTableRow(context, new Column[]
								{
									globalsTable.EventCounterLowerBound
								}, new object[]
								{
									this.eventCounterLowerBound
								});
							}
						}
						if (int2 > this.eventCounterUpperBound)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(readOnly, "Persisted upper bound should never be ahead of the cached value on active databases.");
							this.eventCounterUpperBound = int2;
						}
						else if (int2 < this.eventCounterUpperBound && !readOnly)
						{
							GlobalsTableHelper.UpdateGlobalsTableRow(context, new Column[]
							{
								globalsTable.EventCounterUpperBound
							}, new object[]
							{
								this.eventCounterUpperBound
							});
						}
					}
				}
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000392AC File Offset: 0x000374AC
		public ErrorCode ReadEvents(Context context, long startCounter, uint eventsWant, uint eventsToCheck, Restriction restriction, EventHistory.ReadEventsFlags readFlags, out List<EventEntry> events, out long endCounter)
		{
			bool flag = this.IsReadingEventsFromPassive(context);
			events = null;
			endCounter = 0L;
			if (restriction != null)
			{
				if (flag)
				{
					if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ReadEventsTracer.TraceDebug<Restriction>(0L, "ReadEvents() on a passive database failed with a NotSupported error because a restriction was specified: {0}", restriction);
					}
					return ErrorCode.CreateNotSupported((LID)63692U);
				}
				if ((readFlags & (EventHistory.ReadEventsFlags)2147483648U) == EventHistory.ReadEventsFlags.None)
				{
					ErrorCode errorCode = this.VerifyRestriction(restriction);
					if (errorCode != ErrorCode.NoError)
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug<ErrorCode, Restriction>(0L, "ReadEvents() failed with error code {0} attempting to verify the specified restriction: {1}", errorCode, restriction);
						}
						return errorCode.Propagate((LID)46165U);
					}
				}
			}
			Interlocked.Increment(ref this.readerCount);
			try
			{
				if (flag)
				{
					if (!AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database))
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug(0L, "ReadEvents() from a passive database failed with a NotSupported error because the database hasn't been sufficiently upgraded yet.");
						}
						return ErrorCode.CreateNotSupported((LID)49484U);
					}
					context.BeginTransactionIfNeeded();
					this.SyncEventCounterBounds(context, true);
					if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadEvents() synced event counter bounds for the passive database: eventCounterLowerBound={0}, eventCounterUpperBound={1}", this.eventCounterLowerBound, this.eventCounterUpperBound);
					}
				}
				if (startCounter < this.eventCounterLowerBound)
				{
					if ((readFlags & EventHistory.ReadEventsFlags.FailIfEventsDeleted) != EventHistory.ReadEventsFlags.None)
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadEvents() failed with an EventsDeleted error (LID 62549) because the FailIfEventsDeleted flag was specified and the startCounter ({0}) is less than the event counter lower bound ({1}).", startCounter, this.eventCounterLowerBound);
						}
						return ErrorCode.CreateEventsDeleted((LID)62549U);
					}
					startCounter = this.eventCounterLowerBound;
				}
				if (eventsWant == 0U)
				{
					if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ReadEventsTracer.TraceDebug(0L, "ReadEvents() could not return any events because eventsWant=0.");
					}
					return ErrorCode.NoError;
				}
				if (eventsWant > 1000U)
				{
					eventsWant = 1000U;
				}
				if (eventsToCheck > 10000U || eventsToCheck < 1000U)
				{
					eventsToCheck = 10000U;
				}
				long num = this.eventCounterUpperBound;
				long num2;
				if (flag)
				{
					num2 = startCounter + (long)((ulong)eventsToCheck);
				}
				else
				{
					num2 = num;
					if (startCounter >= num2)
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadEvents() could not return any events because the start counter equals or exceeds the upper bound (startCounter={0}, effectiveUpperBound={1}).", startCounter, num2);
						}
						return ErrorCode.NoError;
					}
					if (num2 - startCounter > (long)((ulong)eventsToCheck))
					{
						num2 = startCounter + (long)((ulong)eventsToCheck);
					}
				}
				long num3 = (long)((ulong)((eventsWant < 50U) ? eventsWant : 50U));
				if (num3 > num2 - startCounter)
				{
					num3 = num2 - startCounter;
				}
				events = new List<EventEntry>((int)num3);
				SearchCriteria searchCriteria = null;
				bool flag2 = false;
				if (!ConfigurationSchema.SkipMoveEventExclusion.Value && (readFlags & EventHistory.ReadEventsFlags.IncludeMoveDestinationEvents) == EventHistory.ReadEventsFlags.None)
				{
					flag2 = true;
					if (!flag)
					{
						Restriction restriction2 = new RestrictionBitmask(PropTag.Event.EventExtendedFlags, 16L, BitmaskOperation.EqualToZero);
						if (restriction != null)
						{
							restriction = new RestrictionAND(new Restriction[]
							{
								restriction2,
								restriction
							});
						}
						else
						{
							restriction = restriction2;
						}
					}
				}
				if (restriction != null)
				{
					searchCriteria = restriction.ToSearchCriteria(this.database, ObjectType.Event);
					searchCriteria = this.FixEventReadCriteria(context, searchCriteria);
				}
				if (searchCriteria is SearchCriteriaFalse)
				{
					if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ReadEventsTracer.TraceDebug<Restriction>(0L, "ReadEvents() didn't return any events because it was determined that no entries would satisfy the specified restriction: {0}", restriction);
					}
				}
				else
				{
					long num4 = startCounter - 1L;
					StartStopKey startKey = new StartStopKey(true, new object[]
					{
						startCounter
					});
					StartStopKey stopKey = new StartStopKey(false, new object[]
					{
						num2
					});
					using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, this.eventsFetchList, searchCriteria, null, 0, 0, new KeyRange(startKey, stopKey), false, true))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							if (context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
							{
								context.BeginTransactionIfNeeded();
							}
							int num5 = 0;
							while ((long)events.Count < (long)((ulong)eventsWant))
							{
								if (++num5 % 128 == 0 && context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
								{
									context.Commit();
									context.BeginTransactionIfNeeded();
								}
								if (!reader.Read())
								{
									break;
								}
								if (EventHistory.readerRaceTestHook != null)
								{
									EventHistory.readerRaceTestHook();
								}
								long @int = reader.GetInt64(this.eventsTable.EventCounter);
								if (flag)
								{
									if (@int != num4 + 1L)
									{
										long num6 = EventHistory.simulateReadEventsFromPassiveTestHook.Value ? this.GetPersistedEventCounterBound(context, true) : this.eventCounterUpperBound;
										if (@int >= num6)
										{
											if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
											{
												ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long, long>(0L, "ReadEvents() stopped before reaching the end of the EventHistory table because there was a counter gap between {0} and {1} (eventCounterUpperBound={2}).", num4, @int, num6);
												break;
											}
											break;
										}
									}
									num4 = @int;
									if (flag2)
									{
										long? nullableInt = reader.GetNullableInt64(this.eventsTable.ExtendedFlags);
										ExtendedEventFlags? extendedEventFlags = (nullableInt != null) ? new ExtendedEventFlags?((ExtendedEventFlags)nullableInt.GetValueOrDefault()) : null;
										if (extendedEventFlags != null && (extendedEventFlags & ExtendedEventFlags.MoveDestination) != ExtendedEventFlags.None)
										{
											continue;
										}
									}
								}
								int int2 = reader.GetInt32(this.eventsTable.MailboxNumber);
								MailboxState mailboxState = MailboxStateCache.Get(context, int2);
								if (mailboxState != null && !mailboxState.IsTombstone)
								{
									EventType int3 = (EventType)reader.GetInt32(this.eventsTable.EventType);
									if ((int3 & (EventType.MailboxCreated | EventType.MailboxDeleted | EventType.MailboxMoveStarted | EventType.MailboxMoveSucceeded | EventType.MailboxMoveFailed)) != (EventType)0 || !mailboxState.IsDeleted)
									{
										Guid mailboxGuid = mailboxState.MailboxGuid;
										Guid mailboxInstanceGuid = mailboxState.MailboxInstanceGuid;
										DateTime dateTime = reader.GetDateTime(this.eventsTable.CreateTime);
										int int4 = reader.GetInt32(this.eventsTable.TransactionId);
										ClientType clientType = (ClientType)reader.GetInt32(this.eventsTable.ClientType);
										EventFlags int5 = (EventFlags)reader.GetInt32(this.eventsTable.Flags);
										long? nullableInt2 = reader.GetNullableInt64(this.eventsTable.ExtendedFlags);
										ExtendedEventFlags? extendedFlags = (nullableInt2 != null) ? new ExtendedEventFlags?((ExtendedEventFlags)nullableInt2.GetValueOrDefault()) : null;
										string @string = reader.GetString(this.eventsTable.ObjectClass);
										byte[] binary = reader.GetBinary(this.eventsTable.Fid);
										byte[] binary2 = reader.GetBinary(this.eventsTable.Mid);
										byte[] binary3 = reader.GetBinary(this.eventsTable.ParentFid);
										byte[] binary4 = reader.GetBinary(this.eventsTable.OldFid);
										byte[] binary5 = reader.GetBinary(this.eventsTable.OldMid);
										byte[] binary6 = reader.GetBinary(this.eventsTable.OldParentFid);
										int? nullableInt3 = reader.GetNullableInt32(this.eventsTable.ItemCount);
										int? nullableInt4 = reader.GetNullableInt32(this.eventsTable.UnreadCount);
										byte[] binary7 = reader.GetBinary(this.eventsTable.Sid);
										int? nullableInt5 = reader.GetNullableInt32(this.eventsTable.DocumentId);
										if ((readFlags & EventHistory.ReadEventsFlags.FailIfEventsDeleted) != EventHistory.ReadEventsFlags.None && startCounter < this.eventCounterLowerBound)
										{
											if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
											{
												ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadEvents() failed with an EventsDeleted error (LID 37973) because the FailIfEventsDeleted flag was specified and the startCounter ({0}) is less than the event counter lower bound ({1}).", startCounter, this.eventCounterLowerBound);
											}
											return ErrorCode.CreateEventsDeleted((LID)37973U);
										}
										Guid? unifiedMailboxGuid = null;
										if (mailboxState.UnifiedState != null)
										{
											unifiedMailboxGuid = new Guid?(mailboxState.UnifiedState.UnifiedMailboxGuid);
										}
										EventEntry item = new EventEntry(@int, dateTime, int4, int3, new int?(int2), new Guid?(mailboxGuid), new Guid?(mailboxInstanceGuid), @string, binary, binary2, binary3, binary4, binary5, binary6, nullableInt3, nullableInt4, int5, extendedFlags, clientType, binary7, nullableInt5, mailboxState.TenantHint, unifiedMailboxGuid);
										events.Add(item);
										if ((long)events.Count == (long)((ulong)eventsWant))
										{
											break;
										}
									}
								}
							}
						}
					}
					if ((readFlags & EventHistory.ReadEventsFlags.FailIfEventsDeleted) != EventHistory.ReadEventsFlags.None && startCounter < this.eventCounterLowerBound)
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadEvents() failed with an EventsDeleted error (LID 54357) because the FailIfEventsDeleted flag was specified and the startCounter ({0}) is less than the event counter lower bound ({1}).", startCounter, this.eventCounterLowerBound);
						}
						return ErrorCode.CreateEventsDeleted((LID)54357U);
					}
				}
				if (flag)
				{
					if (events.Count > 0)
					{
						endCounter = events[events.Count - 1].EventCounter;
						this.UpdateEventCounterUpperBoundAfterGapScan(context, endCounter);
					}
					else if (num > startCounter)
					{
						endCounter = num - 1L;
					}
					else
					{
						endCounter = 0L;
						events = null;
					}
				}
				else if ((long)events.Count == (long)((ulong)eventsWant))
				{
					endCounter = events[events.Count - 1].EventCounter;
				}
				else
				{
					endCounter = num2 - 1L;
				}
				if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.ReadEventsTracer.TraceDebug(0L, "ReadEvents() is returning {0} events (isPassive={1}, startCounter={2}, endCounter={3}, effectiveUpperBound={4}, initialUpperBound={5}, eventsWant={6}, eventsToCheck={7}, readFlags=[{8}], restriction=[{9}]).", new object[]
					{
						(events != null) ? events.Count : 0,
						flag,
						startCounter,
						endCounter,
						num2,
						num,
						eventsWant,
						eventsToCheck,
						readFlags,
						searchCriteria
					});
				}
			}
			finally
			{
				context.Commit();
				Interlocked.Decrement(ref this.readerCount);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00039C38 File Offset: 0x00037E38
		public ErrorCode ReadLastEvent(Context context, out EventEntry e)
		{
			bool flag = this.IsReadingEventsFromPassive(context);
			if (flag)
			{
				if (!AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database))
				{
					if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ReadEventsTracer.TraceDebug(0L, "ReadLastEvent() from a passive database failed with a NotSupported error because the database hasn't been sufficiently upgraded yet.");
					}
					e = null;
					return ErrorCode.CreateNotSupported((LID)48972U);
				}
				this.SyncEventCounterBounds(context, true);
				if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadLastEvent() synced event counter bounds for the passive database: eventCounterLowerBound={0}, eventCounterUpperBound={1}", this.eventCounterLowerBound, this.eventCounterUpperBound);
				}
			}
			ErrorCode errorCode = ErrorCode.NoError;
			long num = EventHistory.simulateReadEventsFromPassiveTestHook.Value ? this.GetPersistedEventCounterBound(context, true) : this.eventCounterUpperBound;
			StartStopKey startKey = new StartStopKey(false, new object[]
			{
				num
			});
			StartStopKey empty = StartStopKey.Empty;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, this.lastEventFetchList, null, null, 0, 1, new KeyRange(startKey, empty), true, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						long @int = reader.GetInt64(this.eventsTable.EventCounter);
						DateTime dateTime = reader.GetDateTime(this.eventsTable.CreateTime);
						e = new EventEntry(@int, dateTime);
					}
					else if (num == 1L)
					{
						e = new EventEntry(0L, DateTime.UtcNow);
					}
					else
					{
						DiagnosticContext.TraceDword((LID)41504U, (uint)((ulong)num >> 32));
						DiagnosticContext.TraceDword((LID)53792U, (uint)num);
						errorCode = ErrorCode.CreateEventNotFound((LID)42069U);
						e = null;
					}
				}
			}
			if (flag && e != null)
			{
				long num2 = e.EventCounter;
				DateTime createTime = e.CreateTime;
				if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "ReadLastEvent() is scanning for event counter gaps beginning from counter value {0} (effectiveUpperBound={1}).", num2, num);
				}
				startKey = new StartStopKey(false, new object[]
				{
					num2
				});
				empty = StartStopKey.Empty;
				using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, this.lastEventFetchList, null, null, 0, 0, new KeyRange(startKey, empty), false, true))
				{
					using (Reader reader2 = tableOperator2.ExecuteReader(false))
					{
						try
						{
							if (context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
							{
								context.BeginTransactionIfNeeded();
							}
							int num3 = 0;
							while (reader2.Read())
							{
								if (++num3 % 128 == 0 && context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
								{
									context.Commit();
									context.BeginTransactionIfNeeded();
								}
								long int2 = reader2.GetInt64(this.eventsTable.EventCounter);
								if (int2 != num2 + 1L)
								{
									if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long, long>(0L, "ReadLastEvent() stopped before reaching the end of the EventHistory table because there was a counter gap between {0} and {1} (eventCounterUpperBound={2}).", num2, int2, num);
										break;
									}
									break;
								}
								else
								{
									num2 = int2;
									createTime = reader2.GetDateTime(this.eventsTable.CreateTime);
								}
							}
							if (num3 > 0)
							{
								e = new EventEntry(num2, createTime);
							}
						}
						finally
						{
							context.Commit();
						}
					}
				}
				this.UpdateEventCounterUpperBoundAfterGapScan(context, e.EventCounter);
			}
			if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ReadEventsTracer.TraceDebug(0L, "ReadLastEvent(): isPassive={0}, eventCounter={1}, createTime={2}, ec={3}, effectiveUpperBound={4}", new object[]
				{
					flag,
					(e != null) ? e.EventCounter : 0L,
					(e != null) ? e.CreateTime : DateTime.MinValue,
					errorCode,
					num
				});
			}
			return errorCode;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0003A054 File Offset: 0x00038254
		private bool IsReadingEventsFromPassive(Context context)
		{
			bool flag = context.Database.IsSharedLockHeld() || context.Database.IsExclusiveLockHeld();
			return EventHistory.simulateReadEventsFromPassiveTestHook.Value || (flag && context.Database.IsOnlinePassiveAttachedReadOnly);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0003A09C File Offset: 0x0003829C
		internal long GetPersistedEventCounterBound(Context context, bool upperBound)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(context.Database);
			Column column = upperBound ? globalsTable.EventCounterUpperBound : globalsTable.EventCounterLowerBound;
			long @int;
			using (TableOperator globalsTableRow = GlobalsTableHelper.GetGlobalsTableRow(context, new Column[]
			{
				column
			}))
			{
				using (Reader reader = globalsTableRow.ExecuteReader(false))
				{
					reader.Read();
					@int = reader.GetInt64(column);
				}
			}
			return @int;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0003A12C File Offset: 0x0003832C
		private void UpdateEventCounterUpperBoundAfterGapScan(Context context, long endCounter)
		{
			if (endCounter + 1L > this.eventCounterUpperBound)
			{
				using (LockManager.Lock(this, LockManager.LockType.EventCounterBounds, context.Diagnostics))
				{
					if (endCounter + 1L > this.eventCounterUpperBound)
					{
						if (ExTraceGlobals.ReadEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ReadEventsTracer.TraceDebug<long, long>(0L, "The event counter upper bound will be updated from {0} to {1} after scanning events and determining that there were no counter gaps between the two values.", this.eventCounterUpperBound, endCounter + 1L);
						}
						this.eventCounterUpperBound = endCounter + 1L;
					}
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0003A1B4 File Offset: 0x000383B4
		public ErrorCode WriteEvents(Context context, List<EventEntry> events, out List<long> eventCounters)
		{
			eventCounters = new List<long>(events.Count);
			if (events.Count != 0)
			{
				for (int i = 0; i < events.Count; i++)
				{
					EventEntry eventEntry = events[i];
					int value;
					if (eventEntry.MailboxNumber == null)
					{
						if (!MailboxStateCache.TryGetMailboxNumber(context, eventEntry.MailboxGuid.Value, true, out value))
						{
							return ErrorCode.CreateUnknownMailbox((LID)44056U);
						}
					}
					else
					{
						value = eventEntry.MailboxNumber.Value;
					}
					long item;
					this.InsertOneEvent(context, eventEntry.TransactionId, eventEntry.EventType, value, eventEntry.ObjectClass, eventEntry.Fid24, eventEntry.Mid24, eventEntry.ParentFid24, eventEntry.OldFid24, eventEntry.OldMid24, eventEntry.OldParentFid24, eventEntry.ItemCount, eventEntry.UnreadCount, eventEntry.Flags, eventEntry.ExtendedFlags, eventEntry.ClientType, eventEntry.Sid, null, out item);
					eventCounters.Add(item);
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0003A2C0 File Offset: 0x000384C0
		public ErrorCode SaveWatermarks(Context context, List<EventWatermark> watermarks)
		{
			ErrorCode result = ErrorCode.NoError;
			if (watermarks != null && watermarks.Count > 0)
			{
				bool flag = false;
				for (int i = 1; i < watermarks.Count; i++)
				{
					if (watermarks[i].ConsumerGuid != watermarks[0].ConsumerGuid)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableExclusive, context.Diagnostics))
					{
						return this.WriteWatermarks(context, watermarks);
					}
				}
				using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableShared, context.Diagnostics))
				{
					using (LockManager.Lock(EventHistory.WatermarksConsumerLockName(this.database.MdbGuid, watermarks[0].ConsumerGuid), LockManager.LockType.WatermarkConsumer, context.Diagnostics))
					{
						result = this.WriteWatermarks(context, watermarks);
					}
				}
			}
			return result;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003A3EC File Offset: 0x000385EC
		public ErrorCode ReadWatermarksForMailbox(Context context, Guid mailboxGuid, out List<EventWatermark> watermarks)
		{
			ErrorCode result = ErrorCode.NoError;
			int estimateNumber = 10;
			int value = 0;
			if (mailboxGuid != Guid.Empty && !MailboxStateCache.TryGetMailboxNumber(context, mailboxGuid, true, out value))
			{
				watermarks = new List<EventWatermark>(0);
				return ErrorCode.NoError;
			}
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableExclusive, context.Diagnostics))
			{
				result = this.ReadWatermarks(context, new int?(value), null, estimateNumber, out watermarks);
			}
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003A47C File Offset: 0x0003867C
		public ErrorCode ReadWatermarksForMailboxForTest(Context context, int mailboxNumber, out List<EventWatermark> watermarks)
		{
			ErrorCode result;
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableExclusive, context.Diagnostics))
			{
				result = this.ReadWatermarks(context, new int?(mailboxNumber), null, 10, out watermarks);
			}
			return result;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0003A4D8 File Offset: 0x000386D8
		public ErrorCode ReadWatermarksForConsumer(Context context, Guid consumerGuid, Guid? mailboxGuid, out List<EventWatermark> watermarks)
		{
			ErrorCode result = ErrorCode.NoError;
			int estimateNumber = 100;
			int? mailboxNumber = null;
			if (mailboxGuid != null)
			{
				int value = 0;
				if (mailboxGuid != Guid.Empty && !MailboxStateCache.TryGetMailboxNumber(context, mailboxGuid.Value, true, out value))
				{
					watermarks = new List<EventWatermark>(0);
					return ErrorCode.NoError;
				}
				mailboxNumber = new int?(value);
				estimateNumber = 1;
			}
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableShared, context.Diagnostics))
			{
				using (LockManager.Lock(EventHistory.WatermarksConsumerLockName(this.database.MdbGuid, consumerGuid), LockManager.LockType.WatermarkConsumer, context.Diagnostics))
				{
					result = this.ReadWatermarks(context, mailboxNumber, new Guid?(consumerGuid), estimateNumber, out watermarks);
				}
			}
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003A5D8 File Offset: 0x000387D8
		public ErrorCode DeleteWatermarksForMailbox(Context context, Guid mailboxGuid, out uint deletedCount)
		{
			ErrorCode noError = ErrorCode.NoError;
			int mailboxNumber = 0;
			if (mailboxGuid != Guid.Empty && !MailboxStateCache.TryGetMailboxNumber(context, mailboxGuid, true, out mailboxNumber))
			{
				deletedCount = 0U;
				return ErrorCode.NoError;
			}
			this.DeleteWatermarksForMailbox(context, mailboxNumber, out deletedCount);
			return noError;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003A61C File Offset: 0x0003881C
		public void DeleteWatermarksForMailbox(Context context, int mailboxNumber, out uint deletedCount)
		{
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableExclusive, context.Diagnostics))
			{
				this.DeleteWatermarks(context, new int?(mailboxNumber), null, out deletedCount);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0003A674 File Offset: 0x00038874
		public ErrorCode DeleteWatermarksForConsumer(Context context, Guid consumerGuid, Guid? mailboxGuid, out uint deletedCount)
		{
			ErrorCode noError = ErrorCode.NoError;
			int? mailboxNumber = null;
			if (mailboxGuid != null)
			{
				int value = 0;
				if (mailboxGuid != Guid.Empty && !MailboxStateCache.TryGetMailboxNumber(context, mailboxGuid.Value, true, out value))
				{
					deletedCount = 0U;
					return ErrorCode.NoError;
				}
				mailboxNumber = new int?(value);
			}
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableShared, context.Diagnostics))
			{
				using (LockManager.Lock(EventHistory.WatermarksConsumerLockName(this.database.MdbGuid, consumerGuid), LockManager.LockType.WatermarkConsumer, context.Diagnostics))
				{
					this.DeleteWatermarks(context, mailboxNumber, new Guid?(consumerGuid), out deletedCount);
				}
			}
			return noError;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0003A768 File Offset: 0x00038968
		public static void EventHistoryCleanupMaintenance(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			EventHistory eventHistory = EventHistory.GetEventHistory(context.Database);
			if (!eventHistory.CleanupEventHistory(context, databaseInfo.EventHistoryRetentionPeriod))
			{
				completed = false;
				return;
			}
			eventHistory.CleanupWatermarks(context);
			completed = true;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003A7A0 File Offset: 0x000389A0
		private ErrorCode WriteWatermarks(Context context, List<EventWatermark> watermarks)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			long effectiveUpperBound = this.eventCounterUpperBound;
			try
			{
				for (int i = 0; i < watermarks.Count; i++)
				{
					EventWatermark eventWatermark = watermarks[i];
					int num;
					if (!this.WatermarkValid(context, ref eventWatermark, effectiveUpperBound, out num))
					{
						if (errorCode == ErrorCode.NoError)
						{
							errorCode = ErrorCode.CreatePartialCompletion((LID)58453U);
						}
					}
					else
					{
						DataRow dataRow = null;
						try
						{
							dataRow = Factory.OpenDataRow(context.Culture, context, this.watermarksTable.Table, true, new ColumnValue[]
							{
								new ColumnValue(this.watermarksTable.MailboxNumber, num),
								new ColumnValue(this.watermarksTable.ConsumerGuid, eventWatermark.ConsumerGuid)
							});
							if (dataRow != null)
							{
								dataRow.SetValue(context, this.watermarksTable.EventCounter, eventWatermark.EventCounter);
							}
							else
							{
								dataRow = Factory.CreateDataRow(context.Culture, context, this.watermarksTable.Table, true, new ColumnValue[]
								{
									new ColumnValue(this.watermarksTable.MailboxNumber, num),
									new ColumnValue(this.watermarksTable.ConsumerGuid, eventWatermark.ConsumerGuid),
									new ColumnValue(this.watermarksTable.EventCounter, eventWatermark.EventCounter)
								});
							}
							dataRow.Flush(context);
						}
						finally
						{
							if (dataRow != null)
							{
								dataRow.Dispose();
							}
						}
					}
				}
				context.Commit();
			}
			finally
			{
				if (context.TransactionStarted)
				{
					context.Abort();
				}
			}
			return errorCode;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		private bool WatermarkValid(Context context, ref EventWatermark watermark, long effectiveUpperBound, out int mailboxNumber)
		{
			mailboxNumber = 0;
			return watermark.EventCounter <= effectiveUpperBound && (!(watermark.MailboxGuid != Guid.Empty) || MailboxStateCache.TryGetMailboxNumber(context, watermark.MailboxGuid, true, out mailboxNumber));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0003A9DC File Offset: 0x00038BDC
		private ErrorCode ReadWatermarks(Context context, int? mailboxNumber, Guid? consumerGuid, int estimateNumber, out List<EventWatermark> watermarks)
		{
			watermarks = new List<EventWatermark>(estimateNumber);
			StartStopKey empty = StartStopKey.Empty;
			SearchCriteriaCompare restriction = null;
			if (consumerGuid != null)
			{
				if (mailboxNumber != null)
				{
					empty = new StartStopKey(true, new object[]
					{
						consumerGuid.Value,
						mailboxNumber.Value
					});
				}
				else
				{
					empty = new StartStopKey(true, new object[]
					{
						consumerGuid.Value
					});
				}
			}
			else
			{
				restriction = Factory.CreateSearchCriteriaCompare(this.watermarksTable.MailboxNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailboxNumber));
			}
			try
			{
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.watermarksTable.Table, this.watermarksTable.Table.PrimaryKeyIndex, this.watermarksFetchList, restriction, null, 0, 0, new KeyRange(empty, empty), false, false))
				{
					using (SortOperator sortOperator = this.CreateWatermarkTableSortOperatorIfNeeded(context, tableOperator, mailboxNumber, consumerGuid))
					{
						SimpleQueryOperator simpleQueryOperator = (sortOperator != null) ? sortOperator : tableOperator;
						using (Reader reader = simpleQueryOperator.ExecuteReader(false))
						{
							while (reader.Read())
							{
								Guid guid = reader.GetGuid(this.watermarksTable.ConsumerGuid);
								int @int = reader.GetInt32(this.watermarksTable.MailboxNumber);
								long int2 = reader.GetInt64(this.watermarksTable.EventCounter);
								Guid mailboxGuid = Guid.Empty;
								if (@int != 0)
								{
									MailboxState mailboxState = MailboxStateCache.Get(context, @int);
									if (mailboxState == null || mailboxState.IsDeleted || mailboxState.IsDisconnected)
									{
										continue;
									}
									mailboxGuid = mailboxState.MailboxGuid;
								}
								EventWatermark item = new EventWatermark(mailboxGuid, guid, int2);
								watermarks.Add(item);
							}
						}
					}
				}
				context.Commit();
			}
			finally
			{
				if (context.TransactionStarted)
				{
					context.Abort();
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003AC24 File Offset: 0x00038E24
		private SortOperator CreateWatermarkTableSortOperatorIfNeeded(IConnectionProvider connectionProvider, TableOperator tableOperator, int? mailboxNumber, Guid? consumerGuid)
		{
			if (mailboxNumber != null && consumerGuid != null)
			{
				return null;
			}
			SortOrder sortOrder = new SortOrder(new Column[]
			{
				this.watermarksTable.EventCounter,
				this.watermarksTable.ConsumerGuid,
				this.watermarksTable.MailboxNumber
			}, new bool[]
			{
				true,
				true,
				true
			});
			return Factory.CreateSortOperator(tableOperator.Culture, connectionProvider, tableOperator, 0, 0, sortOrder, KeyRange.AllRows, false, false);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		private void DeleteWatermarks(Context context, int? mailboxNumber, Guid? consumerGuid, out uint deletedCount)
		{
			deletedCount = 0U;
			StartStopKey empty = StartStopKey.Empty;
			StartStopKey empty2 = StartStopKey.Empty;
			SearchCriteriaCompare restriction = null;
			if (consumerGuid != null)
			{
				if (mailboxNumber != null)
				{
					empty = new StartStopKey(true, new object[]
					{
						consumerGuid.Value,
						mailboxNumber.Value
					});
				}
				else
				{
					empty = new StartStopKey(true, new object[]
					{
						consumerGuid.Value
					});
				}
				empty2 = new StartStopKey(true, new object[]
				{
					consumerGuid.Value
				});
			}
			if (mailboxNumber != null)
			{
				restriction = Factory.CreateSearchCriteriaCompare(this.watermarksTable.MailboxNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailboxNumber));
			}
			try
			{
				using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, this.watermarksTable.Table, this.watermarksTable.Table.PrimaryKeyIndex, null, restriction, null, 0, 0, new KeyRange(empty, empty2), false, false), false))
				{
					deletedCount = (uint)((int)deleteOperator.ExecuteScalar());
				}
				context.Commit();
			}
			finally
			{
				if (context.TransactionStarted)
				{
					context.Abort();
				}
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0003AE0C File Offset: 0x0003900C
		internal void InsertOneEvent(Context context, int transactionId, EventType eventType, int mailboxNumber, string objectClass, byte[] fid24, byte[] mid24, byte[] parentFid24, byte[] oldFid24, byte[] oldMid24, byte[] oldParentFid24, int? itemCount, int? unreadCount, EventFlags flags, ExtendedEventFlags? extendedFlags, ClientType clientType, byte[] sid, int? documentId, out long eventCounter)
		{
			bool flag = false;
			if (objectClass != null && objectClass.Length > this.eventsTable.ObjectClass.MaxLength)
			{
				objectClass = objectClass.Substring(0, this.eventsTable.ObjectClass.MaxLength);
				flag = true;
			}
			if (transactionId == 0)
			{
				transactionId = context.GetConnection().TransactionId;
			}
			MailboxState mailboxState = MailboxStateCache.Get(context, mailboxNumber);
			if (mailboxState.IsMailboxLocked())
			{
				InTransitStatus inTransitStatus = InTransitInfo.GetInTransitStatus(mailboxState);
				if ((inTransitStatus & InTransitStatus.DirectionMask) == InTransitStatus.DestinationOfMove && (eventType & (EventType.MailboxMoveStarted | EventType.MailboxMoveSucceeded | EventType.MailboxMoveFailed)) == (EventType)0)
				{
					if (extendedFlags == null)
					{
						extendedFlags = new ExtendedEventFlags?(ExtendedEventFlags.None);
					}
					extendedFlags = new ExtendedEventFlags?(extendedFlags.Value | ExtendedEventFlags.MoveDestination);
				}
			}
			switch (mailboxState.MailboxType)
			{
			case MailboxInfo.MailboxType.Private:
				break;
			case MailboxInfo.MailboxType.PublicFolderPrimary:
			case MailboxInfo.MailboxType.PublicFolderSecondary:
				extendedFlags = new ExtendedEventFlags?((extendedFlags != null) ? (extendedFlags.Value | ExtendedEventFlags.PublicFolderMailbox) : ExtendedEventFlags.PublicFolderMailbox);
				break;
			default:
				throw new StoreException((LID)45992U, ErrorCodeValue.InvalidParameter, "Invalid mailbox type");
			}
			ColumnValue[] array = new ColumnValue[18];
			array[0] = new ColumnValue(this.eventsTable.CreateTime, DateTime.UtcNow);
			array[1] = new ColumnValue(this.eventsTable.TransactionId, transactionId);
			array[2] = new ColumnValue(this.eventsTable.EventType, SerializedValue.GetBoxedInt32((int)eventType));
			array[3] = new ColumnValue(this.eventsTable.MailboxNumber, mailboxNumber);
			array[4] = new ColumnValue(this.eventsTable.ObjectClass, objectClass);
			array[5] = new ColumnValue(this.eventsTable.Fid, fid24);
			array[6] = new ColumnValue(this.eventsTable.Mid, mid24);
			array[7] = new ColumnValue(this.eventsTable.ParentFid, parentFid24);
			array[8] = new ColumnValue(this.eventsTable.OldFid, oldFid24);
			array[9] = new ColumnValue(this.eventsTable.OldMid, oldMid24);
			array[10] = new ColumnValue(this.eventsTable.OldParentFid, oldParentFid24);
			array[11] = new ColumnValue(this.eventsTable.ItemCount, itemCount);
			array[12] = new ColumnValue(this.eventsTable.UnreadCount, unreadCount);
			array[13] = new ColumnValue(this.eventsTable.Flags, SerializedValue.GetBoxedInt32((int)flags));
			ColumnValue[] array2 = array;
			int num = 14;
			Column extendedFlags2 = this.eventsTable.ExtendedFlags;
			ExtendedEventFlags? extendedEventFlags = extendedFlags;
			array2[num] = new ColumnValue(extendedFlags2, (extendedEventFlags != null) ? new long?((long)extendedEventFlags.GetValueOrDefault()) : null);
			array[15] = new ColumnValue(this.eventsTable.ClientType, SerializedValue.GetBoxedInt32((int)clientType));
			array[16] = new ColumnValue(this.eventsTable.Sid, sid);
			array[17] = new ColumnValue(this.eventsTable.DocumentId, documentId);
			ColumnValue[] columnValues = array;
			bool flag2 = false;
			if (context.EventHistoryUncommittedTransactionLink != null)
			{
				LinkedListNode<EventHistory.UncommittedListEntry> linkedListNode = context.EventHistoryUncommittedTransactionLink as LinkedListNode<EventHistory.UncommittedListEntry>;
				eventCounter = this.InsertEventHistoryRecord(context, columnValues);
				flag2 = true;
				linkedListNode.Value.LastCounter = eventCounter;
				if (EventHistory.eventCounterAllocatedTestHook.Value != null)
				{
					EventHistory.eventCounterAllocatedTestHook.Value(mailboxNumber, eventType, eventCounter);
				}
			}
			else
			{
				eventCounter = 0L;
			}
			using (LockManager.Lock(this, LockManager.LockType.EventCounterBounds, context.Diagnostics))
			{
				if (!flag2)
				{
					bool flag3 = this.eventCounterAllocationPotentiallyLost;
					this.eventCounterAllocationPotentiallyLost = true;
					eventCounter = this.InsertEventHistoryRecord(context, columnValues);
					this.lastEventCounter = eventCounter;
					LinkedListNode<EventHistory.UncommittedListEntry> eventHistoryUncommittedTransactionLink = this.uncommittedList.AddLast(new EventHistory.UncommittedListEntry(context, eventCounter));
					context.EventHistoryUncommittedTransactionLink = eventHistoryUncommittedTransactionLink;
					context.RegisterStateObject(this);
					if (flag3)
					{
						this.flushEventCounterUpperBoundTaskManager.HandlePotentiallyLostEventCounterAllocation(context, this);
					}
					this.eventCounterAllocationPotentiallyLost = false;
				}
				else if (eventCounter > this.lastEventCounter)
				{
					this.lastEventCounter = eventCounter;
				}
			}
			if (EventHistory.eventCounterAllocatedTestHook.Value != null && !flag2)
			{
				EventHistory.eventCounterAllocatedTestHook.Value(mailboxNumber, eventType, eventCounter);
			}
			if (ExTraceGlobals.EventsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append("Event Registered: eventCounter:[");
				stringBuilder.Append(eventCounter);
				stringBuilder.Append("] eventType:[");
				stringBuilder.Append(eventType);
				stringBuilder.Append("] Database:[");
				stringBuilder.Append(context.Database.MdbGuid);
				stringBuilder.Append("] mailboxNumber:[");
				stringBuilder.AppendAsString(mailboxNumber);
				stringBuilder.Append("] transactionId:[");
				stringBuilder.Append(transactionId);
				stringBuilder.Append("] fid:[");
				stringBuilder.AppendAsString(fid24);
				stringBuilder.Append("] mid:[");
				stringBuilder.AppendAsString(mid24);
				stringBuilder.Append("] parentFid:[");
				stringBuilder.AppendAsString(parentFid24);
				stringBuilder.Append("] oldFid:[");
				stringBuilder.AppendAsString(oldFid24);
				stringBuilder.Append("] oldMid:[");
				stringBuilder.AppendAsString(oldMid24);
				stringBuilder.Append("] oldParentFid:[");
				stringBuilder.AppendAsString(oldParentFid24);
				stringBuilder.Append("] objectClass:[");
				stringBuilder.AppendAsString(objectClass);
				if (flag)
				{
					stringBuilder.Append("...");
				}
				stringBuilder.Append("] itemCount:[");
				stringBuilder.AppendAsString(itemCount);
				stringBuilder.Append("] unreadCount:[");
				stringBuilder.AppendAsString(unreadCount);
				stringBuilder.Append("] flags:[");
				stringBuilder.Append(flags);
				stringBuilder.Append("] extendedFlags:[");
				stringBuilder.AppendAsString(extendedFlags);
				stringBuilder.Append("] clientType:[");
				stringBuilder.Append(clientType);
				stringBuilder.Append("] sid:[");
				stringBuilder.AppendAsString(sid);
				stringBuilder.Append("] documentId:[");
				stringBuilder.AppendAsString(documentId);
				stringBuilder.Append("]");
				ExTraceGlobals.EventsTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003B4E4 File Offset: 0x000396E4
		private long InsertEventHistoryRecord(Context context, ColumnValue[] columnValues)
		{
			long num;
			using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, this.eventsTable.Table, true, columnValues))
			{
				dataRow.Flush(context);
				num = (long)dataRow.GetValue(context, this.eventsTable.EventCounter);
				if (EventHistory.insertedEventHistoryRecordTestHook.Value != null)
				{
					EventHistory.insertedEventHistoryRecordTestHook.Value(num);
				}
			}
			return num;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0003B564 File Offset: 0x00039764
		void IStateObject.OnBeforeCommit(Context context)
		{
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0003B566 File Offset: 0x00039766
		void IStateObject.OnCommit(Context context)
		{
			this.EndTransactionHandler(context, true);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0003B570 File Offset: 0x00039770
		void IStateObject.OnAbort(Context context)
		{
			this.EndTransactionHandler(context, false);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0003B57C File Offset: 0x0003977C
		private void EndTransactionHandler(Context context, bool committed)
		{
			LinkedListNode<EventHistory.UncommittedListEntry> linkedListNode = context.EventHistoryUncommittedTransactionLink as LinkedListNode<EventHistory.UncommittedListEntry>;
			bool flag = false;
			bool flag2 = false;
			using (LockManager.Lock(this, LockManager.LockType.EventCounterBounds, context.Diagnostics))
			{
				long lastCounter = linkedListNode.Value.LastCounter;
				if (committed && lastCounter > this.highestCommittedEventCounter)
				{
					this.highestCommittedEventCounter = lastCounter;
					flag = true;
				}
				if (linkedListNode == this.uncommittedList.First)
				{
					long num;
					if (linkedListNode.Next != null && linkedListNode.Next.Value.FirstCounter < this.highestCommittedEventCounter)
					{
						num = linkedListNode.Next.Value.FirstCounter;
					}
					else
					{
						num = this.highestCommittedEventCounter + 1L;
					}
					if (this.eventCounterUpperBound != num)
					{
						this.eventCounterUpperBound = num;
						flag2 = true;
					}
				}
				this.uncommittedList.Remove(linkedListNode);
				this.flushEventCounterUpperBoundTaskManager.EndTransactionHandler(context, this, lastCounter, committed);
			}
			context.EventHistoryUncommittedTransactionLink = null;
			if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.EventCounterBoundsTracer.TraceDebug(0L, "Transaction {0}: highestCommittedEventCounter={1} ({2} updated), eventCounterUpperBound={3} ({4} updated)", new object[]
				{
					committed ? "committed" : "rolled back",
					this.highestCommittedEventCounter,
					flag ? "was" : "was not",
					this.eventCounterUpperBound,
					flag2 ? "was" : "was not"
				});
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0003B6EC File Offset: 0x000398EC
		private long FindCleanupBoundary(Context context, TimeSpan retentionPeriod)
		{
			long num = this.eventCounterLowerBound;
			long @int = this.eventCounterUpperBound;
			StartStopKey startStopKey = new StartStopKey(false, new object[]
			{
				@int
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, new Column[]
			{
				this.eventsTable.EventCounter
			}, null, null, 0, 1, new KeyRange(startStopKey, StartStopKey.Empty), true, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (!reader.Read())
					{
						return num;
					}
					@int = reader.GetInt64(this.eventsTable.EventCounter);
					startStopKey = new StartStopKey(false, new object[]
					{
						@int
					});
				}
			}
			DateTime value = DateTime.UtcNow.Subtract(retentionPeriod);
			Column[] columnsToFetch = new Column[]
			{
				this.eventsTable.CreateTime
			};
			long num2 = num;
			long num3 = @int - 1L;
			while (num2 <= num3)
			{
				long num4 = num2 + (num3 - num2 >> 1);
				int num5 = 1;
				StartStopKey startKey = new StartStopKey(true, new object[]
				{
					num4
				});
				using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, new KeyRange(startKey, startStopKey), false, true))
				{
					using (Reader reader2 = tableOperator2.ExecuteReader(false))
					{
						if (reader2.Read())
						{
							num5 = reader2.GetDateTime(this.eventsTable.CreateTime).CompareTo(value);
						}
					}
				}
				if (num5 < 0)
				{
					num2 = num4 + 1L;
				}
				else
				{
					num3 = num4 - 1L;
				}
			}
			return num2;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0003B91C File Offset: 0x00039B1C
		internal bool CleanupEventHistory(Context context, TimeSpan retentionPeriod)
		{
			long num = this.FindCleanupBoundary(context, retentionPeriod);
			using (LockManager.Lock(this, LockManager.LockType.EventCounterBounds, context.Diagnostics))
			{
				if (this.eventCounterLowerBound < num)
				{
					this.eventCounterLowerBound = num;
					if (AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database))
					{
						GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(context.Database);
						GlobalsTableHelper.UpdateGlobalsTableRow(context, new Column[]
						{
							globalsTable.EventCounterLowerBound,
							globalsTable.EventCounterUpperBound
						}, new object[]
						{
							num,
							this.eventCounterUpperBound
						});
					}
				}
			}
			if (this.readerCount != 0)
			{
				return false;
			}
			if (num != 0L)
			{
				StartStopKey startKey = new StartStopKey(true, new object[]
				{
					0L
				});
				StartStopKey stopKey = new StartStopKey(false, new object[]
				{
					num
				});
				using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, this.eventsTable.Table, this.eventsTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startKey, stopKey), false, false), false))
				{
					deleteOperator.EnableInterrupts(new WriteChunkingInterruptControl(EventHistory.eventHistoryCleanupChunkSize.Value, null));
					int obj = (int)deleteOperator.ExecuteScalar();
					while (deleteOperator.Interrupted)
					{
						if (EventHistory.eventHistoryCleanupRowsDeletedTestHook.Value != null)
						{
							EventHistory.eventHistoryCleanupRowsDeletedTestHook.Value(obj);
						}
						context.Commit();
						if (MaintenanceHandler.ShouldStopDatabaseMaintenanceTask(context, EventHistory.EventHistoryCleanupMaintenanceId))
						{
							return false;
						}
						obj = (int)deleteOperator.ExecuteScalar();
					}
					if (EventHistory.eventHistoryCleanupRowsDeletedTestHook.Value != null)
					{
						EventHistory.eventHistoryCleanupRowsDeletedTestHook.Value(obj);
					}
					context.Commit();
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0003BB18 File Offset: 0x00039D18
		internal void CleanupWatermarks(Context context)
		{
			using (LockManager.Lock(this.watermarkTableLockName, LockManager.LockType.WatermarkTableExclusive, context.Diagnostics))
			{
				SearchCriteriaCompare restriction = Factory.CreateSearchCriteriaCompare(this.watermarksTable.EventCounter, SearchCriteriaCompare.SearchRelOp.LessThan, Factory.CreateConstantColumn(this.eventCounterLowerBound));
				try
				{
					using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, this.watermarksTable.Table, this.watermarksTable.Table.PrimaryKeyIndex, null, restriction, null, 0, 0, KeyRange.AllRows, false, false), false))
					{
						deleteOperator.ExecuteScalar();
					}
					context.Commit();
				}
				finally
				{
					if (context.TransactionStarted)
					{
						context.Abort();
					}
				}
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0003BC00 File Offset: 0x00039E00
		private ErrorCode VerifyRestriction(Restriction restriction)
		{
			if (restriction is RestrictionProperty)
			{
				RestrictionProperty restrictionProperty = restriction as RestrictionProperty;
				if (restrictionProperty.PropertyTag != PropTag.Event.EventMailboxGuid || restrictionProperty.Operator != RelationOperator.Equal)
				{
					return ErrorCode.CreateTooComplex((LID)60440U);
				}
			}
			else if (restriction is RestrictionBitmask)
			{
				RestrictionBitmask restrictionBitmask = restriction as RestrictionBitmask;
				if (restrictionBitmask.PropertyTag != PropTag.Event.EventMask || restrictionBitmask.Operation == BitmaskOperation.EqualToZero)
				{
					return ErrorCode.CreateTooComplex((LID)35864U);
				}
			}
			else
			{
				if (!(restriction is RestrictionAND))
				{
					return ErrorCode.CreateTooComplex((LID)42008U);
				}
				RestrictionAND restrictionAND = restriction as RestrictionAND;
				if (restrictionAND.NestedRestrictions.Length != 2)
				{
					return ErrorCode.CreateTooComplex((LID)52248U);
				}
				bool flag = false;
				bool flag2 = false;
				foreach (Restriction restriction2 in restrictionAND.NestedRestrictions)
				{
					if (restriction2 is RestrictionProperty)
					{
						RestrictionProperty restrictionProperty2 = restriction2 as RestrictionProperty;
						if ((restrictionProperty2.PropertyTag != PropTag.Event.EventMailboxGuid && restrictionProperty2.PropertyTag != PropTag.Event.EventMask) || restrictionProperty2.Operator != RelationOperator.Equal)
						{
							return ErrorCode.CreateTooComplex((LID)46104U);
						}
						if (restrictionProperty2.PropertyTag == PropTag.Event.EventMailboxGuid)
						{
							if (flag2)
							{
								return ErrorCode.CreateTooComplex((LID)39960U);
							}
							flag2 = true;
						}
						else if (restrictionProperty2.PropertyTag == PropTag.Event.EventMask)
						{
							if (flag)
							{
								return ErrorCode.CreateTooComplex((LID)56344U);
							}
							flag = true;
						}
					}
					else
					{
						if (!(restriction2 is RestrictionBitmask))
						{
							return ErrorCode.CreateTooComplex((LID)54296U);
						}
						RestrictionBitmask restrictionBitmask2 = restriction2 as RestrictionBitmask;
						if (restrictionBitmask2.PropertyTag != PropTag.Event.EventMask || restrictionBitmask2.Operation == BitmaskOperation.EqualToZero)
						{
							return ErrorCode.CreateTooComplex((LID)62488U);
						}
						if (flag)
						{
							return ErrorCode.CreateTooComplex((LID)37912U);
						}
						flag = true;
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
		private SearchCriteria FixEventReadCriteria(Context context, SearchCriteria criteria)
		{
			criteria = criteria.InspectAndFix(delegate(SearchCriteria inspectCriteria, CompareInfo compareInfo)
			{
				SearchCriteriaCompare searchCriteriaCompare = inspectCriteria as SearchCriteriaCompare;
				if (searchCriteriaCompare != null)
				{
					PropertyColumn propertyColumn = searchCriteriaCompare.Lhs as PropertyColumn;
					if (propertyColumn != null && propertyColumn.StorePropTag == PropTag.Event.EventMailboxGuid)
					{
						if (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal || searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.NotEqual)
						{
							ConstantColumn constantColumn = searchCriteriaCompare.Rhs as ConstantColumn;
							if (constantColumn != null)
							{
								byte[] array = (byte[])constantColumn.Value;
								if (array.Length != 16)
								{
									throw new StoreException((LID)43000U, ErrorCodeValue.InvalidParameter, "invalid mailbox GUID value");
								}
								int num = 0;
								Guid guid = ParseSerialize.GetGuid(array, ref num, array.Length);
								int mailboxNumber;
								MailboxState mailboxState;
								if (MailboxStateCache.TryGetMailboxNumber(context, guid, false, out mailboxNumber) && (mailboxState = MailboxStateCache.Get(context, mailboxNumber)) != null && !mailboxState.IsDeleted)
								{
									return Factory.CreateSearchCriteriaCompare(this.eventsTable.MailboxNumber, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(mailboxState.MailboxNumber));
								}
								if (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
								{
									return Factory.CreateSearchCriteriaFalse();
								}
								return Factory.CreateSearchCriteriaTrue();
							}
						}
						throw new StoreException((LID)59384U, ErrorCodeValue.TooComplex, criteria.ToString());
					}
				}
				return inspectCriteria;
			}, null, true);
			criteria.EnumerateColumns(delegate(Column column, object state)
			{
				if (!(column is PhysicalColumn) && !(column is ConstantColumn) && (!(column is MappedPropertyColumn) || !(column.ActualColumn is PhysicalColumn)))
				{
					throw new StoreException((LID)34808U, ErrorCodeValue.TooComplex, state.ToString());
				}
			}, criteria);
			return criteria;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0003C03F File Offset: 0x0003A23F
		private static LockName<Guid> WatermarkTableLockName(Guid databaseGuid)
		{
			return new LockName<Guid>(databaseGuid, LockManager.LockLevel.WatermarkTable);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0003C049 File Offset: 0x0003A249
		private static LockName<Guid, Guid> WatermarksConsumerLockName(Guid databaseGuid, Guid consumerGuid)
		{
			return new LockName<Guid, Guid>(databaseGuid, consumerGuid, LockManager.LockLevel.WatermarkConsumer);
		}

		// Token: 0x0400034F RID: 847
		private const int DefaultEventHistoryCleanupChunkSize = 1000;

		// Token: 0x04000350 RID: 848
		private const int NumEventsToReadPerTransaction = 128;

		// Token: 0x04000351 RID: 849
		public static readonly Guid EventHistoryCleanupMaintenanceId = new Guid("{9a0932ca-268a-4a60-b90e-fa9335a2f139}");

		// Token: 0x04000352 RID: 850
		private static IDatabaseMaintenance eventHistoryCleanupMaintenance;

		// Token: 0x04000353 RID: 851
		private static Hookable<int> eventHistoryCleanupChunkSize = Hookable<int>.Create(true, 1000);

		// Token: 0x04000354 RID: 852
		private static Hookable<Action<int>> eventHistoryCleanupRowsDeletedTestHook = Hookable<Action<int>>.Create(true, null);

		// Token: 0x04000355 RID: 853
		private static Action readerRaceTestHook = null;

		// Token: 0x04000356 RID: 854
		private static readonly Hookable<bool> simulateReadEventsFromPassiveTestHook = Hookable<bool>.Create(true, false);

		// Token: 0x04000357 RID: 855
		private static readonly Hookable<Action<long>> insertedEventHistoryRecordTestHook = Hookable<Action<long>>.Create(true, null);

		// Token: 0x04000358 RID: 856
		private static readonly Hookable<Action<int, EventType, long>> eventCounterAllocatedTestHook = Hookable<Action<int, EventType, long>>.Create(true, null);

		// Token: 0x04000359 RID: 857
		private readonly Column[] eventsFetchList;

		// Token: 0x0400035A RID: 858
		private readonly Column[] lastEventFetchList;

		// Token: 0x0400035B RID: 859
		private readonly Column[] watermarksFetchList;

		// Token: 0x0400035C RID: 860
		private StoreDatabase database;

		// Token: 0x0400035D RID: 861
		private Guid mdbVersionGuid;

		// Token: 0x0400035E RID: 862
		private long lastEventCounter;

		// Token: 0x0400035F RID: 863
		private long eventCounterLowerBound;

		// Token: 0x04000360 RID: 864
		private long eventCounterUpperBound;

		// Token: 0x04000361 RID: 865
		private long highestCommittedEventCounter;

		// Token: 0x04000362 RID: 866
		private EventHistory.FlushEventCounterUpperBoundTaskManager flushEventCounterUpperBoundTaskManager = new EventHistory.FlushEventCounterUpperBoundTaskManager();

		// Token: 0x04000363 RID: 867
		private bool eventCounterAllocationPotentiallyLost;

		// Token: 0x04000364 RID: 868
		private readonly LockName<Guid> watermarkTableLockName;

		// Token: 0x04000365 RID: 869
		private LinkedList<EventHistory.UncommittedListEntry> uncommittedList = new LinkedList<EventHistory.UncommittedListEntry>();

		// Token: 0x04000366 RID: 870
		private EventsTable eventsTable;

		// Token: 0x04000367 RID: 871
		private WatermarksTable watermarksTable;

		// Token: 0x04000368 RID: 872
		private int readerCount;

		// Token: 0x04000369 RID: 873
		private static int eventHistoryDataSlot = -1;

		// Token: 0x02000041 RID: 65
		[Flags]
		public enum ReadEventsFlags : uint
		{
			// Token: 0x0400036C RID: 876
			None = 0U,
			// Token: 0x0400036D RID: 877
			FailIfEventsDeleted = 1U,
			// Token: 0x0400036E RID: 878
			IncludeMoveDestinationEvents = 2U,
			// Token: 0x0400036F RID: 879
			EnableArbitraryRestriction = 2147483648U
		}

		// Token: 0x02000042 RID: 66
		private class UncommittedListEntry
		{
			// Token: 0x0600066C RID: 1644 RVA: 0x0003C0BC File Offset: 0x0003A2BC
			public UncommittedListEntry(Context context, long firstCounter)
			{
				this.context = context;
				this.firstCounter = firstCounter;
				this.lastCounter = firstCounter;
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x0600066D RID: 1645 RVA: 0x0003C0D9 File Offset: 0x0003A2D9
			public Context Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x0003C0E1 File Offset: 0x0003A2E1
			public long FirstCounter
			{
				get
				{
					return this.firstCounter;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x0600066F RID: 1647 RVA: 0x0003C0E9 File Offset: 0x0003A2E9
			// (set) Token: 0x06000670 RID: 1648 RVA: 0x0003C0F1 File Offset: 0x0003A2F1
			public long LastCounter
			{
				get
				{
					return this.lastCounter;
				}
				set
				{
					this.lastCounter = value;
				}
			}

			// Token: 0x04000370 RID: 880
			private readonly Context context;

			// Token: 0x04000371 RID: 881
			private readonly long firstCounter;

			// Token: 0x04000372 RID: 882
			private long lastCounter;
		}

		// Token: 0x02000043 RID: 67
		internal class FlushEventCounterUpperBoundTaskManager
		{
			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000671 RID: 1649 RVA: 0x0003C0FA File Offset: 0x0003A2FA
			internal bool IsFlushNeeded
			{
				get
				{
					return this.minimumEventCounterUpperBoundToFlush > 0L;
				}
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x0003C108 File Offset: 0x0003A308
			private static void FlushEventCounterUpperBoundTask(Context context, EventHistory eventHistory, Func<bool> shouldTaskContinue)
			{
				using (context.AssociateWithDatabase(eventHistory.Database))
				{
					if (EventHistory.FlushEventCounterUpperBoundTaskManager.taskInvokedTestHook.Value != null)
					{
						EventHistory.FlushEventCounterUpperBoundTaskManager.taskInvokedTestHook.Value(context);
					}
					int num = 0;
					for (;;)
					{
						bool preemptFlush = !context.Database.IsOnlineActive || !shouldTaskContinue();
						num++;
						if (eventHistory.flushEventCounterUpperBoundTaskManager.TryFlushEventCounterUpperBound(context, eventHistory, num >= 10, preemptFlush))
						{
							break;
						}
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num <= 10, "Should not be possible to make more than the max attempts to flush the event counter upper bound.");
						Thread.Sleep(TimeSpan.FromMilliseconds(500.0));
					}
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<int>(0L, "The FlushEventCounterUpperBound task is terminating. It made {0} flush attempts.", num);
					}
					if (EventHistory.FlushEventCounterUpperBoundTaskManager.taskExecutedTestHook.Value != null)
					{
						EventHistory.FlushEventCounterUpperBoundTaskManager.taskExecutedTestHook.Value(num);
					}
				}
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0003C1FC File Offset: 0x0003A3FC
			internal static IDisposable SetTaskInvokedTestHook(Action<Context> testDelegate)
			{
				return EventHistory.FlushEventCounterUpperBoundTaskManager.taskInvokedTestHook.SetTestHook(testDelegate);
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0003C209 File Offset: 0x0003A409
			internal static IDisposable SetTaskExecutedTestHook(Action<int> testDelegate)
			{
				return EventHistory.FlushEventCounterUpperBoundTaskManager.taskExecutedTestHook.SetTestHook(testDelegate);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0003C218 File Offset: 0x0003A418
			public void HandlePotentiallyLostEventCounterAllocation(Context context, EventHistory eventHistory)
			{
				if (AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database))
				{
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "A potential event counter allocation leak was detected. Updating minimumEventCounterUpperBoundToFlush from {0} to {1}.", this.minimumEventCounterUpperBoundToFlush, eventHistory.LastEventCounter);
					}
					this.minimumEventCounterUpperBoundToFlush = eventHistory.LastEventCounter;
					return;
				}
				if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.EventCounterBoundsTracer.TraceDebug(0L, "A potential event counter allocation leak was detected after successfully allocating event counter {0}. However, the database has not been sufficiently upgraded to be able to flush the event counter upper bound.");
				}
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0003C288 File Offset: 0x0003A488
			public void EndTransactionHandler(Context context, EventHistory eventHistory, long lastCounter, bool committed)
			{
				if (AddEventCounterBoundsToGlobalsTable.IsReady(context, context.Database))
				{
					if (!committed)
					{
						if (lastCounter + 1L > this.minimumEventCounterUpperBoundToFlush)
						{
							this.minimumEventCounterUpperBoundToFlush = lastCounter + 1L;
							if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "Rollback performed. Updating minimumEventCounterUpperBoundToFlush from {0} to {1}.", this.minimumEventCounterUpperBoundToFlush, lastCounter + 1L);
							}
						}
						else if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "Rollback performed. Last counter was {0}, but minimumEventCounterUpperBoundToFlush is {1}, so it won't be updated.", lastCounter, this.minimumEventCounterUpperBoundToFlush);
						}
					}
					if (this.IsFlushNeeded)
					{
						this.CheckAndDispatchFlushEventCounterUpperBoundTask(eventHistory);
						return;
					}
				}
				else if (!committed && ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.EventCounterBoundsTracer.TraceDebug(0L, "A rollback was performed which will result in event counter gaps. However, the database has not been sufficiently upgraded to be able to flush the event counter upper bound.");
				}
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0003C340 File Offset: 0x0003A540
			private void CheckAndDispatchFlushEventCounterUpperBoundTask(EventHistory eventHistory)
			{
				bool flag;
				if (!this.isFlushTaskOutstanding)
				{
					flag = true;
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long>(0L, "Dispatching the FlushEventCounterUpperBound task (minimumEventCounterUpperBoundToFlush={0}).", this.minimumEventCounterUpperBoundToFlush);
					}
				}
				else if (DateTime.UtcNow - this.lastFlushTaskDispatched < TimeSpan.FromSeconds(100.0))
				{
					flag = false;
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<DateTime>(0L, "The FlushEventCounterUpperBound task will not be dispatched because it was already recently dispatched at {0}.", this.lastFlushTaskDispatched);
					}
				}
				else
				{
					this.numLostFlushTasks++;
					flag = true;
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<DateTime, long>(0L, "The FlushEventCounterUpperBound task dispatched at {0} has been outstanding for too long and is now considered 'lost'. Another task will be dispatched (minimumEventCounterUpperBoundToFlush={1}).", this.lastFlushTaskDispatched, this.minimumEventCounterUpperBoundToFlush);
					}
				}
				if (flag)
				{
					SingleExecutionTask<EventHistory>.CreateSingleExecutionTask(eventHistory.Database.TaskList, TaskExecutionWrapper<EventHistory>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.FlushEventCounterUpperBound, ClientType.System, eventHistory.Database.MdbGuid), new TaskExecutionWrapper<EventHistory>.TaskCallback<Context>(EventHistory.FlushEventCounterUpperBoundTaskManager.FlushEventCounterUpperBoundTask)), eventHistory, true);
					this.lastFlushTaskDispatched = DateTime.UtcNow;
					this.isFlushTaskOutstanding = true;
					this.numFlushTasksDispatched++;
				}
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0003C464 File Offset: 0x0003A664
			private bool TryFlushEventCounterUpperBound(Context context, EventHistory eventHistory, bool forceTaskCompletion, bool preemptFlush)
			{
				bool? flag = null;
				using (LockManager.Lock(eventHistory, LockManager.LockType.EventCounterBounds, context.Diagnostics))
				{
					if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long>(0L, "Executing the FlushEventCounterUpperBound task (minimumEventCounterUpperBoundToFlush={0}).", this.minimumEventCounterUpperBoundToFlush);
					}
					try
					{
						if (preemptFlush)
						{
							if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.EventCounterBoundsTracer.TraceDebug(0L, "The flush task was pre-empted.");
							}
							flag = new bool?(true);
						}
						else if (this.lastFlushedEventCounterUpperBound >= this.minimumEventCounterUpperBoundToFlush)
						{
							if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "The flush task skipped flushing because the last flushed event counter upper bound {0} already covers the minimum event counter upper bound to flush {1}.", this.lastFlushedEventCounterUpperBound, this.minimumEventCounterUpperBoundToFlush);
							}
							this.minimumEventCounterUpperBoundToFlush = 0L;
							flag = new bool?(true);
						}
						else
						{
							if (this.lastFlushedEventCounterUpperBound != eventHistory.EventCounterUpperBound)
							{
								Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.lastFlushedEventCounterUpperBound < eventHistory.EventCounterUpperBound, "Yikes! Event counter upper bound is moving backwards.");
								GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(context.Database);
								GlobalsTableHelper.UpdateGlobalsTableRow(context, new Column[]
								{
									globalsTable.EventCounterUpperBound
								}, new object[]
								{
									eventHistory.EventCounterUpperBound
								});
								this.lastFlushedEventCounterUpperBound = eventHistory.EventCounterUpperBound;
								if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "The flush task successfully flushed event counter upper bound {0} (minimumEventCounterUpperBoundToFlush={1}).", this.lastFlushedEventCounterUpperBound, this.minimumEventCounterUpperBoundToFlush);
								}
							}
							else if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long>(0L, "The flush task skipped flushing because the current event counter upper bound {0} is also the last flushed value.", this.lastFlushedEventCounterUpperBound);
							}
							if (this.minimumEventCounterUpperBoundToFlush <= this.lastFlushedEventCounterUpperBound)
							{
								if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long>(0L, "The event counter upper bound has been flushed up to or exceeding the minimum ({0}), so the flush task is being marked as completed.", this.minimumEventCounterUpperBoundToFlush);
								}
								this.minimumEventCounterUpperBoundToFlush = 0L;
								flag = new bool?(true);
							}
							else
							{
								if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long, string>(0L, "The last flushed event counter upper bound ({0}) was not sufficient to satisfy the minimum ({1}). {2}.", this.lastFlushedEventCounterUpperBound, this.minimumEventCounterUpperBoundToFlush, forceTaskCompletion ? "However, the task is being forced to complete" : "The task will retry flushing");
								}
								flag = new bool?(forceTaskCompletion);
							}
						}
					}
					finally
					{
						if (flag == null || flag == true)
						{
							this.lastFlushTaskCompleted = DateTime.UtcNow;
							this.isFlushTaskOutstanding = false;
							this.numFlushTasksCompleted++;
							if (flag == null)
							{
								if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.EventCounterBoundsTracer.TraceDebug<long, long>(0L, "The FlushEventCounterUpperBound task raised an exception and is terminating prematurely (minimumEventCounterUpperBoundToFlush={0}, lastFlushedEventCounterUpperBound={1}). ", this.minimumEventCounterUpperBoundToFlush, this.lastFlushedEventCounterUpperBound);
								}
								this.numFlushTaskFailures++;
							}
							if (ExTraceGlobals.EventCounterBoundsTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.EventCounterBoundsTracer.TraceDebug(0L, "FlushEventCounterUpperBound task summary: lastDispatched={0}, lastCompleted={1}, numDispatched={2}, numCompleted={3}, numLost={4}, numFailed={5}", new object[]
								{
									this.lastFlushTaskDispatched,
									this.lastFlushTaskCompleted,
									this.numFlushTasksDispatched,
									this.numFlushTasksCompleted,
									this.numLostFlushTasks,
									this.numFlushTaskFailures
								});
							}
						}
					}
				}
				return flag.Value;
			}

			// Token: 0x04000373 RID: 883
			internal const int MaxFlushAttemptsPerTaskDispatch = 10;

			// Token: 0x04000374 RID: 884
			private const int WaitTimeInMillisecondsBetweenFlushAttempts = 500;

			// Token: 0x04000375 RID: 885
			private const int LostFlushTaskThresholdInSeconds = 100;

			// Token: 0x04000376 RID: 886
			private static readonly Hookable<Action<Context>> taskInvokedTestHook = Hookable<Action<Context>>.Create(true, null);

			// Token: 0x04000377 RID: 887
			private static readonly Hookable<Action<int>> taskExecutedTestHook = Hookable<Action<int>>.Create(true, null);

			// Token: 0x04000378 RID: 888
			private long minimumEventCounterUpperBoundToFlush;

			// Token: 0x04000379 RID: 889
			private long lastFlushedEventCounterUpperBound;

			// Token: 0x0400037A RID: 890
			private bool isFlushTaskOutstanding;

			// Token: 0x0400037B RID: 891
			private DateTime lastFlushTaskDispatched;

			// Token: 0x0400037C RID: 892
			private DateTime lastFlushTaskCompleted;

			// Token: 0x0400037D RID: 893
			private int numFlushTasksDispatched;

			// Token: 0x0400037E RID: 894
			private int numFlushTasksCompleted;

			// Token: 0x0400037F RID: 895
			private int numLostFlushTasks;

			// Token: 0x04000380 RID: 896
			private int numFlushTaskFailures;
		}
	}
}
