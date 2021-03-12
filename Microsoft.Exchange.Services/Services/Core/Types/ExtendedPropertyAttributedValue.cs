using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D8 RID: 1496
	[XmlType(TypeName = "ExtendedPropertyAttributedValue", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ExtendedPropertyAttributedValue")]
	[Serializable]
	public class ExtendedPropertyAttributedValue
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000B1BD0 File Offset: 0x000AFDD0
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x000B1BD8 File Offset: 0x000AFDD8
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public ExtendedPropertyType Value { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000B1BE1 File Offset: 0x000AFDE1
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x000B1BE9 File Offset: 0x000AFDE9
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = true, Order = 2)]
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] Attributions { get; set; }

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B1BF2 File Offset: 0x000AFDF2
		public ExtendedPropertyAttributedValue()
		{
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B1BFA File Offset: 0x000AFDFA
		public ExtendedPropertyAttributedValue(ExtendedPropertyType value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
