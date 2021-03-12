using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200029F RID: 671
	internal class SafeMsiHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x00065F00 File Offset: 0x00064100
		private SafeMsiHandle() : base(true)
		{
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00065F09 File Offset: 0x00064109
		internal SafeMsiHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00065F19 File Offset: 0x00064119
		protected override bool ReleaseHandle()
		{
			return SafeMsiHandle.CloseHandle(this.handle) == 0U;
		}

		// Token: 0x06001826 RID: 6182
		[DllImport("msi", EntryPoint = "MsiCloseHandle")]
		private static extern uint CloseHandle(IntPtr any);
	}
}
