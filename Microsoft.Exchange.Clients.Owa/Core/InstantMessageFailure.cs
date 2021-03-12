using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000137 RID: 311
	public enum InstantMessageFailure
	{
		// Token: 0x0400078F RID: 1935
		None,
		// Token: 0x04000790 RID: 1936
		SipEndpointConnectionFailure,
		// Token: 0x04000791 RID: 1937
		SipEndpointOperationTimeout,
		// Token: 0x04000792 RID: 1938
		SipEndpointRegister,
		// Token: 0x04000793 RID: 1939
		SipEndpointFailureResponse,
		// Token: 0x04000794 RID: 1940
		AddressNotAvailable = 2000,
		// Token: 0x04000795 RID: 1941
		ExternalAuthenticationDisabled,
		// Token: 0x04000796 RID: 1942
		ExternalIdentityUnknown,
		// Token: 0x04000797 RID: 1943
		TermsOfUseNotSigned,
		// Token: 0x04000798 RID: 1944
		OverMaxPayloadSize,
		// Token: 0x04000799 RID: 1945
		CreateEndpointFailure,
		// Token: 0x0400079A RID: 1946
		ServerTimeout = 3000,
		// Token: 0x0400079B RID: 1947
		SignInFailure,
		// Token: 0x0400079C RID: 1948
		SignInDisconnected,
		// Token: 0x0400079D RID: 1949
		SessionDisconnected,
		// Token: 0x0400079E RID: 1950
		SessionSignOut,
		// Token: 0x0400079F RID: 1951
		ServiceShutdown,
		// Token: 0x040007A0 RID: 1952
		ReInitializeOwa,
		// Token: 0x040007A1 RID: 1953
		PrivacyMigrationInProgress = 3008,
		// Token: 0x040007A2 RID: 1954
		PrivacyMigrationNeeded,
		// Token: 0x040007A3 RID: 1955
		PrivacyPolicyChanged,
		// Token: 0x040007A4 RID: 1956
		ClientSignOut = 4000,
		// Token: 0x040007A5 RID: 1957
		ClientLimit,
		// Token: 0x040007A6 RID: 1958
		BeginSignInError = 5000,
		// Token: 0x040007A7 RID: 1959
		SignInError
	}
}
