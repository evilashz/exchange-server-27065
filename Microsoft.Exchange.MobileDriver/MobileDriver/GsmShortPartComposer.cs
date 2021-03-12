using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200002E RID: 46
	internal class GsmShortPartComposer : ShortPartComposerBase
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000060AF File Offset: 0x000042AF
		public GsmShortPartComposer(int gsmDefaultPerPart, int unicodePerPart, int maxParts) : base(maxParts)
		{
			this.splittingToMaximumParts = new GsmShortPartSplitter(gsmDefaultPerPart, unicodePerPart, maxParts);
			this.splittingToTheEnd = new GsmShortPartSplitter(gsmDefaultPerPart, unicodePerPart);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000060D3 File Offset: 0x000042D3
		protected override PureSplitterBase SplittingToMaximumParts
		{
			get
			{
				return this.splittingToMaximumParts;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000060DB File Offset: 0x000042DB
		protected override PureSplitterBase SplittingToTheEnd
		{
			get
			{
				return this.splittingToTheEnd;
			}
		}

		// Token: 0x0400009A RID: 154
		private PureSplitterBase splittingToMaximumParts;

		// Token: 0x0400009B RID: 155
		private PureSplitterBase splittingToTheEnd;
	}
}
