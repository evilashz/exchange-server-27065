using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200027B RID: 635
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("83BB0082-568A-4227-A830-C1A3844B9331")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExRpcMessage
	{
		// Token: 0x06000B5A RID: 2906
		[PreserveSig]
		int Deliver(int ulFlags);

		// Token: 0x06000B5B RID: 2907
		[PreserveSig]
		int DoneWithMessage();

		// Token: 0x06000B5C RID: 2908
		[PreserveSig]
		int TransportSendMessage(out int lpcValues, [PointerType("SPropValue*")] out SafeExLinkedMemoryHandle lppPropArray);

		// Token: 0x06000B5D RID: 2909
		[PreserveSig]
		int SubmitMessageEx(int ulFlags);
	}
}
