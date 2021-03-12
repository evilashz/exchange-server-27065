using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000069 RID: 105
	[Flags]
	internal enum MimeReaderState
	{
		// Token: 0x040002E8 RID: 744
		Start = 1,
		// Token: 0x040002E9 RID: 745
		PartStart = 2,
		// Token: 0x040002EA RID: 746
		HeaderStart = 4,
		// Token: 0x040002EB RID: 747
		HeaderIncomplete = 8,
		// Token: 0x040002EC RID: 748
		HeaderComplete = 16,
		// Token: 0x040002ED RID: 749
		EndOfHeaders = 32,
		// Token: 0x040002EE RID: 750
		PartPrologue = 64,
		// Token: 0x040002EF RID: 751
		PartBody = 128,
		// Token: 0x040002F0 RID: 752
		PartEpilogue = 256,
		// Token: 0x040002F1 RID: 753
		PartEnd = 512,
		// Token: 0x040002F2 RID: 754
		InlineStart = 1024,
		// Token: 0x040002F3 RID: 755
		InlineBody = 2048,
		// Token: 0x040002F4 RID: 756
		InlineEnd = 4096,
		// Token: 0x040002F5 RID: 757
		InlineJunk = 8192,
		// Token: 0x040002F6 RID: 758
		Embedded = 16384,
		// Token: 0x040002F7 RID: 759
		EmbeddedEnd = 32768,
		// Token: 0x040002F8 RID: 760
		End = 65536
	}
}
