using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B48 RID: 2888
	internal sealed class SafeWin32HGlobalHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06003E34 RID: 15924 RVA: 0x000A28A3 File Offset: 0x000A0AA3
		private SafeWin32HGlobalHandle()
		{
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000A28AB File Offset: 0x000A0AAB
		internal SafeWin32HGlobalHandle(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x000A28BA File Offset: 0x000A0ABA
		private SafeWin32HGlobalHandle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
		{
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000A28C4 File Offset: 0x000A0AC4
		public static SafeWin32HGlobalHandle InvalidHandle
		{
			get
			{
				return new SafeWin32HGlobalHandle(IntPtr.Zero, false);
			}
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000A28D1 File Offset: 0x000A0AD1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeWin32HGlobalHandle.GlobalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x06003E39 RID: 15929
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GlobalFree([In] IntPtr hMem);
	}
}
