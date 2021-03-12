using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000340 RID: 832
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindConversationResponseMessageType : ResponseMessageType
	{
		// Token: 0x040013B0 RID: 5040
		[XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationType[] Conversations;

		// Token: 0x040013B1 RID: 5041
		[XmlArrayItem("Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public HighlightTermType[] HighlightTerms;

		// Token: 0x040013B2 RID: 5042
		public int TotalConversationsInView;

		// Token: 0x040013B3 RID: 5043
		[XmlIgnore]
		public bool TotalConversationsInViewSpecified;

		// Token: 0x040013B4 RID: 5044
		public int IndexedOffset;

		// Token: 0x040013B5 RID: 5045
		[XmlIgnore]
		public bool IndexedOffsetSpecified;
	}
}
