using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200016C RID: 364
	internal class E2ELatencyBucketsPerfCountersWrapper
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x00040228 File Offset: 0x0003E428
		internal E2ELatencyBucketsPerfCountersWrapper()
		{
			this.instanceLe90Sec = E2ELatencyBucketsPerfCounters.GetInstance("<=90sec");
			this.instanceGt90SecLe15Min = E2ELatencyBucketsPerfCounters.GetInstance("90sec_to_15min");
			this.instanceGt15Min = E2ELatencyBucketsPerfCounters.GetInstance(">15min");
			this.latencyBucketsMap = new Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>, Tuple<SlidingTotalCounter, ExPerformanceCounter>>
			{
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.DeliverHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.DeliverHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.DeliverHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.DeliverNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.DeliverNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.DeliverNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.DeliverLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.DeliverLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.DeliverLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.DeliverNonePriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.DeliverNonePriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.DeliverNonePriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.SendToExternalHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.SendToExternalHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.High, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.SendToExternalHighPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.SendToExternalNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.SendToExternalNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Normal, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.SendToExternalNormalPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.SendToExternalLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.SendToExternalLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Low, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.SendToExternalLowPriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceLe90Sec.SendToExternalNonePriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt90SecLe15Min.SendToExternalNonePriority)
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.None, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min),
					new Tuple<SlidingTotalCounter, ExPerformanceCounter>(new SlidingTotalCounter(E2ELatencyBucketsPerfCountersWrapper.SlidingWindowLength, E2ELatencyBucketsPerfCountersWrapper.BucketLength), this.instanceGt15Min.SendToExternalNonePriority)
				}
			};
			this.slaInstanceHigh = E2ELatencySlaPerfCounters.GetInstance("high");
			this.slaInstanceNormal = E2ELatencySlaPerfCounters.GetInstance("normal");
			this.slaInstanceLow = E2ELatencySlaPerfCounters.GetInstance("low");
			this.slaInstanceNone = E2ELatencySlaPerfCounters.GetInstance("none");
			this.latencySlaMap = new Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>, ExPerformanceCounter>
			{
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.High),
					this.slaInstanceHigh.DeliverPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Normal),
					this.slaInstanceNormal.DeliverPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Low),
					this.slaInstanceLow.DeliverPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.None),
					this.slaInstanceNone.DeliverPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.High),
					this.slaInstanceHigh.SendToExternalPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Normal),
					this.slaInstanceNormal.SendToExternalPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Low),
					this.slaInstanceLow.SendToExternalPercentMeetingSla
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.None),
					this.slaInstanceNone.SendToExternalPercentMeetingSla
				}
			};
			this.instanceTotal = E2ELatencyBucketsPerfCounters.TotalInstance;
			this.totalPerfCountersMap = new Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>, ExPerformanceCounter>
			{
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.High),
					this.instanceTotal.DeliverHighPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Normal),
					this.instanceTotal.DeliverNormalPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.Low),
					this.instanceTotal.DeliverLowPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, DeliveryPriority.None),
					this.instanceTotal.DeliverNonePriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.High),
					this.instanceTotal.SendToExternalHighPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Normal),
					this.instanceTotal.SendToExternalNormalPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.Low),
					this.instanceTotal.SendToExternalLowPriority
				},
				{
					new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, DeliveryPriority.None),
					this.instanceTotal.SendToExternalNonePriority
				}
			};
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00040898 File Offset: 0x0003EA98
		internal void Flush()
		{
			lock (this)
			{
				foreach (Tuple<SlidingTotalCounter, ExPerformanceCounter> tuple in this.latencyBucketsMap.Values)
				{
					SlidingTotalCounter item = tuple.Item1;
					ExPerformanceCounter item2 = tuple.Item2;
					item2.RawValue = item.Sum;
				}
				foreach (Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority> tuple2 in this.latencySlaMap.Keys)
				{
					this.UpdateLatencySlaCounters(tuple2.Item1, tuple2.Item2);
				}
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00040980 File Offset: 0x0003EB80
		internal void Reset()
		{
			foreach (Tuple<SlidingTotalCounter, ExPerformanceCounter> tuple in this.latencyBucketsMap.Values)
			{
				ExPerformanceCounter item = tuple.Item2;
				item.RawValue = 0L;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in this.latencySlaMap.Values)
			{
				exPerformanceCounter.RawValue = 0L;
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00040A2C File Offset: 0x0003EC2C
		internal SlidingTotalCounter GetSlidingTotalCounterForUnitTestsOnly(E2ELatencyBucketsPerfCountersWrapper.EventType eventType, DeliveryPriority priority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket bucket)
		{
			Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket> key = new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(eventType, priority, bucket);
			Tuple<SlidingTotalCounter, ExPerformanceCounter> tuple = this.latencyBucketsMap[key];
			return tuple.Item1;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00040A55 File Offset: 0x0003EC55
		internal void RecordMailboxDeliveryLatency(DeliveryPriority priority, TimeSpan latency, int recipientCount)
		{
			this.RecordMessageLatency(E2ELatencyBucketsPerfCountersWrapper.EventType.MailboxDelivery, priority, latency, recipientCount);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00040A61 File Offset: 0x0003EC61
		internal void RecordExternalSendLatency(DeliveryPriority priority, TimeSpan latency, int recipientCount)
		{
			this.RecordMessageLatency(E2ELatencyBucketsPerfCountersWrapper.EventType.ExternalSend, priority, latency, recipientCount);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00040A70 File Offset: 0x0003EC70
		private void RecordMessageLatency(E2ELatencyBucketsPerfCountersWrapper.EventType eventType, DeliveryPriority priority, TimeSpan latency, int recipientCount)
		{
			if (recipientCount <= 0)
			{
				throw new ArgumentOutOfRangeException("recipientCount: " + recipientCount);
			}
			E2ELatencyBucketsPerfCountersWrapper.LatencyBucket item;
			if (latency.TotalSeconds <= 90.0)
			{
				item = E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec;
			}
			else if (latency.TotalMinutes <= 15.0)
			{
				item = E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt90SecLe15Min;
			}
			else
			{
				item = E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Gt15Min;
			}
			Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket> key = new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(eventType, priority, item);
			Tuple<SlidingTotalCounter, ExPerformanceCounter> tuple = this.latencyBucketsMap[key];
			SlidingTotalCounter item2 = tuple.Item1;
			ExPerformanceCounter item3 = tuple.Item2;
			lock (this)
			{
				item3.RawValue = item2.AddValue((long)recipientCount);
				this.UpdateLatencySlaCounters(eventType, priority);
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00040B30 File Offset: 0x0003ED30
		private void UpdateLatencySlaCounters(E2ELatencyBucketsPerfCountersWrapper.EventType eventType, DeliveryPriority priority)
		{
			Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket> key = new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>(eventType, priority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket.Le90Sec);
			Tuple<SlidingTotalCounter, ExPerformanceCounter> tuple = this.latencyBucketsMap[key];
			ExPerformanceCounter item = tuple.Item2;
			Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority> key2 = new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(eventType, priority);
			ExPerformanceCounter exPerformanceCounter = this.totalPerfCountersMap[key2];
			double num = (exPerformanceCounter.RawValue > 0L) ? ((double)item.RawValue / (double)exPerformanceCounter.RawValue) : 1.0;
			Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority> key3 = new Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>(eventType, priority);
			ExPerformanceCounter exPerformanceCounter2 = this.latencySlaMap[key3];
			exPerformanceCounter2.RawValue = (long)(num * 100.0);
		}

		// Token: 0x040007F1 RID: 2033
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x040007F2 RID: 2034
		private static readonly TimeSpan BucketLength = TimeSpan.FromSeconds(1.0);

		// Token: 0x040007F3 RID: 2035
		private readonly E2ELatencyBucketsPerfCountersInstance instanceLe90Sec;

		// Token: 0x040007F4 RID: 2036
		private readonly E2ELatencyBucketsPerfCountersInstance instanceGt90SecLe15Min;

		// Token: 0x040007F5 RID: 2037
		private readonly E2ELatencyBucketsPerfCountersInstance instanceGt15Min;

		// Token: 0x040007F6 RID: 2038
		private readonly E2ELatencyBucketsPerfCountersInstance instanceTotal;

		// Token: 0x040007F7 RID: 2039
		private readonly E2ELatencySlaPerfCountersInstance slaInstanceHigh;

		// Token: 0x040007F8 RID: 2040
		private readonly E2ELatencySlaPerfCountersInstance slaInstanceNormal;

		// Token: 0x040007F9 RID: 2041
		private readonly E2ELatencySlaPerfCountersInstance slaInstanceLow;

		// Token: 0x040007FA RID: 2042
		private readonly E2ELatencySlaPerfCountersInstance slaInstanceNone;

		// Token: 0x040007FB RID: 2043
		private readonly Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority, E2ELatencyBucketsPerfCountersWrapper.LatencyBucket>, Tuple<SlidingTotalCounter, ExPerformanceCounter>> latencyBucketsMap;

		// Token: 0x040007FC RID: 2044
		private readonly Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>, ExPerformanceCounter> latencySlaMap;

		// Token: 0x040007FD RID: 2045
		private readonly Dictionary<Tuple<E2ELatencyBucketsPerfCountersWrapper.EventType, DeliveryPriority>, ExPerformanceCounter> totalPerfCountersMap;

		// Token: 0x0200016D RID: 365
		internal enum LatencyBucket
		{
			// Token: 0x040007FF RID: 2047
			Le90Sec,
			// Token: 0x04000800 RID: 2048
			Gt90SecLe15Min,
			// Token: 0x04000801 RID: 2049
			Gt15Min
		}

		// Token: 0x0200016E RID: 366
		internal enum EventType
		{
			// Token: 0x04000803 RID: 2051
			MailboxDelivery,
			// Token: 0x04000804 RID: 2052
			ExternalSend
		}
	}
}
