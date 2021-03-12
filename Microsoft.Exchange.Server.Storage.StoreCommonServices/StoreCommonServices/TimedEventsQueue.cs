using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000150 RID: 336
	internal class TimedEventsQueue
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00040F58 File Offset: 0x0003F158
		internal static int TimedEventsQueueSlot
		{
			get
			{
				return TimedEventsQueue.timedEventsQueueSlot;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00040F5F File Offset: 0x0003F15F
		internal static TimeSpan TimedEventsQueueInterval
		{
			get
			{
				return TimedEventsQueue.Interval;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00040F66 File Offset: 0x0003F166
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00040F6D File Offset: 0x0003F16D
		internal static bool StopDispatch
		{
			get
			{
				return TimedEventsQueue.stopDispatch;
			}
			set
			{
				TimedEventsQueue.stopDispatch = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00040F75 File Offset: 0x0003F175
		internal StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00040F7D File Offset: 0x0003F17D
		internal ITimedEventHandler TimedEventDispatcher
		{
			get
			{
				return this.timedEventDispatcher;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00040F88 File Offset: 0x0003F188
		internal static void Initialize()
		{
			if (TimedEventsQueue.timedEventsQueueSlot == -1)
			{
				TimedEventsQueue.timedEventsQueueSlot = StoreDatabase.AllocateComponentDataSlot();
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug(57672L, "TimedEventsQueue.timedEventsQueueSlot=" + TimedEventsQueue.timedEventsQueueSlot.ToString());
				}
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00040FD8 File Offset: 0x0003F1D8
		internal static void TimedEventsProcessing(Context context, TimedEventsQueue queue, Func<bool> shouldCallbackContinue)
		{
			using (context.AssociateWithDatabase(queue.Database))
			{
				if (queue.Database.IsOnlineActive)
				{
					if (TimedEventsQueue.StopDispatch)
					{
						ExTraceGlobals.TimedEventsTracer.TraceDebug(33096L, "Timed event dispatch is configured to not run");
					}
					else
					{
						TimedEventsQueue.TimedEventsProcessingInternal(context, queue, shouldCallbackContinue);
					}
				}
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00041048 File Offset: 0x0003F248
		internal static void TimedEventsProcessingInternal(Context context, TimedEventsQueue queue, Func<bool> shouldCallbackContinue)
		{
			List<TimedEventEntry> list = queue.ReadTimedEventEntries(context);
			if (list == null)
			{
				return;
			}
			foreach (TimedEventEntry timedEvent in list)
			{
				if (shouldCallbackContinue())
				{
					try
					{
						try
						{
							queue.TimedEventDispatcher.Invoke(context, timedEvent);
						}
						finally
						{
							queue.DeleteTimedEventEntry(context, timedEvent);
						}
						context.Commit();
						continue;
					}
					finally
					{
						context.Abort();
					}
				}
				ExTraceGlobals.TimedEventsTracer.TraceDebug(49480L, "Task is asked to stop");
				break;
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000410FC File Offset: 0x0003F2FC
		internal static void MountEventHandler(Context context, StoreDatabase database, bool readOnly)
		{
			TimedEventsQueue timedEventsQueue = new TimedEventsQueue(database);
			database.ComponentData[TimedEventsQueue.timedEventsQueueSlot] = timedEventsQueue;
			if (!readOnly)
			{
				RecurringTask<TimedEventsQueue> task = new RecurringTask<TimedEventsQueue>(TaskExecutionWrapper<TimedEventsQueue>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.TimedEventsProcessing, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<TimedEventsQueue>.TaskCallback<Context>(TimedEventsQueue.TimedEventsProcessing)), timedEventsQueue, TimedEventsQueue.Interval, false);
				database.TaskList.Add(task, true);
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00041160 File Offset: 0x0003F360
		internal void InsertTimedEventEntry(Context context, TimedEventEntry timedEvent)
		{
			TimedEventsTable timedEventsTable = DatabaseSchema.TimedEventsTable(context.Database);
			using (InsertOperator insertOperator = Factory.CreateInsertOperator(context.Culture, context, timedEventsTable.Table, null, new Column[]
			{
				timedEventsTable.EventTime,
				timedEventsTable.MailboxNumber,
				timedEventsTable.EventSource,
				timedEventsTable.EventType,
				timedEventsTable.QoS,
				timedEventsTable.EventData
			}, new object[]
			{
				timedEvent.EventTime,
				timedEvent.MailboxNumber,
				timedEvent.EventSource,
				timedEvent.EventType,
				(int)timedEvent.QualityOfService,
				timedEvent.EventData
			}, null, true))
			{
				int num = (int)insertOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0004124C File Offset: 0x0003F44C
		internal List<TimedEventEntry> ReadTimedEventEntries(Context context)
		{
			return this.ReadTimedEventEntries(context, DateTime.UtcNow);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0004125C File Offset: 0x0003F45C
		internal List<TimedEventEntry> ReadTimedEventEntries(Context context, DateTime stopAt)
		{
			TimedEventsTable timedEventsTable = DatabaseSchema.TimedEventsTable(this.database);
			List<TimedEventEntry> list = new List<TimedEventEntry>(100);
			StartStopKey empty = StartStopKey.Empty;
			StartStopKey stopKey = (stopAt == DateTime.MaxValue) ? StartStopKey.Empty : new StartStopKey(true, new object[]
			{
				stopAt
			});
			Column[] columnsToFetch = new Column[]
			{
				timedEventsTable.EventTime,
				timedEventsTable.UniqueId,
				timedEventsTable.MailboxNumber,
				timedEventsTable.EventSource,
				timedEventsTable.EventType,
				timedEventsTable.QoS,
				timedEventsTable.EventData
			};
			try
			{
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, timedEventsTable.Table, timedEventsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 100, new KeyRange(empty, stopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							DateTime dateTime = reader.GetDateTime(timedEventsTable.EventTime);
							long @int = reader.GetInt64(timedEventsTable.UniqueId);
							int? nullableInt = reader.GetNullableInt32(timedEventsTable.MailboxNumber);
							Guid guid = reader.GetGuid(timedEventsTable.EventSource);
							int int2 = reader.GetInt32(timedEventsTable.EventType);
							int int3 = reader.GetInt32(timedEventsTable.QoS);
							byte[] binary = reader.GetBinary(timedEventsTable.EventData);
							list.Add(new TimedEventEntry(dateTime, @int, nullableInt, guid, int2, (TimedEventEntry.QualityOfServiceType)int3, binary));
						}
					}
				}
				context.Commit();
			}
			finally
			{
				context.Abort();
			}
			return list;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00041424 File Offset: 0x0003F624
		internal void DeleteTimedEventEntry(Context context, TimedEventEntry timedEvent)
		{
			TimedEventsTable timedEventsTable = DatabaseSchema.TimedEventsTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				timedEvent.EventTime,
				timedEvent.UniqueId
			});
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, timedEventsTable.Table, timedEventsTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false), false))
			{
				deleteOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000414C8 File Offset: 0x0003F6C8
		private TimedEventsQueue(StoreDatabase database)
		{
			this.database = database;
			this.timedEventDispatcher = new TimedEventDispatcher();
		}

		// Token: 0x0400073E RID: 1854
		private const int BatchReadCount = 100;

		// Token: 0x0400073F RID: 1855
		private static readonly TimeSpan Interval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000740 RID: 1856
		private static int timedEventsQueueSlot = -1;

		// Token: 0x04000741 RID: 1857
		private static bool stopDispatch = false;

		// Token: 0x04000742 RID: 1858
		private StoreDatabase database;

		// Token: 0x04000743 RID: 1859
		private ITimedEventHandler timedEventDispatcher;
	}
}
