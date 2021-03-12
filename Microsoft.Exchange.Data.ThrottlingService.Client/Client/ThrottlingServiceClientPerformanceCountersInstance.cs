using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x0200000D RID: 13
	internal sealed class ThrottlingServiceClientPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000326C File Offset: 0x0000146C
		internal ThrottlingServiceClientPerformanceCountersInstance(string instanceName, ThrottlingServiceClientPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Throttling Service Client")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SuccessfulSubmissionRequests = new ExPerformanceCounter(base.CategoryName, "Percentage of Successful Submission Requests.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulSubmissionRequests);
				this.DeniedSubmissionRequest = new ExPerformanceCounter(base.CategoryName, "Percentage of Denied Submission Request.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DeniedSubmissionRequest);
				this.BypassedSubmissionRequests = new ExPerformanceCounter(base.CategoryName, "Percentage of Bypassed Submission Requests.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BypassedSubmissionRequests);
				this.AverageSubmissionRequestTime = new ExPerformanceCounter(base.CategoryName, "Average request processing time.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubmissionRequestTime);
				long num = this.SuccessfulSubmissionRequests.RawValue;
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

		// Token: 0x0600003B RID: 59 RVA: 0x000033AC File Offset: 0x000015AC
		internal ThrottlingServiceClientPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Throttling Service Client")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SuccessfulSubmissionRequests = new ExPerformanceCounter(base.CategoryName, "Percentage of Successful Submission Requests.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulSubmissionRequests);
				this.DeniedSubmissionRequest = new ExPerformanceCounter(base.CategoryName, "Percentage of Denied Submission Request.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DeniedSubmissionRequest);
				this.BypassedSubmissionRequests = new ExPerformanceCounter(base.CategoryName, "Percentage of Bypassed Submission Requests.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BypassedSubmissionRequests);
				this.AverageSubmissionRequestTime = new ExPerformanceCounter(base.CategoryName, "Average request processing time.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubmissionRequestTime);
				long num = this.SuccessfulSubmissionRequests.RawValue;
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

		// Token: 0x0600003C RID: 60 RVA: 0x000034EC File Offset: 0x000016EC
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

		// Token: 0x04000041 RID: 65
		public readonly ExPerformanceCounter SuccessfulSubmissionRequests;

		// Token: 0x04000042 RID: 66
		public readonly ExPerformanceCounter DeniedSubmissionRequest;

		// Token: 0x04000043 RID: 67
		public readonly ExPerformanceCounter BypassedSubmissionRequests;

		// Token: 0x04000044 RID: 68
		public readonly ExPerformanceCounter AverageSubmissionRequestTime;
	}
}
