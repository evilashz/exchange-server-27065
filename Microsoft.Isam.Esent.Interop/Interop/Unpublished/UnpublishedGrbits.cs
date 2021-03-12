using System;
using Microsoft.Isam.Esent.Interop.Server2003;
using Microsoft.Isam.Esent.Interop.Windows8;
using Microsoft.Isam.Esent.Interop.Windows81;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000082 RID: 130
	public static class UnpublishedGrbits
	{
		// Token: 0x040002B3 RID: 691
		public const IdleGrbit CompactAsync = (IdleGrbit)16;

		// Token: 0x040002B4 RID: 692
		public const InitGrbit ExternalRecoveryControl = (InitGrbit)2048;

		// Token: 0x040002B5 RID: 693
		public const ResizeDatabaseGrbit ShrinkCompactSize = (ResizeDatabaseGrbit)4;

		// Token: 0x040002B6 RID: 694
		public const RetrieveColumnGrbit RetrievePageNumber = (RetrieveColumnGrbit)4096;

		// Token: 0x040002B7 RID: 695
		public const RetrieveColumnGrbit RetrievePrereadOnly = (RetrieveColumnGrbit)16384;

		// Token: 0x040002B8 RID: 696
		public const RetrieveColumnGrbit RetrievePrereadMany = (RetrieveColumnGrbit)32768;

		// Token: 0x040002B9 RID: 697
		public const RetrieveColumnGrbit RetrievePhysicalSize = (RetrieveColumnGrbit)65536;

		// Token: 0x040002BA RID: 698
		public const ShrinkDatabaseGrbit Eof = (ShrinkDatabaseGrbit)16384;

		// Token: 0x040002BB RID: 699
		public const ShrinkDatabaseGrbit Periodically = (ShrinkDatabaseGrbit)32768;

		// Token: 0x040002BC RID: 700
		public const TermGrbit TermShrink = (TermGrbit)16;

		// Token: 0x040002BD RID: 701
		public const UpdateGrbit UpdateNoVersion = (UpdateGrbit)2;

		// Token: 0x040002BE RID: 702
		public const DurableCommitCallbackGrbit LogUnavailable = (DurableCommitCallbackGrbit)1;

		// Token: 0x040002BF RID: 703
		public const CreateTableColumnIndexGrbit TableImmutableStructure = (CreateTableColumnIndexGrbit)8;

		// Token: 0x040002C0 RID: 704
		public const CreateIndexGrbit IndexImmutableStructure = (CreateIndexGrbit)524288;
	}
}
