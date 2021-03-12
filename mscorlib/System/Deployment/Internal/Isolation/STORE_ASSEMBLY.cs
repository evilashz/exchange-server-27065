using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200064A RID: 1610
	internal struct STORE_ASSEMBLY
	{
		// Token: 0x0400212D RID: 8493
		public uint Status;

		// Token: 0x0400212E RID: 8494
		public IDefinitionIdentity DefinitionIdentity;

		// Token: 0x0400212F RID: 8495
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x04002130 RID: 8496
		public ulong AssemblySize;

		// Token: 0x04002131 RID: 8497
		public ulong ChangeId;
	}
}
