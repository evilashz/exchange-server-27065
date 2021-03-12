using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000004 RID: 4
	public class Controller
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		public Controller(WorkBroker[] brokers, TracingContext traceContext)
		{
			this.brokers = brokers;
			foreach (WorkBroker workBroker in this.brokers)
			{
				workBroker.RestartRequestEvent = delegate(RestartRequest reason)
				{
					this.RestartRequest = reason;
				};
			}
			this.traceContext = traceContext;
			WTFDiagnostics.TraceInformation<int, int>(WTFLog.Core, this.traceContext, "[Controller.Controller]: Started with MaxRunningTasks={0} and MaxWorkitemBatchSize={1}.", this.maxRunningTasks, this.maxWorkitemBatchSize, null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 138);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002228 File Offset: 0x00000428
		public Controller(WorkBroker[] brokers, TracingContext traceContext, bool perfCountersExist) : this(brokers, traceContext)
		{
			if (perfCountersExist)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					this.wtfPerfCountersInstance = WTFPerfCounters.GetInstance(currentProcess.ProcessName);
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002274 File Offset: 0x00000474
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000227C File Offset: 0x0000047C
		public RestartRequest RestartRequest
		{
			get
			{
				return this.restartRequest;
			}
			internal set
			{
				this.restartRequest = value;
				this.controllerExitingEvent.Set();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002291 File Offset: 0x00000491
		internal ManualResetEvent ControllerExitingEvent
		{
			get
			{
				return this.controllerExitingEvent;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A8 File Offset: 0x000004A8
		public void QueueWork(CancellationToken cancellationToken)
		{
			cancellationToken.Register(delegate()
			{
				this.controllerExitingEvent.Set();
			});
			Dictionary<long, WorkItem> dictionary = new Dictionary<long, WorkItem>(this.maxRunningTasks + this.maxWorkitemBatchSize);
			Dictionary<long, WorkItem> dictionary2 = new Dictionary<long, WorkItem>();
			BlockingCollection<WorkItem>[] array = new BlockingCollection<WorkItem>[this.brokers.Length];
			List<long> list = new List<long>(this.maxRunningTasks + this.maxWorkitemBatchSize);
			do
			{
				Thread.Sleep(this.throttleAmount);
				if (dictionary2.Count > 0)
				{
					foreach (KeyValuePair<long, WorkItem> keyValuePair in dictionary2)
					{
						WorkItem value = keyValuePair.Value;
						if (value.IsCompleted)
						{
							if (this.wtfPerfCountersInstance != null)
							{
								this.wtfPerfCountersInstance.WorkItemExecutionRate.Increment();
								this.wtfPerfCountersInstance.TimedOutWorkItemCount.Decrement();
							}
							list.Add(keyValuePair.Key);
						}
					}
					foreach (long key in list)
					{
						dictionary2.Remove(key);
					}
					list.Clear();
				}
				DateTime utcNow = DateTime.UtcNow;
				foreach (KeyValuePair<long, WorkItem> keyValuePair2 in dictionary)
				{
					WorkItem value2 = keyValuePair2.Value;
					if (value2.IsCompleted || value2.DueTime < utcNow)
					{
						list.Add(keyValuePair2.Key);
						if (value2.IsCompleted)
						{
							if (this.wtfPerfCountersInstance != null)
							{
								this.wtfPerfCountersInstance.WorkItemExecutionRate.Increment();
							}
						}
						else
						{
							if (this.wtfPerfCountersInstance != null)
							{
								this.wtfPerfCountersInstance.TimedOutWorkItemCount.Increment();
							}
							value2.StartCancel(this.waitAmountBeforeRestartRequest, new Action<WorkItemResult, TracingContext>(value2.Broker.PublishResult), false);
							dictionary2.Add(keyValuePair2.Key, keyValuePair2.Value);
						}
					}
				}
				foreach (long key2 in list)
				{
					dictionary.Remove(key2);
				}
				list.Clear();
				if (this.wtfPerfCountersInstance != null)
				{
					this.wtfPerfCountersInstance.WorkItemCount.RawValue = (long)(dictionary.Count + dictionary2.Count);
				}
				if (cancellationToken.IsCancellationRequested || this.RestartRequest != null)
				{
					WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: We were told to quit or we ran aground, skip scheduling more workitems", null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 291);
					for (int i = 0; i < this.brokers.Length; i++)
					{
						if (array[i] != null)
						{
							WorkItem workItem;
							while (array[i].TryTake(out workItem, this.waitForWorkAmount))
							{
								workItem.Broker.Abandon(workItem);
								WTFDiagnostics.TraceInformation(WTFLog.Core, workItem.TraceContext, "[Controller.QueueWork]: Abandoning workItem because the controller is quiting.", null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 305);
							}
						}
					}
					if (Settings.IsCancelWorkItemsOnQuitRequestFeatureEnabled)
					{
						WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: Settings.IsCancelWorkItemsOnQuitRequestFeatureEnabled is enabled", null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 312);
						foreach (KeyValuePair<long, WorkItem> keyValuePair3 in dictionary)
						{
							WorkItem value3 = keyValuePair3.Value;
							list.Add(keyValuePair3.Key);
							if (value3.IsCompleted)
							{
								if (this.wtfPerfCountersInstance != null)
								{
									this.wtfPerfCountersInstance.WorkItemExecutionRate.Increment();
								}
							}
							else
							{
								if (this.wtfPerfCountersInstance != null)
								{
									this.wtfPerfCountersInstance.TimedOutWorkItemCount.Increment();
								}
								value3.StartCancel(this.waitAmountBeforeRestartRequest, new Action<WorkItemResult, TracingContext>(value3.Broker.PublishResult), true);
								WTFDiagnostics.TraceInformation<DateTime>(WTFLog.Core, value3.TraceContext, "[Controller.QueueWork]: Cancelling workItem because the controller is quiting. The DueTime was {0}.", value3.DueTime, null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 339);
							}
						}
						foreach (long key3 in list)
						{
							dictionary.Remove(key3);
						}
						list.Clear();
					}
				}
				else
				{
					int num = this.maxRunningTasks - dictionary.Count;
					if (num <= 0)
					{
						if (this.wtfPerfCountersInstance != null)
						{
							this.wtfPerfCountersInstance.ThrottleRate.Increment();
						}
					}
					else
					{
						for (int j = 0; j < this.brokers.Length; j++)
						{
							if (array[j] == null || array[j].IsCompleted)
							{
								array[j] = this.brokers[j].AsyncGetWork(this.maxWorkitemBatchSize, cancellationToken);
								if (this.wtfPerfCountersInstance != null)
								{
									this.wtfPerfCountersInstance.QueryRate.Increment();
								}
							}
						}
						WorkItem workItem2;
						while (BlockingCollection<WorkItem>.TryTakeFromAny(array, out workItem2, this.waitForWorkAmount) != -1)
						{
							if (this.wtfPerfCountersInstance != null)
							{
								this.wtfPerfCountersInstance.WorkItemRetrievalRate.Increment();
							}
							long key4 = ((long)workItem2.Broker.GetHashCode() << 32) + (long)workItem2.Id;
							if (dictionary.ContainsKey(key4) || dictionary2.ContainsKey(key4))
							{
								WTFDiagnostics.TraceInformation(WTFLog.Core, workItem2.TraceContext, "[Controller.QueueWork]: Rejected. An instance of this workitem is already executing", null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 398);
								workItem2.Broker.Reject(workItem2);
							}
							else
							{
								if (this.wtfPerfCountersInstance != null)
								{
									int num2 = (int)(DateTime.UtcNow - workItem2.Definition.IntendedStartTime).TotalMilliseconds;
									this.wtfPerfCountersInstance.SchedulingLatency.RawValue = (long)((num2 > 0) ? num2 : 0);
								}
								workItem2.StartExecuting(new Action<WorkItemResult, TracingContext>(workItem2.Broker.PublishResult));
								dictionary.Add(key4, workItem2);
								num--;
								if (num == 0)
								{
									break;
								}
							}
						}
					}
				}
			}
			while (dictionary.Count > 0 || (!cancellationToken.IsCancellationRequested && this.RestartRequest == null));
			WTFDiagnostics.TraceInformation<DateTime>(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: Stopped at {0}.", DateTime.UtcNow, null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 435);
			if (this.RestartRequest != null)
			{
				WTFDiagnostics.TraceInformation<string>(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: Stopped because {0}", this.RestartRequest.ToString(), null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 439);
				return;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: Stopped because Cancellation was Requested", null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 443);
				return;
			}
			WTFDiagnostics.TraceInformation<int>(WTFLog.Core, this.traceContext, "[Controller.QueueWork]: Stopped because scheduledWorkItems.Count = {0}", dictionary.Count, null, "QueueWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Controller.cs", 447);
		}

		// Token: 0x04000006 RID: 6
		private readonly int throttleAmount = Settings.ThrottleAmount;

		// Token: 0x04000007 RID: 7
		private readonly int waitForWorkAmount = Settings.WaitForWorkAmount;

		// Token: 0x04000008 RID: 8
		private readonly int waitAmountBeforeRestartRequest = Settings.WaitAmountBeforeRestartRequest;

		// Token: 0x04000009 RID: 9
		private readonly int maxWorkitemBatchSize = Settings.MaxWorkitemBatchSize;

		// Token: 0x0400000A RID: 10
		private readonly int maxRunningTasks = Settings.MaxRunningTasks;

		// Token: 0x0400000B RID: 11
		private readonly WTFPerfCountersInstance wtfPerfCountersInstance;

		// Token: 0x0400000C RID: 12
		private readonly ManualResetEvent controllerExitingEvent = new ManualResetEvent(false);

		// Token: 0x0400000D RID: 13
		private WorkBroker[] brokers;

		// Token: 0x0400000E RID: 14
		private TracingContext traceContext;

		// Token: 0x0400000F RID: 15
		private RestartRequest restartRequest;
	}
}
