using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200008A RID: 138
	public static class VirtualColumnNames
	{
		// Token: 0x040001E1 RID: 481
		public const string PageNumber = "DatabasePageNumber";

		// Token: 0x040001E2 RID: 482
		public const string DataSize = "RecordDataSize";

		// Token: 0x040001E3 RID: 483
		public const string LongValueDataSize = "RecordLongValueDataSize";

		// Token: 0x040001E4 RID: 484
		public const string OverheadSize = "RecordOverheadSize";

		// Token: 0x040001E5 RID: 485
		public const string LongValueOverheadSize = "RecordLongValueOverheadSize";

		// Token: 0x040001E6 RID: 486
		public const string NonTaggedColumnCount = "RecordNonTaggedColumnCount";

		// Token: 0x040001E7 RID: 487
		public const string TaggedColumnCount = "RecordTaggedColumnCount";

		// Token: 0x040001E8 RID: 488
		public const string LongValueCount = "RecordLongValueCount";

		// Token: 0x040001E9 RID: 489
		public const string MultiValueCount = "RecordMultiValueCount";

		// Token: 0x040001EA RID: 490
		public const string CompressedColumnCount = "RecordCompressedColumnCount";

		// Token: 0x040001EB RID: 491
		public const string CompressedDataSize = "RecordCompressedDataSize";

		// Token: 0x040001EC RID: 492
		public const string CompressedLongValueDataSize = "RecordCompressedLongValueDataSize";
	}
}
