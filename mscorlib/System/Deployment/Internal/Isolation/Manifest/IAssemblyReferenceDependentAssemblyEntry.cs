using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C4 RID: 1732
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C31FF59E-CD25-47b8-9EF3-CF4433EB97CC")]
	[ComImport]
	internal interface IAssemblyReferenceDependentAssemblyEntry
	{
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004F97 RID: 20375
		AssemblyReferenceDependentAssemblyEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004F98 RID: 20376
		string Group { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004F99 RID: 20377
		string Codebase { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004F9A RID: 20378
		ulong Size { [SecurityCritical] get; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004F9B RID: 20379
		object HashValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004F9C RID: 20380
		uint HashAlgorithm { [SecurityCritical] get; }

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004F9D RID: 20381
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004F9E RID: 20382
		string ResourceFallbackCulture { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004F9F RID: 20383
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004FA0 RID: 20384
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004FA1 RID: 20385
		ISection HashElements { [SecurityCritical] get; }
	}
}
