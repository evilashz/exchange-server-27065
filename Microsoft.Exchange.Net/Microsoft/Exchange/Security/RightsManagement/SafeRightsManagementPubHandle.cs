using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A5 RID: 2469
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeRightsManagementPubHandle : SafeHandle, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003583 RID: 13699 RVA: 0x000875D8 File Offset: 0x000857D8
		private SafeRightsManagementPubHandle() : this(false)
		{
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000875E1 File Offset: 0x000857E1
		private SafeRightsManagementPubHandle(bool disableDisposeTracker) : base(IntPtr.Zero, true)
		{
			if (!disableDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00087600 File Offset: 0x00085800
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				num = SafeNativeMethods.DRMClosePubHandle((uint)((int)this.handle));
				base.SetHandle(IntPtr.Zero);
			}
			return num >= 0;
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x0008763A File Offset: 0x0008583A
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRightsManagementPubHandle>(this);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00087642 File Offset: 0x00085842
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x00087658 File Offset: 0x00085858
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

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x00087698 File Offset: 0x00085898
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero.Equals(this.handle);
			}
		}

		// Token: 0x04002DC4 RID: 11716
		public static readonly SafeRightsManagementPubHandle InvalidHandle = new SafeRightsManagementPubHandle(true);

		// Token: 0x04002DC5 RID: 11717
		private DisposeTracker disposeTracker;
	}
}
