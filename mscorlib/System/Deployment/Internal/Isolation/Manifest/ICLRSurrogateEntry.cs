using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C1 RID: 1729
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1E0422A1-F0D2-44ae-914B-8A2DECCFD22B")]
	[ComImport]
	internal interface ICLRSurrogateEntry
	{
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004F8F RID: 20367
		CLRSurrogateEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004F90 RID: 20368
		Guid Clsid { [SecurityCritical] get; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004F91 RID: 20369
		string RuntimeVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004F92 RID: 20370
		string ClassName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
