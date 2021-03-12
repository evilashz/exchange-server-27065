using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003A3 RID: 931
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConversationRequestType
	{
		// Token: 0x040014B2 RID: 5298
		public ItemIdType ConversationId;

		// Token: 0x040014B3 RID: 5299
		[XmlElement(DataType = "base64Binary")]
		public byte[] SyncState;
	}
}
