using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001AF RID: 431
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserPhotoResponseMessageType : ResponseMessageType
	{
		// Token: 0x04000A05 RID: 2565
		public bool HasChanged;

		// Token: 0x04000A06 RID: 2566
		[XmlElement(DataType = "base64Binary")]
		public byte[] PictureData;
	}
}
