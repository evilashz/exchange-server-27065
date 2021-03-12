using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D4 RID: 212
	internal enum MessageType
	{
		// Token: 0x040002E1 RID: 737
		Undefined,
		// Token: 0x040002E2 RID: 738
		Unknown,
		// Token: 0x040002E3 RID: 739
		SingleAttachment,
		// Token: 0x040002E4 RID: 740
		MultipleAttachments,
		// Token: 0x040002E5 RID: 741
		Normal,
		// Token: 0x040002E6 RID: 742
		NormalWithRegularAttachments,
		// Token: 0x040002E7 RID: 743
		SummaryTnef,
		// Token: 0x040002E8 RID: 744
		LegacyTnef,
		// Token: 0x040002E9 RID: 745
		SuperLegacyTnef,
		// Token: 0x040002EA RID: 746
		SuperLegacyTnefWithRegularAttachments,
		// Token: 0x040002EB RID: 747
		Voice,
		// Token: 0x040002EC RID: 748
		Fax,
		// Token: 0x040002ED RID: 749
		Journal,
		// Token: 0x040002EE RID: 750
		Dsn,
		// Token: 0x040002EF RID: 751
		Mdn,
		// Token: 0x040002F0 RID: 752
		MsRightsProtected,
		// Token: 0x040002F1 RID: 753
		Quota,
		// Token: 0x040002F2 RID: 754
		AdReplicationMessage,
		// Token: 0x040002F3 RID: 755
		PgpEncrypted,
		// Token: 0x040002F4 RID: 756
		SmimeSignedNormal,
		// Token: 0x040002F5 RID: 757
		SmimeSignedUnknown,
		// Token: 0x040002F6 RID: 758
		SmimeSignedEncrypted,
		// Token: 0x040002F7 RID: 759
		SmimeOpaqueSigned,
		// Token: 0x040002F8 RID: 760
		SmimeEncrypted,
		// Token: 0x040002F9 RID: 761
		ApprovalInitiation,
		// Token: 0x040002FA RID: 762
		UMPartner
	}
}
