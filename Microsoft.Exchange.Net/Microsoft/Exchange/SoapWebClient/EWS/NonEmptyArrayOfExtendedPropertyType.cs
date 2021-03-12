using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001BC RID: 444
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class NonEmptyArrayOfExtendedPropertyType
	{
		// Token: 0x04000A36 RID: 2614
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] Items;
	}
}
