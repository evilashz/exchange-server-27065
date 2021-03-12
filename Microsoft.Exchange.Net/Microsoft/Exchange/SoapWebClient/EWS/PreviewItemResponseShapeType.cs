using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003A4 RID: 932
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PreviewItemResponseShapeType
	{
		// Token: 0x040014B4 RID: 5300
		public PreviewItemBaseShapeType BaseShape;

		// Token: 0x040014B5 RID: 5301
		[XmlArrayItem("ExtendedFieldURI", IsNullable = false)]
		public PathToExtendedFieldType[] AdditionalProperties;
	}
}
