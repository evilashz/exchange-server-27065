using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Shared.MountPoint
{
	// Token: 0x020000AB RID: 171
	internal sealed class SafeVolumeMountPointFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00017910 File Offset: 0x00015B10
		public SafeVolumeMountPointFindHandle() : base(true)
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00017919 File Offset: 0x00015B19
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeVolumeMountPointFindHandle.FindVolumeMountPointClose(this.handle);
		}

		// Token: 0x06000666 RID: 1638
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FindVolumeMountPointClose([In] IntPtr handle);
	}
}
