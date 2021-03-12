using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B2 RID: 1714
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DA0C3B27-6B6B-4b80-A8F8-6CE14F4BC0A4")]
	[ComImport]
	internal interface ICategoryMembershipDataEntry
	{
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004F74 RID: 20340
		CategoryMembershipDataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004F75 RID: 20341
		uint index { [SecurityCritical] get; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004F76 RID: 20342
		string Xml { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004F77 RID: 20343
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
