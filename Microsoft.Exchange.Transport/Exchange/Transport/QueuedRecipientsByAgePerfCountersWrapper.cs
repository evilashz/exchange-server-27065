using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000198 RID: 408
	internal class QueuedRecipientsByAgePerfCountersWrapper
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x00048790 File Offset: 0x00046990
		internal QueuedRecipientsByAgePerfCountersWrapper(bool enabled)
		{
			this.enabled = enabled;
			this.counterSubmission = new SegmentedSlidingCounter(QueuedRecipientsByAgePerfCountersWrapper.slidingWindowSegments, QueuedRecipientsByAgePerfCountersWrapper.bucketLength);
			this.counterInternalHop = new SegmentedSlidingCounter(QueuedRecipientsByAgePerfCountersWrapper.slidingWindowSegments, QueuedRecipientsByAgePerfCountersWrapper.bucketLength);
			this.counterInternalMailboxDelivery = new SegmentedSlidingCounter(QueuedRecipientsByAgePerfCountersWrapper.slidingWindowSegments, QueuedRecipientsByAgePerfCountersWrapper.bucketLength);
			this.counterExternalDelivery = new SegmentedSlidingCounter(QueuedRecipientsByAgePerfCountersWrapper.slidingWindowSegments, QueuedRecipientsByAgePerfCountersWrapper.bucketLength);
			int num = 0;
			this.instances = new QueuedRecipientsByAgePerfCountersInstance[QueuedRecipientsByAgePerfCountersWrapper.slidingWindowSegments.Length + 1];
			this.instances[num++] = QueuedRecipientsByAgePerfCounters.GetInstance("<=90sec");
			this.instances[num++] = QueuedRecipientsByAgePerfCounters.GetInstance("90sec_to_5min");
			this.instances[num++] = QueuedRecipientsByAgePerfCounters.GetInstance("5min_to_15min");
			this.instances[num++] = QueuedRecipientsByAgePerfCounters.GetInstance("15min_to_1hr");
			this.instances[num++] = QueuedRecipientsByAgePerfCounters.GetInstance(">1hr");
			this.instanceValues = new long[4][];
			for (int i = 0; i < 4; i++)
			{
				this.instanceValues[i] = new long[this.instances.Length];
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000488B0 File Offset: 0x00046AB0
		internal void TimedUpdate()
		{
			if (!this.enabled)
			{
				return;
			}
			this.counterSubmission.TimedUpdate(this.instanceValues[0]);
			this.counterInternalHop.TimedUpdate(this.instanceValues[1]);
			this.counterInternalMailboxDelivery.TimedUpdate(this.instanceValues[2]);
			this.counterExternalDelivery.TimedUpdate(this.instanceValues[3]);
			for (int i = 0; i < this.instances.Length; i++)
			{
				this.instances[i].SubmissionNormalPriority.RawValue = this.instanceValues[0][i];
				this.instances[i].InternalHopNormalPriority.RawValue = this.instanceValues[1][i];
				this.instances[i].InternalMailboxDeliveryNormalPriority.RawValue = this.instanceValues[2][i];
				this.instances[i].ExternalDeliveryNormalPriority.RawValue = this.instanceValues[3][i];
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004899C File Offset: 0x00046B9C
		internal void Reset()
		{
			if (!this.enabled)
			{
				return;
			}
			foreach (QueuedRecipientsByAgePerfCountersInstance queuedRecipientsByAgePerfCountersInstance in this.instances)
			{
				queuedRecipientsByAgePerfCountersInstance.SubmissionNormalPriority.RawValue = 0L;
				queuedRecipientsByAgePerfCountersInstance.InternalHopNormalPriority.RawValue = 0L;
				queuedRecipientsByAgePerfCountersInstance.InternalMailboxDeliveryNormalPriority.RawValue = 0L;
				queuedRecipientsByAgePerfCountersInstance.ExternalDeliveryNormalPriority.RawValue = 0L;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00048A00 File Offset: 0x00046C00
		internal void TrackEnteringSubmissionQueue(TransportMailItem item)
		{
			if (!this.enabled)
			{
				return;
			}
			if (item.QueuedRecipientsByAgeToken != null)
			{
				throw new InvalidOperationException("item already has a token!");
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = QueuedRecipientsByAgeToken.Generate(item);
			lock (queuedRecipientsByAgeToken)
			{
				item.QueuedRecipientsByAgeToken = queuedRecipientsByAgeToken;
				QueuedRecipientsByAgePerfCountersWrapper.TrackEntering(queuedRecipientsByAgeToken, this.counterSubmission);
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00048A6C File Offset: 0x00046C6C
		internal void TrackExitingSubmissionQueue(TransportMailItem item)
		{
			if (!this.enabled)
			{
				return;
			}
			if (item.QueuedRecipientsByAgeToken == null)
			{
				throw new InvalidOperationException("item leaving submission queue is not being tracked!");
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = item.QueuedRecipientsByAgeToken;
			lock (queuedRecipientsByAgeToken)
			{
				item.QueuedRecipientsByAgeToken = null;
				QueuedRecipientsByAgePerfCountersWrapper.TrackExiting(queuedRecipientsByAgeToken, this.counterSubmission);
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00048AD8 File Offset: 0x00046CD8
		internal QueuedRecipientsByAgeToken TrackEnteringCategorizer(TransportMailItem item)
		{
			if (!this.enabled)
			{
				return null;
			}
			if (item.QueuedRecipientsByAgeToken != null)
			{
				throw new InvalidOperationException("item already has a token!");
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = QueuedRecipientsByAgeToken.Generate(item);
			lock (queuedRecipientsByAgeToken)
			{
				QueuedRecipientsByAgePerfCountersWrapper.TrackEntering(queuedRecipientsByAgeToken, this.counterSubmission);
			}
			return queuedRecipientsByAgeToken;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00048B40 File Offset: 0x00046D40
		internal void TrackExitingCategorizer(QueuedRecipientsByAgeToken token)
		{
			if (!this.enabled)
			{
				return;
			}
			lock (token)
			{
				QueuedRecipientsByAgePerfCountersWrapper.TrackExiting(token, this.counterSubmission);
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00048B8C File Offset: 0x00046D8C
		internal void TrackEnteringDeliveryQueue(RoutedMailItem item)
		{
			if (!this.enabled)
			{
				return;
			}
			if (item.DeliveryType == DeliveryType.Unreachable)
			{
				return;
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = QueuedRecipientsByAgeToken.Generate(item);
			lock (queuedRecipientsByAgeToken)
			{
				item.QueuedRecipientsByAgeToken = queuedRecipientsByAgeToken;
				QueuedRecipientsByAgePerfCountersWrapper.TrackEntering(queuedRecipientsByAgeToken, this.GetCounter(queuedRecipientsByAgeToken.DeliveryType));
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00048BF8 File Offset: 0x00046DF8
		internal void TrackExitingDeliveryQueue(RoutedMailItem item)
		{
			if (!this.enabled)
			{
				return;
			}
			if (item.DeliveryType == DeliveryType.Unreachable)
			{
				return;
			}
			if (item.QueuedRecipientsByAgeToken == null)
			{
				throw new InvalidOperationException("item leaving delivery queue is not being tracked!");
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = item.QueuedRecipientsByAgeToken;
			lock (queuedRecipientsByAgeToken)
			{
				item.QueuedRecipientsByAgeToken = null;
				QueuedRecipientsByAgePerfCountersWrapper.TrackExiting(queuedRecipientsByAgeToken, this.GetCounter(queuedRecipientsByAgeToken.DeliveryType));
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00048C74 File Offset: 0x00046E74
		internal void TrackEnteringSmtpSend(RoutedMailItem item)
		{
			if (!this.enabled)
			{
				return;
			}
			if (item.QueuedRecipientsByAgeToken != null)
			{
				throw new InvalidOperationException("item already has a token!");
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = QueuedRecipientsByAgeToken.Generate(item);
			lock (queuedRecipientsByAgeToken)
			{
				item.QueuedRecipientsByAgeToken = queuedRecipientsByAgeToken;
				QueuedRecipientsByAgePerfCountersWrapper.TrackEntering(queuedRecipientsByAgeToken, this.GetCounter(queuedRecipientsByAgeToken.DeliveryType));
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00048CE8 File Offset: 0x00046EE8
		internal void TrackExitingSmtpSend(QueuedRecipientsByAgeToken token)
		{
			if (!this.enabled)
			{
				return;
			}
			lock (token)
			{
				QueuedRecipientsByAgePerfCountersWrapper.TrackExiting(token, this.GetCounter(token.DeliveryType));
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00048D38 File Offset: 0x00046F38
		private SegmentedSlidingCounter GetCounter(DeliveryType deliveryType)
		{
			SegmentedSlidingCounter result;
			if (NextHopType.IsMailboxDeliveryType(deliveryType))
			{
				result = this.counterInternalMailboxDelivery;
			}
			else if (TransportDeliveryTypes.internalDeliveryTypes.Contains(deliveryType))
			{
				result = this.counterInternalHop;
			}
			else
			{
				if (!TransportDeliveryTypes.externalDeliveryTypes.Contains(deliveryType))
				{
					throw new ArgumentException("cannot get the right counter for Delivery Type: " + deliveryType);
				}
				result = this.counterExternalDelivery;
			}
			return result;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00048D99 File Offset: 0x00046F99
		private static void TrackEntering(QueuedRecipientsByAgeToken token, SegmentedSlidingCounter counter)
		{
			if (token.DeliveryPriority == DeliveryPriority.Normal && token.RecipientCount > 0)
			{
				token.OrgArrivalTimeUsed = counter.AddEventsAt(token.OrgArrivalTimeUtc, (long)token.RecipientCount);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00048DC6 File Offset: 0x00046FC6
		private static void TrackExiting(QueuedRecipientsByAgeToken token, SegmentedSlidingCounter counter)
		{
			if (token.DeliveryPriority == DeliveryPriority.Normal && token.RecipientCount > 0)
			{
				counter.RemoveEventsAt(token.OrgArrivalTimeUsed, (long)token.RecipientCount);
			}
		}

		// Token: 0x04000967 RID: 2407
		private static readonly TimeSpan bucketLength = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000968 RID: 2408
		private static readonly TimeSpan[] slidingWindowSegments = new TimeSpan[]
		{
			TimeSpan.FromSeconds(90.0),
			TimeSpan.FromMinutes(5.0) - TimeSpan.FromSeconds(90.0),
			TimeSpan.FromMinutes(15.0) - TimeSpan.FromMinutes(5.0),
			TimeSpan.FromHours(1.0) - TimeSpan.FromMinutes(15.0)
		};

		// Token: 0x04000969 RID: 2409
		private readonly bool enabled;

		// Token: 0x0400096A RID: 2410
		private SegmentedSlidingCounter counterSubmission;

		// Token: 0x0400096B RID: 2411
		private SegmentedSlidingCounter counterInternalHop;

		// Token: 0x0400096C RID: 2412
		private SegmentedSlidingCounter counterInternalMailboxDelivery;

		// Token: 0x0400096D RID: 2413
		private SegmentedSlidingCounter counterExternalDelivery;

		// Token: 0x0400096E RID: 2414
		private QueuedRecipientsByAgePerfCountersInstance[] instances;

		// Token: 0x0400096F RID: 2415
		private long[][] instanceValues;
	}
}
