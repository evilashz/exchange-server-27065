using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200075C RID: 1884
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "EmailAddressAttributedValue")]
	[XmlType(TypeName = "EmailAddressAttributedValue", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddressAttributedValue
	{
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000C7486 File Offset: 0x000C5686
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000C748E File Offset: 0x000C568E
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public EmailAddressWrapper Value { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000C7497 File Offset: 0x000C5697
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x000C749F File Offset: 0x000C569F
		[XmlArrayItem("Attribution", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(IsRequired = true, Order = 2)]
		[XmlArray(ElementName = "Attributions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] Attributions { get; set; }

		// Token: 0x06003851 RID: 14417 RVA: 0x000C74A8 File Offset: 0x000C56A8
		public EmailAddressAttributedValue()
		{
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000C74B0 File Offset: 0x000C56B0
		public EmailAddressAttributedValue(EmailAddressWrapper value, string[] attributions)
		{
			this.Value = value;
			this.Attributions = attributions;
		}
	}
}
