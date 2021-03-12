using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000660 RID: 1632
	[Guid("5ba7cb30-8508-4114-8c77-262fcda4fadb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_INSTANCE
	{
		// Token: 0x06004E56 RID: 20054
		[SecurityCritical]
		uint Next([In] uint ulElements, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_INSTANCE[] rgInstances);

		// Token: 0x06004E57 RID: 20055
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004E58 RID: 20056
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E59 RID: 20057
		[SecurityCritical]
		IEnumSTORE_CATEGORY_INSTANCE Clone();
	}
}
