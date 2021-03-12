using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026C RID: 620
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("82D370F5-6F10-457d-99F9-11977856A7AA")]
	[ComImport]
	internal interface IExchangeExportManifest
	{
		// Token: 0x06000AB7 RID: 2743
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AB8 RID: 2744
		[PreserveSig]
		unsafe int Config(IStream pIStream, SyncConfigFlags flags, [In] IExchangeManifestCallback pCallback, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps);

		// Token: 0x06000AB9 RID: 2745
		[PreserveSig]
		int Synchronize(int ulFlags);

		// Token: 0x06000ABA RID: 2746
		[PreserveSig]
		int Checkpoint(IStream lpStream, [MarshalAs(UnmanagedType.Bool)] [In] bool clearCnsets, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeMids, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] changeAssociatedCns, [MarshalAs(UnmanagedType.LPArray)] [In] long[] deleteMids);
	}
}
