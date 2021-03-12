using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.UMWorkerProcess
{
	// Token: 0x02000002 RID: 2
	internal class WPInstrumentationStrategy : InstrumentationBaseStrategy
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override void InternalInitialize()
		{
			this.SetThreadPoolMinThreads();
			this.InitializePerfCounterCollection();
			this.InitializeThreadPoolQueueCounter();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
		protected override void InternalCollectData(StringBuilder sb)
		{
			this.CollectThreadPoolInfo(sb);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		private void SetThreadPoolMinThreads()
		{
			int num;
			int num2;
			ThreadPool.GetMinThreads(out num, out num2);
			if (num >= 10)
			{
				InstrumentationBaseStrategy.TraceDebug("ProcessPerfMonitor: Not Changing current ThreadPool Min Threads {0}", new object[]
				{
					10
				});
				return;
			}
			if (ThreadPool.SetMinThreads(10, 10))
			{
				InstrumentationBaseStrategy.TraceDebug("ProcessPerfMonitor: Change the ThreadPool Min Threads to {0}", new object[]
				{
					10
				});
				return;
			}
			InstrumentationBaseStrategy.TraceDebug("ProcessPerfMonitor: Failed to change the ThreadPool Min Threads to {0}", new object[]
			{
				10
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
		private void InitializePerfCounterCollection()
		{
			try
			{
				PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Process");
				foreach (string text in performanceCounterCategory.GetInstanceNames())
				{
					if (text.StartsWith("UMWorkerProcess", StringComparison.OrdinalIgnoreCase))
					{
						base.PerfCounters.Add(new PerformanceCounter(performanceCounterCategory.CategoryName, "Working Set", text));
						base.PerfCounters.Add(new PerformanceCounter(performanceCounterCategory.CategoryName, "Thread Count", text));
					}
				}
			}
			catch (Exception ex)
			{
				InstrumentationBaseStrategy.TraceDebug("UMWPInstrumentationStrategy: Failed to setup performance counters. error={0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000221C File Offset: 0x0000041C
		private void InitializeThreadPoolQueueCounter()
		{
			try
			{
				Type type = Type.GetType("System.Threading.ThreadPoolGlobals, mscorlib");
				foreach (FieldInfo fieldInfo in type.GetFields())
				{
					if (string.Equals(fieldInfo.Name, "tpQueue", StringComparison.OrdinalIgnoreCase))
					{
						this.instanceThreadPoolRequestQueue = fieldInfo.GetValue(null);
						if (this.instanceThreadPoolRequestQueue != null)
						{
							foreach (FieldInfo fieldInfo2 in this.instanceThreadPoolRequestQueue.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
							{
								if (string.Equals(fieldInfo2.Name, "tpCount", StringComparison.OrdinalIgnoreCase))
								{
									InstrumentationBaseStrategy.TraceDebug("ProcessPerfMonitor: Reference to ThreadPool Queue is set", new object[0]);
									this.threadPoolCount = fieldInfo2;
									break;
								}
							}
						}
						else
						{
							InstrumentationBaseStrategy.TraceDebug("UMWPInstrumentationStrategy: Could not find instance of ThreadPoolRequestQueue", new object[0]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				InstrumentationBaseStrategy.TraceDebug("UMWPInstrumentationStrategy: Failed to find the ThreadPool Queue reference. error={0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002320 File Offset: 0x00000520
		private void CollectThreadPoolInfo(StringBuilder sb)
		{
			try
			{
				int num;
				int num2;
				ThreadPool.GetAvailableThreads(out num, out num2);
				sb.AppendFormat("TP Avail. Threads(Worker={0} IO={1}),", num, num2);
				if (this.instanceThreadPoolRequestQueue != null && this.threadPoolCount != null)
				{
					sb.AppendFormat("TP Queue Count={0},", this.threadPoolCount.GetValue(this.instanceThreadPoolRequestQueue));
				}
			}
			catch (Exception ex)
			{
				InstrumentationBaseStrategy.TraceDebug("UMWPInstrumentationStrategy: Encountered error while getting threadpool information. error={0}", new object[]
				{
					ex.Message ?? string.Empty
				});
			}
		}

		// Token: 0x04000001 RID: 1
		private object instanceThreadPoolRequestQueue;

		// Token: 0x04000002 RID: 2
		private FieldInfo threadPoolCount;
	}
}
