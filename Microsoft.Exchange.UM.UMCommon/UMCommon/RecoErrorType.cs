using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B1 RID: 177
	internal enum RecoErrorType
	{
		// Token: 0x04000397 RID: 919
		Success,
		// Token: 0x04000398 RID: 920
		AudioQualityPoor,
		// Token: 0x04000399 RID: 921
		LanguageNotSupported,
		// Token: 0x0400039A RID: 922
		Rejected,
		// Token: 0x0400039B RID: 923
		BadRequest,
		// Token: 0x0400039C RID: 924
		SystemError,
		// Token: 0x0400039D RID: 925
		Timeout,
		// Token: 0x0400039E RID: 926
		MessageTooLong,
		// Token: 0x0400039F RID: 927
		ProtectedVoiceMail,
		// Token: 0x040003A0 RID: 928
		Throttled,
		// Token: 0x040003A1 RID: 929
		Other,
		// Token: 0x040003A2 RID: 930
		ErrorReadingSettings
	}
}
