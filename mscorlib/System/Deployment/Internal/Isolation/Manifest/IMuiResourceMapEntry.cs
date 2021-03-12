using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A6 RID: 1702
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
	[ComImport]
	internal interface IMuiResourceMapEntry
	{
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004F4B RID: 20299
		MuiResourceMapEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004F4C RID: 20300
		object ResourceTypeIdInt { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004F4D RID: 20301
		object ResourceTypeIdString { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
