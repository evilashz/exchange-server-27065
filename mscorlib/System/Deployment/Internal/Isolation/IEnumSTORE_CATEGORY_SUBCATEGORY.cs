using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065E RID: 1630
	[Guid("19be1967-b2fc-4dc1-9627-f3cb6305d2a7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_SUBCATEGORY
	{
		// Token: 0x06004E4B RID: 20043
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_SUBCATEGORY[] rgElements);

		// Token: 0x06004E4C RID: 20044
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004E4D RID: 20045
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E4E RID: 20046
		[SecurityCritical]
		IEnumSTORE_CATEGORY_SUBCATEGORY Clone();
	}
}
