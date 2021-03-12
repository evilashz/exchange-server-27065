using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000613 RID: 1555
	public enum MimeTextFormat
	{
		// Token: 0x0400331C RID: 13084
		[LocDescription(DirectoryStrings.IDs.TextOnly)]
		TextOnly,
		// Token: 0x0400331D RID: 13085
		[LocDescription(DirectoryStrings.IDs.HtmlOnly)]
		HtmlOnly,
		// Token: 0x0400331E RID: 13086
		[LocDescription(DirectoryStrings.IDs.HtmlAndTextAlternative)]
		HtmlAndTextAlternative,
		// Token: 0x0400331F RID: 13087
		[LocDescription(DirectoryStrings.IDs.TextEnrichedOnly)]
		TextEnrichedOnly,
		// Token: 0x04003320 RID: 13088
		[LocDescription(DirectoryStrings.IDs.TextEnrichedAndTextAlternative)]
		TextEnrichedAndTextAlternative,
		// Token: 0x04003321 RID: 13089
		[LocDescription(DirectoryStrings.IDs.BestBodyFormat)]
		BestBodyFormat,
		// Token: 0x04003322 RID: 13090
		[LocDescription(DirectoryStrings.IDs.Tnef)]
		Tnef
	}
}
