using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007B RID: 123
	internal abstract class SafeHGlobalHandleBase : SafeHandleZeroIsInvalid
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00011816 File Offset: 0x0000FA16
		protected SafeHGlobalHandleBase()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001181E File Offset: 0x0000FA1E
		protected SafeHGlobalHandleBase(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001182D File Offset: 0x0000FA2D
		protected SafeHGlobalHandleBase(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011837 File Offset: 0x0000FA37
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeHGlobalHandleBase.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x0600042B RID: 1067
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr LocalFree([In] IntPtr hMem);
	}
}
