using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A08 RID: 2568
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002C92 RID: 11410
		[__DynamicallyInvokable]
		public string pwcsName;

		// Token: 0x04002C93 RID: 11411
		[__DynamicallyInvokable]
		public int type;

		// Token: 0x04002C94 RID: 11412
		[__DynamicallyInvokable]
		public long cbSize;

		// Token: 0x04002C95 RID: 11413
		[__DynamicallyInvokable]
		public FILETIME mtime;

		// Token: 0x04002C96 RID: 11414
		[__DynamicallyInvokable]
		public FILETIME ctime;

		// Token: 0x04002C97 RID: 11415
		[__DynamicallyInvokable]
		public FILETIME atime;

		// Token: 0x04002C98 RID: 11416
		[__DynamicallyInvokable]
		public int grfMode;

		// Token: 0x04002C99 RID: 11417
		[__DynamicallyInvokable]
		public int grfLocksSupported;

		// Token: 0x04002C9A RID: 11418
		[__DynamicallyInvokable]
		public Guid clsid;

		// Token: 0x04002C9B RID: 11419
		[__DynamicallyInvokable]
		public int grfStateBits;

		// Token: 0x04002C9C RID: 11420
		[__DynamicallyInvokable]
		public int reserved;
	}
}
