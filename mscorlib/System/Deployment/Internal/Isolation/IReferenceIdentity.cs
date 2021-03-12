using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000662 RID: 1634
	[Guid("6eaf5ace-7917-4f3c-b129-e046a9704766")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IReferenceIdentity
	{
		// Token: 0x06004E61 RID: 20065
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name);

		// Token: 0x06004E62 RID: 20066
		[SecurityCritical]
		void SetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name, [MarshalAs(UnmanagedType.LPWStr)] [In] string Value);

		// Token: 0x06004E63 RID: 20067
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE EnumAttributes();

		// Token: 0x06004E64 RID: 20068
		[SecurityCritical]
		IReferenceIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray)] [In] IDENTITY_ATTRIBUTE[] Deltas);
	}
}
