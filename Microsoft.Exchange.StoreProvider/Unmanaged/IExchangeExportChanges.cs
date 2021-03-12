using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026A RID: 618
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("a3ea9cc0-d1b2-11cd-80fc-00aa004bba0b")]
	[ComImport]
	internal interface IExchangeExportChanges
	{
		// Token: 0x06000AAE RID: 2734
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AAF RID: 2735
		[PreserveSig]
		unsafe int Config(IStream pIStream, int ulFlags, [MarshalAs(UnmanagedType.IUnknown)] [In] object pIUnknown, [In] SRestriction* lpRestriction, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpIncludeProps, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpExcludeProps, int ulBufferSize);

		// Token: 0x06000AB0 RID: 2736
		[PreserveSig]
		int Synchronize(out int lpulSteps, out int lpulProgress);

		// Token: 0x06000AB1 RID: 2737
		[PreserveSig]
		int UpdateState(IStream lpStream);
	}
}
