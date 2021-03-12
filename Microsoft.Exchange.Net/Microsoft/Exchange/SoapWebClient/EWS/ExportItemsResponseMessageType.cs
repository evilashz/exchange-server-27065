using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200034B RID: 843
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ExportItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001403 RID: 5123
		public ItemIdType ItemId;

		// Token: 0x04001404 RID: 5124
		[XmlElement(DataType = "base64Binary")]
		public byte[] Data;
	}
}
