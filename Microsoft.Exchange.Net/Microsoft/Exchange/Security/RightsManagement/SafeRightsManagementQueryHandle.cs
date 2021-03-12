using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A6 RID: 2470
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeRightsManagementQueryHandle : SafeHandle, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600358B RID: 13707 RVA: 0x000876D0 File Offset: 0x000858D0
		private SafeRightsManagementQueryHandle() : this(false)
		{
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000876D9 File Offset: 0x000858D9
		private SafeRightsManagementQueryHandle(bool disableDisposeTracker) : base(IntPtr.Zero, true)
		{
			if (!disableDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000876F8 File Offset: 0x000858F8
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				num = SafeNativeMethods.DRMCloseQueryHandle((uint)((int)this.handle));
				base.SetHandle(IntPtr.Zero);
			}
			return num >= 0;
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x00087732 File Offset: 0x00085932
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRightsManagementQueryHandle>(this);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x0008773A File Offset: 0x0008593A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00087750 File Offset: 0x00085950
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

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x00087790 File Offset: 0x00085990
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero.Equals(this.handle);
			}
		}

		// Token: 0x04002DC6 RID: 11718
		private DisposeTracker disposeTracker;
	}
}
