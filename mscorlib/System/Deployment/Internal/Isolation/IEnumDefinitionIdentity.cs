using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000665 RID: 1637
	[Guid("f3549d9c-fc73-4793-9c00-1cd204254c0c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumDefinitionIdentity
	{
		// Token: 0x06004E6E RID: 20078
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionIdentity[] DefinitionIdentity);

		// Token: 0x06004E6F RID: 20079
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E70 RID: 20080
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E71 RID: 20081
		[SecurityCritical]
		IEnumDefinitionIdentity Clone();
	}
}
