using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000466 RID: 1126
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ApplyConversationActionType : BaseRequestType
	{
		// Token: 0x04001731 RID: 5937
		[XmlArrayItem("ConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationActionType[] ConversationActions;
	}
}
