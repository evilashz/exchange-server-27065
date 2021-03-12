using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000666 RID: 1638
	[Guid("b30352cf-23da-4577-9b3f-b4e6573be53b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumReferenceIdentity
	{
		// Token: 0x06004E72 RID: 20082
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IReferenceIdentity[] ReferenceIdentity);

		// Token: 0x06004E73 RID: 20083
		[SecurityCritical]
		void Skip(uint celt);

		// Token: 0x06004E74 RID: 20084
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E75 RID: 20085
		[SecurityCritical]
		IEnumReferenceIdentity Clone();
	}
}
