using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084A RID: 2122
	[XmlType(TypeName = "PredictedActionReasonType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PredictedActionReasonType
	{
		// Token: 0x040021B7 RID: 8631
		None,
		// Token: 0x040021B8 RID: 8632
		ConversationStarterIsYou,
		// Token: 0x040021B9 RID: 8633
		OnlyRecipient,
		// Token: 0x040021BA RID: 8634
		ConversationContributions,
		// Token: 0x040021BB RID: 8635
		MarkedImportantBySender,
		// Token: 0x040021BC RID: 8636
		SenderIsManager,
		// Token: 0x040021BD RID: 8637
		SenderIsInManagementChain,
		// Token: 0x040021BE RID: 8638
		SenderIsDirectReport,
		// Token: 0x040021BF RID: 8639
		ActionBasedOnSender,
		// Token: 0x040021C0 RID: 8640
		NameOnToLine,
		// Token: 0x040021C1 RID: 8641
		NameOnCcLine,
		// Token: 0x040021C2 RID: 8642
		ManagerPosition,
		// Token: 0x040021C3 RID: 8643
		ReplyToAMessageFromMe,
		// Token: 0x040021C4 RID: 8644
		PreviouslyFlagged,
		// Token: 0x040021C5 RID: 8645
		ActionBasedOnRecipients,
		// Token: 0x040021C6 RID: 8646
		ActionBasedOnSubjectWords,
		// Token: 0x040021C7 RID: 8647
		ActionBasedOnBasedOnBodyWords
	}
}
