using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Shared.MountPoint
{
	// Token: 0x020000AA RID: 170
	internal sealed class SafeVolumeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x000178FA File Offset: 0x00015AFA
		public SafeVolumeFindHandle() : base(true)
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00017903 File Offset: 0x00015B03
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeVolumeFindHandle.FindVolumeClose(this.handle);
		}

		// Token: 0x06000663 RID: 1635
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FindVolumeClose([In] IntPtr handle);
	}
}
