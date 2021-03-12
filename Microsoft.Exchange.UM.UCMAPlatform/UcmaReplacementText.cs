using System;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000057 RID: 87
	internal class UcmaReplacementText
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x000118A2 File Offset: 0x0000FAA2
		public UcmaReplacementText(string text, DisplayAttributes displayAttributes, int firstWordIndex, int countOfWords)
		{
			this.text = text;
			this.displayAttributes = displayAttributes;
			this.firstWordIndex = firstWordIndex;
			this.countOfWords = countOfWords;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000118C7 File Offset: 0x0000FAC7
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000118CF File Offset: 0x0000FACF
		public DisplayAttributes DisplayAttributes
		{
			get
			{
				return this.displayAttributes;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000118D7 File Offset: 0x0000FAD7
		public int FirstWordIndex
		{
			get
			{
				return this.firstWordIndex;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000118DF File Offset: 0x0000FADF
		public int CountOfWords
		{
			get
			{
				return this.countOfWords;
			}
		}

		// Token: 0x0400012F RID: 303
		private readonly string text;

		// Token: 0x04000130 RID: 304
		private readonly DisplayAttributes displayAttributes;

		// Token: 0x04000131 RID: 305
		private readonly int firstWordIndex;

		// Token: 0x04000132 RID: 306
		private readonly int countOfWords;
	}
}
