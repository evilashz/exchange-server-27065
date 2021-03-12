using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D9 RID: 1753
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
	[ComImport]
	internal interface IDescriptionMetadataEntry
	{
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004FC0 RID: 20416
		DescriptionMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004FC1 RID: 20417
		string Publisher { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004FC2 RID: 20418
		string Product { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004FC3 RID: 20419
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004FC4 RID: 20420
		string IconFile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004FC5 RID: 20421
		string ErrorReportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004FC6 RID: 20422
		string SuiteName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
