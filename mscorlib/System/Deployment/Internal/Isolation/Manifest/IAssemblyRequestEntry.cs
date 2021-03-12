using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D6 RID: 1750
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("2474ECB4-8EFD-4410-9F31-B3E7C4A07731")]
	[ComImport]
	internal interface IAssemblyRequestEntry
	{
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004FBC RID: 20412
		AssemblyRequestEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004FBD RID: 20413
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004FBE RID: 20414
		string permissionSetID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
