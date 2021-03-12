using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A16 RID: 2582
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000000C-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IStream
	{
		// Token: 0x0600385C RID: 14428
		int Read(IntPtr buf, int len);

		// Token: 0x0600385D RID: 14429
		int Write(IntPtr buf, int len);

		// Token: 0x0600385E RID: 14430
		[return: MarshalAs(UnmanagedType.I8)]
		long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, int dwOrigin);

		// Token: 0x0600385F RID: 14431
		void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

		// Token: 0x06003860 RID: 14432
		[return: MarshalAs(UnmanagedType.I8)]
		long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

		// Token: 0x06003861 RID: 14433
		void Commit([MarshalAs(UnmanagedType.I4)] [In] STGC grfCommitFlags);

		// Token: 0x06003862 RID: 14434
		void Revert();

		// Token: 0x06003863 RID: 14435
		void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

		// Token: 0x06003864 RID: 14436
		void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

		// Token: 0x06003865 RID: 14437
		void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pStatstg, [MarshalAs(UnmanagedType.I4)] [In] STATFLAG grfStatFlag);

		// Token: 0x06003866 RID: 14438
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream Clone();
	}
}
