using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007BA RID: 1978
	internal enum FailureReason
	{
		// Token: 0x04002405 RID: 9221
		Unknown,
		// Token: 0x04002406 RID: 9222
		CancelationRequested,
		// Token: 0x04002407 RID: 9223
		NameResolution,
		// Token: 0x04002408 RID: 9224
		NetworkConnection,
		// Token: 0x04002409 RID: 9225
		ServerUnreachable,
		// Token: 0x0400240A RID: 9226
		RequestTimeout,
		// Token: 0x0400240B RID: 9227
		ConnectionTimeout,
		// Token: 0x0400240C RID: 9228
		UnexpectedHttpResponseCode,
		// Token: 0x0400240D RID: 9229
		MissingKeyword,
		// Token: 0x0400240E RID: 9230
		ScenarioTimeout,
		// Token: 0x0400240F RID: 9231
		SslNegotiation,
		// Token: 0x04002410 RID: 9232
		BrokenAffinity,
		// Token: 0x04002411 RID: 9233
		PassiveDatabase,
		// Token: 0x04002412 RID: 9234
		BadUserNameOrPassword,
		// Token: 0x04002413 RID: 9235
		AccountLocked,
		// Token: 0x04002414 RID: 9236
		LogonError,
		// Token: 0x04002415 RID: 9237
		CafeFailure,
		// Token: 0x04002416 RID: 9238
		CafeActiveDirectoryFailure,
		// Token: 0x04002417 RID: 9239
		CafeHighAvailabilityFailure,
		// Token: 0x04002418 RID: 9240
		CafeToMailboxNetworkingFailure,
		// Token: 0x04002419 RID: 9241
		CafeTimeoutContactingBackend,
		// Token: 0x0400241A RID: 9242
		OwaMailboxErrorPage,
		// Token: 0x0400241B RID: 9243
		OwaActiveDirectoryErrorPage,
		// Token: 0x0400241C RID: 9244
		OwaMServErrorPage,
		// Token: 0x0400241D RID: 9245
		OwaErrorPage,
		// Token: 0x0400241E RID: 9246
		OwaFindPlacesError,
		// Token: 0x0400241F RID: 9247
		EcpMailboxErrorResponse,
		// Token: 0x04002420 RID: 9248
		EcpActiveDirectoryErrorResponse,
		// Token: 0x04002421 RID: 9249
		EcpErrorPage,
		// Token: 0x04002422 RID: 9250
		EcpJsonErrorResponse,
		// Token: 0x04002423 RID: 9251
		EcpJsonResultErrorResponse,
		// Token: 0x04002424 RID: 9252
		RwsDataMartErrorResponse,
		// Token: 0x04002425 RID: 9253
		RwsActiveDirectoryErrorResponse,
		// Token: 0x04002426 RID: 9254
		RwsError
	}
}
