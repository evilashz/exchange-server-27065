using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A4 RID: 1700
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceMapEntry : IDisposable
	{
		// Token: 0x06004F47 RID: 20295 RVA: 0x001190B4 File Offset: 0x001172B4
		~MuiResourceMapEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x001190E4 File Offset: 0x001172E4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x001190F0 File Offset: 0x001172F0
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ResourceTypeIdInt != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdInt);
				this.ResourceTypeIdInt = IntPtr.Zero;
			}
			if (this.ResourceTypeIdString != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdString);
				this.ResourceTypeIdString = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04002235 RID: 8757
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdInt;

		// Token: 0x04002236 RID: 8758
		public uint ResourceTypeIdIntSize;

		// Token: 0x04002237 RID: 8759
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdString;

		// Token: 0x04002238 RID: 8760
		public uint ResourceTypeIdStringSize;
	}
}
