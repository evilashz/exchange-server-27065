using System;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200006D RID: 109
	internal enum AuthStatus
	{
		// Token: 0x040003AA RID: 938
		LogonSuccess = 1,
		// Token: 0x040003AB RID: 939
		LogonFailed = 0,
		// Token: 0x040003AC RID: 940
		Redirect = -1,
		// Token: 0x040003AD RID: 941
		LiveIDFailed = -2,
		// Token: 0x040003AE RID: 942
		FederatedStsFailed = -3,
		// Token: 0x040003AF RID: 943
		RecoverableLogonFailed = -4,
		// Token: 0x040003B0 RID: 944
		ExpiredCredentials = -5,
		// Token: 0x040003B1 RID: 945
		RepeatedLogonFailure = -6,
		// Token: 0x040003B2 RID: 946
		RepeatedLiveIDFailure = -7,
		// Token: 0x040003B3 RID: 947
		RepeatedFederatedStsFailure = -8,
		// Token: 0x040003B4 RID: 948
		RepeatedRecoverableFailure = -9,
		// Token: 0x040003B5 RID: 949
		RepeatedExpiredCredentials = -10,
		// Token: 0x040003B6 RID: 950
		LowConfidence = -11,
		// Token: 0x040003B7 RID: 951
		BadPassword = -12,
		// Token: 0x040003B8 RID: 952
		RepeatedBadPassword = -13,
		// Token: 0x040003B9 RID: 953
		S4ULogonFailed = -14,
		// Token: 0x040003BA RID: 954
		HRDFailed = -15,
		// Token: 0x040003BB RID: 955
		OffineOrgIdAuthFailed = -16,
		// Token: 0x040003BC RID: 956
		AmbigiousMailboxFound = -17,
		// Token: 0x040003BD RID: 957
		UnableToOpenTicket = -18,
		// Token: 0x040003BE RID: 958
		PuidMismatch = -19,
		// Token: 0x040003BF RID: 959
		PuidNotFound = -20,
		// Token: 0x040003C0 RID: 960
		OfflineHrdFailed = -21,
		// Token: 0x040003C1 RID: 961
		AppPasswordRequired = -22,
		// Token: 0x040003C2 RID: 962
		FederatedStsUrlNotEncrypted = -23,
		// Token: 0x040003C3 RID: 963
		ADFSRulesDenied = -24,
		// Token: 0x040003C4 RID: 964
		RepeatedADFSRulesDenied = -25,
		// Token: 0x040003C5 RID: 965
		AccountNotProvisioned = -26,
		// Token: 0x040003C6 RID: 966
		InternalServerError = -27,
		// Token: 0x040003C7 RID: 967
		Forbidden = -28,
		// Token: 0x040003C8 RID: 968
		UnfamiliarLocation = -29,
		// Token: 0x040003C9 RID: 969
		MaxValue = -29
	}
}
