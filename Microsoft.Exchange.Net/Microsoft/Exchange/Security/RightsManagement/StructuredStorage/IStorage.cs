using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A19 RID: 2585
	[Guid("0000000B-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IStorage
	{
		// Token: 0x06003880 RID: 14464
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream CreateStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

		// Token: 0x06003881 RID: 14465
		[PreserveSig]
		int OpenStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr reserved1, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved2, [MarshalAs(UnmanagedType.Interface)] out IStream stream);

		// Token: 0x06003882 RID: 14466
		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage CreateStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

		// Token: 0x06003883 RID: 14467
		[PreserveSig]
		int OpenStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] [In] int reserved, [MarshalAs(UnmanagedType.Interface)] out IStorage storage);

		// Token: 0x06003884 RID: 14468
		void CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] pIIDExclude, IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] IStorage stgDest);

		// Token: 0x06003885 RID: 14469
		void MoveElementTo([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] IStorage stgDest, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName, [MarshalAs(UnmanagedType.U4)] [In] int grfFlags);

		// Token: 0x06003886 RID: 14470
		void Commit([MarshalAs(UnmanagedType.I4)] [In] STGC grfCommitFlags);

		// Token: 0x06003887 RID: 14471
		void Revert();

		// Token: 0x06003888 RID: 14472
		void EnumElements([MarshalAs(UnmanagedType.U4)] [In] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4)] [In] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

		// Token: 0x06003889 RID: 14473
		void DestroyElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName);

		// Token: 0x0600388A RID: 14474
		void RenameElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName);

		// Token: 0x0600388B RID: 14475
		void SetElementTimes([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

		// Token: 0x0600388C RID: 14476
		void SetClass([In] ref Guid clsid);

		// Token: 0x0600388D RID: 14477
		void SetStateBits(int grfStateBits, int grfMask);

		// Token: 0x0600388E RID: 14478
		void Stat([In] [Out] ref System.Runtime.InteropServices.ComTypes.STATSTG pStatStg, int grfStatFlag);
	}
}
