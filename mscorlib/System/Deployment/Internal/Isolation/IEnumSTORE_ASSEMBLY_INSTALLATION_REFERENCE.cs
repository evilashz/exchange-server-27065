using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000653 RID: 1619
	[Guid("d8b1aacb-5142-4abb-bcc1-e9dc9052a89e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
	{
		// Token: 0x06004E10 RID: 19984
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreApplicationReference[] rgelt);

		// Token: 0x06004E11 RID: 19985
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E12 RID: 19986
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E13 RID: 19987
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE Clone();
	}
}
