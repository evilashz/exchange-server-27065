using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E2 RID: 1762
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4A33D662-2210-463A-BE9F-FBDF1AA554E3")]
	[ComImport]
	internal interface ICompatibleFrameworksMetadataEntry
	{
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004FD8 RID: 20440
		CompatibleFrameworksMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004FD9 RID: 20441
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
