using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200010D RID: 269
	public sealed class ResourceMonitorDigest
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x00037B68 File Offset: 0x00035D68
		public ResourceMonitorDigest()
		{
			this.activityLog = ResourceMonitorDigest.ActivityLog.GetEmpty(1L, 0);
			this.aggregatingLogBytesLog = new ResourceMonitorDigest.AggregatingActivityLog(10);
			this.timeInServerHistory = new ResourceMonitorDigest.DigestHistory();
			this.logRecodBytesHistory = new ResourceMonitorDigest.DigestHistory();
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00037BB7 File Offset: 0x00035DB7
		public static IDigestCollector NullCollector
		{
			get
			{
				if (ResourceMonitorDigest.nullCollector == null)
				{
					ResourceMonitorDigest.nullCollector = new ResourceMonitorDigest.NullDigestCollector();
				}
				return ResourceMonitorDigest.nullCollector;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00037BCF File Offset: 0x00035DCF
		public IDigestCollector CreateDigestCollector(Guid mailboxGuid, int mailboxNumber)
		{
			if (mailboxGuid.Equals(Guid.Empty))
			{
				return ResourceMonitorDigest.NullCollector;
			}
			return new ResourceMonitorDigest.DigestCollector(this, mailboxGuid, mailboxNumber);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00037BF0 File Offset: 0x00035DF0
		public ResourceMonitorDigestSnapshot GetDigestHistory()
		{
			return new ResourceMonitorDigestSnapshot
			{
				TimeInServerDigest = this.timeInServerHistory.GetHistory(),
				LogRecordBytesDigest = this.logRecodBytesHistory.GetHistory()
			};
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00037C34 File Offset: 0x00035E34
		internal static void MountEventHandler(StoreDatabase database)
		{
			Task<ResourceMonitorDigest>.TaskCallback callback = TaskExecutionWrapper<ResourceMonitorDigest>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.ResourceMonitorDigest, ClientType.System, database.MdbGuid), delegate(Context _1, ResourceMonitorDigest digest, Func<bool> _2)
			{
				digest.TakeSnapshot();
			});
			RecurringTask<ResourceMonitorDigest> task = new RecurringTask<ResourceMonitorDigest>(callback, database.ResourceDigest, TimeSpan.FromMinutes(1.0), false);
			database.TaskList.Add(task, true);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00037CC8 File Offset: 0x00035EC8
		private void TakeSnapshot()
		{
			ResourceMonitorDigest.ActivityLog activityLog;
			using (LockManager.Lock(this.syncRoot))
			{
				activityLog = this.GetActivityLog();
				int num = activityLog.CurrentActivity.Array.Length;
				if (activityLog.CurrentActivity.Count < num / 4)
				{
					num /= 2;
				}
				ResourceMonitorDigest.ActivityLog empty = ResourceMonitorDigest.ActivityLog.GetEmpty(activityLog.Version + 1L, num);
				Interlocked.Exchange<ResourceMonitorDigest.ActivityLog>(ref this.activityLog, empty);
			}
			DateTime utcNow = DateTime.UtcNow;
			ResourceMonitorDigest.Digest digest = new ResourceMonitorDigest.Digest(25, (ResourceDigestStats left, ResourceDigestStats right) => left.TimeInServer.CompareTo(right.TimeInServer));
			for (int i = 0; i < activityLog.CurrentActivity.Count; i++)
			{
				ResourceDigestStats data = activityLog.CurrentActivity.Array[i].Data;
				data.SampleTime = utcNow;
				digest.Update(data);
				this.aggregatingLogBytesLog.AggregateSample(data);
			}
			ResourceMonitorDigest.Digest digest2;
			if (this.aggregatingLogBytesLog.TryComputeDigestAndReset(25, (ResourceDigestStats left, ResourceDigestStats right) => left.LogRecordBytes.CompareTo(right.LogRecordBytes), out digest2))
			{
				this.logRecodBytesHistory.AddDigest(digest2);
			}
			this.timeInServerHistory.AddDigest(digest);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00037E1C File Offset: 0x0003601C
		private ResourceMonitorDigest.ActivityLog GetActivityLog()
		{
			return Interlocked.CompareExchange<ResourceMonitorDigest.ActivityLog>(ref this.activityLog, null, null);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00037E2C File Offset: 0x0003602C
		private void LogActivity(ResourceDigestStats activity, ResourceMonitorDigest.DigestCollector collector)
		{
			ResourceMonitorDigest.ActivityLog activityLog = this.GetActivityLog();
			ResourceMonitorDigest.ActivityHandle activityHandle = collector.Handle;
			if (activityLog.Version != activityHandle.Version || activityHandle.Slot >= activityLog.CurrentActivity.Array.Length)
			{
				using (LockManager.Lock(this.syncRoot))
				{
					activityLog = this.GetActivityLog();
					activityHandle = collector.Handle;
					if (activityLog.Version != activityHandle.Version)
					{
						int num;
						ResourceMonitorDigest.ActivityLog activityLog2 = activityLog.ReserveSlot(out num);
						activityLog2.CurrentActivity.Array[num] = new ResourceMonitorDigest.Activity(collector.MailboxGuid, collector.MailboxNumber);
						activityHandle = new ResourceMonitorDigest.ActivityHandle(activityLog2.Version, num);
						Interlocked.Exchange<ResourceMonitorDigest.ActivityLog>(ref this.activityLog, activityLog2);
						collector.Handle = activityHandle;
						activityLog = activityLog2;
					}
				}
			}
			activityLog.CurrentActivity.Array[activityHandle.Slot].Aggregate(activity);
		}

		// Token: 0x040005DB RID: 1499
		public const int DigestCapacity = 25;

		// Token: 0x040005DC RID: 1500
		public const int MaximumSnapshots = 10;

		// Token: 0x040005DD RID: 1501
		public const int LogRecordBytesLogAggregate = 10;

		// Token: 0x040005DE RID: 1502
		private static IDigestCollector nullCollector;

		// Token: 0x040005DF RID: 1503
		private object syncRoot = new object();

		// Token: 0x040005E0 RID: 1504
		private ResourceMonitorDigest.ActivityLog activityLog;

		// Token: 0x040005E1 RID: 1505
		private ResourceMonitorDigest.AggregatingActivityLog aggregatingLogBytesLog;

		// Token: 0x040005E2 RID: 1506
		private ResourceMonitorDigest.DigestHistory timeInServerHistory;

		// Token: 0x040005E3 RID: 1507
		private ResourceMonitorDigest.DigestHistory logRecodBytesHistory;

		// Token: 0x0200010E RID: 270
		private class Activity
		{
			// Token: 0x06000AD7 RID: 2775 RVA: 0x00037F24 File Offset: 0x00036124
			public Activity(Guid mailboxGuid, int mailboxNumber)
			{
				this.activity = default(ResourceDigestStats);
				this.activity.MailboxGuid = mailboxGuid;
				this.activity.MailboxNumber = mailboxNumber;
			}

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00037F50 File Offset: 0x00036150
			public ResourceDigestStats Data
			{
				get
				{
					ResourceDigestStats result;
					using (LockManager.Lock(this))
					{
						result = this.activity;
					}
					return result;
				}
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00037F8C File Offset: 0x0003618C
			public void Aggregate(ResourceDigestStats activity)
			{
				using (LockManager.Lock(this))
				{
					this.activity.TimeInServer = this.activity.TimeInServer + activity.TimeInServer;
					this.activity.TimeInCPU = this.activity.TimeInCPU + activity.TimeInCPU;
					this.activity.ROPCount = this.activity.ROPCount + activity.ROPCount;
					this.activity.PageRead = this.activity.PageRead + activity.PageRead;
					this.activity.PagePreread = this.activity.PagePreread + activity.PagePreread;
					this.activity.LogRecordCount = this.activity.LogRecordCount + activity.LogRecordCount;
					this.activity.LogRecordBytes = this.activity.LogRecordBytes + activity.LogRecordBytes;
					this.activity.LdapReads = this.activity.LdapReads + activity.LdapReads;
					this.activity.LdapSearches = this.activity.LdapSearches + activity.LdapSearches;
				}
			}

			// Token: 0x040005E7 RID: 1511
			private ResourceDigestStats activity;
		}

		// Token: 0x0200010F RID: 271
		private class ActivityLog
		{
			// Token: 0x06000ADA RID: 2778 RVA: 0x000380AC File Offset: 0x000362AC
			private ActivityLog(long version, ArraySegment<ResourceMonitorDigest.Activity> activityLog)
			{
				this.Version = version;
				this.CurrentActivity = activityLog;
			}

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000380C2 File Offset: 0x000362C2
			// (set) Token: 0x06000ADC RID: 2780 RVA: 0x000380CA File Offset: 0x000362CA
			public long Version { get; private set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000ADD RID: 2781 RVA: 0x000380D3 File Offset: 0x000362D3
			// (set) Token: 0x06000ADE RID: 2782 RVA: 0x000380DB File Offset: 0x000362DB
			public ArraySegment<ResourceMonitorDigest.Activity> CurrentActivity { get; private set; }

			// Token: 0x06000ADF RID: 2783 RVA: 0x000380E4 File Offset: 0x000362E4
			public static ResourceMonitorDigest.ActivityLog GetEmpty(long version, int initialCapacity)
			{
				if (initialCapacity < 16)
				{
					initialCapacity = 16;
				}
				return new ResourceMonitorDigest.ActivityLog(version, new ArraySegment<ResourceMonitorDigest.Activity>(new ResourceMonitorDigest.Activity[initialCapacity], 0, 0));
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x00038104 File Offset: 0x00036304
			public ResourceMonitorDigest.ActivityLog ReserveSlot(out int dataSlot)
			{
				if (this.CurrentActivity.Count < this.CurrentActivity.Array.Length)
				{
					dataSlot = this.CurrentActivity.Count;
					return new ResourceMonitorDigest.ActivityLog(this.Version, new ArraySegment<ResourceMonitorDigest.Activity>(this.CurrentActivity.Array, 0, this.CurrentActivity.Count + 1));
				}
				ResourceMonitorDigest.Activity[] array = new ResourceMonitorDigest.Activity[this.CurrentActivity.Array.Length * 2];
				Array.Copy(this.CurrentActivity.Array, 0, array, 0, this.CurrentActivity.Array.Length);
				dataSlot = this.CurrentActivity.Count;
				return new ResourceMonitorDigest.ActivityLog(this.Version, new ArraySegment<ResourceMonitorDigest.Activity>(array, 0, this.CurrentActivity.Count + 1));
			}

			// Token: 0x040005E8 RID: 1512
			private const int MimimalCapacity = 16;
		}

		// Token: 0x02000110 RID: 272
		private class AggregatingActivityLog
		{
			// Token: 0x06000AE1 RID: 2785 RVA: 0x000381E8 File Offset: 0x000363E8
			public AggregatingActivityLog(int maximumLogsToAggregate)
			{
				this.maximumAggregateCount = maximumLogsToAggregate;
				this.aggregateCount = 0;
				this.aggregatedLog = new Dictionary<Guid, ResourceDigestStats>();
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x0003820C File Offset: 0x0003640C
			public void AggregateSample(ResourceDigestStats sample)
			{
				using (LockManager.Lock(this))
				{
					if (this.aggregatedLog.ContainsKey(sample.MailboxGuid))
					{
						ResourceDigestStats value = this.aggregatedLog[sample.MailboxGuid];
						value.TimeInServer += sample.TimeInServer;
						value.TimeInCPU += sample.TimeInCPU;
						value.ROPCount += sample.ROPCount;
						value.PageRead += sample.PageRead;
						value.PagePreread += sample.PagePreread;
						value.LogRecordCount += sample.LogRecordCount;
						value.LogRecordBytes += sample.LogRecordBytes;
						value.LdapReads += sample.LdapReads;
						value.LdapSearches += sample.LdapSearches;
						value.SampleTime = sample.SampleTime;
						this.aggregatedLog[sample.MailboxGuid] = value;
					}
					else
					{
						this.aggregatedLog[sample.MailboxGuid] = sample;
					}
				}
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00038374 File Offset: 0x00036574
			public bool TryComputeDigestAndReset(int capacity, Func<ResourceDigestStats, ResourceDigestStats, int> comparator, out ResourceMonitorDigest.Digest digest)
			{
				digest = null;
				bool result;
				using (LockManager.Lock(this))
				{
					this.aggregateCount++;
					if (this.aggregateCount >= this.maximumAggregateCount)
					{
						digest = new ResourceMonitorDigest.Digest(capacity, comparator);
						foreach (ResourceDigestStats stats in this.aggregatedLog.Values)
						{
							digest.Update(stats);
						}
						this.aggregateCount = 0;
						this.aggregatedLog.Clear();
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x040005EB RID: 1515
			private readonly int maximumAggregateCount;

			// Token: 0x040005EC RID: 1516
			private Dictionary<Guid, ResourceDigestStats> aggregatedLog;

			// Token: 0x040005ED RID: 1517
			private int aggregateCount;
		}

		// Token: 0x02000111 RID: 273
		private class Digest
		{
			// Token: 0x06000AE4 RID: 2788 RVA: 0x00038434 File Offset: 0x00036634
			public Digest(int capacity, Func<ResourceDigestStats, ResourceDigestStats, int> comparator)
			{
				this.capacity = capacity;
				this.count = 0;
				this.data = new ResourceDigestStats[capacity];
				this.comparator = comparator;
				this.isHeapBuilt = false;
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x00038464 File Offset: 0x00036664
			public ResourceDigestStats[] GetSamples()
			{
				if (this.count == 0)
				{
					return Array<ResourceDigestStats>.Empty;
				}
				ResourceDigestStats[] array = new ResourceDigestStats[this.count];
				Array.Copy(this.data, 0, array, 0, this.count);
				return array;
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x000384A0 File Offset: 0x000366A0
			public void Update(ResourceDigestStats stats)
			{
				if (this.count < this.capacity)
				{
					this.data[this.count] = stats;
					this.count++;
					return;
				}
				if (!this.isHeapBuilt)
				{
					for (int i = this.Parent(this.count - 1); i >= 0; i--)
					{
						this.PushToHeap(this.data[i], i);
					}
					this.isHeapBuilt = true;
				}
				int num = this.comparator(stats, this.data[0]);
				if (num > 0)
				{
					this.PushToHeap(stats, 0);
				}
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x0003854C File Offset: 0x0003674C
			private void PushToHeap(ResourceDigestStats stats, int root)
			{
				int i;
				int num4;
				for (i = root; i < this.count; i = num4)
				{
					int num = this.Left(i);
					int num2 = this.Right(i);
					int num3;
					if (num2 > 0)
					{
						num3 = this.comparator(this.data[num], this.data[num2]);
						num4 = ((num3 < 0) ? num : num2);
					}
					else
					{
						if (num <= 0)
						{
							break;
						}
						num4 = num;
					}
					num3 = this.comparator(this.data[num4], stats);
					if (num3 >= 0)
					{
						break;
					}
					this.data[i] = this.data[num4];
				}
				this.data[i] = stats;
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x0003861C File Offset: 0x0003681C
			private int Left(int parent)
			{
				int num = 2 * parent + 1;
				if (num < 0 || num >= this.count)
				{
					return -1;
				}
				return num;
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x00038640 File Offset: 0x00036840
			private int Right(int parent)
			{
				int num = 2 * parent + 2;
				if (num < 0 || num >= this.count)
				{
					return -1;
				}
				return num;
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x00038663 File Offset: 0x00036863
			private int Parent(int current)
			{
				if (current <= 0)
				{
					return -1;
				}
				return (current - 1) / 2;
			}

			// Token: 0x040005EE RID: 1518
			private readonly int capacity;

			// Token: 0x040005EF RID: 1519
			private int count;

			// Token: 0x040005F0 RID: 1520
			private ResourceDigestStats[] data;

			// Token: 0x040005F1 RID: 1521
			private Func<ResourceDigestStats, ResourceDigestStats, int> comparator;

			// Token: 0x040005F2 RID: 1522
			private bool isHeapBuilt;
		}

		// Token: 0x02000112 RID: 274
		private class DigestHistory
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x00038670 File Offset: 0x00036870
			public DigestHistory()
			{
				this.history = new ResourceMonitorDigest.Digest[10];
				this.nextSnapshot = 0;
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x0003868C File Offset: 0x0003688C
			public void AddDigest(ResourceMonitorDigest.Digest digest)
			{
				using (LockManager.Lock(this))
				{
					this.history[this.nextSnapshot] = digest;
					this.nextSnapshot = (this.nextSnapshot + 1) % this.history.Length;
				}
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x000386E8 File Offset: 0x000368E8
			public ResourceDigestStats[][] GetHistory()
			{
				ResourceDigestStats[][] result;
				using (LockManager.Lock(this))
				{
					int num = this.history.Length;
					for (int i = 0; i < this.history.Length; i++)
					{
						if (this.history[i] == null)
						{
							num--;
						}
					}
					if (num == 0)
					{
						result = Array<ResourceDigestStats[]>.Empty;
					}
					else
					{
						ResourceDigestStats[][] array = new ResourceDigestStats[num][];
						int num2 = 0;
						for (int j = 0; j < this.history.Length; j++)
						{
							int num3 = (2 * this.history.Length + this.nextSnapshot - 1 - j) % this.history.Length;
							if (this.history[num3] != null)
							{
								array[num2] = this.history[num3].GetSamples();
								num2++;
							}
						}
						result = array;
					}
				}
				return result;
			}

			// Token: 0x040005F3 RID: 1523
			private ResourceMonitorDigest.Digest[] history;

			// Token: 0x040005F4 RID: 1524
			private int nextSnapshot;
		}

		// Token: 0x02000113 RID: 275
		private class ActivityHandle
		{
			// Token: 0x06000AEE RID: 2798 RVA: 0x000387BC File Offset: 0x000369BC
			public ActivityHandle(long version, int dataSlot)
			{
				this.Version = version;
				this.Slot = dataSlot;
			}

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000387D2 File Offset: 0x000369D2
			// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x000387DA File Offset: 0x000369DA
			public long Version { get; private set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000387E3 File Offset: 0x000369E3
			// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x000387EB File Offset: 0x000369EB
			public int Slot { get; private set; }
		}

		// Token: 0x02000114 RID: 276
		private class DigestCollector : IDigestCollector
		{
			// Token: 0x06000AF3 RID: 2803 RVA: 0x000387F4 File Offset: 0x000369F4
			public DigestCollector(ResourceMonitorDigest monitor, Guid mailboxGuid, int mailboxNumber)
			{
				this.monitor = monitor;
				this.mailboxGuid = mailboxGuid;
				this.mailboxNumber = mailboxNumber;
				this.activityHandle = new ResourceMonitorDigest.ActivityHandle(-1L, -1);
			}

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0003881F File Offset: 0x00036A1F
			public Guid MailboxGuid
			{
				get
				{
					return this.mailboxGuid;
				}
			}

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00038827 File Offset: 0x00036A27
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0003882F File Offset: 0x00036A2F
			// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x0003883E File Offset: 0x00036A3E
			public ResourceMonitorDigest.ActivityHandle Handle
			{
				get
				{
					return Interlocked.CompareExchange<ResourceMonitorDigest.ActivityHandle>(ref this.activityHandle, null, null);
				}
				set
				{
					Interlocked.Exchange<ResourceMonitorDigest.ActivityHandle>(ref this.activityHandle, value);
				}
			}

			// Token: 0x06000AF8 RID: 2808 RVA: 0x0003884D File Offset: 0x00036A4D
			public void LogActivity(ResourceDigestStats activity)
			{
				this.monitor.LogActivity(activity, this);
			}

			// Token: 0x040005F7 RID: 1527
			private readonly Guid mailboxGuid;

			// Token: 0x040005F8 RID: 1528
			private readonly int mailboxNumber;

			// Token: 0x040005F9 RID: 1529
			private ResourceMonitorDigest monitor;

			// Token: 0x040005FA RID: 1530
			private ResourceMonitorDigest.ActivityHandle activityHandle;
		}

		// Token: 0x02000115 RID: 277
		private class NullDigestCollector : IDigestCollector
		{
			// Token: 0x06000AFA RID: 2810 RVA: 0x00038864 File Offset: 0x00036A64
			public void LogActivity(ResourceDigestStats activity)
			{
			}
		}
	}
}
