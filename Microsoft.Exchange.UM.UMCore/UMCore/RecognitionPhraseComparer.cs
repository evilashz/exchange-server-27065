using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001BE RID: 446
	internal class RecognitionPhraseComparer : IComparer<IUMRecognitionPhrase>
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00039064 File Offset: 0x00037264
		public int Compare(IUMRecognitionPhrase ol, IUMRecognitionPhrase or)
		{
			if (ol == null && or == null)
			{
				return 0;
			}
			if (ol == null && or != null)
			{
				return 1;
			}
			if (ol != null && or == null)
			{
				return -1;
			}
			float num = ol.Confidence - or.Confidence;
			if (num > 0f)
			{
				return -1;
			}
			if (num < 0f)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000A66 RID: 2662
		internal static readonly RecognitionPhraseComparer StaticInstance = new RecognitionPhraseComparer();
	}
}
