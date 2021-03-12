using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001BF RID: 447
	internal class RecognitionPhraseListComparer : IComparer<List<IUMRecognitionPhrase>>
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x000390C0 File Offset: 0x000372C0
		public int Compare(List<IUMRecognitionPhrase> oll, List<IUMRecognitionPhrase> olr)
		{
			IUMRecognitionPhrase ol = (oll != null && oll.Count > 0) ? oll[0] : null;
			IUMRecognitionPhrase or = (olr != null && olr.Count > 0) ? olr[0] : null;
			return RecognitionPhraseComparer.StaticInstance.Compare(ol, or);
		}

		// Token: 0x04000A67 RID: 2663
		internal static readonly RecognitionPhraseListComparer StaticInstance = new RecognitionPhraseListComparer();
	}
}
