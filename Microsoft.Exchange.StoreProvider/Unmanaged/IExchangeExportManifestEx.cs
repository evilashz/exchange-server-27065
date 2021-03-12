using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026D RID: 621
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("17E58114-B412-40ac-918C-C0B170DD2026")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExchangeExportManifestEx
	{
		// Token: 0x06000ABB RID: 2747
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000ABC RID: 2748
		[PreserveSig]
		unsafe int Config([MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetRead, int cbCnsetRead, SyncConfigFlags flags, [In] IExchangeManifestExCallback pCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps);

		// Token: 0x06000ABD RID: 2749
		[PreserveSig]
		int Synchronize(int ulFlags);

		// Token: 0x06000ABE RID: 2750
		[PreserveSig]
		int GetState(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead);
	}
}
