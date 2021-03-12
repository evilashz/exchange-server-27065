using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000281 RID: 641
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IFastStream
	{
		// Token: 0x06000B79 RID: 2937
		[PreserveSig]
		int Read(IntPtr pv, uint cb, out uint cbRead);

		// Token: 0x06000B7A RID: 2938
		[PreserveSig]
		int Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, out int pcbWritten);

		// Token: 0x06000B7B RID: 2939
		[PreserveSig]
		int Seek(long dlibMove, int dwOrigin, out long plibNewPosition);

		// Token: 0x06000B7C RID: 2940
		[PreserveSig]
		int SetSize(long libNewSize);

		// Token: 0x06000B7D RID: 2941
		[PreserveSig]
		int CopyTo(IFastStream pstm, long cb, IntPtr pcbRead, out long pcbWritten);

		// Token: 0x06000B7E RID: 2942
		[PreserveSig]
		int Commit(int grfCommitFlags);

		// Token: 0x06000B7F RID: 2943
		[PreserveSig]
		int Revert();

		// Token: 0x06000B80 RID: 2944
		[PreserveSig]
		int LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000B81 RID: 2945
		[PreserveSig]
		int UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000B82 RID: 2946
		[PreserveSig]
		int Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06000B83 RID: 2947
		[PreserveSig]
		int Clone(out IFastStream ppstm);
	}
}
