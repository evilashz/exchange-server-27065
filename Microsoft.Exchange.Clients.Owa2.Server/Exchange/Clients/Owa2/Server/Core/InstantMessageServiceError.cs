using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000148 RID: 328
	public enum InstantMessageServiceError
	{
		// Token: 0x04000784 RID: 1924
		None,
		// Token: 0x04000785 RID: 1925
		SipEndpointConnectionFailure,
		// Token: 0x04000786 RID: 1926
		SipEndpointOperationTimeout,
		// Token: 0x04000787 RID: 1927
		SipEndpointRegister,
		// Token: 0x04000788 RID: 1928
		SipEndpointFailureResponse,
		// Token: 0x04000789 RID: 1929
		AddressNotAvailable = 2000,
		// Token: 0x0400078A RID: 1930
		ExternalAuthenticationDisabled,
		// Token: 0x0400078B RID: 1931
		ExternalIdentityUnknown,
		// Token: 0x0400078C RID: 1932
		OverMaxPayloadSize = 2004,
		// Token: 0x0400078D RID: 1933
		CreateEndpointFailure,
		// Token: 0x0400078E RID: 1934
		ServerTimeout = 3000,
		// Token: 0x0400078F RID: 1935
		SignInFailure,
		// Token: 0x04000790 RID: 1936
		SignInDisconnected,
		// Token: 0x04000791 RID: 1937
		SessionDisconnected,
		// Token: 0x04000792 RID: 1938
		SessionSignOut,
		// Token: 0x04000793 RID: 1939
		ServiceShutdown,
		// Token: 0x04000794 RID: 1940
		ReInitializeOwa,
		// Token: 0x04000795 RID: 1941
		PrivacyMigrationInProgress = 3008,
		// Token: 0x04000796 RID: 1942
		PrivacyMigrationNeeded,
		// Token: 0x04000797 RID: 1943
		PrivacyPolicyChanged,
		// Token: 0x04000798 RID: 1944
		ClientSignOut = 4000,
		// Token: 0x04000799 RID: 1945
		ClientLimit,
		// Token: 0x0400079A RID: 1946
		BeginSignInError = 5000,
		// Token: 0x0400079B RID: 1947
		SignInError
	}
}
