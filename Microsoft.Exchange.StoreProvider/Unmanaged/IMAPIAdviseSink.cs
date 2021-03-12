using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000283 RID: 643
	[Guid("00020302-0000-0000-C000-000000000046")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMAPIAdviseSink
	{
		// Token: 0x06000B86 RID: 2950
		[PreserveSig]
		unsafe int OnNotify(int cNotif, NOTIFICATION* lpNotifications);
	}
}
