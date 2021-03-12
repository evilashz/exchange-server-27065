using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000668 RID: 1640
	[Guid("054f0bef-9e45-4363-8f5a-2f8e142d9a3b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IReferenceAppId
	{
		// Token: 0x06004E7C RID: 20092
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06004E7D RID: 20093
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06004E7E RID: 20094
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06004E7F RID: 20095
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x06004E80 RID: 20096
		[SecurityCritical]
		IEnumReferenceIdentity EnumAppPath();
	}
}
