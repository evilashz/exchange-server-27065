using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000188 RID: 392
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PredictedActionReasonType
	{
		// Token: 0x04000B8D RID: 2957
		None,
		// Token: 0x04000B8E RID: 2958
		ConversationStarterIsYou,
		// Token: 0x04000B8F RID: 2959
		OnlyRecipient,
		// Token: 0x04000B90 RID: 2960
		ConversationContributions,
		// Token: 0x04000B91 RID: 2961
		MarkedImportantBySender,
		// Token: 0x04000B92 RID: 2962
		SenderIsManager,
		// Token: 0x04000B93 RID: 2963
		SenderIsInManagementChain,
		// Token: 0x04000B94 RID: 2964
		SenderIsDirectReport,
		// Token: 0x04000B95 RID: 2965
		ActionBasedOnSender,
		// Token: 0x04000B96 RID: 2966
		NameOnToLine,
		// Token: 0x04000B97 RID: 2967
		NameOnCcLine,
		// Token: 0x04000B98 RID: 2968
		ManagerPosition,
		// Token: 0x04000B99 RID: 2969
		ReplyToAMessageFromMe,
		// Token: 0x04000B9A RID: 2970
		PreviouslyFlagged,
		// Token: 0x04000B9B RID: 2971
		ActionBasedOnRecipients,
		// Token: 0x04000B9C RID: 2972
		ActionBasedOnSubjectWords,
		// Token: 0x04000B9D RID: 2973
		ActionBasedOnBasedOnBodyWords
	}
}
