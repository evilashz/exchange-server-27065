using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B8 RID: 1720
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("97FDCA77-B6F2-4718-A1EB-29D0AECE9C03")]
	[ComImport]
	internal interface ICategoryMembershipEntry
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004F7D RID: 20349
		CategoryMembershipEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004F7E RID: 20350
		IDefinitionIdentity Identity { [SecurityCritical] get; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004F7F RID: 20351
		ISection SubcategoryMembership { [SecurityCritical] get; }
	}
}
