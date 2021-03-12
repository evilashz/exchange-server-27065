using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E5 RID: 1765
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AB1ED79F-943E-407d-A80B-0744E3A95B28")]
	[ComImport]
	internal interface IMetadataSectionEntry
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004FDE RID: 20446
		MetadataSectionEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004FDF RID: 20447
		uint SchemaVersion { [SecurityCritical] get; }

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004FE0 RID: 20448
		uint ManifestFlags { [SecurityCritical] get; }

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004FE1 RID: 20449
		uint UsagePatterns { [SecurityCritical] get; }

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004FE2 RID: 20450
		IDefinitionIdentity CdfIdentity { [SecurityCritical] get; }

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004FE3 RID: 20451
		string LocalPath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004FE4 RID: 20452
		uint HashAlgorithm { [SecurityCritical] get; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004FE5 RID: 20453
		object ManifestHash { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06004FE6 RID: 20454
		string ContentType { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004FE7 RID: 20455
		string RuntimeImageVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06004FE8 RID: 20456
		object MvidValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06004FE9 RID: 20457
		IDescriptionMetadataEntry DescriptionData { [SecurityCritical] get; }

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06004FEA RID: 20458
		IDeploymentMetadataEntry DeploymentData { [SecurityCritical] get; }

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06004FEB RID: 20459
		IDependentOSMetadataEntry DependentOSData { [SecurityCritical] get; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004FEC RID: 20460
		string defaultPermissionSetID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004FED RID: 20461
		string RequestedExecutionLevel { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004FEE RID: 20462
		bool RequestedExecutionLevelUIAccess { [SecurityCritical] get; }

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004FEF RID: 20463
		IReferenceIdentity ResourceTypeResourcesDependency { [SecurityCritical] get; }

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004FF0 RID: 20464
		IReferenceIdentity ResourceTypeManifestResourcesDependency { [SecurityCritical] get; }

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06004FF1 RID: 20465
		string KeyInfoElement { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004FF2 RID: 20466
		ICompatibleFrameworksMetadataEntry CompatibleFrameworksData { [SecurityCritical] get; }
	}
}
