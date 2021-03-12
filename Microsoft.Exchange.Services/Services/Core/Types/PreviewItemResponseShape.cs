using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084C RID: 2124
	[DataContract(Name = "PreviewItemResponseShape", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PreviewItemResponseShapeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PreviewItemResponseShape
	{
		// Token: 0x06003D49 RID: 15689 RVA: 0x000D73E0 File Offset: 0x000D55E0
		public PreviewItemResponseShape()
		{
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x000D73E8 File Offset: 0x000D55E8
		internal PreviewItemResponseShape(PreviewItemBaseShape baseShape, ExtendedPropertyUri[] additionalProperties)
		{
			this.BaseShape = baseShape;
			this.AdditionalProperties = additionalProperties;
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000D73FE File Offset: 0x000D55FE
		// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000D7406 File Offset: 0x000D5606
		[DataMember(Name = "BaseShape", IsRequired = true)]
		[XmlElement("BaseShape")]
		public PreviewItemBaseShape BaseShape { get; set; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000D740F File Offset: 0x000D560F
		// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000D7417 File Offset: 0x000D5617
		[XmlArrayItem(ElementName = "ExtendedFieldURI", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ExtendedPropertyUri))]
		[IgnoreDataMember]
		[XmlArray(ElementName = "AdditionalProperties", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ExtendedPropertyUri[] AdditionalProperties { get; set; }
	}
}
