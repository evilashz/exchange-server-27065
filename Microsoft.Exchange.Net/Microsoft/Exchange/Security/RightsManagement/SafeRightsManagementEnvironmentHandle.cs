using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A3 RID: 2467
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeRightsManagementEnvironmentHandle : SafeHandle, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x000873F3 File Offset: 0x000855F3
		private SafeRightsManagementEnvironmentHandle() : this(false)
		{
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000873FC File Offset: 0x000855FC
		private SafeRightsManagementEnvironmentHandle(bool disableDisposeTracker) : base(IntPtr.Zero, true)
		{
			if (!disableDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x0008741C File Offset: 0x0008561C
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				num = SafeNativeMethods.DRMCloseEnvironmentHandle((uint)((int)this.handle));
				base.SetHandle(IntPtr.Zero);
			}
			return num >= 0;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00087456 File Offset: 0x00085656
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRightsManagementEnvironmentHandle>(this);
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0008745E File Offset: 0x0008565E
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00087474 File Offset: 0x00085674
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

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000874B4 File Offset: 0x000856B4
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero.Equals(this.handle);
			}
		}

		// Token: 0x04002DC1 RID: 11713
		private DisposeTracker disposeTracker;
	}
}
