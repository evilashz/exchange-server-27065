using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A0 RID: 1696
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("11df5cad-c183-479b-9a44-3842b71639ce")]
	[ComImport]
	internal interface IMuiResourceTypeIdStringEntry
	{
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004F3D RID: 20285
		MuiResourceTypeIdStringEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004F3E RID: 20286
		object StringIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004F3F RID: 20287
		object IntegerIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
