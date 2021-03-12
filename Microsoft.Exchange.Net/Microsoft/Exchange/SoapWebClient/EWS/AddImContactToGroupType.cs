using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200040A RID: 1034
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class AddImContactToGroupType : BaseRequestType
	{
		// Token: 0x040015EC RID: 5612
		public ItemIdType ContactId;

		// Token: 0x040015ED RID: 5613
		public ItemIdType GroupId;
	}
}
