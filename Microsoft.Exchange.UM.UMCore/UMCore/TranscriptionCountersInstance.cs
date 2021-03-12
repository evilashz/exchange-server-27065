using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000325 RID: 805
	internal sealed class TranscriptionCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x0006BA34 File Offset: 0x00069C34
		internal TranscriptionCountersInstance(string instanceName, TranscriptionCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeUMVoiceMailSpeechRecognition")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.CurrentSessions = new ExPerformanceCounter(base.CategoryName, "Current Sessions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentSessions);
				this.VoiceMessagesProcessed = new ExPerformanceCounter(base.CategoryName, "Voice Messages Processed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesProcessed);
				this.VoiceMessagesProcessedWithLowConfidence = new ExPerformanceCounter(base.CategoryName, "Voice Messages Processed with Low Confidence", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesProcessedWithLowConfidence);
				this.VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources = new ExPerformanceCounter(base.CategoryName, "Voice Messages Not Processed Because of Low Availability of Resources", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources);
				this.VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources = new ExPerformanceCounter(base.CategoryName, "Voice Messages Partially Processed Because of Low Availability of Resources", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources);
				this.AverageConfidence = new ExPerformanceCounter(base.CategoryName, "Average Confidence %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageConfidence);
				long num = this.CurrentSessions.RawValue;
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

		// Token: 0x06001BCD RID: 7117 RVA: 0x0006BBDC File Offset: 0x00069DDC
		internal TranscriptionCountersInstance(string instanceName) : base(instanceName, "MSExchangeUMVoiceMailSpeechRecognition")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.CurrentSessions = new ExPerformanceCounter(base.CategoryName, "Current Sessions", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentSessions);
				this.VoiceMessagesProcessed = new ExPerformanceCounter(base.CategoryName, "Voice Messages Processed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesProcessed);
				this.VoiceMessagesProcessedWithLowConfidence = new ExPerformanceCounter(base.CategoryName, "Voice Messages Processed with Low Confidence", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesProcessedWithLowConfidence);
				this.VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources = new ExPerformanceCounter(base.CategoryName, "Voice Messages Not Processed Because of Low Availability of Resources", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources);
				this.VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources = new ExPerformanceCounter(base.CategoryName, "Voice Messages Partially Processed Because of Low Availability of Resources", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources);
				this.AverageConfidence = new ExPerformanceCounter(base.CategoryName, "Average Confidence %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageConfidence);
				long num = this.CurrentSessions.RawValue;
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

		// Token: 0x06001BCE RID: 7118 RVA: 0x0006BD84 File Offset: 0x00069F84
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

		// Token: 0x04000E6E RID: 3694
		public readonly ExPerformanceCounter CurrentSessions;

		// Token: 0x04000E6F RID: 3695
		public readonly ExPerformanceCounter VoiceMessagesProcessed;

		// Token: 0x04000E70 RID: 3696
		public readonly ExPerformanceCounter VoiceMessagesProcessedWithLowConfidence;

		// Token: 0x04000E71 RID: 3697
		public readonly ExPerformanceCounter VoiceMessagesNotProcessedBecauseOfLowAvailabilityOfResources;

		// Token: 0x04000E72 RID: 3698
		public readonly ExPerformanceCounter VoiceMessagesPartiallyProcessedBecauseOfLowAvailabilityOfResources;

		// Token: 0x04000E73 RID: 3699
		public readonly ExPerformanceCounter AverageConfidence;
	}
}
