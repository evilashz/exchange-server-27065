using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BackLog
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x000262E4 File Offset: 0x000244E4
		internal BackLog(LatencyReportingThreshold threshold)
		{
			this.threshold = threshold;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00026314 File Offset: 0x00024514
		internal int Count
		{
			get
			{
				int count;
				lock (this.lockObject)
				{
					count = this.list.Count;
				}
				return count;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002635C File Offset: 0x0002455C
		internal void ChangeThresholdAndClear(LatencyReportingThreshold newThreshold)
		{
			lock (this.lockObject)
			{
				this.threshold = newThreshold;
				this.Clear();
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000263A4 File Offset: 0x000245A4
		internal void MoveToList(List<LatencyDetectionContext> destinationList)
		{
			lock (this.lockObject)
			{
				destinationList.AddRange(this.list);
				this.Clear();
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000263F0 File Offset: 0x000245F0
		internal bool AddAndQueryThreshold(LatencyDetectionContext data)
		{
			bool flag = false;
			try
			{
				if (Monitor.TryEnter(this.lockObject))
				{
					flag = (this.countAboveThreshold >= (int)this.threshold.NumberRequired);
					if (flag)
					{
						this.Add(data);
					}
					else
					{
						this.TrimOldEntries();
						flag = this.IsLatencyAboveThreshold(data);
						if (flag)
						{
							this.SetTrigger(data);
						}
						else
						{
							this.Add(data);
						}
						this.LimitBacklogSize();
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.lockObject))
				{
					Monitor.Exit(this.lockObject);
				}
			}
			return flag;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00026484 File Offset: 0x00024684
		internal void Clear()
		{
			lock (this.lockObject)
			{
				this.list.Clear();
				this.countAboveThreshold = 0;
				this.trigger = null;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000264D8 File Offset: 0x000246D8
		internal bool IsBeyondThreshold(out LatencyDetectionContext triggerContext)
		{
			bool flag = false;
			triggerContext = null;
			lock (this.lockObject)
			{
				flag = (this.countAboveThreshold >= (int)this.threshold.NumberRequired);
				if (flag)
				{
					triggerContext = this.trigger;
				}
			}
			return flag;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002653C File Offset: 0x0002473C
		private void SetTrigger(LatencyDetectionContext data)
		{
			this.trigger = data;
			this.IncrementCountAboveThreshold(data);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002654C File Offset: 0x0002474C
		private bool IsLatencyAboveThreshold(LatencyDetectionContext data)
		{
			bool result = false;
			if ((data.TriggerOptions & TriggerOptions.DoNotTrigger) == TriggerOptions.None)
			{
				result = (data.Elapsed >= this.threshold.Threshold);
			}
			return result;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002657D File Offset: 0x0002477D
		private void IncrementCountAboveThreshold(LatencyDetectionContext data)
		{
			if (this.IsLatencyAboveThreshold(data))
			{
				this.countAboveThreshold++;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00026596 File Offset: 0x00024796
		private void Add(LatencyDetectionContext data)
		{
			this.list.AddLast(data);
			this.IncrementCountAboveThreshold(data);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000265AC File Offset: 0x000247AC
		private void TrimOldEntries()
		{
			this.minToKeep = DateTime.UtcNow - BackLog.options.BacklogRetirementAge;
			while (this.list.Count > 0)
			{
				LinkedListNode<LatencyDetectionContext> first = this.list.First;
				if (!(first.Value.TimeStarted < this.minToKeep))
				{
					break;
				}
				this.RemoveNode(first);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00026610 File Offset: 0x00024810
		private void RemoveNode(LinkedListNode<LatencyDetectionContext> node)
		{
			LatencyDetectionContext value = node.Value;
			if (this.IsLatencyAboveThreshold(value))
			{
				this.countAboveThreshold--;
			}
			this.list.Remove(node);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00026648 File Offset: 0x00024848
		private void LimitBacklogSize()
		{
			uint maximumBacklogSize = BackLog.options.MaximumBacklogSize;
			while ((long)this.list.Count > (long)((ulong)maximumBacklogSize))
			{
				this.RemoveNode(this.list.First);
			}
		}

		// Token: 0x040006EB RID: 1771
		private static readonly PerformanceReportingOptions options = PerformanceReportingOptions.Instance;

		// Token: 0x040006EC RID: 1772
		private readonly LinkedList<LatencyDetectionContext> list = new LinkedList<LatencyDetectionContext>();

		// Token: 0x040006ED RID: 1773
		private readonly object lockObject = new object();

		// Token: 0x040006EE RID: 1774
		private DateTime minToKeep = DateTime.MinValue;

		// Token: 0x040006EF RID: 1775
		private int countAboveThreshold;

		// Token: 0x040006F0 RID: 1776
		private LatencyReportingThreshold threshold;

		// Token: 0x040006F1 RID: 1777
		private LatencyDetectionContext trigger;
	}
}
