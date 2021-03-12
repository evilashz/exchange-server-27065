using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A7 RID: 2471
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeRightsManagementSessionHandle : SafeHandle, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x000877BB File Offset: 0x000859BB
		private SafeRightsManagementSessionHandle() : this(false)
		{
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000877C4 File Offset: 0x000859C4
		private SafeRightsManagementSessionHandle(bool disableDisposeTracker) : base(IntPtr.Zero, true)
		{
			if (!disableDisposeTracker)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000877E4 File Offset: 0x000859E4
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				num = SafeNativeMethods.DRMCloseSession((uint)((int)this.handle));
				base.SetHandle(IntPtr.Zero);
			}
			return num >= 0;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0008781E File Offset: 0x00085A1E
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRightsManagementSessionHandle>(this);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00087826 File Offset: 0x00085A26
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0008783C File Offset: 0x00085A3C
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

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x0008787C File Offset: 0x00085A7C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero.Equals(this.handle);
			}
		}

		// Token: 0x04002DC7 RID: 11719
		public static readonly SafeRightsManagementSessionHandle InvalidHandle = new SafeRightsManagementSessionHandle(true);

		// Token: 0x04002DC8 RID: 11720
		private DisposeTracker disposeTracker;
	}
}
