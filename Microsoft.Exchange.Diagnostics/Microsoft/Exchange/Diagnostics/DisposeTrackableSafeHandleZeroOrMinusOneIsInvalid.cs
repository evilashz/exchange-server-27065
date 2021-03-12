using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001E RID: 30
	public abstract class DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid : SafeHandleZeroOrMinusOneIsInvalid, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000325A File Offset: 0x0000145A
		protected DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid() : this(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003268 File Offset: 0x00001468
		protected DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid(IntPtr handle) : this(handle, true)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003272 File Offset: 0x00001472
		protected DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000328E File Offset: 0x0000148E
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000077 RID: 119
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000078 RID: 120 RVA: 0x000032A4 File Offset: 0x000014A4
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
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

		// Token: 0x04000082 RID: 130
		private readonly DisposeTracker disposeTracker;
	}
}
