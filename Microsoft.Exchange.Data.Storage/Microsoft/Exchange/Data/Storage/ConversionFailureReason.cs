using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071E RID: 1822
	internal enum ConversionFailureReason
	{
		// Token: 0x04002727 RID: 10023
		ExceedsLimit = 1,
		// Token: 0x04002728 RID: 10024
		MaliciousContent,
		// Token: 0x04002729 RID: 10025
		CorruptContent,
		// Token: 0x0400272A RID: 10026
		ConverterInternalFailure,
		// Token: 0x0400272B RID: 10027
		ConverterUnsupportedContent
	}
}
