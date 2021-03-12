using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200018D RID: 397
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ResponseMessageTypeMessageXml
	{
		// Token: 0x0400098C RID: 2444
		[XmlAnyElement]
		public XmlElement[] Any;
	}
}
