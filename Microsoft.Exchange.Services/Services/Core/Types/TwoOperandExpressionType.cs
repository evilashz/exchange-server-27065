using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000644 RID: 1604
	[XmlInclude(typeof(IsGreaterThanOrEqualToType))]
	[KnownType(typeof(IsLessThanType))]
	[XmlInclude(typeof(IsEqualToType))]
	[KnownType(typeof(IsNotEqualToType))]
	[XmlInclude(typeof(IsLessThanOrEqualToType))]
	[XmlInclude(typeof(IsLessThanType))]
	[XmlInclude(typeof(IsGreaterThanType))]
	[XmlInclude(typeof(IsNotEqualToType))]
	[KnownType(typeof(IsEqualToType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(IsLessThanOrEqualToType))]
	[KnownType(typeof(IsGreaterThanOrEqualToType))]
	[KnownType(typeof(IsGreaterThanType))]
	[Serializable]
	public abstract class TwoOperandExpressionType : SearchExpressionType
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000B7622 File Offset: 0x000B5822
		// (set) Token: 0x060031E9 RID: 12777 RVA: 0x000B762A File Offset: 0x000B582A
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		public PropertyPath Item { get; set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000B7633 File Offset: 0x000B5833
		// (set) Token: 0x060031EB RID: 12779 RVA: 0x000B763B File Offset: 0x000B583B
		[DataMember(EmitDefaultValue = false)]
		public FieldURIOrConstantType FieldURIOrConstant { get; set; }
	}
}
