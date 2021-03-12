using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000656 RID: 1622
	[Guid("5fa4f590-a416-4b22-ac79-7c3f0d31f303")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY
	{
		// Token: 0x06004E1F RID: 19999
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreOperationMetadataProperty[] AppIds);

		// Token: 0x06004E20 RID: 20000
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004E21 RID: 20001
		[SecurityCritical]
		void Reset();

		// Token: 0x06004E22 RID: 20002
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY Clone();
	}
}
