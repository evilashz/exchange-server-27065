using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B8 RID: 952
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AttachmentResponseShapeType
	{
		// Token: 0x040014FB RID: 5371
		public bool IncludeMimeContent;

		// Token: 0x040014FC RID: 5372
		[XmlIgnore]
		public bool IncludeMimeContentSpecified;

		// Token: 0x040014FD RID: 5373
		public BodyTypeResponseType BodyType;

		// Token: 0x040014FE RID: 5374
		[XmlIgnore]
		public bool BodyTypeSpecified;

		// Token: 0x040014FF RID: 5375
		public bool FilterHtmlContent;

		// Token: 0x04001500 RID: 5376
		[XmlIgnore]
		public bool FilterHtmlContentSpecified;

		// Token: 0x04001501 RID: 5377
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		public BasePathToElementType[] AdditionalProperties;
	}
}
