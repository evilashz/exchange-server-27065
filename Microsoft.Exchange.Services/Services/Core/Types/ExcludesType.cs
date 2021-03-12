using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000641 RID: 1601
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Excludes")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExcludesType : SearchExpressionType
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000B75B8 File Offset: 0x000B57B8
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000B75C0 File Offset: 0x000B57C0
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		public PropertyPath Item { get; set; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000B75C9 File Offset: 0x000B57C9
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x000B75D1 File Offset: 0x000B57D1
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public ExcludesValueType Bitmask { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000B75DA File Offset: 0x000B57DA
		internal override string FilterType
		{
			get
			{
				return "Excludes";
			}
		}
	}
}
