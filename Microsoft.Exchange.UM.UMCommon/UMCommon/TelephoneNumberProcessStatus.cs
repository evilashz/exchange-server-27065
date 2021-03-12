using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000037 RID: 55
	internal enum TelephoneNumberProcessStatus
	{
		// Token: 0x040000DC RID: 220
		Success,
		// Token: 0x040000DD RID: 221
		DialPlanNotSupported,
		// Token: 0x040000DE RID: 222
		PhoneNumberAlreadyRegistered,
		// Token: 0x040000DF RID: 223
		PhoneNumberReachQuota,
		// Token: 0x040000E0 RID: 224
		PhoneNumberUsedByOthers,
		// Token: 0x040000E1 RID: 225
		PhoneNumberInvalidFormat,
		// Token: 0x040000E2 RID: 226
		PhoneNumberInvalidCountryCode,
		// Token: 0x040000E3 RID: 227
		PhoneNumberInvalidLength,
		// Token: 0x040000E4 RID: 228
		Failure
	}
}
