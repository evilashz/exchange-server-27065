using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000048 RID: 72
	internal sealed class MSExchangeUserWorkloadManagerInstance : PerformanceCounterInstance
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000CE1C File Offset: 0x0000B01C
		internal MSExchangeUserWorkloadManagerInstance(string instanceName, MSExchangeUserWorkloadManagerInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange User WorkloadManager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageTaskWaitTime = new ExPerformanceCounter(base.CategoryName, "Average Task Wait Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTaskWaitTime);
				this.MaxTaskQueueLength = new ExPerformanceCounter(base.CategoryName, "Max Task Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxTaskQueueLength);
				this.MaxWorkerThreadCount = new ExPerformanceCounter(base.CategoryName, "Max Worker Thread Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxWorkerThreadCount);
				this.TaskQueueLength = new ExPerformanceCounter(base.CategoryName, "Task Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TaskQueueLength);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "New Task Rejections/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalNewTaskRejections = new ExPerformanceCounter(base.CategoryName, "New Task Rejections", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalNewTaskRejections);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "New Tasks Submitted/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalNewTasks = new ExPerformanceCounter(base.CategoryName, "Total New Tasks", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalNewTasks);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Task Failures/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalTaskExecutionFailures = new ExPerformanceCounter(base.CategoryName, "Total Task Failures", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalTaskExecutionFailures);
				this.CurrentDelayedTasks = new ExPerformanceCounter(base.CategoryName, "Current Delayed Tasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentDelayedTasks);
				this.MaxDelayPerMinute = new ExPerformanceCounter(base.CategoryName, "Max Delay Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDelayPerMinute);
				this.UsersDelayedXMillisecondsPerMinute = new ExPerformanceCounter(base.CategoryName, "Users Delayed X Milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersDelayedXMillisecondsPerMinute);
				this.DelayTimeThreshold = new ExPerformanceCounter(base.CategoryName, "Delay Time Threshold", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayTimeThreshold);
				this.TaskTimeoutsPerMinute = new ExPerformanceCounter(base.CategoryName, "Task Timeouts Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TaskTimeoutsPerMinute);
				this.MaxDelayedTasks = new ExPerformanceCounter(base.CategoryName, "Max Delayed Tasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDelayedTasks);
				long num = this.AverageTaskWaitTime.RawValue;
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

		// Token: 0x060002D8 RID: 728 RVA: 0x0000D184 File Offset: 0x0000B384
		internal MSExchangeUserWorkloadManagerInstance(string instanceName) : base(instanceName, "MSExchange User WorkloadManager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageTaskWaitTime = new ExPerformanceCounter(base.CategoryName, "Average Task Wait Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTaskWaitTime);
				this.MaxTaskQueueLength = new ExPerformanceCounter(base.CategoryName, "Max Task Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxTaskQueueLength);
				this.MaxWorkerThreadCount = new ExPerformanceCounter(base.CategoryName, "Max Worker Thread Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxWorkerThreadCount);
				this.TaskQueueLength = new ExPerformanceCounter(base.CategoryName, "Task Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TaskQueueLength);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "New Task Rejections/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalNewTaskRejections = new ExPerformanceCounter(base.CategoryName, "New Task Rejections", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalNewTaskRejections);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "New Tasks Submitted/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalNewTasks = new ExPerformanceCounter(base.CategoryName, "Total New Tasks", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalNewTasks);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Task Failures/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalTaskExecutionFailures = new ExPerformanceCounter(base.CategoryName, "Total Task Failures", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalTaskExecutionFailures);
				this.CurrentDelayedTasks = new ExPerformanceCounter(base.CategoryName, "Current Delayed Tasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentDelayedTasks);
				this.MaxDelayPerMinute = new ExPerformanceCounter(base.CategoryName, "Max Delay Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDelayPerMinute);
				this.UsersDelayedXMillisecondsPerMinute = new ExPerformanceCounter(base.CategoryName, "Users Delayed X Milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersDelayedXMillisecondsPerMinute);
				this.DelayTimeThreshold = new ExPerformanceCounter(base.CategoryName, "Delay Time Threshold", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayTimeThreshold);
				this.TaskTimeoutsPerMinute = new ExPerformanceCounter(base.CategoryName, "Task Timeouts Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TaskTimeoutsPerMinute);
				this.MaxDelayedTasks = new ExPerformanceCounter(base.CategoryName, "Max Delayed Tasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDelayedTasks);
				long num = this.AverageTaskWaitTime.RawValue;
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

		// Token: 0x060002D9 RID: 729 RVA: 0x0000D4EC File Offset: 0x0000B6EC
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

		// Token: 0x0400016D RID: 365
		public readonly ExPerformanceCounter AverageTaskWaitTime;

		// Token: 0x0400016E RID: 366
		public readonly ExPerformanceCounter MaxTaskQueueLength;

		// Token: 0x0400016F RID: 367
		public readonly ExPerformanceCounter MaxWorkerThreadCount;

		// Token: 0x04000170 RID: 368
		public readonly ExPerformanceCounter TaskQueueLength;

		// Token: 0x04000171 RID: 369
		public readonly ExPerformanceCounter TotalNewTaskRejections;

		// Token: 0x04000172 RID: 370
		public readonly ExPerformanceCounter TotalNewTasks;

		// Token: 0x04000173 RID: 371
		public readonly ExPerformanceCounter TotalTaskExecutionFailures;

		// Token: 0x04000174 RID: 372
		public readonly ExPerformanceCounter CurrentDelayedTasks;

		// Token: 0x04000175 RID: 373
		public readonly ExPerformanceCounter MaxDelayPerMinute;

		// Token: 0x04000176 RID: 374
		public readonly ExPerformanceCounter UsersDelayedXMillisecondsPerMinute;

		// Token: 0x04000177 RID: 375
		public readonly ExPerformanceCounter DelayTimeThreshold;

		// Token: 0x04000178 RID: 376
		public readonly ExPerformanceCounter TaskTimeoutsPerMinute;

		// Token: 0x04000179 RID: 377
		public readonly ExPerformanceCounter MaxDelayedTasks;
	}
}
