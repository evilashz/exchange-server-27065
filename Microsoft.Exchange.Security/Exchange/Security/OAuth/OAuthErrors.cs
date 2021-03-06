using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D6 RID: 214
	internal enum OAuthErrors
	{
		// Token: 0x040006C2 RID: 1730
		[Description("No Error")]
		NoError,
		// Token: 0x040006C3 RID: 1731
		[Description("The token has an invalid signature.")]
		InvalidSignature = 1001,
		// Token: 0x040006C4 RID: 1732
		[Description("The user context must be unsigned.")]
		OuterTokenAlsoSigned = 2001,
		// Token: 0x040006C5 RID: 1733
		[Description("The inner actor token must be signed.")]
		ActorTokenMustBeSigned,
		// Token: 0x040006C6 RID: 1734
		[Description("The token is missing the issuer.")]
		MissingIssuer,
		// Token: 0x040006C7 RID: 1735
		[Description("The callback token is missing one or more expected claim types.")]
		CallbackClaimNotFound,
		// Token: 0x040006C8 RID: 1736
		[Description("The token is missing the claim type '{0}'.")]
		NoClaimFound,
		// Token: 0x040006C9 RID: 1737
		[Description("The issuer claim value is invalid '{0}'.")]
		InvalidIssuerFormat,
		// Token: 0x040006CA RID: 1738
		[Description("The outer token issuer claim value is invalid '{0}'")]
		InvalidOuterTokenIssuerFormat,
		// Token: 0x040006CB RID: 1739
		[Description("The issuer of the token is unknown. Issuer was '{0}'.")]
		NoConfiguredIssuerMatched,
		// Token: 0x040006CC RID: 1740
		[Description("The nameid claim value is invalid '{0}'.")]
		InvalidNameIdFormat,
		// Token: 0x040006CD RID: 1741
		[Description("The audience claim value is invalid '{0}'.")]
		InvalidAudience,
		// Token: 0x040006CE RID: 1742
		[Description("The audience contains a realm that is different than the issuer's realm.")]
		MismatchedRealmBetweenAudienceAndIssuer,
		// Token: 0x040006CF RID: 1743
		[Description("The callback token contains one or more invalid claim values.")]
		InvalidCallbackClaimValue,
		// Token: 0x040006D0 RID: 1744
		[Description("The appctx claim type in the token is invalid '{0}'.")]
		ExtensionInvalidAppCtxFormat,
		// Token: 0x040006D1 RID: 1745
		[Description("The callback token contains an invalid issuer '{0}'.")]
		InvalidCallbackTokenIssuer,
		// Token: 0x040006D2 RID: 1746
		[Description("The user-context issuer '{1}' does not match the application-context nameid '{0}'")]
		InvalidOuterTokenIssuerIdValue,
		// Token: 0x040006D3 RID: 1747
		[Description("The outer token issuer realm '{0}' is invalid.")]
		InvalidRealmFromOuterTokenIssuer,
		// Token: 0x040006D4 RID: 1748
		[Description("The realm in the nameid claim value is invalid.  Expected '{0}'. Actual '{1}'.")]
		UnexpectedRealmInNameId,
		// Token: 0x040006D5 RID: 1749
		[Description("No applicable user context claims found.")]
		NoUserClaimsFound,
		// Token: 0x040006D6 RID: 1750
		[Description("Invalid SID value '{0}' in primary SID claim type")]
		InvalidSidValue,
		// Token: 0x040006D7 RID: 1751
		[Description("This token profile is not applicable for the current protocol.")]
		TokenProfileNotApplicable,
		// Token: 0x040006D8 RID: 1752
		[Description("The token has invalid value '{0}' for the claim type '{1}'.")]
		InvalidClaimValueFound,
		// Token: 0x040006D9 RID: 1753
		[Description("Unable to read or process token, additional details: '{0}'.")]
		UnableToReadToken,
		// Token: 0x040006DA RID: 1754
		[Description("Token for app '{0}' does not have smtp or puid claim.")]
		NoSmtpOrPuidClaimFound,
		// Token: 0x040006DB RID: 1755
		[Description("The token with version '{0}' should have valid scope claim or linked account associated with partner application '{1}'.")]
		NoAuthorizationValuePresent,
		// Token: 0x040006DC RID: 1756
		[Description("The token has expired.")]
		TokenExpired = 3001,
		// Token: 0x040006DD RID: 1757
		[Description("The audience in the token does not specify a realm.")]
		EmptyRealmFromAudience = 4001,
		// Token: 0x040006DE RID: 1758
		[Description("The hostname component of the audience claim value is invalid. Expected '{0}'. Actual '{1}'.")]
		UnexpectedHostNameInAudience,
		// Token: 0x040006DF RID: 1759
		[Description("The audience of the token '{0}' doesn't match the endpoint '{1}' that received the request.")]
		WrongAudience,
		// Token: 0x040006E0 RID: 1760
		[Description("The tenant for context-id '{0}' does not exist.")]
		ExternalOrgIdNotFound = 5001,
		// Token: 0x040006E1 RID: 1761
		[Description("The tenant for realm '{0}' does not exist.")]
		OrganizationIdNotFoundFromRealm,
		// Token: 0x040006E2 RID: 1762
		[Description("The tid claim is missing.")]
		MissingTenantIdClaim,
		// Token: 0x040006E3 RID: 1763
		[Description("The tid claim should not be set for Consumer mailbox tokens.")]
		TenantIdClaimShouldNotBeSet,
		// Token: 0x040006E4 RID: 1764
		[Description("The user specified by the user-context in the token does not exist.")]
		NoUserFoundWithGivenClaims = 6001,
		// Token: 0x040006E5 RID: 1765
		[Description("The user specified by the user-context in the token is ambiguous.")]
		MoreThan1UserFoundWithGivenClaims,
		// Token: 0x040006E6 RID: 1766
		[Description("The MasterAccountSid doesn't match the SID claim.")]
		NameIdNotMatchMasterAccountSid,
		// Token: 0x040006E7 RID: 1767
		[Description("The user's mailbox version is not supported for access using OAuth.")]
		UserOAuthNotSupported,
		// Token: 0x040006E8 RID: 1768
		[Description("PUID in the nameid claim was not from BusinessLiveID")]
		NameIdNotMatchLiveIDInstanceType,
		// Token: 0x040006E9 RID: 1769
		[Description("test only: ExceptionDuringProxyDownLevelCheckNullSid")]
		TestOnlyExceptionDuringProxyDownLevelCheckNullSid,
		// Token: 0x040006EA RID: 1770
		[Description("test only: ExceptionDuringRehydration")]
		TestOnlyExceptionDuringRehydration,
		// Token: 0x040006EB RID: 1771
		[Description("test only: ExceptionDuringOAuthCATGeneration")]
		TestOnlyExceptionDuringOAuthCATGeneration,
		// Token: 0x040006EC RID: 1772
		[Description("The Application Identifier '{0}' is unknown.")]
		NoMatchingPartnerAppFound = 7001,
		// Token: 0x040006ED RID: 1773
		[Description("The application identifier in the nameid claim value is invalid.  Expected '{0}'. Actual '{1}'.")]
		UnexpectedAppIdInNameId,
		// Token: 0x040006EE RID: 1774
		[Description("The Active Directory operation failed.")]
		ADOperationFailed = 8001,
		// Token: 0x040006EF RID: 1775
		[Description("An unexpected error occurred.")]
		UnexpectedErrorOccurred,
		// Token: 0x040006F0 RID: 1776
		[Description("The callback token's protocol claim value '{0}' doesn't match the current requested protocol.")]
		InvalidCallbackTokenScope = 9001,
		// Token: 0x040006F1 RID: 1777
		[Description("The token contains no scope information, or scope can not be understood.")]
		NoGrantPresented = 9004,
		// Token: 0x040006F2 RID: 1778
		[Description("The call you are trying to access is not supported with OAuth token.")]
		NotSupportedWithV1AppToken,
		// Token: 0x040006F3 RID: 1779
		[Description("The token contains not enough scope to make this call.")]
		NotEnoughGrantPresented,
		// Token: 0x040006F4 RID: 1780
		[Description("The call should access the mailbox specified in the oauth token.")]
		AllowAccessOwnMailboxOnly,
		// Token: 0x040006F5 RID: 1781
		[Description("The PUID value was not found for [{0}] identity.")]
		NoPuidFound,
		// Token: 0x040006F6 RID: 1782
		[Description("The email address was not found for [{0}] identity, PUID={1}.")]
		NoEmailAddressFound,
		// Token: 0x040006F7 RID: 1783
		[Description("The certificate referenced by token with key {0} could not be located on server {1}.")]
		NoCertificateFound = 10001,
		// Token: 0x040006F8 RID: 1784
		[Description("Office Shared error codes")]
		OfficeSharedErrorCodes = 4000000,
		// Token: 0x040006F9 RID: 1785
		[Description("Flighting is not enabled for domain {0}.")]
		FlightingNotEnabled = 4001001
	}
}
