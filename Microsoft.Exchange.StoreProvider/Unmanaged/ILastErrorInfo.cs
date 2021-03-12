using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000282 RID: 642
	[Guid("42A2AEE7-E53B-49e3-9011-8DF591F16085")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ILastErrorInfo
	{
		// Token: 0x06000B84 RID: 2948
		[PreserveSig]
		int GetLastError(int hResult, out SafeExLinkedMemoryHandle lpMapiError);

		// Token: 0x06000B85 RID: 2949
		[PreserveSig]
		int GetExtendedErrorInfo(out SafeExMemoryHandle pExtendedErrorInfo);
	}
}
