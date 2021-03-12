using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A09 RID: 2569
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IStream
	{
		// Token: 0x06006548 RID: 25928
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x06006549 RID: 25929
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x0600654A RID: 25930
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x0600654B RID: 25931
		[__DynamicallyInvokable]
		void SetSize(long libNewSize);

		// Token: 0x0600654C RID: 25932
		void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x0600654D RID: 25933
		[__DynamicallyInvokable]
		void Commit(int grfCommitFlags);

		// Token: 0x0600654E RID: 25934
		[__DynamicallyInvokable]
		void Revert();

		// Token: 0x0600654F RID: 25935
		[__DynamicallyInvokable]
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06006550 RID: 25936
		[__DynamicallyInvokable]
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06006551 RID: 25937
		[__DynamicallyInvokable]
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06006552 RID: 25938
		[__DynamicallyInvokable]
		void Clone(out IStream ppstm);
	}
}
