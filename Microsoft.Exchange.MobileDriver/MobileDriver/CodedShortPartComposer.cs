using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000007 RID: 7
	internal class CodedShortPartComposer : ShortPartComposerBase
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002C84 File Offset: 0x00000E84
		public CodedShortPartComposer(CodingScheme codingScheme, int radixPerPart, int maxPart) : base(maxPart)
		{
			this.splittingToMaximumParts = new CodedShortPartSplitter(codingScheme, radixPerPart, maxPart);
			this.splittingToTheEnd = new CodedShortPartSplitter(codingScheme, radixPerPart);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public CodedShortPartComposer(CodingScheme codingScheme, int radixPerPart, int maxPart, char fallbackCharacter) : base(maxPart)
		{
			this.splittingToMaximumParts = new CodedShortPartSplitter(codingScheme, radixPerPart, fallbackCharacter, maxPart);
			this.splittingToTheEnd = new CodedShortPartSplitter(codingScheme, radixPerPart, fallbackCharacter);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002CD0 File Offset: 0x00000ED0
		protected override PureSplitterBase SplittingToMaximumParts
		{
			get
			{
				return this.splittingToMaximumParts;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002CD8 File Offset: 0x00000ED8
		protected override PureSplitterBase SplittingToTheEnd
		{
			get
			{
				return this.splittingToTheEnd;
			}
		}

		// Token: 0x0400001A RID: 26
		private PureSplitterBase splittingToMaximumParts;

		// Token: 0x0400001B RID: 27
		private PureSplitterBase splittingToTheEnd;
	}
}
