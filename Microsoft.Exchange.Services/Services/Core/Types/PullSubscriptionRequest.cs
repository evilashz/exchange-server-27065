using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000857 RID: 2135
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PullSubscriptionRequest")]
	[XmlType("PullSubscriptionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class PullSubscriptionRequest : SubscriptionRequestBase
	{
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x000D78E5 File Offset: 0x000D5AE5
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x000D78ED File Offset: 0x000D5AED
		[XmlElement("Timeout", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "Timeout", IsRequired = true)]
		public int Timeout { get; set; }
	}
}
