using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006BE RID: 1726
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("54F198EC-A63A-45ea-A984-452F68D9B35B")]
	[ComImport]
	internal interface IProgIdRedirectionEntry
	{
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004F8B RID: 20363
		ProgIdRedirectionEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004F8C RID: 20364
		string ProgId { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004F8D RID: 20365
		Guid RedirectedGuid { [SecurityCritical] get; }
	}
}
