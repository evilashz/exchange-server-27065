using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000847 RID: 2119
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PhoneNumberAttributedValue")]
	[XmlType(TypeName = "PhoneNumberAttributedValue", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PhoneNumberAttributedValue
	{
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000D70D0 File Offset: 0x000D52D0
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x000D70D8 File Offset: 0x000D52D8
		[DataMember(IsRequired = true, Order = 1)]
		[XmlElement]
		public PhoneNumber Value { get; set; }

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x000D70E1 File Offset: 0x000D52E1
		// (set) Token: 0x06003D12 RID: 15634 RVA: 0x000D70E9 File Offset: 0x000D52E9
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(IsRequired = true, Order = 2)]
		public string[] Attributions { get; set; }

		// Token: 0x06003D13 RID: 15635 RVA: 0x000D70F2 File Offset: 0x000D52F2
		public PhoneNumberAttributedValue()
		{
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000D70FA File Offset: 0x000D52FA
		public PhoneNumberAttributedValue(PhoneNumber value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
