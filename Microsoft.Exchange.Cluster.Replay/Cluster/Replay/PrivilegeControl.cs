using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000129 RID: 297
	internal class PrivilegeControl : DisposeTrackableBase
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x000326F0 File Offset: 0x000308F0
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmServiceMonitorTracer;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000326F7 File Offset: 0x000308F7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PrivilegeControl>(this);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00032700 File Offset: 0x00030900
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && this.privControl != null)
				{
					this.privControl.Dispose();
					this.privControl = null;
				}
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00032754 File Offset: 0x00030954
		public bool TryEnable(string privilege, out Exception ex)
		{
			bool result = false;
			ex = null;
			try
			{
				this.privControl = new Privilege("SeDebugPrivilege");
				this.privControl.Enable();
				result = true;
			}
			catch (PrivilegeNotHeldException ex2)
			{
				ex = ex2;
				PrivilegeControl.Tracer.TraceError<string, PrivilegeNotHeldException>(0L, "TryEnable failed to set priv({0}): {1}", privilege, ex2);
			}
			return result;
		}

		// Token: 0x040004BA RID: 1210
		private Privilege privControl;
	}
}
