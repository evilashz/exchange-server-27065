using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000015 RID: 21
	public class Worker
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00008E44 File Offset: 0x00007044
		public Worker(WorkBroker[] brokers) : this(brokers, null)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008E4E File Offset: 0x0000704E
		public Worker(WorkBroker[] brokers, Action onStopNotification) : this(brokers, onStopNotification, false)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008EA0 File Offset: 0x000070A0
		public Worker(WorkBroker[] brokers, Action onStopNotification, bool perfCountersExist)
		{
			this.traceContext = new TracingContext(null)
			{
				LId = this.GetHashCode(),
				Id = base.GetType().GetHashCode()
			};
			this.controller = new Controller(brokers, this.traceContext, perfCountersExist);
			this.controllerExitCallback = onStopNotification;
			this.cancellationSource = new CancellationTokenSource();
			TaskScheduler.UnobservedTaskException += delegate(object sender, UnobservedTaskExceptionEventArgs eventArgs)
			{
				eventArgs.SetObserved();
				eventArgs.Exception.Handle(delegate(Exception ex)
				{
					WTFDiagnostics.TraceDebug<Exception>(WTFLog.Core, this.traceContext, "[Worker.Worker]: unobserved exception encountered.\n{0}", ex, null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 110);
					return true;
				});
			};
			this.packageSets = new BlockingCollection<string>[brokers.Count<WorkBroker>()];
			for (int i = 0; i < brokers.Count<WorkBroker>(); i++)
			{
				this.packageSets[i] = brokers[i].AsyncGetWorkItemPackages(this.cancellationSource.Token);
			}
			WTFDiagnostics.TraceInformation<Type>(WTFLog.Core, this.traceContext, "[Worker.Worker]: {0} created.", base.GetType(), null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 121);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008F80 File Offset: 0x00007180
		public RestartRequest RestartRequest
		{
			get
			{
				return this.controller.RestartRequest;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008F90 File Offset: 0x00007190
		public List<string> GetWorkItemPackages()
		{
			List<string> list = new List<string>();
			int millisecondsTimeout = 50;
			int num = this.packageSets.Count<BlockingCollection<string>>();
			int i = 0;
			while (i < num)
			{
				Thread.Sleep(millisecondsTimeout);
				if (this.controller.RestartRequest != null)
				{
					WTFDiagnostics.TraceError(WTFLog.Core, this.traceContext, "[Worker.GetWorkItemPackages]: Restart was requested before work item packages could be discovered.", null, "GetWorkItemPackages", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 159);
					break;
				}
				string item;
				if (BlockingCollection<string>.TryTakeFromAny(this.packageSets, out item, 15) != -1)
				{
					list.Add(item);
					i++;
				}
			}
			return list;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000091E8 File Offset: 0x000073E8
		public void Start()
		{
			if (this.cancellationSource.IsCancellationRequested)
			{
				this.cancellationSource.Dispose();
				this.cancellationSource = new CancellationTokenSource();
			}
			Task task = Task.Factory.StartNew(delegate()
			{
				WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Start]: Starting Controller.", null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 188);
				this.controller.QueueWork(this.cancellationSource.Token);
			}, this.cancellationSource.Token, TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning, TaskScheduler.Current);
			this.workTask = task.ContinueWith(delegate(Task t)
			{
				if (this.cancellationSource.IsCancellationRequested)
				{
					WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Start]: we come here because of cancellation (request from host process).", null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 202);
					if (this.controller.RestartRequest != null)
					{
						WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Start]: clearing pending controller restart request.", null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 209);
						this.controller.RestartRequest = null;
					}
					if (t.Exception != null)
					{
						WTFDiagnostics.TraceInformation<AggregateException>(WTFLog.Core, this.traceContext, "[Worker.Start]: controller task is concelled but finished with exception: {0}", t.Exception, null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 215);
					}
				}
				else if (this.controller.RestartRequest == null)
				{
					WTFDiagnostics.TraceError<AggregateException>(WTFLog.Core, this.traceContext, "[Worker.Start]: controller task finished with exception: {0}", t.Exception, null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 223);
					this.controller.RestartRequest = RestartRequest.CreateUnknownRestartRequest(t.Exception);
				}
				else
				{
					WTFDiagnostics.TraceError<RestartRequest>(WTFLog.Core, this.traceContext, "[Worker.Start]: controller task finished with RestartRequest: {0}", this.controller.RestartRequest, null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 228);
				}
				if (this.controllerExitCallback != null)
				{
					WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Start]: Starting controllerExitCallback.", null, "Start", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 233);
					Task.Factory.StartNew(delegate()
					{
						this.controllerExitCallback();
					});
				}
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009258 File Offset: 0x00007458
		public void Stop()
		{
			WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Stop]: Cancelling controller execution loop.", null, "Stop", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 246);
			this.cancellationSource.Cancel();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000928C File Offset: 0x0000748C
		public void Wait()
		{
			WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Wait]: Waiting for Controller to stop.", null, "Wait", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 256);
			if (this.workTask != null)
			{
				this.workTask.Wait();
				return;
			}
			WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Wait]: Controller is not started yet. No action is required.", null, "Wait", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 264);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000092F8 File Offset: 0x000074F8
		public void WaitWithRestartSLA(TimeSpan waitSpan)
		{
			WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Wait]: Waiting for Controller to stop.", null, "WaitWithRestartSLA", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 281);
			if (this.workTask != null)
			{
				this.controller.ControllerExitingEvent.WaitOne();
				this.workTask.Wait(waitSpan);
				return;
			}
			WTFDiagnostics.TraceInformation(WTFLog.Core, this.traceContext, "[Worker.Wait]: Controller is not started yet. No action is required.", null, "WaitWithRestartSLA", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\Worker.cs", 291);
		}

		// Token: 0x040000CB RID: 203
		private Controller controller;

		// Token: 0x040000CC RID: 204
		private CancellationTokenSource cancellationSource;

		// Token: 0x040000CD RID: 205
		private Task workTask;

		// Token: 0x040000CE RID: 206
		private TracingContext traceContext;

		// Token: 0x040000CF RID: 207
		private BlockingCollection<string>[] packageSets;

		// Token: 0x040000D0 RID: 208
		private Action controllerExitCallback;
	}
}
