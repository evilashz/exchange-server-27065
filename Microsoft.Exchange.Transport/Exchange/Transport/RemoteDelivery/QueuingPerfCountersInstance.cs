using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x02000549 RID: 1353
	internal sealed class QueuingPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003E9C RID: 16028 RVA: 0x0010B218 File Offset: 0x00109418
		internal QueuingPerfCountersInstance(string instanceName, QueuingPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Queues")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ExternalActiveRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Active Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalActiveRemoteDeliveryQueueLength);
				this.InternalActiveRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Active Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalActiveRemoteDeliveryQueueLength);
				this.ExternalRetryRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Retry Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalRetryRemoteDeliveryQueueLength);
				this.InternalRetryRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Retry Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalRetryRemoteDeliveryQueueLength);
				this.ActiveMailboxDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Active Mailbox Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMailboxDeliveryQueueLength);
				this.RetryMailboxDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Retry Mailbox Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryMailboxDeliveryQueueLength);
				this.ActiveNonSmtpDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Active Non-Smtp Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveNonSmtpDeliveryQueueLength);
				this.RetryNonSmtpDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Retry Non-Smtp Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryNonSmtpDeliveryQueueLength);
				this.InternalAggregateDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Aggregate Delivery Queue Length (All Internal Queues)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalAggregateDeliveryQueueLength);
				this.ExternalAggregateDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Aggregate Delivery Queue Length (All External Queues)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalAggregateDeliveryQueueLength);
				this.InternalLargestDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Largest Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalLargestDeliveryQueueLength);
				this.InternalLargestUnlockedDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Largest Unlocked Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalLargestUnlockedDeliveryQueueLength);
				this.ExternalLargestDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Largest Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalLargestDeliveryQueueLength);
				this.ExternalLargestUnlockedDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Largest Unlocked Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalLargestUnlockedDeliveryQueueLength);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Items Queued for Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.ItemsQueuedForDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Items Queued For Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.ItemsQueuedForDeliveryTotal);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Items Completed Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.ItemsCompletedDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Items Completed Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.ItemsCompletedDeliveryTotal);
				this.ItemsQueuedForDeliveryExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Items Queued For Delivery Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsQueuedForDeliveryExpiredTotal);
				this.LocksExpiredInDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Locks Expired In Delivery Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LocksExpiredInDeliveryTotal);
				this.ItemsDeletedByAdminTotal = new ExPerformanceCounter(base.CategoryName, "Items Deleted By Admin Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsDeletedByAdminTotal);
				this.ItemsResubmittedTotal = new ExPerformanceCounter(base.CategoryName, "Items Resubmitted Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsResubmittedTotal);
				this.MessagesQueuedForDelivery = new ExPerformanceCounter(base.CategoryName, "Messages Queued For Delivery", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesQueuedForDelivery);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Messages Queued for Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.MessagesQueuedForDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Messages Queued For Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.MessagesQueuedForDeliveryTotal);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Completed Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesCompletedDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Messages Completed Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.MessagesCompletedDeliveryTotal);
				this.UnreachableQueueLength = new ExPerformanceCounter(base.CategoryName, "Unreachable Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnreachableQueueLength);
				this.PoisonQueueLength = new ExPerformanceCounter(base.CategoryName, "Poison Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonQueueLength);
				this.SubmissionQueueLength = new ExPerformanceCounter(base.CategoryName, "Submission Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueLength);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MessagesSubmittedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.MessagesSubmittedTotal);
				this.MessagesSubmittedRecently = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Recently", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesSubmittedRecently);
				this.SubmissionQueueItemsExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Submission Queue Items Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueItemsExpiredTotal);
				this.SubmissionQueueLocksExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Submission Queue Locks Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueLocksExpiredTotal);
				this.AggregateShadowQueueLength = new ExPerformanceCounter(base.CategoryName, "Aggregate Shadow Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateShadowQueueLength);
				this.ShadowQueueAutoDiscardsTotal = new ExPerformanceCounter(base.CategoryName, "Shadow Queue Auto Discards Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowQueueAutoDiscardsTotal);
				this.MessagesCompletingCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Completing Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCompletingCategorization);
				this.MessagesDeferredDuringCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Deferred during Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferredDuringCategorization);
				this.MessagesResubmittedDuringCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Resubmitted during Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesResubmittedDuringCategorization);
				this.CategorizerJobAvailability = new ExPerformanceCounter(base.CategoryName, "Categorizer Job Availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CategorizerJobAvailability);
				long num = this.ExternalActiveRemoteDeliveryQueueLength.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x0010B9BC File Offset: 0x00109BBC
		internal QueuingPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Queues")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ExternalActiveRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Active Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalActiveRemoteDeliveryQueueLength);
				this.InternalActiveRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Active Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalActiveRemoteDeliveryQueueLength);
				this.ExternalRetryRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Retry Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalRetryRemoteDeliveryQueueLength);
				this.InternalRetryRemoteDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Retry Remote Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalRetryRemoteDeliveryQueueLength);
				this.ActiveMailboxDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Active Mailbox Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveMailboxDeliveryQueueLength);
				this.RetryMailboxDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Retry Mailbox Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryMailboxDeliveryQueueLength);
				this.ActiveNonSmtpDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Active Non-Smtp Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveNonSmtpDeliveryQueueLength);
				this.RetryNonSmtpDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Retry Non-Smtp Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryNonSmtpDeliveryQueueLength);
				this.InternalAggregateDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Aggregate Delivery Queue Length (All Internal Queues)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalAggregateDeliveryQueueLength);
				this.ExternalAggregateDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Aggregate Delivery Queue Length (All External Queues)", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalAggregateDeliveryQueueLength);
				this.InternalLargestDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Largest Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalLargestDeliveryQueueLength);
				this.InternalLargestUnlockedDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "Internal Largest Unlocked Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InternalLargestUnlockedDeliveryQueueLength);
				this.ExternalLargestDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Largest Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalLargestDeliveryQueueLength);
				this.ExternalLargestUnlockedDeliveryQueueLength = new ExPerformanceCounter(base.CategoryName, "External Largest Unlocked Delivery Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalLargestUnlockedDeliveryQueueLength);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Items Queued for Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.ItemsQueuedForDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Items Queued For Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.ItemsQueuedForDeliveryTotal);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Items Completed Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.ItemsCompletedDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Items Completed Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.ItemsCompletedDeliveryTotal);
				this.ItemsQueuedForDeliveryExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Items Queued For Delivery Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsQueuedForDeliveryExpiredTotal);
				this.LocksExpiredInDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Locks Expired In Delivery Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LocksExpiredInDeliveryTotal);
				this.ItemsDeletedByAdminTotal = new ExPerformanceCounter(base.CategoryName, "Items Deleted By Admin Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsDeletedByAdminTotal);
				this.ItemsResubmittedTotal = new ExPerformanceCounter(base.CategoryName, "Items Resubmitted Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ItemsResubmittedTotal);
				this.MessagesQueuedForDelivery = new ExPerformanceCounter(base.CategoryName, "Messages Queued For Delivery", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesQueuedForDelivery);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Messages Queued for Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.MessagesQueuedForDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Messages Queued For Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.MessagesQueuedForDeliveryTotal);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Completed Delivery Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesCompletedDeliveryTotal = new ExPerformanceCounter(base.CategoryName, "Messages Completed Delivery Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.MessagesCompletedDeliveryTotal);
				this.UnreachableQueueLength = new ExPerformanceCounter(base.CategoryName, "Unreachable Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnreachableQueueLength);
				this.PoisonQueueLength = new ExPerformanceCounter(base.CategoryName, "Poison Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonQueueLength);
				this.SubmissionQueueLength = new ExPerformanceCounter(base.CategoryName, "Submission Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueLength);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MessagesSubmittedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.MessagesSubmittedTotal);
				this.MessagesSubmittedRecently = new ExPerformanceCounter(base.CategoryName, "Messages Submitted Recently", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesSubmittedRecently);
				this.SubmissionQueueItemsExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Submission Queue Items Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueItemsExpiredTotal);
				this.SubmissionQueueLocksExpiredTotal = new ExPerformanceCounter(base.CategoryName, "Submission Queue Locks Expired Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionQueueLocksExpiredTotal);
				this.AggregateShadowQueueLength = new ExPerformanceCounter(base.CategoryName, "Aggregate Shadow Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateShadowQueueLength);
				this.ShadowQueueAutoDiscardsTotal = new ExPerformanceCounter(base.CategoryName, "Shadow Queue Auto Discards Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowQueueAutoDiscardsTotal);
				this.MessagesCompletingCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Completing Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCompletingCategorization);
				this.MessagesDeferredDuringCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Deferred during Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferredDuringCategorization);
				this.MessagesResubmittedDuringCategorization = new ExPerformanceCounter(base.CategoryName, "Messages Resubmitted during Categorization", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesResubmittedDuringCategorization);
				this.CategorizerJobAvailability = new ExPerformanceCounter(base.CategoryName, "Categorizer Job Availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CategorizerJobAvailability);
				long num = this.ExternalActiveRemoteDeliveryQueueLength.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x0010C160 File Offset: 0x0010A360
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

		// Token: 0x040022AA RID: 8874
		public readonly ExPerformanceCounter ExternalActiveRemoteDeliveryQueueLength;

		// Token: 0x040022AB RID: 8875
		public readonly ExPerformanceCounter InternalActiveRemoteDeliveryQueueLength;

		// Token: 0x040022AC RID: 8876
		public readonly ExPerformanceCounter ExternalRetryRemoteDeliveryQueueLength;

		// Token: 0x040022AD RID: 8877
		public readonly ExPerformanceCounter InternalRetryRemoteDeliveryQueueLength;

		// Token: 0x040022AE RID: 8878
		public readonly ExPerformanceCounter ActiveMailboxDeliveryQueueLength;

		// Token: 0x040022AF RID: 8879
		public readonly ExPerformanceCounter RetryMailboxDeliveryQueueLength;

		// Token: 0x040022B0 RID: 8880
		public readonly ExPerformanceCounter ActiveNonSmtpDeliveryQueueLength;

		// Token: 0x040022B1 RID: 8881
		public readonly ExPerformanceCounter RetryNonSmtpDeliveryQueueLength;

		// Token: 0x040022B2 RID: 8882
		public readonly ExPerformanceCounter InternalAggregateDeliveryQueueLength;

		// Token: 0x040022B3 RID: 8883
		public readonly ExPerformanceCounter ExternalAggregateDeliveryQueueLength;

		// Token: 0x040022B4 RID: 8884
		public readonly ExPerformanceCounter InternalLargestDeliveryQueueLength;

		// Token: 0x040022B5 RID: 8885
		public readonly ExPerformanceCounter InternalLargestUnlockedDeliveryQueueLength;

		// Token: 0x040022B6 RID: 8886
		public readonly ExPerformanceCounter ExternalLargestDeliveryQueueLength;

		// Token: 0x040022B7 RID: 8887
		public readonly ExPerformanceCounter ExternalLargestUnlockedDeliveryQueueLength;

		// Token: 0x040022B8 RID: 8888
		public readonly ExPerformanceCounter ItemsQueuedForDeliveryTotal;

		// Token: 0x040022B9 RID: 8889
		public readonly ExPerformanceCounter ItemsCompletedDeliveryTotal;

		// Token: 0x040022BA RID: 8890
		public readonly ExPerformanceCounter ItemsQueuedForDeliveryExpiredTotal;

		// Token: 0x040022BB RID: 8891
		public readonly ExPerformanceCounter LocksExpiredInDeliveryTotal;

		// Token: 0x040022BC RID: 8892
		public readonly ExPerformanceCounter ItemsDeletedByAdminTotal;

		// Token: 0x040022BD RID: 8893
		public readonly ExPerformanceCounter ItemsResubmittedTotal;

		// Token: 0x040022BE RID: 8894
		public readonly ExPerformanceCounter MessagesQueuedForDelivery;

		// Token: 0x040022BF RID: 8895
		public readonly ExPerformanceCounter MessagesQueuedForDeliveryTotal;

		// Token: 0x040022C0 RID: 8896
		public readonly ExPerformanceCounter MessagesCompletedDeliveryTotal;

		// Token: 0x040022C1 RID: 8897
		public readonly ExPerformanceCounter UnreachableQueueLength;

		// Token: 0x040022C2 RID: 8898
		public readonly ExPerformanceCounter PoisonQueueLength;

		// Token: 0x040022C3 RID: 8899
		public readonly ExPerformanceCounter SubmissionQueueLength;

		// Token: 0x040022C4 RID: 8900
		public readonly ExPerformanceCounter MessagesSubmittedTotal;

		// Token: 0x040022C5 RID: 8901
		public readonly ExPerformanceCounter MessagesSubmittedRecently;

		// Token: 0x040022C6 RID: 8902
		public readonly ExPerformanceCounter SubmissionQueueItemsExpiredTotal;

		// Token: 0x040022C7 RID: 8903
		public readonly ExPerformanceCounter SubmissionQueueLocksExpiredTotal;

		// Token: 0x040022C8 RID: 8904
		public readonly ExPerformanceCounter AggregateShadowQueueLength;

		// Token: 0x040022C9 RID: 8905
		public readonly ExPerformanceCounter ShadowQueueAutoDiscardsTotal;

		// Token: 0x040022CA RID: 8906
		public readonly ExPerformanceCounter MessagesCompletingCategorization;

		// Token: 0x040022CB RID: 8907
		public readonly ExPerformanceCounter MessagesDeferredDuringCategorization;

		// Token: 0x040022CC RID: 8908
		public readonly ExPerformanceCounter MessagesResubmittedDuringCategorization;

		// Token: 0x040022CD RID: 8909
		public readonly ExPerformanceCounter CategorizerJobAvailability;
	}
}
