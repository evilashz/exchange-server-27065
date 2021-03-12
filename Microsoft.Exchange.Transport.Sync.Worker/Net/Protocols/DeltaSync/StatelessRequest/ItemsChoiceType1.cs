using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000172 RID: 370
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(Namespace = "DeltaSyncV2:", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType1
	{
		// Token: 0x040005F5 RID: 1525
		Categories,
		// Token: 0x040005F6 RID: 1526
		Flag,
		// Token: 0x040005F7 RID: 1527
		Size,
		// Token: 0x040005F8 RID: 1528
		TotalMessageCount,
		// Token: 0x040005F9 RID: 1529
		UnreadMessageCount,
		// Token: 0x040005FA RID: 1530
		[XmlEnum("EMAIL::DateReceived")]
		DateReceived,
		// Token: 0x040005FB RID: 1531
		[XmlEnum("EMAIL::From")]
		From,
		// Token: 0x040005FC RID: 1532
		[XmlEnum("EMAIL::Importance")]
		Importance,
		// Token: 0x040005FD RID: 1533
		[XmlEnum("EMAIL::MessageClass")]
		MessageClass,
		// Token: 0x040005FE RID: 1534
		[XmlEnum("EMAIL::Read")]
		Read,
		// Token: 0x040005FF RID: 1535
		[XmlEnum("EMAIL::Subject")]
		Subject,
		// Token: 0x04000600 RID: 1536
		[XmlEnum("HMFOLDER::DisplayName")]
		DisplayName,
		// Token: 0x04000601 RID: 1537
		[XmlEnum("HMFOLDER::ParentId")]
		ParentId,
		// Token: 0x04000602 RID: 1538
		[XmlEnum("HMFOLDER::Version")]
		Version,
		// Token: 0x04000603 RID: 1539
		[XmlEnum("HMMAIL::ConfirmedJunk")]
		ConfirmedJunk,
		// Token: 0x04000604 RID: 1540
		[XmlEnum("HMMAIL::ConversationIndex")]
		ConversationIndex,
		// Token: 0x04000605 RID: 1541
		[XmlEnum("HMMAIL::ConversationTopic")]
		ConversationTopic,
		// Token: 0x04000606 RID: 1542
		[XmlEnum("HMMAIL::FolderId")]
		FolderId,
		// Token: 0x04000607 RID: 1543
		[XmlEnum("HMMAIL::HasAttachments")]
		HasAttachments,
		// Token: 0x04000608 RID: 1544
		[XmlEnum("HMMAIL::IsBondedSender")]
		IsBondedSender,
		// Token: 0x04000609 RID: 1545
		[XmlEnum("HMMAIL::IsFromSomeoneAddressBook")]
		IsFromSomeoneAddressBook,
		// Token: 0x0400060A RID: 1546
		[XmlEnum("HMMAIL::IsToAllowList")]
		IsToAllowList,
		// Token: 0x0400060B RID: 1547
		[XmlEnum("HMMAIL::LegacyId")]
		LegacyId,
		// Token: 0x0400060C RID: 1548
		[XmlEnum("HMMAIL::Message")]
		Message,
		// Token: 0x0400060D RID: 1549
		[XmlEnum("HMMAIL::PopAccountID")]
		PopAccountID,
		// Token: 0x0400060E RID: 1550
		[XmlEnum("HMMAIL::ReplyToOrForwardState")]
		ReplyToOrForwardState,
		// Token: 0x0400060F RID: 1551
		[XmlEnum("HMMAIL::Sensitivity")]
		Sensitivity,
		// Token: 0x04000610 RID: 1552
		[XmlEnum("HMMAIL::Size")]
		Size1,
		// Token: 0x04000611 RID: 1553
		[XmlEnum("HMMAIL::TrustedSource")]
		TrustedSource,
		// Token: 0x04000612 RID: 1554
		[XmlEnum("HMMAIL::TypeData")]
		TypeData,
		// Token: 0x04000613 RID: 1555
		[XmlEnum("HMMAIL::Version")]
		Version1
	}
}
