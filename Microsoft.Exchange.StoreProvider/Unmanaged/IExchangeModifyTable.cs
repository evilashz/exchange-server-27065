using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000276 RID: 630
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("2d734cb0-53fd-101b-b19d-08002b3056e3")]
	[ComImport]
	internal interface IExchangeModifyTable
	{
		// Token: 0x06000ADD RID: 2781
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000ADE RID: 2782
		[PreserveSig]
		int GetTable(int ulFlags, out IMAPITable iMAPITable);

		// Token: 0x06000ADF RID: 2783
		[PreserveSig]
		unsafe int ModifyTable(int ulFlags, [In] _RowList* lpRowList);
	}
}
