using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003ED RID: 1005
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class FolderResponseShapeType
	{
		// Token: 0x040015AC RID: 5548
		public DefaultShapeNamesType BaseShape;

		// Token: 0x040015AD RID: 5549
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		public BasePathToElementType[] AdditionalProperties;
	}
}
