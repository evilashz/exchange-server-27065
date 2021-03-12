using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000285 RID: 645
	[Guid("0000000B-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IStorage
	{
		// Token: 0x06000B9E RID: 2974
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream CreateStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

		// Token: 0x06000B9F RID: 2975
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream OpenStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, IntPtr reserved1, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

		// Token: 0x06000BA0 RID: 2976
		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage CreateStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

		// Token: 0x06000BA1 RID: 2977
		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage OpenStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] [In] int reserved);

		// Token: 0x06000BA2 RID: 2978
		void CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] pIIDExclude, IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] IStorage stgDest);

		// Token: 0x06000BA3 RID: 2979
		void MoveElementTo([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] IStorage stgDest, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName, [MarshalAs(UnmanagedType.U4)] [In] int grfFlags);

		// Token: 0x06000BA4 RID: 2980
		void Commit(int grfCommitFlags);

		// Token: 0x06000BA5 RID: 2981
		void Revert();

		// Token: 0x06000BA6 RID: 2982
		void EnumElements([MarshalAs(UnmanagedType.U4)] [In] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4)] [In] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

		// Token: 0x06000BA7 RID: 2983
		void DestroyElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName);

		// Token: 0x06000BA8 RID: 2984
		void RenameElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName);

		// Token: 0x06000BA9 RID: 2985
		void SetElementTimes([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

		// Token: 0x06000BAA RID: 2986
		void SetClass([In] ref Guid clsid);

		// Token: 0x06000BAB RID: 2987
		void SetStateBits(int grfStateBits, int grfMask);

		// Token: 0x06000BAC RID: 2988
		void Stat([Out] System.Runtime.InteropServices.ComTypes.STATSTG pStatStg, int grfStatFlag);
	}
}
