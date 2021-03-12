using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001EB RID: 491
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetAppManifestsResponseType : ResponseMessageType
	{
		// Token: 0x04000CBB RID: 3259
		[XmlArrayItem("Manifest", DataType = "base64Binary", IsNullable = false)]
		public byte[][] Manifests;
	}
}
