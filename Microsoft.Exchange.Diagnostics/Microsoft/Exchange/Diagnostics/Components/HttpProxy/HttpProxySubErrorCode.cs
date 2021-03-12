using System;

namespace Microsoft.Exchange.Diagnostics.Components.HttpProxy
{
	// Token: 0x02000133 RID: 307
	internal enum HttpProxySubErrorCode
	{
		// Token: 0x040005D8 RID: 1496
		DirectoryOperationError = 2001,
		// Token: 0x040005D9 RID: 1497
		MServOperationError,
		// Token: 0x040005DA RID: 1498
		ServerDiscoveryError,
		// Token: 0x040005DB RID: 1499
		ServerLocatorError,
		// Token: 0x040005DC RID: 1500
		TooManyOutstandingProxyRequests = 2011,
		// Token: 0x040005DD RID: 1501
		TooManyOutstandingAuthenticationRequests,
		// Token: 0x040005DE RID: 1502
		RpcHttpConnectionEstablishmentTimeout,
		// Token: 0x040005DF RID: 1503
		BackEndRequestTimedOut,
		// Token: 0x040005E0 RID: 1504
		CacheDeserializationError,
		// Token: 0x040005E1 RID: 1505
		ServerKerberosAuthenticationFailure,
		// Token: 0x040005E2 RID: 1506
		TooManyOutstandingProxyRequestsToForest = 2016,
		// Token: 0x040005E3 RID: 1507
		TooManyOutstandingProxyRequestsToDag,
		// Token: 0x040005E4 RID: 1508
		EndpointNotFound = 3001,
		// Token: 0x040005E5 RID: 1509
		UserNotFound,
		// Token: 0x040005E6 RID: 1510
		MailboxGuidWithDomainNotFound,
		// Token: 0x040005E7 RID: 1511
		DatabaseNameNotFound,
		// Token: 0x040005E8 RID: 1512
		DatabaseGuidNotFound,
		// Token: 0x040005E9 RID: 1513
		OrganizationMailboxNotFound,
		// Token: 0x040005EA RID: 1514
		ServerNotFound,
		// Token: 0x040005EB RID: 1515
		ServerVersionNotFound,
		// Token: 0x040005EC RID: 1516
		DomainNotFound,
		// Token: 0x040005ED RID: 1517
		MailboxExternalDirectoryObjectIdNotFound,
		// Token: 0x040005EE RID: 1518
		UnauthenticatedRequest = 4001,
		// Token: 0x040005EF RID: 1519
		BadSamlToken,
		// Token: 0x040005F0 RID: 1520
		ClientDisconnect,
		// Token: 0x040005F1 RID: 1521
		InvalidOAuthToken,
		// Token: 0x040005F2 RID: 1522
		CannotReplayRequest = 5001
	}
}
