using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000667 RID: 1639
	[Guid("d91e12d8-98ed-47fa-9936-39421283d59b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDefinitionAppId
	{
		// Token: 0x06004E76 RID: 20086
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06004E77 RID: 20087
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06004E78 RID: 20088
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06004E79 RID: 20089
		[SecurityCritical]
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x06004E7A RID: 20090
		[SecurityCritical]
		IEnumDefinitionIdentity EnumAppPath();

		// Token: 0x06004E7B RID: 20091
		[SecurityCritical]
		void SetAppPath([In] uint cIDefinitionIdentity, [MarshalAs(UnmanagedType.LPArray)] [In] IDefinitionIdentity[] DefinitionIdentity);
	}
}
