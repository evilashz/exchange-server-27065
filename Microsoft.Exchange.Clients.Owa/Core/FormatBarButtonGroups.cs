using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000119 RID: 281
	[Flags]
	public enum FormatBarButtonGroups
	{
		// Token: 0x040006CC RID: 1740
		None = 0,
		// Token: 0x040006CD RID: 1741
		BoldItalicUnderline = 1,
		// Token: 0x040006CE RID: 1742
		Justification = 2,
		// Token: 0x040006CF RID: 1743
		Lists = 4,
		// Token: 0x040006D0 RID: 1744
		Indenting = 8,
		// Token: 0x040006D1 RID: 1745
		Direction = 16,
		// Token: 0x040006D2 RID: 1746
		ForegroundColor = 32,
		// Token: 0x040006D3 RID: 1747
		BackgroundColor = 64,
		// Token: 0x040006D4 RID: 1748
		RemoveFormatting = 128,
		// Token: 0x040006D5 RID: 1749
		HorizontalRule = 256,
		// Token: 0x040006D6 RID: 1750
		UndoRedo = 1024,
		// Token: 0x040006D7 RID: 1751
		Hyperlinking = 2048,
		// Token: 0x040006D8 RID: 1752
		SuperSubScript = 4096,
		// Token: 0x040006D9 RID: 1753
		Strikethrough = 8192,
		// Token: 0x040006DA RID: 1754
		All = 16383
	}
}
