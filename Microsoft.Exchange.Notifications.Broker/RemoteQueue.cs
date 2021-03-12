using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class RemoteQueue : DisposeTrackableBase, IComparable<RemoteQueue>
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000C758 File Offset: 0x0000A958
		internal RemoteQueue(RemoteMessenger remoteMessenger)
		{
			this.GroupingKey = remoteMessenger.GroupingKey.ToLowerInvariant();
			this.remoteMessenger = remoteMessenger;
			this.Q = new Queue<BrokerNotification>();
			this.qLock = new Semaphore(1, 1);
			this.ContainsSequenceIssueNotification = false;
			this.InflightBatch = null;
			this.LastGetTime = ExDateTime.MinValue;
			this.NumAttempts = 0;
			this.QueueState = QueueState.Created;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		internal Queue<BrokerNotification> Q { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		internal bool ContainsSequenceIssueNotification { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		internal NotificationBatch InflightBatch { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000C7F5 File Offset: 0x0000A9F5
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		internal ExDateTime LastGetTime { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000C806 File Offset: 0x0000AA06
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000C80E File Offset: 0x0000AA0E
		internal int NumAttempts { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000C817 File Offset: 0x0000AA17
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000C81F File Offset: 0x0000AA1F
		internal DateTime TimeToRetry { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000C828 File Offset: 0x0000AA28
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000C830 File Offset: 0x0000AA30
		internal QueueState QueueState { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000C839 File Offset: 0x0000AA39
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000C841 File Offset: 0x0000AA41
		internal string GroupingKey { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C84A File Offset: 0x0000AA4A
		internal int Count
		{
			get
			{
				return this.Q.Count;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000C857 File Offset: 0x0000AA57
		internal bool IsEmpty
		{
			get
			{
				return this.Q.Count == 0;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C868 File Offset: 0x0000AA68
		public int CompareTo(RemoteQueue other)
		{
			return this.TimeToRetry.CompareTo(other.TimeToRetry);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C889 File Offset: 0x0000AA89
		internal void Put(BrokerNotification item)
		{
			if (this.ContainsSequenceIssueNotification)
			{
				return;
			}
			if (this.Q.Count <= 2000)
			{
				this.Q.Enqueue(item);
				return;
			}
			this.Flush();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C8BC File Offset: 0x0000AABC
		internal NotificationBatch Get(int maxItems)
		{
			this.LastGetTime = ExDateTime.UtcNow;
			this.ContainsSequenceIssueNotification = false;
			if (this.InflightBatch != null)
			{
				return this.InflightBatch;
			}
			this.NumAttempts = 1;
			List<BrokerNotification> notifications = this.Q.Dequeue(maxItems);
			this.InflightBatch = new NotificationBatch(notifications, this.remoteMessenger);
			return this.InflightBatch;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C916 File Offset: 0x0000AB16
		internal void DiscardSentBatch()
		{
			this.InflightBatch = null;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C91F File Offset: 0x0000AB1F
		internal bool IsAllowedToRetry()
		{
			return this.NumAttempts < 3;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C92C File Offset: 0x0000AB2C
		internal void PrepareForBackoff()
		{
			this.NumAttempts++;
			int num = RemoteQueue.backoffPeriodsInMilliseconds[this.NumAttempts];
			this.TimeToRetry = DateTime.UtcNow.AddMilliseconds((double)num);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C96C File Offset: 0x0000AB6C
		internal void Flush()
		{
			HashSet<ConsumerId> hashSet = new HashSet<ConsumerId>();
			if (this.InflightBatch != null)
			{
				foreach (BrokerNotification brokerNotification in this.InflightBatch.Notifications)
				{
					hashSet.Add(brokerNotification.ConsumerId);
				}
				this.InflightBatch = null;
			}
			while (this.Q.Count > 0)
			{
				BrokerNotification brokerNotification2 = this.Q.Dequeue();
				hashSet.Add(brokerNotification2.ConsumerId);
			}
			foreach (ConsumerId consumerId in hashSet)
			{
				BrokerNotification item = new BrokerNotification
				{
					ConsumerId = consumerId,
					CreationTime = DateTime.UtcNow,
					NotificationId = Guid.NewGuid(),
					Payload = new DroppedNotification()
				};
				this.Q.Enqueue(item);
			}
			this.ContainsSequenceIssueNotification = true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.qLock != null)
			{
				this.qLock.Dispose();
				this.Q = null;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000CAAB File Offset: 0x0000ACAB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteQueue>(this);
		}

		// Token: 0x040000F3 RID: 243
		internal const int MaximumQueueLength = 2000;

		// Token: 0x040000F4 RID: 244
		internal const int MaximumAttempts = 3;

		// Token: 0x040000F5 RID: 245
		private static readonly int[] backoffPeriodsInMilliseconds = new int[]
		{
			0,
			5000,
			30000,
			60000
		};

		// Token: 0x040000F6 RID: 246
		private readonly Semaphore qLock;

		// Token: 0x040000F7 RID: 247
		private readonly RemoteMessenger remoteMessenger;
	}
}
