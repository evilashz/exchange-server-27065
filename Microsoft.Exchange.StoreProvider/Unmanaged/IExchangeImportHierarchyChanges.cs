using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000271 RID: 625
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Guid("85a66cf0-d0e0-11cd-80fc-00aa004bba0b")]
	[ComImport]
	internal interface IExchangeImportHierarchyChanges
	{
		// Token: 0x06000AD1 RID: 2769
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AD2 RID: 2770
		[PreserveSig]
		int Config(IStream pIStream, int ulFlags);

		// Token: 0x06000AD3 RID: 2771
		[PreserveSig]
		int UpdateState(IStream pIStream);

		// Token: 0x06000AD4 RID: 2772
		[PreserveSig]
		unsafe int ImportFolderChange(int cpvalChanges, SPropValue* ppvalChanges);

		// Token: 0x06000AD5 RID: 2773
		[PreserveSig]
		unsafe int ImportFolderDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);
	}
}
