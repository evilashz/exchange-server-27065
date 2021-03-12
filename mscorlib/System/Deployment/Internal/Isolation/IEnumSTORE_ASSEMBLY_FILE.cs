using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065A RID: 1626
	[Guid("a5c6aaa3-03e4-478d-b9f5-2e45908d5e4f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_FILE
	{
		// Token: 0x06004E35 RID: 20021
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY_FILE[] rgelt);

		// Token: 0x06004E36 RID: 20022
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E37 RID: 20023
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E38 RID: 20024
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_FILE Clone();
	}
}
