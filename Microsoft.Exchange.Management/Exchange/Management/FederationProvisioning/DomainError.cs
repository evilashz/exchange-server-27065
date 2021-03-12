using System;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x0200032F RID: 815
	internal enum DomainError
	{
		// Token: 0x04000BEC RID: 3052
		InvalidPartner = 1001,
		// Token: 0x04000BED RID: 3053
		InvalidPartnerCert,
		// Token: 0x04000BEE RID: 3054
		PartnerNotAuthorized,
		// Token: 0x04000BEF RID: 3055
		MemberNotAuthorized,
		// Token: 0x04000BF0 RID: 3056
		InvalidManagementCertificate,
		// Token: 0x04000BF1 RID: 3057
		MemberNotAuthenticated,
		// Token: 0x04000BF2 RID: 3058
		InvalidDomainName = 2001,
		// Token: 0x04000BF3 RID: 3059
		BlockedDomainName,
		// Token: 0x04000BF4 RID: 3060
		InvalidDomainConfigId,
		// Token: 0x04000BF5 RID: 3061
		DomainNotReserved,
		// Token: 0x04000BF6 RID: 3062
		DomainUnavailable,
		// Token: 0x04000BF7 RID: 3063
		DomainPendingChanges,
		// Token: 0x04000BF8 RID: 3064
		DomainSuspended,
		// Token: 0x04000BF9 RID: 3065
		DomainPendingConfiguration,
		// Token: 0x04000BFA RID: 3066
		NotPermittedForDomain,
		// Token: 0x04000BFB RID: 3067
		InvalidProgramId,
		// Token: 0x04000BFC RID: 3068
		ProofOfOwnershipNotValid,
		// Token: 0x04000BFD RID: 3069
		PasswordChangeRequiredForDomain,
		// Token: 0x04000BFE RID: 3070
		CanNotReleaseNamespace,
		// Token: 0x04000BFF RID: 3071
		DomainNotFederated,
		// Token: 0x04000C00 RID: 3072
		MemberNameInvalid = 3001,
		// Token: 0x04000C01 RID: 3073
		MemberNameBlocked,
		// Token: 0x04000C02 RID: 3074
		MemberNameUnavailable,
		// Token: 0x04000C03 RID: 3075
		MemberNameBlank,
		// Token: 0x04000C04 RID: 3076
		MemberNameIncludesInvalidChars,
		// Token: 0x04000C05 RID: 3077
		MemberNameIncludesDots,
		// Token: 0x04000C06 RID: 3078
		MemberNameInUse,
		// Token: 0x04000C07 RID: 3079
		ManagedMemberExists,
		// Token: 0x04000C08 RID: 3080
		ManagedMemberNotExists,
		// Token: 0x04000C09 RID: 3081
		UnmanagedMemberExists,
		// Token: 0x04000C0A RID: 3082
		UnmanagedMemberNotExists,
		// Token: 0x04000C0B RID: 3083
		MaxMembershipLimit,
		// Token: 0x04000C0C RID: 3084
		PasswordBlank,
		// Token: 0x04000C0D RID: 3085
		PasswordTooShort,
		// Token: 0x04000C0E RID: 3086
		PasswordTooLong,
		// Token: 0x04000C0F RID: 3087
		PasswordIncludesMemberName,
		// Token: 0x04000C10 RID: 3088
		PasswordIncludesInvalidChars,
		// Token: 0x04000C11 RID: 3089
		PasswordInvalid,
		// Token: 0x04000C12 RID: 3090
		InvalidNetId,
		// Token: 0x04000C13 RID: 3091
		InvalidOffer,
		// Token: 0x04000C14 RID: 3092
		InvalidAppId = 4001,
		// Token: 0x04000C15 RID: 3093
		InvalidAppIdAdminKey,
		// Token: 0x04000C16 RID: 3094
		MaxAppIdsReached,
		// Token: 0x04000C17 RID: 3095
		MaxUriReached,
		// Token: 0x04000C18 RID: 3096
		InvalidUri,
		// Token: 0x04000C19 RID: 3097
		InternalError = 9001,
		// Token: 0x04000C1A RID: 3098
		InvalidParameter,
		// Token: 0x04000C1B RID: 3099
		PassportError,
		// Token: 0x04000C1C RID: 3100
		ExchangeError,
		// Token: 0x04000C1D RID: 3101
		SubscriptionServicesError,
		// Token: 0x04000C1E RID: 3102
		TestForcedError,
		// Token: 0x04000C1F RID: 3103
		ServiceDown,
		// Token: 0x04000C20 RID: 3104
		NYI = 10001
	}
}
