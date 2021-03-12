using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x02000323 RID: 803
	internal class TransportMessageQueue : MessageQueue
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x00083804 File Offset: 0x00081A04
		public TransportMessageQueue(RoutedQueueBase queueStorage, PriorityBehaviour behaviour) : base(behaviour)
		{
			this.queueStorage = queueStorage;
			this.incomingRateCounter = new SlidingPercentageCounter(TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(5.0));
			this.outgoingRateCounter = new SlidingPercentageCounter(TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(5.0));
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0008386D File Offset: 0x00081A6D
		protected TransportMessageQueue(PriorityBehaviour behaviour) : base(behaviour)
		{
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00083876 File Offset: 0x00081A76
		public double IncomingRate
		{
			get
			{
				return this.incomingRate;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0008387E File Offset: 0x00081A7E
		public double OutgoingRate
		{
			get
			{
				return this.outgoingRate;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00083886 File Offset: 0x00081A86
		public double Velocity
		{
			get
			{
				return this.outgoingRate - this.incomingRate;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x00083895 File Offset: 0x00081A95
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x000838A2 File Offset: 0x00081AA2
		public virtual bool Suspended
		{
			get
			{
				return this.queueStorage.Suspended;
			}
			set
			{
				this.queueStorage.Suspended = value;
				this.queueStorage.Commit();
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000838BB File Offset: 0x00081ABB
		public virtual void Delete()
		{
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000838BD File Offset: 0x00081ABD
		public virtual bool IsInterestingQueueToLog()
		{
			return base.TotalCount >= Components.TransportAppConfig.QueueConfiguration.QueueLoggingThreshold || (this.Suspended && base.TotalCount > 0);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000838EC File Offset: 0x00081AEC
		protected void UpdateQueueRates()
		{
			DateTime utcNow = DateTime.UtcNow;
			double totalSeconds = (utcNow - this.lastRateUpdateTime).TotalSeconds;
			int num = Interlocked.Exchange(ref this.lastIncomingMessageCount, 0);
			this.incomingRateCounter.Add((long)num, (long)totalSeconds);
			this.incomingRate = this.incomingRateCounter.GetSlidingPercentage() / 100.0;
			int num2 = Interlocked.Exchange(ref this.lastOutgoingMessageCount, 0);
			this.outgoingRateCounter.Add((long)num2, (long)totalSeconds);
			this.outgoingRate = this.outgoingRateCounter.GetSlidingPercentage() / 100.0;
			this.lastRateUpdateTime = utcNow;
		}

		// Token: 0x04001223 RID: 4643
		protected RoutedQueueBase queueStorage;

		// Token: 0x04001224 RID: 4644
		protected int lastIncomingMessageCount;

		// Token: 0x04001225 RID: 4645
		protected int lastOutgoingMessageCount;

		// Token: 0x04001226 RID: 4646
		private double incomingRate;

		// Token: 0x04001227 RID: 4647
		private SlidingPercentageCounter incomingRateCounter;

		// Token: 0x04001228 RID: 4648
		private double outgoingRate;

		// Token: 0x04001229 RID: 4649
		private SlidingPercentageCounter outgoingRateCounter;

		// Token: 0x0400122A RID: 4650
		private DateTime lastRateUpdateTime;
	}
}
