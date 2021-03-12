using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B5 RID: 1717
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("5A7A54D7-5AD5-418e-AB7A-CF823A8D48D0")]
	[ComImport]
	internal interface ISubcategoryMembershipEntry
	{
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004F79 RID: 20345
		SubcategoryMembershipEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004F7A RID: 20346
		string Subcategory { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004F7B RID: 20347
		ISection CategoryMembershipData { [SecurityCritical] get; }
	}
}
