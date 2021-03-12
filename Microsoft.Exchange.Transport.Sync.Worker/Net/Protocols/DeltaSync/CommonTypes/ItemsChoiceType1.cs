using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000084 RID: 132
	internal enum ItemsChoiceType1
	{
		// Token: 0x04000313 RID: 787
		Categories,
		// Token: 0x04000314 RID: 788
		Flag,
		// Token: 0x04000315 RID: 789
		Size,
		// Token: 0x04000316 RID: 790
		TotalMessageCount,
		// Token: 0x04000317 RID: 791
		UnreadMessageCount,
		// Token: 0x04000318 RID: 792
		[XmlEnum("EMAIL::DateReceived")]
		DateReceived,
		// Token: 0x04000319 RID: 793
		[XmlEnum("EMAIL::From")]
		From,
		// Token: 0x0400031A RID: 794
		[XmlEnum("EMAIL::Importance")]
		Importance,
		// Token: 0x0400031B RID: 795
		[XmlEnum("EMAIL::MessageClass")]
		MessageClass,
		// Token: 0x0400031C RID: 796
		[XmlEnum("EMAIL::Read")]
		Read,
		// Token: 0x0400031D RID: 797
		[XmlEnum("EMAIL::Subject")]
		Subject,
		// Token: 0x0400031E RID: 798
		[XmlEnum("HMFOLDER::DisplayName")]
		DisplayName,
		// Token: 0x0400031F RID: 799
		[XmlEnum("HMFOLDER::ParentId")]
		ParentId,
		// Token: 0x04000320 RID: 800
		[XmlEnum("HMFOLDER::Version")]
		Version,
		// Token: 0x04000321 RID: 801
		[XmlEnum("HMMAIL::ConfirmedJunk")]
		ConfirmedJunk,
		// Token: 0x04000322 RID: 802
		[XmlEnum("HMMAIL::ConversationIndex")]
		ConversationIndex,
		// Token: 0x04000323 RID: 803
		[XmlEnum("HMMAIL::ConversationTopic")]
		ConversationTopic,
		// Token: 0x04000324 RID: 804
		[XmlEnum("HMMAIL::FolderId")]
		FolderId,
		// Token: 0x04000325 RID: 805
		[XmlEnum("HMMAIL::HasAttachments")]
		HasAttachments,
		// Token: 0x04000326 RID: 806
		[XmlEnum("HMMAIL::IsBondedSender")]
		IsBondedSender,
		// Token: 0x04000327 RID: 807
		[XmlEnum("HMMAIL::IsFromSomeoneAddressBook")]
		IsFromSomeoneAddressBook,
		// Token: 0x04000328 RID: 808
		[XmlEnum("HMMAIL::IsToAllowList")]
		IsToAllowList,
		// Token: 0x04000329 RID: 809
		[XmlEnum("HMMAIL::LegacyId")]
		LegacyId,
		// Token: 0x0400032A RID: 810
		[XmlEnum("HMMAIL::Message")]
		Message,
		// Token: 0x0400032B RID: 811
		[XmlEnum("HMMAIL::ReplyToOrForwardState")]
		ReplyToOrForwardState,
		// Token: 0x0400032C RID: 812
		[XmlEnum("HMMAIL::Sensitivity")]
		Sensitivity,
		// Token: 0x0400032D RID: 813
		[XmlEnum("HMMAIL::Size")]
		Size1,
		// Token: 0x0400032E RID: 814
		[XmlEnum("HMMAIL::TrustedSource")]
		TrustedSource,
		// Token: 0x0400032F RID: 815
		[XmlEnum("HMMAIL::TypeData")]
		TypeData,
		// Token: 0x04000330 RID: 816
		[XmlEnum("HMMAIL::Version")]
		Version1
	}
}
