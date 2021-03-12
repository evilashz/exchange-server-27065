using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000647 RID: 1607
	internal struct BLOB : IDisposable
	{
		// Token: 0x06004E0F RID: 19983 RVA: 0x00117933 File Offset: 0x00115B33
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.BlobData != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.BlobData);
				this.BlobData = IntPtr.Zero;
			}
		}

		// Token: 0x04002124 RID: 8484
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002125 RID: 8485
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr BlobData;
	}
}
