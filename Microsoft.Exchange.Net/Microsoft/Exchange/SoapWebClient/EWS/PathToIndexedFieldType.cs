using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C4 RID: 452
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToIndexedFieldType : BasePathToElementType
	{
		// Token: 0x04000A74 RID: 2676
		[XmlAttribute]
		public DictionaryURIType FieldURI;

		// Token: 0x04000A75 RID: 2677
		[XmlAttribute]
		public string FieldIndex;
	}
}
