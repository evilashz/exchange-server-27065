using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000E1 RID: 225
	public enum OAuthOutboundErrorCodes
	{
		// Token: 0x0400072C RID: 1836
		[Description("No Error")]
		NoError,
		// Token: 0x0400072D RID: 1837
		[Description("Unable to load OAuth configuration.")]
		OAuthConfigurationUnavailable,
		// Token: 0x0400072E RID: 1838
		[Description("Missing signing certificate.")]
		MissingSigningCertificate,
		// Token: 0x0400072F RID: 1839
		[Description("The signing certificate should have a private key with at least 2048 bits.")]
		CertificatePrivateKeySizeTooSmall,
		// Token: 0x04000730 RID: 1840
		[Description("Unable to get token from Auth Server. Error code: '{0}'. Description: '{1}'.")]
		UnableToGetTokenFromACS,
		// Token: 0x04000731 RID: 1841
		[Description("The client ID inside the challenge returned from '{0}' was empty.")]
		EmptyClientId,
		// Token: 0x04000732 RID: 1842
		[Description("The challenge value returned from '{0}' is not valid.")]
		InvalidChallenge,
		// Token: 0x04000733 RID: 1843
		[Description("Multiple Auth Servers have an empty realm configured.")]
		InvalidConfigurationMultipleAuthServerWithEmptyRealm,
		// Token: 0x04000734 RID: 1844
		[Description("Multiple Auth Servers have same realm '{0}'.")]
		InvalidConfigurationMultipleAuthServerWithSameRealm,
		// Token: 0x04000735 RID: 1845
		[Description("The UserPrincipalName property was missing on the mailbox object and couldn't be added to the user context in the token.")]
		UPNValueNotProvided,
		// Token: 0x04000736 RID: 1846
		[Description("The trusted issuers contained the following entries '{0}'. None of them are configured locally.")]
		NoMatchedTokenIssuer,
		// Token: 0x04000737 RID: 1847
		[Description("The matched Auth Server '{0}' has an empty realm.")]
		MissingRealmInAuthServer,
		// Token: 0x04000738 RID: 1848
		[Description("Unable to get at least one valid claim-value for the user to build the user context.")]
		EmptyClaimsForUser,
		// Token: 0x04000739 RID: 1849
		[Description("The specified url may not support OAuth.")]
		InvalidOAuthEndpoint,
		// Token: 0x0400073A RID: 1850
		[Description("The claim nameidClaim is empty.")]
		EmptyNameIdClaim
	}
}
