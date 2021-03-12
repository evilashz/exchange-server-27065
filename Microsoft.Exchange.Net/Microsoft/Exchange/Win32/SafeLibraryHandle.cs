using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B44 RID: 2884
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E20 RID: 15904 RVA: 0x000A26C8 File Offset: 0x000A08C8
		internal SafeLibraryHandle() : base(true)
		{
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000A26D1 File Offset: 0x000A08D1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}

		// Token: 0x06003E22 RID: 15906
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeLibrary([In] IntPtr hModule);
	}
}
