using System;

namespace Microsoft.Exchange.Diagnostics.Components.Security
{
	// Token: 0x02000135 RID: 309
	public enum LiveIdAuthResult
	{
		// Token: 0x040005F7 RID: 1527
		Success,
		// Token: 0x040005F8 RID: 1528
		UserNotFoundInAD,
		// Token: 0x040005F9 RID: 1529
		LiveServerUnreachable,
		// Token: 0x040005FA RID: 1530
		FederatedStsUnreachable,
		// Token: 0x040005FB RID: 1531
		OperationTimedOut,
		// Token: 0x040005FC RID: 1532
		CommunicationFailure,
		// Token: 0x040005FD RID: 1533
		AuthFailure,
		// Token: 0x040005FE RID: 1534
		ExpiredCreds,
		// Token: 0x040005FF RID: 1535
		InvalidCreds,
		// Token: 0x04000600 RID: 1536
		RecoverableAuthFailure,
		// Token: 0x04000601 RID: 1537
		S4ULogonFailure,
		// Token: 0x04000602 RID: 1538
		HRDFailure,
		// Token: 0x04000603 RID: 1539
		OfflineOrgIdAuthFailure,
		// Token: 0x04000604 RID: 1540
		AmbigiousMailboxFoundFailure,
		// Token: 0x04000605 RID: 1541
		UnableToOpenTicketFailure,
		// Token: 0x04000606 RID: 1542
		PuidMismatchFailure,
		// Token: 0x04000607 RID: 1543
		InvalidUsername,
		// Token: 0x04000608 RID: 1544
		FaultException,
		// Token: 0x04000609 RID: 1545
		LowPasswordConfidence,
		// Token: 0x0400060A RID: 1546
		OfflineHrdFailed,
		// Token: 0x0400060B RID: 1547
		AppPasswordRequired,
		// Token: 0x0400060C RID: 1548
		FederatedStsUrlNotEncrypted,
		// Token: 0x0400060D RID: 1549
		FederatedStsADFSRulesDenied,
		// Token: 0x0400060E RID: 1550
		InternalServerError,
		// Token: 0x0400060F RID: 1551
		AccountNotProvisioned,
		// Token: 0x04000610 RID: 1552
		Forbidden,
		// Token: 0x04000611 RID: 1553
		UnfamiliarLocation
	}
}
