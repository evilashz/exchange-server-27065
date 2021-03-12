using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A3 RID: 1699
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("55b2dec1-d0f6-4bf4-91b1-30f73ad8e4df")]
	[ComImport]
	internal interface IMuiResourceTypeIdIntEntry
	{
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004F44 RID: 20292
		MuiResourceTypeIdIntEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004F45 RID: 20293
		object StringIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004F46 RID: 20294
		object IntegerIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
