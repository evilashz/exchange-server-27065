using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000960 RID: 2400
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IStream instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIStream
	{
		// Token: 0x060061B4 RID: 25012
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x060061B5 RID: 25013
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x060061B6 RID: 25014
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x060061B7 RID: 25015
		void SetSize(long libNewSize);

		// Token: 0x060061B8 RID: 25016
		void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x060061B9 RID: 25017
		void Commit(int grfCommitFlags);

		// Token: 0x060061BA RID: 25018
		void Revert();

		// Token: 0x060061BB RID: 25019
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060061BC RID: 25020
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060061BD RID: 25021
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x060061BE RID: 25022
		void Clone(out UCOMIStream ppstm);
	}
}
