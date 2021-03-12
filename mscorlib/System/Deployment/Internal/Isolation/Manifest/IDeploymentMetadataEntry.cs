using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DC RID: 1756
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CFA3F59F-334D-46bf-A5A5-5D11BB2D7EBC")]
	[ComImport]
	internal interface IDeploymentMetadataEntry
	{
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004FC8 RID: 20424
		DeploymentMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004FC9 RID: 20425
		string DeploymentProviderCodebase { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004FCA RID: 20426
		string MinimumRequiredVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004FCB RID: 20427
		ushort MaximumAge { [SecurityCritical] get; }

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004FCC RID: 20428
		byte MaximumAge_Unit { [SecurityCritical] get; }

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004FCD RID: 20429
		uint DeploymentFlags { [SecurityCritical] get; }
	}
}
