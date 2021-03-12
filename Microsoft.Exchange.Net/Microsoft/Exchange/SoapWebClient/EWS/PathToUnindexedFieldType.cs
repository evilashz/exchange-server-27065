using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C6 RID: 454
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToUnindexedFieldType : BasePathToElementType
	{
		// Token: 0x04000A81 RID: 2689
		[XmlAttribute]
		public UnindexedFieldURIType FieldURI;
	}
}
