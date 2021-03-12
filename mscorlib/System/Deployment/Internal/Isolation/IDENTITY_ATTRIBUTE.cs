using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000648 RID: 1608
	internal struct IDENTITY_ATTRIBUTE
	{
		// Token: 0x04002126 RID: 8486
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Namespace;

		// Token: 0x04002127 RID: 8487
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04002128 RID: 8488
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
