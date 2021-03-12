using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A1 RID: 1697
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdIntEntry : IDisposable
	{
		// Token: 0x06004F40 RID: 20288 RVA: 0x00119008 File Offset: 0x00117208
		~MuiResourceTypeIdIntEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x00119038 File Offset: 0x00117238
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00119044 File Offset: 0x00117244
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

		// Token: 0x0400222C RID: 8748
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x0400222D RID: 8749
		public uint StringIdsSize;

		// Token: 0x0400222E RID: 8750
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x0400222F RID: 8751
		public uint IntegerIdsSize;
	}
}
