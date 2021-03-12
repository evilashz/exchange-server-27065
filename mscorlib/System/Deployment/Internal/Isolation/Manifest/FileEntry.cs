using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006AA RID: 1706
	[StructLayout(LayoutKind.Sequential)]
	internal class FileEntry : IDisposable
	{
		// Token: 0x06004F59 RID: 20313 RVA: 0x0011920C File Offset: 0x0011740C
		~FileEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0011923C File Offset: 0x0011743C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00119248 File Offset: 0x00117448
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.HashValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.HashValue);
				this.HashValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				if (this.MuiMapping != null)
				{
					this.MuiMapping.Dispose(true);
					this.MuiMapping = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400224E RID: 8782
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400224F RID: 8783
		public uint HashAlgorithm;

		// Token: 0x04002250 RID: 8784
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LoadFrom;

		// Token: 0x04002251 RID: 8785
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourcePath;

		// Token: 0x04002252 RID: 8786
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ImportPath;

		// Token: 0x04002253 RID: 8787
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceName;

		// Token: 0x04002254 RID: 8788
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Location;

		// Token: 0x04002255 RID: 8789
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x04002256 RID: 8790
		public uint HashValueSize;

		// Token: 0x04002257 RID: 8791
		public ulong Size;

		// Token: 0x04002258 RID: 8792
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x04002259 RID: 8793
		public uint Flags;

		// Token: 0x0400225A RID: 8794
		public MuiResourceMapEntry MuiMapping;

		// Token: 0x0400225B RID: 8795
		public uint WritableType;

		// Token: 0x0400225C RID: 8796
		public ISection HashElements;
	}
}
