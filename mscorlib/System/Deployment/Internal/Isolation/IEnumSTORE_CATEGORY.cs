using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065C RID: 1628
	[Guid("b840a2f5-a497-4a6d-9038-cd3ec2fbd222")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY
	{
		// Token: 0x06004E40 RID: 20032
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY[] rgElements);

		// Token: 0x06004E41 RID: 20033
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004E42 RID: 20034
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E43 RID: 20035
		[SecurityCritical]
		IEnumSTORE_CATEGORY Clone();
	}
}
