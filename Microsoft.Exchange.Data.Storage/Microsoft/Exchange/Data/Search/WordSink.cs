using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CF2 RID: 3314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WordSink : IWordSink
	{
		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06007241 RID: 29249 RVA: 0x001F9A84 File Offset: 0x001F7C84
		public List<Token> Tokens
		{
			get
			{
				return this.tokens;
			}
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x001F9A8C File Offset: 0x001F7C8C
		public void PutWord(int inputBufferCharacterCount, string buffer, int wordCharacterCount, int wordStartIndex)
		{
			if (this.alternatePhrases <= 1)
			{
				this.tokens.Add(new Token(wordStartIndex, wordCharacterCount));
			}
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x001F9AAA File Offset: 0x001F7CAA
		public void PutAltWord(int inputBufferCharacterCount, string buffer, int wordCharacterCount, int wordStartIndex)
		{
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x001F9AAC File Offset: 0x001F7CAC
		public void PutBreak(WordBreakType breakType)
		{
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x001F9AAE File Offset: 0x001F7CAE
		public void StartAltPhrase()
		{
			this.alternatePhrases++;
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x001F9ABE File Offset: 0x001F7CBE
		public void EndAltPhrase()
		{
			this.alternatePhrases = 0;
		}

		// Token: 0x04004F9F RID: 20383
		private List<Token> tokens = new List<Token>();

		// Token: 0x04004FA0 RID: 20384
		private int alternatePhrases;
	}
}
