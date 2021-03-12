using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DF RID: 1759
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CF168CF4-4E8F-4d92-9D2A-60E5CA21CF85")]
	[ComImport]
	internal interface IDependentOSMetadataEntry
	{
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004FCF RID: 20431
		DependentOSMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004FD0 RID: 20432
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004FD1 RID: 20433
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004FD2 RID: 20434
		ushort MajorVersion { [SecurityCritical] get; }

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004FD3 RID: 20435
		ushort MinorVersion { [SecurityCritical] get; }

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004FD4 RID: 20436
		ushort BuildNumber { [SecurityCritical] get; }

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004FD5 RID: 20437
		byte ServicePackMajor { [SecurityCritical] get; }

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004FD6 RID: 20438
		byte ServicePackMinor { [SecurityCritical] get; }
	}
}
