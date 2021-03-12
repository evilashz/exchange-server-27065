using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A7 RID: 1703
	[StructLayout(LayoutKind.Sequential)]
	internal class HashElementEntry : IDisposable
	{
		// Token: 0x06004F4E RID: 20302 RVA: 0x00119160 File Offset: 0x00117360
		~HashElementEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00119190 File Offset: 0x00117390
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0011919C File Offset: 0x0011739C
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.TransformMetadata != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.TransformMetadata);
				this.TransformMetadata = IntPtr.Zero;
			}
			if (this.DigestValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.DigestValue);
				this.DigestValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400223E RID: 8766
		public uint index;

		// Token: 0x0400223F RID: 8767
		public byte Transform;

		// Token: 0x04002240 RID: 8768
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr TransformMetadata;

		// Token: 0x04002241 RID: 8769
		public uint TransformMetadataSize;

		// Token: 0x04002242 RID: 8770
		public byte DigestMethod;

		// Token: 0x04002243 RID: 8771
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr DigestValue;

		// Token: 0x04002244 RID: 8772
		public uint DigestValueSize;

		// Token: 0x04002245 RID: 8773
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;
	}
}
