using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000658 RID: 1624
	[Guid("a5c637bf-6eaa-4e5f-b535-55299657e33e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY
	{
		// Token: 0x06004E2A RID: 20010
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY[] rgelt);

		// Token: 0x06004E2B RID: 20011
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E2C RID: 20012
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E2D RID: 20013
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY Clone();
	}
}
