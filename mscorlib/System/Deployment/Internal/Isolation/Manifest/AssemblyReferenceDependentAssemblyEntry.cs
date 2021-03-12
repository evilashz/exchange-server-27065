using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C2 RID: 1730
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
	{
		// Token: 0x06004F93 RID: 20371 RVA: 0x001192E4 File Offset: 0x001174E4
		~AssemblyReferenceDependentAssemblyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00119314 File Offset: 0x00117514
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0011931D File Offset: 0x0011751D
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
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400229E RID: 8862
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x0400229F RID: 8863
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Codebase;

		// Token: 0x040022A0 RID: 8864
		public ulong Size;

		// Token: 0x040022A1 RID: 8865
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x040022A2 RID: 8866
		public uint HashValueSize;

		// Token: 0x040022A3 RID: 8867
		public uint HashAlgorithm;

		// Token: 0x040022A4 RID: 8868
		public uint Flags;

		// Token: 0x040022A5 RID: 8869
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ResourceFallbackCulture;

		// Token: 0x040022A6 RID: 8870
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040022A7 RID: 8871
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040022A8 RID: 8872
		public ISection HashElements;
	}
}
