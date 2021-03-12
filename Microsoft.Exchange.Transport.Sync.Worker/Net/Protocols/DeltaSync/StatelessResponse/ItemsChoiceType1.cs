using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000192 RID: 402
	[XmlType(Namespace = "DeltaSyncV2:", IncludeInSchema = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public enum ItemsChoiceType1
	{
		// Token: 0x04000665 RID: 1637
		Categories,
		// Token: 0x04000666 RID: 1638
		DisplayName,
		// Token: 0x04000667 RID: 1639
		Flag,
		// Token: 0x04000668 RID: 1640
		Size,
		// Token: 0x04000669 RID: 1641
		TotalMessageCount,
		// Token: 0x0400066A RID: 1642
		UnreadMessageCount,
		// Token: 0x0400066B RID: 1643
		[XmlEnum("EMAIL::DateReceived")]
		DateReceived,
		// Token: 0x0400066C RID: 1644
		[XmlEnum("EMAIL::From")]
		From,
		// Token: 0x0400066D RID: 1645
		[XmlEnum("EMAIL::Importance")]
		Importance,
		// Token: 0x0400066E RID: 1646
		[XmlEnum("EMAIL::MessageClass")]
		MessageClass,
		// Token: 0x0400066F RID: 1647
		[XmlEnum("EMAIL::Read")]
		Read,
		// Token: 0x04000670 RID: 1648
		[XmlEnum("EMAIL::Subject")]
		Subject,
		// Token: 0x04000671 RID: 1649
		[XmlEnum("HMFOLDER::ParentId")]
		ParentId,
		// Token: 0x04000672 RID: 1650
		[XmlEnum("HMFOLDER::Version")]
		Version,
		// Token: 0x04000673 RID: 1651
		[XmlEnum("HMMAIL::ConfirmedJunk")]
		ConfirmedJunk,
		// Token: 0x04000674 RID: 1652
		[XmlEnum("HMMAIL::ConversationIndex")]
		ConversationIndex,
		// Token: 0x04000675 RID: 1653
		[XmlEnum("HMMAIL::ConversationTopic")]
		ConversationTopic,
		// Token: 0x04000676 RID: 1654
		[XmlEnum("HMMAIL::FolderId")]
		FolderId,
		// Token: 0x04000677 RID: 1655
		[XmlEnum("HMMAIL::HasAttachments")]
		HasAttachments,
		// Token: 0x04000678 RID: 1656
		[XmlEnum("HMMAIL::IsBondedSender")]
		IsBondedSender,
		// Token: 0x04000679 RID: 1657
		[XmlEnum("HMMAIL::IsFromSomeoneAddressBook")]
		IsFromSomeoneAddressBook,
		// Token: 0x0400067A RID: 1658
		[XmlEnum("HMMAIL::IsToAllowList")]
		IsToAllowList,
		// Token: 0x0400067B RID: 1659
		[XmlEnum("HMMAIL::LegacyId")]
		LegacyId,
		// Token: 0x0400067C RID: 1660
		[XmlEnum("HMMAIL::Message")]
		Message,
		// Token: 0x0400067D RID: 1661
		[XmlEnum("HMMAIL::PopAccountID")]
		PopAccountID,
		// Token: 0x0400067E RID: 1662
		[XmlEnum("HMMAIL::ReplyToOrForwardState")]
		ReplyToOrForwardState,
		// Token: 0x0400067F RID: 1663
		[XmlEnum("HMMAIL::Sensitivity")]
		Sensitivity,
		// Token: 0x04000680 RID: 1664
		[XmlEnum("HMMAIL::Size")]
		Size1,
		// Token: 0x04000681 RID: 1665
		[XmlEnum("HMMAIL::TrustedSource")]
		TrustedSource,
		// Token: 0x04000682 RID: 1666
		[XmlEnum("HMMAIL::TypeData")]
		TypeData,
		// Token: 0x04000683 RID: 1667
		[XmlEnum("HMMAIL::Version")]
		Version1
	}
}
