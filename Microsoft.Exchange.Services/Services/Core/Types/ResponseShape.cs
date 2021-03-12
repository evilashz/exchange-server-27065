using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C0 RID: 1728
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ItemResponseShape))]
	[XmlInclude(typeof(FolderResponseShape))]
	[XmlInclude(typeof(ItemResponseShape))]
	[KnownType(typeof(FolderResponseShape))]
	[Serializable]
	public class ResponseShape
	{
		// Token: 0x06003522 RID: 13602 RVA: 0x000BF896 File Offset: 0x000BDA96
		public ResponseShape()
		{
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000BF89E File Offset: 0x000BDA9E
		internal ResponseShape(ShapeEnum baseShape, PropertyPath[] additionalProperties)
		{
			this.BaseShape = baseShape;
			this.AdditionalProperties = additionalProperties;
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000BF8B4 File Offset: 0x000BDAB4
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x000BF8BC File Offset: 0x000BDABC
		[IgnoreDataMember]
		[XmlElement]
		public ShapeEnum BaseShape { get; set; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000BF8C5 File Offset: 0x000BDAC5
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000BF8D2 File Offset: 0x000BDAD2
		[DataMember(Name = "BaseShape", IsRequired = true)]
		[XmlIgnore]
		public string BaseShapeString
		{
			get
			{
				return EnumUtilities.ToString<ShapeEnum>(this.BaseShape);
			}
			set
			{
				this.BaseShape = EnumUtilities.Parse<ShapeEnum>(value);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000BF8E0 File Offset: 0x000BDAE0
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000BF8E8 File Offset: 0x000BDAE8
		[XmlArrayItem("IndexedFieldURI", typeof(DictionaryPropertyUri), IsNullable = false)]
		[DataMember(IsRequired = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(ExtendedPropertyUri), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PropertyUri), IsNullable = false)]
		public PropertyPath[] AdditionalProperties { get; set; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000BF8F1 File Offset: 0x000BDAF1
		// (set) Token: 0x0600352B RID: 13611 RVA: 0x000BF8F9 File Offset: 0x000BDAF9
		[IgnoreDataMember]
		[XmlIgnore]
		public Dictionary<string, PropertyPath[]> FlightedProperties { get; set; }
	}
}
