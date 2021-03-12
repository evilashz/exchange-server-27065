using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Worker
{
	// Token: 0x02000002 RID: 2
	internal sealed class ControlListener : DisposableBase, IWorkerProcess
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ControlListener(Guid instanceId, PipeStream readPipeStream, Action shutdownTimeoutCallback, int shutdownTimeout, bool timeoutOnRetire)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.instanceId = instanceId;
				this.shutdownEvent = disposeGuard.Add<ManualResetEvent>(new ManualResetEvent(false));
				this.controlListener = new ControlListener.Listener(readPipeStream, this);
				this.shutdownCompleteEvent = disposeGuard.Add<ManualResetEvent>(new ManualResetEvent(false));
				this.shutdownTimer = null;
				this.shutdownTimeoutCallback = shutdownTimeoutCallback;
				this.shutdownTimeout = shutdownTimeout;
				this.timeoutOnRetire = timeoutOnRetire;
				this.workerShutdown = 0;
				disposeGuard.Success();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002185 File Offset: 0x00000385
		public bool IsShuttingDown
		{
			get
			{
				return 0 != Interlocked.CompareExchange(ref this.workerShutdown, 0, 0);
			}
			set
			{
				Interlocked.Exchange(ref this.workerShutdown, value ? 1 : 0);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000219A File Offset: 0x0000039A
		public bool Initialize()
		{
			return this.controlListener.Initialize();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021A7 File Offset: 0x000003A7
		public void WaitForShutdown()
		{
			this.shutdownEvent.WaitOne();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B8 File Offset: 0x000003B8
		public void Retire()
		{
			try
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Worker: Shutdown control command received");
				if (this.timeoutOnRetire)
				{
					this.SetupShutdownTimeoutMonitor();
				}
			}
			finally
			{
				this.shutdownEvent.Set();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002204 File Offset: 0x00000404
		public void Stop()
		{
			if (!this.IsShuttingDown)
			{
				try
				{
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Worker: Terminate control command received");
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ServiceProcessTerminated, new object[]
					{
						this.instanceId,
						DiagnosticsNativeMethods.GetCurrentProcessId().ToString()
					});
				}
				finally
				{
					WatsonOnUnhandledException.KillCurrentProcess();
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002274 File Offset: 0x00000474
		public void Pause()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002276 File Offset: 0x00000476
		public void Continue()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002278 File Offset: 0x00000478
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ControlListener>(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002280 File Offset: 0x00000480
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.shutdownEvent != null)
				{
					this.shutdownEvent.Dispose();
					this.shutdownEvent = null;
				}
				if (this.shutdownTimer != null)
				{
					this.shutdownTimer.Unregister(null);
					this.shutdownTimer = null;
				}
				if (this.shutdownCompleteEvent != null)
				{
					this.shutdownCompleteEvent.Dispose();
					this.shutdownCompleteEvent = null;
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002320 File Offset: 0x00000520
		private void SetupShutdownTimeoutMonitor()
		{
			if (this.shutdownTimeoutCallback != null)
			{
				this.shutdownTimer = ThreadPool.RegisterWaitForSingleObject(this.shutdownCompleteEvent, delegate(object _, bool timeout)
				{
					if (timeout)
					{
						WatsonOnUnhandledException.Guard(NullExecutionDiagnostics.Instance, new TryDelegate(this, (UIntPtr)ldftn(<SetupShutdownTimeoutMonitor>b__1)));
					}
				}, null, this.shutdownTimeout, true);
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly bool timeoutOnRetire;

		// Token: 0x04000002 RID: 2
		private readonly Guid instanceId;

		// Token: 0x04000003 RID: 3
		private ManualResetEvent shutdownEvent;

		// Token: 0x04000004 RID: 4
		private ControlListener.Listener controlListener;

		// Token: 0x04000005 RID: 5
		private RegisteredWaitHandle shutdownTimer;

		// Token: 0x04000006 RID: 6
		private ManualResetEvent shutdownCompleteEvent;

		// Token: 0x04000007 RID: 7
		private int shutdownTimeout;

		// Token: 0x04000008 RID: 8
		private Action shutdownTimeoutCallback;

		// Token: 0x04000009 RID: 9
		private int workerShutdown;

		// Token: 0x02000003 RID: 3
		private class Listener : WorkerControlObject
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002361 File Offset: 0x00000561
			public Listener(PipeStream readPipeStream, IWorkerProcess controlCallback) : base(readPipeStream, controlCallback, ExTraceGlobals.StartupShutdownTracer)
			{
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002370 File Offset: 0x00000570
			protected override bool ProcessCommand(char command, int size, Stream data)
			{
				return true;
			}
		}
	}
}
