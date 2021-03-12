using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000646 RID: 1606
	[KnownType(typeof(PropertyPath))]
	[KnownType(typeof(DictionaryPropertyUri))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(PropertyUri))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(ConstantValueType))]
	[KnownType(typeof(ExtendedPropertyUri))]
	[Serializable]
	public class FieldURIOrConstantType
	{
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000B765B File Offset: 0x000B585B
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x000B7663 File Offset: 0x000B5863
		[XmlElement("Constant", typeof(ConstantValueType))]
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		[XmlElement("Path", typeof(PropertyPath))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		public object Item { get; set; }
	}
}
