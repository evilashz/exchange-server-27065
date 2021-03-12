using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000654 RID: 1620
	[Guid("f9fd4090-93db-45c0-af87-624940f19cff")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA
	{
		// Token: 0x06004E14 RID: 19988
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionAppId[] AppIds);

		// Token: 0x06004E15 RID: 19989
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E16 RID: 19990
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E17 RID: 19991
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA Clone();
	}
}
