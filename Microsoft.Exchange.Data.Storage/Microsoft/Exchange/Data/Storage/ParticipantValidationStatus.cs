using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001D6 RID: 470
	public enum ParticipantValidationStatus
	{
		// Token: 0x04000D0E RID: 3342
		NoError,
		// Token: 0x04000D0F RID: 3343
		AddressAndRoutingTypeMismatch = 16,
		// Token: 0x04000D10 RID: 3344
		AddressRequiredForRoutingType = 32,
		// Token: 0x04000D11 RID: 3345
		DisplayNameRequiredForRoutingType = 37,
		// Token: 0x04000D12 RID: 3346
		RoutingTypeRequired = 48,
		// Token: 0x04000D13 RID: 3347
		InvalidAddressFormat = 64,
		// Token: 0x04000D14 RID: 3348
		InvalidRoutingTypeFormat = 80,
		// Token: 0x04000D15 RID: 3349
		AddressAndOriginMismatch = 96,
		// Token: 0x04000D16 RID: 3350
		OperationNotSupportedForRoutingType = 112,
		// Token: 0x04000D17 RID: 3351
		DisplayNameTooBig = 128,
		// Token: 0x04000D18 RID: 3352
		EmailAddressTooBig = 144,
		// Token: 0x04000D19 RID: 3353
		RoutingTypeTooBig = 160
	}
}
