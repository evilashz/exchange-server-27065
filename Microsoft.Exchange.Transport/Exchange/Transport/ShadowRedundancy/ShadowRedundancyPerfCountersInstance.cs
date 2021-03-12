using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000560 RID: 1376
	internal sealed class ShadowRedundancyPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F31 RID: 16177 RVA: 0x00111DC4 File Offset: 0x0010FFC4
		internal ShadowRedundancyPerfCountersInstance(string instanceName, ShadowRedundancyPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Shadow Redundancy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RedundantMessageDiscardEvents = new ExPerformanceCounter(base.CategoryName, "Redundant Message Discard Events", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RedundantMessageDiscardEvents);
				this.RedundantMessageDiscardEventsExpired = new ExPerformanceCounter(base.CategoryName, "Redundant Message Discard Events Expired", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RedundantMessageDiscardEventsExpired);
				this.CurrentMessagesAckBeforeRelayCompleted = new ExPerformanceCounter(base.CategoryName, "Current Messages Acknowledged before Relay Completed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentMessagesAckBeforeRelayCompleted);
				this.ShadowHostSelectionAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Selection Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSelectionAverageTime);
				this.ShadowHostSelectionAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Selection Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSelectionAverageTimeBase);
				this.ShadowHostNegotiationAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Negotiation Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostNegotiationAverageTime);
				this.ShadowHostNegotiationAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Negotiation Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostNegotiationAverageTimeBase);
				this.ShadowHostSuccessfulNegotiationAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Successful Negotiation Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSuccessfulNegotiationAverageTime);
				this.ShadowHostSuccessfulNegotiationAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Successful Negotiation Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSuccessfulNegotiationAverageTimeBase);
				this.LocalSiteShadowPercentage = new ExPerformanceCounter(base.CategoryName, "Percentage of Messages Shadowed to Local Site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LocalSiteShadowPercentage);
				this.MessagesFailedToBeMadeRedundant = new ExPerformanceCounter(base.CategoryName, "Messages Failed to be made Redundant", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFailedToBeMadeRedundant);
				this.MessagesFailedToBeMadeRedundantPercentage = new ExPerformanceCounter(base.CategoryName, "Percentage of Messages Failed to be made Redundant", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFailedToBeMadeRedundantPercentage);
				this.TotalSmtpTimeouts = new ExPerformanceCounter(base.CategoryName, "Total SMTP Timeouts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSmtpTimeouts);
				this.ClientAckFailureCount = new ExPerformanceCounter(base.CategoryName, "Client ACK Failure Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ClientAckFailureCount);
				long num = this.RedundantMessageDiscardEvents.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x001120CC File Offset: 0x001102CC
		internal ShadowRedundancyPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Shadow Redundancy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RedundantMessageDiscardEvents = new ExPerformanceCounter(base.CategoryName, "Redundant Message Discard Events", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RedundantMessageDiscardEvents);
				this.RedundantMessageDiscardEventsExpired = new ExPerformanceCounter(base.CategoryName, "Redundant Message Discard Events Expired", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RedundantMessageDiscardEventsExpired);
				this.CurrentMessagesAckBeforeRelayCompleted = new ExPerformanceCounter(base.CategoryName, "Current Messages Acknowledged before Relay Completed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentMessagesAckBeforeRelayCompleted);
				this.ShadowHostSelectionAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Selection Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSelectionAverageTime);
				this.ShadowHostSelectionAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Selection Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSelectionAverageTimeBase);
				this.ShadowHostNegotiationAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Negotiation Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostNegotiationAverageTime);
				this.ShadowHostNegotiationAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Negotiation Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostNegotiationAverageTimeBase);
				this.ShadowHostSuccessfulNegotiationAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Host Successful Negotiation Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSuccessfulNegotiationAverageTime);
				this.ShadowHostSuccessfulNegotiationAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Host Successful Negotiation Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHostSuccessfulNegotiationAverageTimeBase);
				this.LocalSiteShadowPercentage = new ExPerformanceCounter(base.CategoryName, "Percentage of Messages Shadowed to Local Site", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LocalSiteShadowPercentage);
				this.MessagesFailedToBeMadeRedundant = new ExPerformanceCounter(base.CategoryName, "Messages Failed to be made Redundant", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFailedToBeMadeRedundant);
				this.MessagesFailedToBeMadeRedundantPercentage = new ExPerformanceCounter(base.CategoryName, "Percentage of Messages Failed to be made Redundant", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFailedToBeMadeRedundantPercentage);
				this.TotalSmtpTimeouts = new ExPerformanceCounter(base.CategoryName, "Total SMTP Timeouts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSmtpTimeouts);
				this.ClientAckFailureCount = new ExPerformanceCounter(base.CategoryName, "Client ACK Failure Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ClientAckFailureCount);
				long num = this.RedundantMessageDiscardEvents.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x001123D4 File Offset: 0x001105D4
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

		// Token: 0x0400237B RID: 9083
		public readonly ExPerformanceCounter RedundantMessageDiscardEvents;

		// Token: 0x0400237C RID: 9084
		public readonly ExPerformanceCounter RedundantMessageDiscardEventsExpired;

		// Token: 0x0400237D RID: 9085
		public readonly ExPerformanceCounter CurrentMessagesAckBeforeRelayCompleted;

		// Token: 0x0400237E RID: 9086
		public readonly ExPerformanceCounter ShadowHostSelectionAverageTime;

		// Token: 0x0400237F RID: 9087
		public readonly ExPerformanceCounter ShadowHostSelectionAverageTimeBase;

		// Token: 0x04002380 RID: 9088
		public readonly ExPerformanceCounter ShadowHostNegotiationAverageTime;

		// Token: 0x04002381 RID: 9089
		public readonly ExPerformanceCounter ShadowHostNegotiationAverageTimeBase;

		// Token: 0x04002382 RID: 9090
		public readonly ExPerformanceCounter ShadowHostSuccessfulNegotiationAverageTime;

		// Token: 0x04002383 RID: 9091
		public readonly ExPerformanceCounter ShadowHostSuccessfulNegotiationAverageTimeBase;

		// Token: 0x04002384 RID: 9092
		public readonly ExPerformanceCounter LocalSiteShadowPercentage;

		// Token: 0x04002385 RID: 9093
		public readonly ExPerformanceCounter MessagesFailedToBeMadeRedundant;

		// Token: 0x04002386 RID: 9094
		public readonly ExPerformanceCounter MessagesFailedToBeMadeRedundantPercentage;

		// Token: 0x04002387 RID: 9095
		public readonly ExPerformanceCounter TotalSmtpTimeouts;

		// Token: 0x04002388 RID: 9096
		public readonly ExPerformanceCounter ClientAckFailureCount;
	}
}
