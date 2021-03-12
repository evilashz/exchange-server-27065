using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000ADE RID: 2782
	internal sealed class SafeNCryptHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BBE RID: 15294 RVA: 0x00099710 File Offset: 0x00097910
		internal SafeNCryptHandle() : base(true)
		{
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00099719 File Offset: 0x00097919
		public SafeNCryptHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00099729 File Offset: 0x00097929
		protected override bool ReleaseHandle()
		{
			return CngNativeMethods.NCryptFreeObject(this.handle) == 0;
		}
	}
}
