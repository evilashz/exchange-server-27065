using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A4 RID: 2468
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeRightsManagementHandle : SafeHandle, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600357B RID: 13691 RVA: 0x000874DF File Offset: 0x000856DF
		private SafeRightsManagementHandle() : this(false)
		{
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000874E8 File Offset: 0x000856E8
		private SafeRightsManagementHandle(bool disableDisposeTracker) : base(IntPtr.Zero, true)
		{
			if (!disableDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00087508 File Offset: 0x00085708
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				num = SafeNativeMethods.DRMCloseHandle((uint)((int)this.handle));
				base.SetHandle(IntPtr.Zero);
			}
			return num >= 0;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00087542 File Offset: 0x00085742
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRightsManagementHandle>(this);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0008754A File Offset: 0x0008574A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00087560 File Offset: 0x00085760
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000875A0 File Offset: 0x000857A0
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero.Equals(this.handle);
			}
		}

		// Token: 0x04002DC2 RID: 11714
		public static readonly SafeRightsManagementHandle InvalidHandle = new SafeRightsManagementHandle(true);

		// Token: 0x04002DC3 RID: 11715
		private DisposeTracker disposeTracker;
	}
}
