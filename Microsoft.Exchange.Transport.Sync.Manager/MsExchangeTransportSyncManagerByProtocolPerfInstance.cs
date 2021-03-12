using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MsExchangeTransportSyncManagerByProtocolPerfInstance : PerformanceCounterInstance
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x0001B18C File Offset: 0x0001938C
		internal MsExchangeTransportSyncManagerByProtocolPerfInstance(string instanceName, MsExchangeTransportSyncManagerByProtocolPerfInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Transport Sync Manager By Protocol")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageDispatchTime = new ExPerformanceCounter(base.CategoryName, "Average Time to Complete Dispatch (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDispatchTime);
				this.AverageDispatchTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Time to Complete Dispatch Base (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDispatchTimeBase);
				this.LastDispatchTime = new ExPerformanceCounter(base.CategoryName, "Time to Complete Last Dispatch (msec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LastDispatchTime);
				this.LastSubscriptionProcessingTime = new ExPerformanceCounter(base.CategoryName, "Last Subscription Dispatch to Completion Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LastSubscriptionProcessingTime);
				this.ProcessingTimeToSyncSubscription95Percent = new ExPerformanceCounter(base.CategoryName, "95 Percentile Processing Time to Sync a Subscription (secs)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingTimeToSyncSubscription95Percent);
				this.SubscriptionsCompletingSync = new ExPerformanceCounter(base.CategoryName, "Subscriptions Completing Sync", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsCompletingSync);
				this.SubscriptionsQueued = new ExPerformanceCounter(base.CategoryName, "Dispatch Queue - Total Subscriptions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsQueued);
				this.SyncNowSubscriptionsQueued = new ExPerformanceCounter(base.CategoryName, "Dispatch Queue - Total Sync Now Subscriptions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SyncNowSubscriptionsQueued);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Attempts per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.SubscriptionsDispatched = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Attempts", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.SubscriptionsDispatched);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Successful Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.SuccessfulSubmissions = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Successful", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.SuccessfulSubmissions);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Duplicate Attempts Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.DuplicateSubmissions = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Duplicate Attempts", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.DuplicateSubmissions);
				this.TemporarySubmissionFailures = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Transient Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TemporarySubmissionFailures);
				long num = this.AverageDispatchTime.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001B4BC File Offset: 0x000196BC
		internal MsExchangeTransportSyncManagerByProtocolPerfInstance(string instanceName) : base(instanceName, "MSExchange Transport Sync Manager By Protocol")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageDispatchTime = new ExPerformanceCounter(base.CategoryName, "Average Time to Complete Dispatch (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDispatchTime);
				this.AverageDispatchTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Time to Complete Dispatch Base (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDispatchTimeBase);
				this.LastDispatchTime = new ExPerformanceCounter(base.CategoryName, "Time to Complete Last Dispatch (msec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LastDispatchTime);
				this.LastSubscriptionProcessingTime = new ExPerformanceCounter(base.CategoryName, "Last Subscription Dispatch to Completion Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LastSubscriptionProcessingTime);
				this.ProcessingTimeToSyncSubscription95Percent = new ExPerformanceCounter(base.CategoryName, "95 Percentile Processing Time to Sync a Subscription (secs)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingTimeToSyncSubscription95Percent);
				this.SubscriptionsCompletingSync = new ExPerformanceCounter(base.CategoryName, "Subscriptions Completing Sync", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsCompletingSync);
				this.SubscriptionsQueued = new ExPerformanceCounter(base.CategoryName, "Dispatch Queue - Total Subscriptions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsQueued);
				this.SyncNowSubscriptionsQueued = new ExPerformanceCounter(base.CategoryName, "Dispatch Queue - Total Sync Now Subscriptions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SyncNowSubscriptionsQueued);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Attempts per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.SubscriptionsDispatched = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Attempts", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.SubscriptionsDispatched);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Successful Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.SuccessfulSubmissions = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Successful", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.SuccessfulSubmissions);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Duplicate Attempts Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.DuplicateSubmissions = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Duplicate Attempts", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.DuplicateSubmissions);
				this.TemporarySubmissionFailures = new ExPerformanceCounter(base.CategoryName, "Subscription Dispatch - Total Transient Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TemporarySubmissionFailures);
				long num = this.AverageDispatchTime.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001B7EC File Offset: 0x000199EC
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000289 RID: 649
		public readonly ExPerformanceCounter AverageDispatchTime;

		// Token: 0x0400028A RID: 650
		public readonly ExPerformanceCounter AverageDispatchTimeBase;

		// Token: 0x0400028B RID: 651
		public readonly ExPerformanceCounter LastDispatchTime;

		// Token: 0x0400028C RID: 652
		public readonly ExPerformanceCounter LastSubscriptionProcessingTime;

		// Token: 0x0400028D RID: 653
		public readonly ExPerformanceCounter ProcessingTimeToSyncSubscription95Percent;

		// Token: 0x0400028E RID: 654
		public readonly ExPerformanceCounter SubscriptionsCompletingSync;

		// Token: 0x0400028F RID: 655
		public readonly ExPerformanceCounter SubscriptionsQueued;

		// Token: 0x04000290 RID: 656
		public readonly ExPerformanceCounter SyncNowSubscriptionsQueued;

		// Token: 0x04000291 RID: 657
		public readonly ExPerformanceCounter SubscriptionsDispatched;

		// Token: 0x04000292 RID: 658
		public readonly ExPerformanceCounter SuccessfulSubmissions;

		// Token: 0x04000293 RID: 659
		public readonly ExPerformanceCounter DuplicateSubmissions;

		// Token: 0x04000294 RID: 660
		public readonly ExPerformanceCounter TemporarySubmissionFailures;
	}
}
