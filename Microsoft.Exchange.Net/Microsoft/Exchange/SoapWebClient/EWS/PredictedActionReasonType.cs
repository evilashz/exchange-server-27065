using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000269 RID: 617
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PredictedActionReasonType
	{
		// Token: 0x04000FDF RID: 4063
		None,
		// Token: 0x04000FE0 RID: 4064
		ConversationStarterIsYou,
		// Token: 0x04000FE1 RID: 4065
		OnlyRecipient,
		// Token: 0x04000FE2 RID: 4066
		ConversationContributions,
		// Token: 0x04000FE3 RID: 4067
		MarkedImportantBySender,
		// Token: 0x04000FE4 RID: 4068
		SenderIsManager,
		// Token: 0x04000FE5 RID: 4069
		SenderIsInManagementChain,
		// Token: 0x04000FE6 RID: 4070
		SenderIsDirectReport,
		// Token: 0x04000FE7 RID: 4071
		ActionBasedOnSender,
		// Token: 0x04000FE8 RID: 4072
		NameOnToLine,
		// Token: 0x04000FE9 RID: 4073
		NameOnCcLine,
		// Token: 0x04000FEA RID: 4074
		ManagerPosition,
		// Token: 0x04000FEB RID: 4075
		ReplyToAMessageFromMe,
		// Token: 0x04000FEC RID: 4076
		PreviouslyFlagged,
		// Token: 0x04000FED RID: 4077
		ActionBasedOnRecipients,
		// Token: 0x04000FEE RID: 4078
		ActionBasedOnSubjectWords,
		// Token: 0x04000FEF RID: 4079
		ActionBasedOnBasedOnBodyWords
	}
}
