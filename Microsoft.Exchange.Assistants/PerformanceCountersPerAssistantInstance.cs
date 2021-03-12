using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000008 RID: 8
	internal sealed class PerformanceCountersPerAssistantInstance : PerformanceCounterInstance
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002FBC File Offset: 0x000011BC
		internal PerformanceCountersPerAssistantInstance(string instanceName, PerformanceCountersPerAssistantInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Assistants - Per Assistant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.EventsInQueueCurrent = new ExPerformanceCounter(base.CategoryName, "Events in Queue", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsInQueueCurrent, new ExPerformanceCounter[0]);
				list.Add(this.EventsInQueueCurrent);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Events Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.EventsProcessed = new ExPerformanceCounter(base.CategoryName, "Events Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsProcessed, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.EventsProcessed);
				this.AverageEventQueueTime = new ExPerformanceCounter(base.CategoryName, "Average Event Queue Time In Seconds", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageEventQueueTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventQueueTime);
				this.AverageEventQueueTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Event Queue Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageEventQueueTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventQueueTimeBase);
				this.AverageEventProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Event Processing Time In Seconds", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageEventProcessingTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventProcessingTime);
				this.AverageEventProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Event Processing Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageEventProcessingTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventProcessingTimeBase);
				this.InterestingEvents = new ExPerformanceCounter(base.CategoryName, "Percentage of Interesting Events", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InterestingEvents, new ExPerformanceCounter[0]);
				list.Add(this.InterestingEvents);
				this.InterestingEventsBase = new ExPerformanceCounter(base.CategoryName, "Base counter for Percentage of Interesting Events", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InterestingEventsBase, new ExPerformanceCounter[0]);
				list.Add(this.InterestingEventsBase);
				this.EventsDiscardedByMailboxFilter = new ExPerformanceCounter(base.CategoryName, "Percentage of Events Discarded by Mailbox Filter", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsDiscardedByMailboxFilter, new ExPerformanceCounter[0]);
				list.Add(this.EventsDiscardedByMailboxFilter);
				this.EventsDiscardedByMailboxFilterBase = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Events Discarded by Mailbox Filter", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsDiscardedByMailboxFilterBase, new ExPerformanceCounter[0]);
				list.Add(this.EventsDiscardedByMailboxFilterBase);
				this.QueuedEventsDiscardedByMailboxFilter = new ExPerformanceCounter(base.CategoryName, "Percentage of Queued Events Discarded by Mailbox Filter", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.QueuedEventsDiscardedByMailboxFilter, new ExPerformanceCounter[0]);
				list.Add(this.QueuedEventsDiscardedByMailboxFilter);
				this.QueuedEventsDiscardedByMailboxFilterBase = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Queued Events Discarded by Mailbox Filter", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.QueuedEventsDiscardedByMailboxFilterBase, new ExPerformanceCounter[0]);
				list.Add(this.QueuedEventsDiscardedByMailboxFilterBase);
				this.ElapsedTimeSinceLastEventQueued = new ExPerformanceCounter(base.CategoryName, "Elapsed Time Since Last Event Queued", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ElapsedTimeSinceLastEventQueued);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Percentage of Failed Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.FailedDispatchers = new ExPerformanceCounter(base.CategoryName, "Failed Event Dispatchers", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedDispatchers, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.FailedDispatchers);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Failed Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.EventDispatchers = new ExPerformanceCounter(base.CategoryName, "Event Dispatchers", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventDispatchers, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.EventDispatchers);
				this.HandledExceptions = new ExPerformanceCounter(base.CategoryName, "Handled Exceptions", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.HandledExceptions, new ExPerformanceCounter[0]);
				list.Add(this.HandledExceptions);
				long num = this.EventsInQueueCurrent.RawValue;
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

		// Token: 0x0600001E RID: 30 RVA: 0x0000344C File Offset: 0x0000164C
		internal PerformanceCountersPerAssistantInstance(string instanceName) : base(instanceName, "MSExchange Assistants - Per Assistant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.EventsInQueueCurrent = new ExPerformanceCounter(base.CategoryName, "Events in Queue", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsInQueueCurrent);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Events Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.EventsProcessed = new ExPerformanceCounter(base.CategoryName, "Events Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.EventsProcessed);
				this.AverageEventQueueTime = new ExPerformanceCounter(base.CategoryName, "Average Event Queue Time In Seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventQueueTime);
				this.AverageEventQueueTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Event Queue Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventQueueTimeBase);
				this.AverageEventProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Event Processing Time In Seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventProcessingTime);
				this.AverageEventProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Event Processing Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageEventProcessingTimeBase);
				this.InterestingEvents = new ExPerformanceCounter(base.CategoryName, "Percentage of Interesting Events", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InterestingEvents);
				this.InterestingEventsBase = new ExPerformanceCounter(base.CategoryName, "Base counter for Percentage of Interesting Events", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InterestingEventsBase);
				this.EventsDiscardedByMailboxFilter = new ExPerformanceCounter(base.CategoryName, "Percentage of Events Discarded by Mailbox Filter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsDiscardedByMailboxFilter);
				this.EventsDiscardedByMailboxFilterBase = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Events Discarded by Mailbox Filter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsDiscardedByMailboxFilterBase);
				this.QueuedEventsDiscardedByMailboxFilter = new ExPerformanceCounter(base.CategoryName, "Percentage of Queued Events Discarded by Mailbox Filter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueuedEventsDiscardedByMailboxFilter);
				this.QueuedEventsDiscardedByMailboxFilterBase = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Queued Events Discarded by Mailbox Filter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueuedEventsDiscardedByMailboxFilterBase);
				this.ElapsedTimeSinceLastEventQueued = new ExPerformanceCounter(base.CategoryName, "Elapsed Time Since Last Event Queued", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ElapsedTimeSinceLastEventQueued);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Percentage of Failed Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.FailedDispatchers = new ExPerformanceCounter(base.CategoryName, "Failed Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.FailedDispatchers);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Base Counter for Percentage of Failed Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.EventDispatchers = new ExPerformanceCounter(base.CategoryName, "Event Dispatchers", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.EventDispatchers);
				this.HandledExceptions = new ExPerformanceCounter(base.CategoryName, "Handled Exceptions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HandledExceptions);
				long num = this.EventsInQueueCurrent.RawValue;
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

		// Token: 0x0600001F RID: 31 RVA: 0x00003834 File Offset: 0x00001A34
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

		// Token: 0x04000071 RID: 113
		public readonly ExPerformanceCounter EventsInQueueCurrent;

		// Token: 0x04000072 RID: 114
		public readonly ExPerformanceCounter EventsProcessed;

		// Token: 0x04000073 RID: 115
		public readonly ExPerformanceCounter AverageEventQueueTime;

		// Token: 0x04000074 RID: 116
		public readonly ExPerformanceCounter AverageEventQueueTimeBase;

		// Token: 0x04000075 RID: 117
		public readonly ExPerformanceCounter AverageEventProcessingTime;

		// Token: 0x04000076 RID: 118
		public readonly ExPerformanceCounter AverageEventProcessingTimeBase;

		// Token: 0x04000077 RID: 119
		public readonly ExPerformanceCounter InterestingEvents;

		// Token: 0x04000078 RID: 120
		public readonly ExPerformanceCounter InterestingEventsBase;

		// Token: 0x04000079 RID: 121
		public readonly ExPerformanceCounter EventsDiscardedByMailboxFilter;

		// Token: 0x0400007A RID: 122
		public readonly ExPerformanceCounter EventsDiscardedByMailboxFilterBase;

		// Token: 0x0400007B RID: 123
		public readonly ExPerformanceCounter QueuedEventsDiscardedByMailboxFilter;

		// Token: 0x0400007C RID: 124
		public readonly ExPerformanceCounter QueuedEventsDiscardedByMailboxFilterBase;

		// Token: 0x0400007D RID: 125
		public readonly ExPerformanceCounter ElapsedTimeSinceLastEventQueued;

		// Token: 0x0400007E RID: 126
		public readonly ExPerformanceCounter EventDispatchers;

		// Token: 0x0400007F RID: 127
		public readonly ExPerformanceCounter FailedDispatchers;

		// Token: 0x04000080 RID: 128
		public readonly ExPerformanceCounter HandledExceptions;
	}
}
