using System;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200007E RID: 126
	internal enum CredFailure
	{
		// Token: 0x040004E0 RID: 1248
		None,
		// Token: 0x040004E1 RID: 1249
		Invalid,
		// Token: 0x040004E2 RID: 1250
		Expired,
		// Token: 0x040004E3 RID: 1251
		LockedOut,
		// Token: 0x040004E4 RID: 1252
		LiveIdFailure,
		// Token: 0x040004E5 RID: 1253
		STSFailure,
		// Token: 0x040004E6 RID: 1254
		AppPasswordRequired,
		// Token: 0x040004E7 RID: 1255
		ADFSRulesDeny,
		// Token: 0x040004E8 RID: 1256
		AccountNotProvisioned,
		// Token: 0x040004E9 RID: 1257
		Forbidden,
		// Token: 0x040004EA RID: 1258
		UnfamiliarLocation,
		// Token: 0x040004EB RID: 1259
		NotSupportedProtocolForOutlookCom
	}
}
