using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.QueueQuota
{
	// Token: 0x02000348 RID: 840
	internal class UsageData
	{
		// Token: 0x0600242A RID: 9258 RVA: 0x00089871 File Offset: 0x00087A71
		internal UsageData(TimeSpan historyInterval, TimeSpan historyBucketLength) : this(historyInterval, historyBucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00089898 File Offset: 0x00087A98
		internal UsageData(TimeSpan historyInterval, TimeSpan historyBucketLength, Func<DateTime> currentTimeProvider)
		{
			this.currentTimeProvider = currentTimeProvider;
			this.pastRejectedMessageSubmissionQueueCount = new SlidingTotalCounter(historyInterval, historyBucketLength, currentTimeProvider);
			this.pastRejectedMessageTotalQueueCount = new SlidingTotalCounter(historyInterval, historyBucketLength, currentTimeProvider);
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000898E4 File Offset: 0x00087AE4
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x000898EC File Offset: 0x00087AEC
		public DateTime LastUpdateTime { get; private set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000898F5 File Offset: 0x00087AF5
		public DateTime ThrottlingStartTime
		{
			get
			{
				if (DateTime.Compare(this.submissionThrottlingStartTime, this.totalQueueThrottlingStartTime) < 0)
				{
					return this.submissionThrottlingStartTime;
				}
				return this.totalQueueThrottlingStartTime;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x00089918 File Offset: 0x00087B18
		public bool IsEmpty
		{
			get
			{
				return this.submissionQueueUsage == 0 && this.totalQueueUsage == 0 && this.messagesRejectedDueToSubmissionQueue == 0 && this.pastRejectedMessageSubmissionQueueCount.Sum == 0L && this.messagesRejectedDueToTotalQueue == 0 && this.pastRejectedMessageTotalQueueCount.Sum == 0L;
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00089968 File Offset: 0x00087B68
		public void SetOverQuotaFlags(QueueQuotaResources resources, bool overThreshold, bool overWarning)
		{
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				this.isSubmissionQueueOverQuota = overThreshold;
				this.isSubmissionQueueOverWarning = overWarning;
				if (overThreshold)
				{
					this.submissionThrottlingStartTime = this.currentTimeProvider();
				}
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				this.isTotalQueueSizeOverQuota = overThreshold;
				this.isTotalQueueOverWarning = overWarning;
				if (overThreshold)
				{
					this.totalQueueThrottlingStartTime = this.currentTimeProvider();
				}
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000899C5 File Offset: 0x00087BC5
		public bool GetIsOverQuota(QueueQuotaResources resources)
		{
			return ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0 && this.isSubmissionQueueOverQuota) || ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0 && this.isTotalQueueSizeOverQuota);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000899E8 File Offset: 0x00087BE8
		public bool GetOverWarningFlag(QueueQuotaResources resources)
		{
			return ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0 && this.isSubmissionQueueOverWarning) || ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0 && this.isTotalQueueOverWarning);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00089A0C File Offset: 0x00087C0C
		public int GetUsage(QueueQuotaResources resources)
		{
			int num = 0;
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				num += this.submissionQueueUsage;
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				num += this.totalQueueUsage;
			}
			return num;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00089A3A File Offset: 0x00087C3A
		public void IncrementUsage(QueueQuotaResources resources)
		{
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				Interlocked.Increment(ref this.submissionQueueUsage);
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				Interlocked.Increment(ref this.totalQueueUsage);
			}
			this.LastUpdateTime = DateTime.UtcNow;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00089A6B File Offset: 0x00087C6B
		public void DecrementUsage(QueueQuotaResources resources)
		{
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				Interlocked.Decrement(ref this.submissionQueueUsage);
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				Interlocked.Decrement(ref this.totalQueueUsage);
			}
			this.LastUpdateTime = DateTime.UtcNow;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00089A9C File Offset: 0x00087C9C
		public void IncrementRejected(QueueQuotaResources resources)
		{
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				Interlocked.Increment(ref this.messagesRejectedDueToSubmissionQueue);
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				Interlocked.Increment(ref this.messagesRejectedDueToTotalQueue);
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00089AC4 File Offset: 0x00087CC4
		public int GetRejectedCount(QueueQuotaResources resources)
		{
			int num = 0;
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				num += this.messagesRejectedDueToSubmissionQueue + (int)this.pastRejectedMessageSubmissionQueueCount.Sum;
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				num += this.messagesRejectedDueToTotalQueue + (int)this.pastRejectedMessageTotalQueueCount.Sum;
			}
			return num;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00089B0C File Offset: 0x00087D0C
		public void ResetThrottledData(QueueQuotaResources resources, out int rejectedCount, out DateTime throttlingStartTime)
		{
			throttlingStartTime = DateTime.MaxValue;
			int num = 0;
			if ((byte)(resources & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				int num2 = Interlocked.Exchange(ref this.messagesRejectedDueToSubmissionQueue, 0);
				this.pastRejectedMessageSubmissionQueueCount.AddValue((long)num2);
				num += num2;
				throttlingStartTime = this.submissionThrottlingStartTime;
				this.submissionThrottlingStartTime = DateTime.MaxValue;
			}
			if ((byte)(resources & QueueQuotaResources.TotalQueueSize) != 0)
			{
				int num3 = Interlocked.Exchange(ref this.messagesRejectedDueToTotalQueue, 0);
				this.pastRejectedMessageTotalQueueCount.AddValue((long)num3);
				num += num3;
				if (throttlingStartTime != DateTime.MaxValue && DateTime.Compare(this.totalQueueThrottlingStartTime, throttlingStartTime) < 0)
				{
					throttlingStartTime = this.totalQueueThrottlingStartTime;
				}
				this.totalQueueThrottlingStartTime = DateTime.MaxValue;
			}
			rejectedCount = num;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00089BC8 File Offset: 0x00087DC8
		public virtual void Merge(UsageData source)
		{
			this.MergeValue(ref this.submissionQueueUsage, ref source.submissionQueueUsage);
			this.MergeValue(ref this.totalQueueUsage, ref source.totalQueueUsage);
			this.MergeValue(ref this.messagesRejectedDueToSubmissionQueue, ref source.messagesRejectedDueToSubmissionQueue);
			this.MergeValue(ref this.messagesRejectedDueToTotalQueue, ref source.messagesRejectedDueToTotalQueue);
			this.LastUpdateTime = DateTime.UtcNow;
			this.submissionThrottlingStartTime = new DateTime(Math.Min(this.submissionThrottlingStartTime.Ticks, source.submissionThrottlingStartTime.Ticks));
			this.totalQueueThrottlingStartTime = new DateTime(Math.Min(this.totalQueueThrottlingStartTime.Ticks, source.totalQueueThrottlingStartTime.Ticks));
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00089C74 File Offset: 0x00087E74
		private void MergeValue(ref int value, ref int newValue)
		{
			int num;
			do
			{
				num = value;
			}
			while (Interlocked.CompareExchange(ref value, num + newValue, num) != num);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00089C94 File Offset: 0x00087E94
		internal static void Cleanup<KT, VT>(ConcurrentDictionary<KT, VT> dictionary, TimeSpan cleanupInterval) where VT : UsageData
		{
			foreach (KeyValuePair<KT, VT> keyValuePair in dictionary)
			{
				DateTime utcNow = DateTime.UtcNow;
				VT value = keyValuePair.Value;
				if (utcNow - value.LastUpdateTime >= cleanupInterval)
				{
					VT value2 = keyValuePair.Value;
					if (value2.IsEmpty)
					{
						UsageData.SafeRemove<KT, VT>(dictionary, keyValuePair.Key);
						continue;
					}
				}
				OrganizationUsageData organizationUsageData = keyValuePair.Value as OrganizationUsageData;
				if (organizationUsageData != null)
				{
					UsageData.Cleanup<string, UsageData>(organizationUsageData.SenderQuotaDictionary, cleanupInterval);
				}
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00089D44 File Offset: 0x00087F44
		private static void SafeRemove<KT, VT>(ConcurrentDictionary<KT, VT> dictionary, KT key) where VT : UsageData
		{
			VT data;
			if (dictionary.TryRemove(key, out data) && !data.IsEmpty)
			{
				UsageData.AddOrMerge<KT, VT>(dictionary, key, data);
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00089D74 File Offset: 0x00087F74
		internal static void AddOrMerge<KT, VT>(ConcurrentDictionary<KT, VT> dictionary, KT key, VT data) where VT : UsageData
		{
			VT orAdd = dictionary.GetOrAdd(key, data);
			if (orAdd != data)
			{
				orAdd.Merge(data);
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00089DAC File Offset: 0x00087FAC
		internal XElement GetUsageElement(string elementName, QueueQuotaResources resource, string id)
		{
			XElement xelement = new XElement(elementName);
			xelement.SetAttributeValue("Id", id);
			xelement.SetAttributeValue("QueueUsage", this.GetUsage(resource));
			xelement.SetAttributeValue("IsOverQuota", this.GetIsOverQuota(resource));
			xelement.SetAttributeValue("RejectedCount", this.GetRejectedCount(resource));
			if ((byte)(resource & QueueQuotaResources.SubmissionQueueSize) != 0 && this.submissionThrottlingStartTime != DateTime.MaxValue)
			{
				xelement.SetAttributeValue("ThrottlingDuration", this.currentTimeProvider().Subtract(this.submissionThrottlingStartTime));
			}
			else if ((byte)(resource & QueueQuotaResources.TotalQueueSize) != 0 && this.totalQueueThrottlingStartTime != DateTime.MaxValue)
			{
				xelement.SetAttributeValue("ThrottlingDuration", this.currentTimeProvider().Subtract(this.totalQueueThrottlingStartTime));
			}
			return xelement;
		}

		// Token: 0x040012BF RID: 4799
		private readonly Func<DateTime> currentTimeProvider;

		// Token: 0x040012C0 RID: 4800
		private int submissionQueueUsage;

		// Token: 0x040012C1 RID: 4801
		private int totalQueueUsage;

		// Token: 0x040012C2 RID: 4802
		private bool isSubmissionQueueOverQuota;

		// Token: 0x040012C3 RID: 4803
		private bool isTotalQueueSizeOverQuota;

		// Token: 0x040012C4 RID: 4804
		private bool isSubmissionQueueOverWarning;

		// Token: 0x040012C5 RID: 4805
		private bool isTotalQueueOverWarning;

		// Token: 0x040012C6 RID: 4806
		private int messagesRejectedDueToSubmissionQueue;

		// Token: 0x040012C7 RID: 4807
		private int messagesRejectedDueToTotalQueue;

		// Token: 0x040012C8 RID: 4808
		private SlidingTotalCounter pastRejectedMessageSubmissionQueueCount;

		// Token: 0x040012C9 RID: 4809
		private SlidingTotalCounter pastRejectedMessageTotalQueueCount;

		// Token: 0x040012CA RID: 4810
		private DateTime submissionThrottlingStartTime = DateTime.MaxValue;

		// Token: 0x040012CB RID: 4811
		private DateTime totalQueueThrottlingStartTime = DateTime.MaxValue;
	}
}
