using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000740 RID: 1856
	[Serializable]
	internal enum InternalParseTypeE
	{
		// Token: 0x04002459 RID: 9305
		Empty,
		// Token: 0x0400245A RID: 9306
		SerializedStreamHeader,
		// Token: 0x0400245B RID: 9307
		Object,
		// Token: 0x0400245C RID: 9308
		Member,
		// Token: 0x0400245D RID: 9309
		ObjectEnd,
		// Token: 0x0400245E RID: 9310
		MemberEnd,
		// Token: 0x0400245F RID: 9311
		Headers,
		// Token: 0x04002460 RID: 9312
		HeadersEnd,
		// Token: 0x04002461 RID: 9313
		SerializedStreamHeaderEnd,
		// Token: 0x04002462 RID: 9314
		Envelope,
		// Token: 0x04002463 RID: 9315
		EnvelopeEnd,
		// Token: 0x04002464 RID: 9316
		Body,
		// Token: 0x04002465 RID: 9317
		BodyEnd
	}
}
