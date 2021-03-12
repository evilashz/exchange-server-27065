using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001F4 RID: 500
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationResponseType
	{
		// Token: 0x04000CE3 RID: 3299
		public ItemIdType ConversationId;

		// Token: 0x04000CE4 RID: 3300
		[XmlElement(DataType = "base64Binary")]
		public byte[] SyncState;

		// Token: 0x04000CE5 RID: 3301
		[XmlArrayItem("ConversationNode", IsNullable = false)]
		public ConversationNodeType[] ConversationNodes;
	}
}
