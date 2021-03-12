using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006BB RID: 1723
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3903B11B-FBE8-477c-825F-DB828B5FD174")]
	[ComImport]
	internal interface ICOMServerEntry
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004F81 RID: 20353
		COMServerEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004F82 RID: 20354
		Guid Clsid { [SecurityCritical] get; }

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004F83 RID: 20355
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004F84 RID: 20356
		Guid ConfiguredGuid { [SecurityCritical] get; }

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004F85 RID: 20357
		Guid ImplementedClsid { [SecurityCritical] get; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004F86 RID: 20358
		Guid TypeLibrary { [SecurityCritical] get; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004F87 RID: 20359
		uint ThreadingModel { [SecurityCritical] get; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004F88 RID: 20360
		string RuntimeVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004F89 RID: 20361
		string HostFile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
