using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C7 RID: 1735
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FD47B733-AFBC-45e4-B7C2-BBEB1D9F766C")]
	[ComImport]
	internal interface IAssemblyReferenceEntry
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004FA3 RID: 20387
		AssemblyReferenceEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004FA4 RID: 20388
		IReferenceIdentity ReferenceIdentity { [SecurityCritical] get; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004FA5 RID: 20389
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004FA6 RID: 20390
		IAssemblyReferenceDependentAssemblyEntry DependentAssembly { [SecurityCritical] get; }
	}
}
