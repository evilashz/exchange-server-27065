using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000483 RID: 1155
	[XmlType("StreamingSubscriptionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class StreamingSubscriptionRequest : SubscriptionRequestBase
	{
	}
}
