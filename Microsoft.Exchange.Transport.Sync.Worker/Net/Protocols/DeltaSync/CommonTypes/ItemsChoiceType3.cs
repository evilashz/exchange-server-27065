using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000088 RID: 136
	internal enum ItemsChoiceType3
	{
		// Token: 0x0400033B RID: 827
		Categories,
		// Token: 0x0400033C RID: 828
		DisplayName,
		// Token: 0x0400033D RID: 829
		Flag,
		// Token: 0x0400033E RID: 830
		Size,
		// Token: 0x0400033F RID: 831
		TotalMessageCount,
		// Token: 0x04000340 RID: 832
		UnreadMessageCount,
		// Token: 0x04000341 RID: 833
		[XmlEnum("EMAIL::DateReceived")]
		DateReceived,
		// Token: 0x04000342 RID: 834
		[XmlEnum("EMAIL::From")]
		From,
		// Token: 0x04000343 RID: 835
		[XmlEnum("EMAIL::Importance")]
		Importance,
		// Token: 0x04000344 RID: 836
		[XmlEnum("EMAIL::MessageClass")]
		MessageClass,
		// Token: 0x04000345 RID: 837
		[XmlEnum("EMAIL::Read")]
		Read,
		// Token: 0x04000346 RID: 838
		[XmlEnum("EMAIL::Subject")]
		Subject,
		// Token: 0x04000347 RID: 839
		[XmlEnum("HMFOLDER::ParentId")]
		ParentId,
		// Token: 0x04000348 RID: 840
		[XmlEnum("HMFOLDER::Version")]
		Version,
		// Token: 0x04000349 RID: 841
		[XmlEnum("HMMAIL::ConfirmedJunk")]
		ConfirmedJunk,
		// Token: 0x0400034A RID: 842
		[XmlEnum("HMMAIL::ConversationIndex")]
		ConversationIndex,
		// Token: 0x0400034B RID: 843
		[XmlEnum("HMMAIL::ConversationTopic")]
		ConversationTopic,
		// Token: 0x0400034C RID: 844
		[XmlEnum("HMMAIL::FolderId")]
		FolderId,
		// Token: 0x0400034D RID: 845
		[XmlEnum("HMMAIL::HasAttachments")]
		HasAttachments,
		// Token: 0x0400034E RID: 846
		[XmlEnum("HMMAIL::IsBondedSender")]
		IsBondedSender,
		// Token: 0x0400034F RID: 847
		[XmlEnum("HMMAIL::IsFromSomeoneAddressBook")]
		IsFromSomeoneAddressBook,
		// Token: 0x04000350 RID: 848
		[XmlEnum("HMMAIL::IsToAllowList")]
		IsToAllowList,
		// Token: 0x04000351 RID: 849
		[XmlEnum("HMMAIL::LegacyId")]
		LegacyId,
		// Token: 0x04000352 RID: 850
		[XmlEnum("HMMAIL::Message")]
		Message,
		// Token: 0x04000353 RID: 851
		[XmlEnum("HMMAIL::ReplyToOrForwardState")]
		ReplyToOrForwardState,
		// Token: 0x04000354 RID: 852
		[XmlEnum("HMMAIL::Sensitivity")]
		Sensitivity,
		// Token: 0x04000355 RID: 853
		[XmlEnum("HMMAIL::Size")]
		Size1,
		// Token: 0x04000356 RID: 854
		[XmlEnum("HMMAIL::TrustedSource")]
		TrustedSource,
		// Token: 0x04000357 RID: 855
		[XmlEnum("HMMAIL::TypeData")]
		TypeData,
		// Token: 0x04000358 RID: 856
		[XmlEnum("HMMAIL::Version")]
		Version1
	}
}
