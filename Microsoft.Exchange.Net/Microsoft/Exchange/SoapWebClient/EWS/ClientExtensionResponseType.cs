using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001EE RID: 494
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ClientExtensionResponseType : ResponseMessageType
	{
		// Token: 0x04000CC2 RID: 3266
		[XmlArrayItem("ClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ClientExtensionType[] ClientExtensions;

		// Token: 0x04000CC3 RID: 3267
		public string RawMasterTableXml;
	}
}
