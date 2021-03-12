using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003DA RID: 986
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ItemResponseShapeType
	{
		// Token: 0x0400156E RID: 5486
		public DefaultShapeNamesType BaseShape;

		// Token: 0x0400156F RID: 5487
		public bool IncludeMimeContent;

		// Token: 0x04001570 RID: 5488
		[XmlIgnore]
		public bool IncludeMimeContentSpecified;

		// Token: 0x04001571 RID: 5489
		public BodyTypeResponseType BodyType;

		// Token: 0x04001572 RID: 5490
		[XmlIgnore]
		public bool BodyTypeSpecified;

		// Token: 0x04001573 RID: 5491
		public BodyTypeResponseType UniqueBodyType;

		// Token: 0x04001574 RID: 5492
		[XmlIgnore]
		public bool UniqueBodyTypeSpecified;

		// Token: 0x04001575 RID: 5493
		public BodyTypeResponseType NormalizedBodyType;

		// Token: 0x04001576 RID: 5494
		[XmlIgnore]
		public bool NormalizedBodyTypeSpecified;

		// Token: 0x04001577 RID: 5495
		public bool FilterHtmlContent;

		// Token: 0x04001578 RID: 5496
		[XmlIgnore]
		public bool FilterHtmlContentSpecified;

		// Token: 0x04001579 RID: 5497
		public bool ConvertHtmlCodePageToUTF8;

		// Token: 0x0400157A RID: 5498
		[XmlIgnore]
		public bool ConvertHtmlCodePageToUTF8Specified;

		// Token: 0x0400157B RID: 5499
		public string InlineImageUrlTemplate;

		// Token: 0x0400157C RID: 5500
		public bool BlockExternalImages;

		// Token: 0x0400157D RID: 5501
		[XmlIgnore]
		public bool BlockExternalImagesSpecified;

		// Token: 0x0400157E RID: 5502
		public bool AddBlankTargetToLinks;

		// Token: 0x0400157F RID: 5503
		[XmlIgnore]
		public bool AddBlankTargetToLinksSpecified;

		// Token: 0x04001580 RID: 5504
		public int MaximumBodySize;

		// Token: 0x04001581 RID: 5505
		[XmlIgnore]
		public bool MaximumBodySizeSpecified;

		// Token: 0x04001582 RID: 5506
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
		public BasePathToElementType[] AdditionalProperties;
	}
}
