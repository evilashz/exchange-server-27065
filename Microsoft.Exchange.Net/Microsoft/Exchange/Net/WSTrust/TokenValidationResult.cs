using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B74 RID: 2932
	internal enum TokenValidationResult
	{
		// Token: 0x040036AF RID: 13999
		Valid,
		// Token: 0x040036B0 RID: 14000
		InvalidUnknownExternalIdentity,
		// Token: 0x040036B1 RID: 14001
		InvalidUnknownEncryption,
		// Token: 0x040036B2 RID: 14002
		InvalidTokenFailedValidation,
		// Token: 0x040036B3 RID: 14003
		InvalidTokenFormat,
		// Token: 0x040036B4 RID: 14004
		InvalidTrustBroker,
		// Token: 0x040036B5 RID: 14005
		InvalidTarget,
		// Token: 0x040036B6 RID: 14006
		InvalidOffer,
		// Token: 0x040036B7 RID: 14007
		InvalidUnknownEmailAddress,
		// Token: 0x040036B8 RID: 14008
		InvalidExpired
	}
}
