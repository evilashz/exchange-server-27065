using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000583 RID: 1411
	[Flags]
	public enum ServerRole
	{
		// Token: 0x04002C95 RID: 11413
		[LocDescription(DirectoryStrings.IDs.ServerRoleNone)]
		None = 0,
		// Token: 0x04002C96 RID: 11414
		[LocDescription(DirectoryStrings.IDs.ServerRoleCafe)]
		Cafe = 1,
		// Token: 0x04002C97 RID: 11415
		[LocDescription(DirectoryStrings.IDs.ServerRoleMailbox)]
		Mailbox = 2,
		// Token: 0x04002C98 RID: 11416
		[LocDescription(DirectoryStrings.IDs.ServerRoleClientAccess)]
		ClientAccess = 4,
		// Token: 0x04002C99 RID: 11417
		[LocDescription(DirectoryStrings.IDs.ServerRoleUnifiedMessaging)]
		UnifiedMessaging = 16,
		// Token: 0x04002C9A RID: 11418
		[LocDescription(DirectoryStrings.IDs.ServerRoleHubTransport)]
		HubTransport = 32,
		// Token: 0x04002C9B RID: 11419
		[LocDescription(DirectoryStrings.IDs.ServerRoleEdge)]
		Edge = 64,
		// Token: 0x04002C9C RID: 11420
		[LocDescription(DirectoryStrings.IDs.ServerRoleAll)]
		All = 16503,
		// Token: 0x04002C9D RID: 11421
		[LocDescription(DirectoryStrings.IDs.ServerRoleMonitoring)]
		Monitoring = 128,
		// Token: 0x04002C9E RID: 11422
		[LocDescription(DirectoryStrings.IDs.ServerRoleExtendedRole2)]
		CentralAdmin = 256,
		// Token: 0x04002C9F RID: 11423
		[LocDescription(DirectoryStrings.IDs.ServerRoleExtendedRole3)]
		CentralAdminDatabase = 512,
		// Token: 0x04002CA0 RID: 11424
		[LocDescription(DirectoryStrings.IDs.ServerRoleExtendedRole4)]
		DomainController = 1024,
		// Token: 0x04002CA1 RID: 11425
		[LocDescription(DirectoryStrings.IDs.ServerRoleExtendedRole5)]
		WindowsDeploymentServer = 2048,
		// Token: 0x04002CA2 RID: 11426
		[LocDescription(DirectoryStrings.IDs.ServerRoleProvisionedServer)]
		ProvisionedServer = 4096,
		// Token: 0x04002CA3 RID: 11427
		[LocDescription(DirectoryStrings.IDs.ServerRoleLanguagePacks)]
		LanguagePacks = 8192,
		// Token: 0x04002CA4 RID: 11428
		[LocDescription(DirectoryStrings.IDs.ServerRoleFrontendTransport)]
		FrontendTransport = 16384,
		// Token: 0x04002CA5 RID: 11429
		[LocDescription(DirectoryStrings.IDs.ServerRoleCafeArray)]
		CafeArray = 32768,
		// Token: 0x04002CA6 RID: 11430
		[LocDescription(DirectoryStrings.IDs.ServerRoleFfoWebServices)]
		FfoWebService = 65536,
		// Token: 0x04002CA7 RID: 11431
		[LocDescription(DirectoryStrings.IDs.ServerRoleOSP)]
		OSP = 131072,
		// Token: 0x04002CA8 RID: 11432
		[LocDescription(DirectoryStrings.IDs.ServerRoleExtendedRole7)]
		ARR = 262144,
		// Token: 0x04002CA9 RID: 11433
		[LocDescription(DirectoryStrings.IDs.ServerRoleManagementFrontEnd)]
		ManagementFrontEnd = 524288,
		// Token: 0x04002CAA RID: 11434
		[LocDescription(DirectoryStrings.IDs.ServerRoleManagementBackEnd)]
		ManagementBackEnd = 1048576,
		// Token: 0x04002CAB RID: 11435
		[LocDescription(DirectoryStrings.IDs.ServerRoleSCOM)]
		SCOM = 2097152,
		// Token: 0x04002CAC RID: 11436
		[LocDescription(DirectoryStrings.IDs.ServerRoleCentralAdminFrontEnd)]
		CentralAdminFrontEnd = 4194304,
		// Token: 0x04002CAD RID: 11437
		[LocDescription(DirectoryStrings.IDs.ServerRoleNAT)]
		NAT = 8388608,
		// Token: 0x04002CAE RID: 11438
		[LocDescription(DirectoryStrings.IDs.ServerRoleDHCP)]
		DHCP = 16777216
	}
}
