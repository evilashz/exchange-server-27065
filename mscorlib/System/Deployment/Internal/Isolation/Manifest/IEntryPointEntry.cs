using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D0 RID: 1744
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1583EFE9-832F-4d08-B041-CAC5ACEDB948")]
	[ComImport]
	internal interface IEntryPointEntry
	{
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004FB1 RID: 20401
		EntryPointEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004FB2 RID: 20402
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004FB3 RID: 20403
		string CommandLine_File { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004FB4 RID: 20404
		string CommandLine_Parameters { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004FB5 RID: 20405
		IReferenceIdentity Identity { [SecurityCritical] get; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004FB6 RID: 20406
		uint Flags { [SecurityCritical] get; }
	}
}
