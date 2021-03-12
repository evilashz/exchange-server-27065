using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000664 RID: 1636
	[Guid("9cdaae75-246e-4b00-a26d-b9aec137a3eb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumIDENTITY_ATTRIBUTE
	{
		// Token: 0x06004E69 RID: 20073
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDENTITY_ATTRIBUTE[] rgAttributes);

		// Token: 0x06004E6A RID: 20074
		[SecurityCritical]
		IntPtr CurrentIntoBuffer([In] IntPtr Available, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] Data);

		// Token: 0x06004E6B RID: 20075
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E6C RID: 20076
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E6D RID: 20077
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE Clone();
	}
}
