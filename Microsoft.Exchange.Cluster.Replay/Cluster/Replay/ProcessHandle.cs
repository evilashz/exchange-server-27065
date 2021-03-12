using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200012A RID: 298
	internal class ProcessHandle : DisposeTrackableBase
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000327B8 File Offset: 0x000309B8
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmServiceMonitorTracer;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000327BF File Offset: 0x000309BF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProcessHandle>(this);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000327C8 File Offset: 0x000309C8
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing)
				{
					if (this.eventWrapper != null)
					{
						this.eventWrapper.Dispose();
						this.eventWrapper = null;
					}
					if (this.safeWaitHandle != null)
					{
						this.safeWaitHandle.Dispose();
						this.safeWaitHandle = null;
					}
					if (this.process != null)
					{
						this.process = null;
					}
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00032844 File Offset: 0x00030A44
		public WaitHandle WaitHandle
		{
			get
			{
				return this.eventWrapper;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003284C File Offset: 0x00030A4C
		public bool TryGetWaitHandle(Process processToWatch, out Exception ex)
		{
			bool result = false;
			ex = null;
			if (this.process != null)
			{
				throw new InvalidOperationException("ProcessHandle is already in use");
			}
			this.process = processToWatch;
			try
			{
				IntPtr handle;
				try
				{
					handle = this.process.Handle;
				}
				catch (InvalidOperationException ex2)
				{
					ProcessHandle.Tracer.TraceError<InvalidOperationException>(0L, "ProcessHandle.TryGetWaitHandle failed to fetch process handle: {0}", ex2);
					ex = ex2;
					return false;
				}
				this.safeWaitHandle = new SafeWaitHandle(handle, false);
				this.eventWrapper = new ManualResetEvent(false);
				this.eventWrapper.SafeWaitHandle = this.safeWaitHandle;
				result = true;
			}
			catch (Win32Exception ex3)
			{
				ex = ex3;
				ProcessHandle.Tracer.TraceError<Win32Exception>(0L, "ProcessHandle.TryGetWaitHandle hit exception opening process handle: {0}", ex3);
				int hrforException = Marshal.GetHRForException(ex);
				if (hrforException != -2147467259)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x040004BB RID: 1211
		private Process process;

		// Token: 0x040004BC RID: 1212
		private SafeWaitHandle safeWaitHandle;

		// Token: 0x040004BD RID: 1213
		private ManualResetEvent eventWrapper;
	}
}
