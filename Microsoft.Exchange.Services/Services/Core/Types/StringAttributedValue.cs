using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000896 RID: 2198
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "StringAttributedValue")]
	[XmlType(TypeName = "StringAttributedValueType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class StringAttributedValue
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000D9956 File Offset: 0x000D7B56
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x000D995E File Offset: 0x000D7B5E
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public string Value { get; set; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000D9967 File Offset: 0x000D7B67
		// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x000D996F File Offset: 0x000D7B6F
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = true, Order = 2)]
		public string[] Attributions { get; set; }

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000D9978 File Offset: 0x000D7B78
		public StringAttributedValue()
		{
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000D9980 File Offset: 0x000D7B80
		public StringAttributedValue(string value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
