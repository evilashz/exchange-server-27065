using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x020000B6 RID: 182
	internal sealed class RulesCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000187B4 File Offset: 0x000169B4
		internal RulesCountersInstance(string instanceName, RulesCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Transport Rules")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Messages Evaluated/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.MessagesEvaluated = new ExPerformanceCounter(base.CategoryName, "Messages Evaluated", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.MessagesEvaluated);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Messages Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.MessagesProcessed = new ExPerformanceCounter(base.CategoryName, "Messages Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.MessagesProcessed);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Messages Skipped/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.MessagesSkipped = new ExPerformanceCounter(base.CategoryName, "Messages Skipped", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.MessagesSkipped);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Deferred/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesDeferredDueToRuleErrors = new ExPerformanceCounter(base.CategoryName, "Messages Deferred Due to Errors", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.MessagesDeferredDueToRuleErrors);
				long num = this.MessagesEvaluated.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000189C8 File Offset: 0x00016BC8
		internal RulesCountersInstance(string instanceName) : base(instanceName, "MSExchange Transport Rules")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Messages Evaluated/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.MessagesEvaluated = new ExPerformanceCounter(base.CategoryName, "Messages Evaluated", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.MessagesEvaluated);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Messages Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.MessagesProcessed = new ExPerformanceCounter(base.CategoryName, "Messages Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.MessagesProcessed);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Messages Skipped/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.MessagesSkipped = new ExPerformanceCounter(base.CategoryName, "Messages Skipped", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.MessagesSkipped);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Deferred/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesDeferredDueToRuleErrors = new ExPerformanceCounter(base.CategoryName, "Messages Deferred Due to Errors", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.MessagesDeferredDueToRuleErrors);
				long num = this.MessagesEvaluated.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00018BDC File Offset: 0x00016DDC
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

		// Token: 0x04000356 RID: 854
		public readonly ExPerformanceCounter MessagesEvaluated;

		// Token: 0x04000357 RID: 855
		public readonly ExPerformanceCounter MessagesProcessed;

		// Token: 0x04000358 RID: 856
		public readonly ExPerformanceCounter MessagesSkipped;

		// Token: 0x04000359 RID: 857
		public readonly ExPerformanceCounter MessagesDeferredDueToRuleErrors;
	}
}
