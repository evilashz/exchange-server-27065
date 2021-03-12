using System;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal abstract class SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid : SafeHandleZeroOrMinusOneIsInvalid, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x000065A8 File Offset: 0x000047A8
		public SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid() : base(true)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000065C8 File Offset: 0x000047C8
		public SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid(bool ownsHandleLifetime) : base(ownsHandleLifetime)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000065E8 File Offset: 0x000047E8
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid>(this);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000065F0 File Offset: 0x000047F0
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006605 File Offset: 0x00004805
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing && this.m_disposeTracker != null)
				{
					this.m_disposeTracker.Dispose();
					this.m_disposeTracker = null;
				}
				this.m_disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x04000058 RID: 88
		private DisposeTracker m_disposeTracker;

		// Token: 0x04000059 RID: 89
		private bool m_disposed;
	}
}
