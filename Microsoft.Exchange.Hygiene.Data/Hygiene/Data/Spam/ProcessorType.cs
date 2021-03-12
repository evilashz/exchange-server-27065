using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E7 RID: 487
	public enum ProcessorType : byte
	{
		// Token: 0x04000A16 RID: 2582
		SpfCheck,
		// Token: 0x04000A17 RID: 2583
		SmartScreen,
		// Token: 0x04000A18 RID: 2584
		Keywords,
		// Token: 0x04000A19 RID: 2585
		RegEx,
		// Token: 0x04000A1A RID: 2586
		SenderIDCheck,
		// Token: 0x04000A1B RID: 2587
		BackscatterCheck,
		// Token: 0x04000A1C RID: 2588
		UriScan,
		// Token: 0x04000A1D RID: 2589
		DirectoryBasedCheck,
		// Token: 0x04000A1E RID: 2590
		SimilarityFingerprint,
		// Token: 0x04000A1F RID: 2591
		ContainmentFingerprint,
		// Token: 0x04000A20 RID: 2592
		RegexTextTarget,
		// Token: 0x04000A21 RID: 2593
		MXLookup,
		// Token: 0x04000A22 RID: 2594
		ALookup,
		// Token: 0x04000A23 RID: 2595
		AsyncProcessor,
		// Token: 0x04000A24 RID: 2596
		CountryCheck,
		// Token: 0x04000A25 RID: 2597
		LanguageCheck,
		// Token: 0x04000A26 RID: 2598
		ConcatTextTarget,
		// Token: 0x04000A27 RID: 2599
		DkimKeyLookup,
		// Token: 0x04000A28 RID: 2600
		DkimVerifier,
		// Token: 0x04000A29 RID: 2601
		PtrLookup
	}
}
