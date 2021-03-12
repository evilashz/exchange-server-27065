using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C7 RID: 967
	[Flags]
	internal enum AnomaliesFlags : long
	{
		// Token: 0x0400186B RID: 6251
		None = 0L,
		// Token: 0x0400186C RID: 6252
		MultipleExceptionsWithSameDate = 1L,
		// Token: 0x0400186D RID: 6253
		MismatchedOriginalDateFromExceptionList = 2L,
		// Token: 0x0400186E RID: 6254
		ExtraExceptionNotInPattern = 4L,
		// Token: 0x0400186F RID: 6255
		NumOccurIsZeroSoTreatAsNoEndRange = 8L
	}
}
