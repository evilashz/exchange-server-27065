using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000C0 RID: 192
	internal sealed class ProcSafeHandle : SafeHandle
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x00014BE8 File Offset: 0x00012DE8
		public ProcSafeHandle() : this(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00014BF6 File Offset: 0x00012DF6
		public ProcSafeHandle(IntPtr handle) : this(handle, true)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00014C00 File Offset: 0x00012E00
		public ProcSafeHandle(IntPtr handle, bool ownHandle) : base(IntPtr.Zero, ownHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014C15 File Offset: 0x00012E15
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00014C27 File Offset: 0x00012E27
		protected override bool ReleaseHandle()
		{
			if (this.handle != (IntPtr)(-1))
			{
				DiagnosticsNativeMethods.CloseHandle(this.handle);
			}
			return true;
		}
	}
}
