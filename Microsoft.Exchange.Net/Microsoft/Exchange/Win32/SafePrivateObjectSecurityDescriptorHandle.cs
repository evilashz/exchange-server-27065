using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B45 RID: 2885
	internal class SafePrivateObjectSecurityDescriptorHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06003E23 RID: 15907 RVA: 0x000A26DE File Offset: 0x000A08DE
		protected override bool ReleaseHandle()
		{
			return SafePrivateObjectSecurityDescriptorHandle.DestroyPrivateObjectSecurity(ref this.handle);
		}

		// Token: 0x06003E24 RID: 15908
		[DllImport("AdvApi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DestroyPrivateObjectSecurity([In] ref IntPtr toDestroy);
	}
}
