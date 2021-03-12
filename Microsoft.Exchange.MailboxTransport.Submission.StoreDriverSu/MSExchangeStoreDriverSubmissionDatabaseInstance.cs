using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000049 RID: 73
	internal sealed class MSExchangeStoreDriverSubmissionDatabaseInstance : PerformanceCounterInstance
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		internal MSExchangeStoreDriverSubmissionDatabaseInstance(string instanceName, MSExchangeStoreDriverSubmissionDatabaseInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Submission Store Driver Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubmissionAttempts = new ExPerformanceCounter(base.CategoryName, "Submission attempts per minute over the last 5 minutes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubmissionAttempts, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionAttempts);
				this.SubmissionFailures = new ExPerformanceCounter(base.CategoryName, "Submission failures per minute over the last 5 minutes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubmissionFailures, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionFailures);
				this.SkippedSubmissions = new ExPerformanceCounter(base.CategoryName, "Submissions skipped per minute over the last 5 minutes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SkippedSubmissions, new ExPerformanceCounter[0]);
				list.Add(this.SkippedSubmissions);
				long num = this.SubmissionAttempts.RawValue;
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

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E12C File Offset: 0x0000C32C
		internal MSExchangeStoreDriverSubmissionDatabaseInstance(string instanceName) : base(instanceName, "MSExchange Submission Store Driver Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubmissionAttempts = new ExPerformanceCounter(base.CategoryName, "Submission attempts per minute over the last 5 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionAttempts);
				this.SubmissionFailures = new ExPerformanceCounter(base.CategoryName, "Submission failures per minute over the last 5 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionFailures);
				this.SkippedSubmissions = new ExPerformanceCounter(base.CategoryName, "Submissions skipped per minute over the last 5 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SkippedSubmissions);
				long num = this.SubmissionAttempts.RawValue;
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

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E240 File Offset: 0x0000C440
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

		// Token: 0x0400019A RID: 410
		public readonly ExPerformanceCounter SubmissionAttempts;

		// Token: 0x0400019B RID: 411
		public readonly ExPerformanceCounter SubmissionFailures;

		// Token: 0x0400019C RID: 412
		public readonly ExPerformanceCounter SkippedSubmissions;
	}
}
