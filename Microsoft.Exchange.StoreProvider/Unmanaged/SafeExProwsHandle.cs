using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CA RID: 714
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SafeExProwsHandle : SafeExMemoryHandle
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x00037270 File Offset: 0x00035470
		internal SafeExProwsHandle()
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00037278 File Offset: 0x00035478
		internal SafeExProwsHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00037281 File Offset: 0x00035481
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExProwsHandle>(this);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00037289 File Offset: 0x00035489
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				SafeExProwsHandle.FreeProwsFnEx(this.handle);
			}
			return true;
		}

		// Token: 0x06000E88 RID: 3720
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void FreeProwsFnEx(IntPtr buffer);
	}
}
