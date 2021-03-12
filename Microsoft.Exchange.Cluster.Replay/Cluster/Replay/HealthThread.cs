using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000102 RID: 258
	internal class HealthThread : ChangePoller
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002F733 File Offset: 0x0002D933
		public HealthThread() : base(true)
		{
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002F747 File Offset: 0x0002D947
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ReplayServiceDiagnosticsTracer;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002F74E File Offset: 0x0002D94E
		public ReplayServiceDiagnostics GetResourceChecker()
		{
			return this.resourceChecker;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002F758 File Offset: 0x0002D958
		protected override void PollerThread()
		{
			while (!this.m_fShutdown)
			{
				TimeSpan timeout = TimeSpan.FromMilliseconds((double)RegistryParameters.ReplayServiceDiagnosticsIntervalMsec);
				if (this.m_shutdownEvent.WaitOne(timeout))
				{
					HealthThread.Tracer.TraceDebug(0L, "HealthThread noticed Shutdown");
					return;
				}
				this.resourceChecker.RunProcessDiagnostics();
			}
		}

		// Token: 0x04000452 RID: 1106
		private ReplayServiceDiagnostics resourceChecker = new ReplayServiceDiagnostics();
	}
}
