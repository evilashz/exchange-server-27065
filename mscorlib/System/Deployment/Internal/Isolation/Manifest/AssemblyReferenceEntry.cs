using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C5 RID: 1733
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceEntry
	{
		// Token: 0x040022B5 RID: 8885
		public IReferenceIdentity ReferenceIdentity;

		// Token: 0x040022B6 RID: 8886
		public uint Flags;

		// Token: 0x040022B7 RID: 8887
		public AssemblyReferenceDependentAssemblyEntry DependentAssembly;
	}
}
