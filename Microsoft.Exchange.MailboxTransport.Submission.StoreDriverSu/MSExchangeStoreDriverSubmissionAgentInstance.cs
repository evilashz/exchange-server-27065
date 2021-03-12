using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000047 RID: 71
	internal sealed class MSExchangeStoreDriverSubmissionAgentInstance : PerformanceCounterInstance
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000DD04 File Offset: 0x0000BF04
		internal MSExchangeStoreDriverSubmissionAgentInstance(string instanceName, MSExchangeStoreDriverSubmissionAgentInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Submission Store Driver Agents")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubmissionAgentFailures = new ExPerformanceCounter(base.CategoryName, "StoreDriverSubmission Agent Failure", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubmissionAgentFailures, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionAgentFailures);
				long num = this.SubmissionAgentFailures.RawValue;
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		internal MSExchangeStoreDriverSubmissionAgentInstance(string instanceName) : base(instanceName, "MSExchange Submission Store Driver Agents")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubmissionAgentFailures = new ExPerformanceCounter(base.CategoryName, "StoreDriverSubmission Agent Failure", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionAgentFailures);
				long num = this.SubmissionAgentFailures.RawValue;
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

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DE8C File Offset: 0x0000C08C
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

		// Token: 0x04000197 RID: 407
		public readonly ExPerformanceCounter SubmissionAgentFailures;
	}
}
