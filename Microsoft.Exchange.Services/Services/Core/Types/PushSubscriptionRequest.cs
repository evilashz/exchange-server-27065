using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000858 RID: 2136
	[XmlType("PushSubscriptionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PushSubscriptionRequest")]
	public class PushSubscriptionRequest : SubscriptionRequestBase
	{
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000D78FE File Offset: 0x000D5AFE
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x000D7906 File Offset: 0x000D5B06
		[DataMember(Name = "StatusFrequency", IsRequired = true, Order = 1)]
		[XmlElement("StatusFrequency", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public int StatusFrequency { get; set; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000D790F File Offset: 0x000D5B0F
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x000D7917 File Offset: 0x000D5B17
		[XmlElement("URL", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "URL", IsRequired = true, Order = 1)]
		public string Url { get; set; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000D7920 File Offset: 0x000D5B20
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000D7928 File Offset: 0x000D5B28
		[DataMember(Name = "CallerData", IsRequired = false, Order = 2)]
		[XmlElement("CallerData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string CallerData { get; set; }
	}
}
