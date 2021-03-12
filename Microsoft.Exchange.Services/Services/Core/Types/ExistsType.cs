using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000643 RID: 1603
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Exists")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExistsType : SearchExpressionType
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000B7602 File Offset: 0x000B5802
		// (set) Token: 0x060031E5 RID: 12773 RVA: 0x000B760A File Offset: 0x000B580A
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		public PropertyPath Item { get; set; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000B7613 File Offset: 0x000B5813
		internal override string FilterType
		{
			get
			{
				return "Exists";
			}
		}
	}
}
