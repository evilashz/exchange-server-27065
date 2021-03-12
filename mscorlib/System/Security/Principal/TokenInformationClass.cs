using System;

namespace System.Security.Principal
{
	// Token: 0x02000303 RID: 771
	[Serializable]
	internal enum TokenInformationClass
	{
		// Token: 0x04000F9D RID: 3997
		TokenUser = 1,
		// Token: 0x04000F9E RID: 3998
		TokenGroups,
		// Token: 0x04000F9F RID: 3999
		TokenPrivileges,
		// Token: 0x04000FA0 RID: 4000
		TokenOwner,
		// Token: 0x04000FA1 RID: 4001
		TokenPrimaryGroup,
		// Token: 0x04000FA2 RID: 4002
		TokenDefaultDacl,
		// Token: 0x04000FA3 RID: 4003
		TokenSource,
		// Token: 0x04000FA4 RID: 4004
		TokenType,
		// Token: 0x04000FA5 RID: 4005
		TokenImpersonationLevel,
		// Token: 0x04000FA6 RID: 4006
		TokenStatistics,
		// Token: 0x04000FA7 RID: 4007
		TokenRestrictedSids,
		// Token: 0x04000FA8 RID: 4008
		TokenSessionId,
		// Token: 0x04000FA9 RID: 4009
		TokenGroupsAndPrivileges,
		// Token: 0x04000FAA RID: 4010
		TokenSessionReference,
		// Token: 0x04000FAB RID: 4011
		TokenSandBoxInert,
		// Token: 0x04000FAC RID: 4012
		TokenAuditPolicy,
		// Token: 0x04000FAD RID: 4013
		TokenOrigin,
		// Token: 0x04000FAE RID: 4014
		TokenElevationType,
		// Token: 0x04000FAF RID: 4015
		TokenLinkedToken,
		// Token: 0x04000FB0 RID: 4016
		TokenElevation,
		// Token: 0x04000FB1 RID: 4017
		TokenHasRestrictions,
		// Token: 0x04000FB2 RID: 4018
		TokenAccessInformation,
		// Token: 0x04000FB3 RID: 4019
		TokenVirtualizationAllowed,
		// Token: 0x04000FB4 RID: 4020
		TokenVirtualizationEnabled,
		// Token: 0x04000FB5 RID: 4021
		TokenIntegrityLevel,
		// Token: 0x04000FB6 RID: 4022
		TokenUIAccess,
		// Token: 0x04000FB7 RID: 4023
		TokenMandatoryPolicy,
		// Token: 0x04000FB8 RID: 4024
		TokenLogonSid,
		// Token: 0x04000FB9 RID: 4025
		TokenIsAppContainer,
		// Token: 0x04000FBA RID: 4026
		TokenCapabilities,
		// Token: 0x04000FBB RID: 4027
		TokenAppContainerSid,
		// Token: 0x04000FBC RID: 4028
		TokenAppContainerNumber,
		// Token: 0x04000FBD RID: 4029
		TokenUserClaimAttributes,
		// Token: 0x04000FBE RID: 4030
		TokenDeviceClaimAttributes,
		// Token: 0x04000FBF RID: 4031
		TokenRestrictedUserClaimAttributes,
		// Token: 0x04000FC0 RID: 4032
		TokenRestrictedDeviceClaimAttributes,
		// Token: 0x04000FC1 RID: 4033
		TokenDeviceGroups,
		// Token: 0x04000FC2 RID: 4034
		TokenRestrictedDeviceGroups,
		// Token: 0x04000FC3 RID: 4035
		MaxTokenInfoClass
	}
}
