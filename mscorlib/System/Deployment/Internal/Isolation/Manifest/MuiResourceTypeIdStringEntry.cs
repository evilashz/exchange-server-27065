using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200069E RID: 1694
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdStringEntry : IDisposable
	{
		// Token: 0x06004F39 RID: 20281 RVA: 0x00118F5C File Offset: 0x0011715C
		~MuiResourceTypeIdStringEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00118F8C File Offset: 0x0011718C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x00118F98 File Offset: 0x00117198
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.StringIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.StringIds);
				this.StringIds = IntPtr.Zero;
			}
			if (this.IntegerIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.IntegerIds);
				this.IntegerIds = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04002223 RID: 8739
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x04002224 RID: 8740
		public uint StringIdsSize;

		// Token: 0x04002225 RID: 8741
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x04002226 RID: 8742
		public uint IntegerIdsSize;
	}
}
