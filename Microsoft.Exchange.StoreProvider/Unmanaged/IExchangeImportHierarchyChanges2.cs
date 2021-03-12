using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000272 RID: 626
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7846EDBA-8287-4d76-BD5F-1E0513D10E0C")]
	[ComImport]
	internal interface IExchangeImportHierarchyChanges2 : IExchangeImportHierarchyChanges
	{
		// Token: 0x06000AD6 RID: 2774
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AD7 RID: 2775
		[PreserveSig]
		int Config(IStream pIStream, int ulFlags);

		// Token: 0x06000AD8 RID: 2776
		[PreserveSig]
		int UpdateState(IStream pIStream);

		// Token: 0x06000AD9 RID: 2777
		[PreserveSig]
		unsafe int ImportFolderChange(int cpvalChanges, SPropValue* ppvalChanges);

		// Token: 0x06000ADA RID: 2778
		[PreserveSig]
		unsafe int ImportFolderDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x06000ADB RID: 2779
		[PreserveSig]
		int ConfigEx([MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, int ulFlags);

		// Token: 0x06000ADC RID: 2780
		[PreserveSig]
		int UpdateStateEx(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen);
	}
}
