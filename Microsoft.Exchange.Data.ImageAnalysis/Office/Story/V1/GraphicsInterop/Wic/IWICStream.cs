using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200003D RID: 61
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("135FF860-22B7-4DDF-B0F6-218F4F299A43")]
	internal interface IWICStream : IStream
	{
		// Token: 0x060001D1 RID: 465
		void Read([In] [Out] byte[] pv, [In] int cb, [In] IntPtr pcbRead);

		// Token: 0x060001D2 RID: 466
		void Write([In] [Out] byte[] pv, [In] int cb, [In] IntPtr pcbWritten);

		// Token: 0x060001D3 RID: 467
		void Seek([In] long dlibMove, [In] int dwOrigin, [In] IntPtr plibNewPosition);

		// Token: 0x060001D4 RID: 468
		void SetSize([In] long libNewSize);

		// Token: 0x060001D5 RID: 469
		void CopyTo([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm, [In] long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x060001D6 RID: 470
		void Commit([In] int grfCommitFlags);

		// Token: 0x060001D7 RID: 471
		void Revert();

		// Token: 0x060001D8 RID: 472
		void LockRegion([In] long libOffset, [In] long cb, [In] int dwLockType);

		// Token: 0x060001D9 RID: 473
		void UnlockRegion([In] long libOffset, [In] long cb, [In] int dwLockType);

		// Token: 0x060001DA RID: 474
		void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In] int grfStatFlag);

		// Token: 0x060001DB RID: 475
		void Clone([MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

		// Token: 0x060001DC RID: 476
		void InitializeFromIStream([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream);

		// Token: 0x060001DD RID: 477
		void InitializeFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename, [In] GenericAccess dwDesiredAccess);

		// Token: 0x060001DE RID: 478
		void InitializeFromMemory([In] IntPtr pbBuffer, [In] int cbBufferSize);

		// Token: 0x060001DF RID: 479
		void InitializeFromIStreamRegion([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] long ulOffset, [In] long ulMaxSize);
	}
}
