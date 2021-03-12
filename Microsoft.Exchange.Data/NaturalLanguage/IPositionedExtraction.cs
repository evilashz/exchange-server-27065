using System;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000066 RID: 102
	public interface IPositionedExtraction
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000338 RID: 824
		// (set) Token: 0x06000339 RID: 825
		int StartIndex { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600033A RID: 826
		// (set) Token: 0x0600033B RID: 827
		EmailPosition Position { get; set; }
	}
}
